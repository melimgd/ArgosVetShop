using ArgosVetShop.Models;
using ArgosVetShop.Repository;
using ArgosVetShop.ViewModels;
using ArgosVetShop.ViewModels.ManagmentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Web.Mvc;
using ArgosVetShop.ViewModels.AppointmentsViewModels;

namespace ArgosVetShop.Services
{
    public class UserManagmentService
    {
        private UserManagmentRepo _repository;
        public UserManagmentService(ApplicationDbContext dbContext, ApplicationUserManager userManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            _repository = new UserManagmentRepo(dbContext, userManager);
        }

        public async Task<ManagmentPresentationViewModel> GetUserInformation(string userId)
        {
            var managmentPresentation = new ManagmentPresentationViewModel();
            var user = await _repository.GetUser(userId);
            managmentPresentation.User = new UserViewModel { Id = user.Id, Name = user.UserName, FullName = user.FullName, Email = user.Email, PhoneNumber = user.PhoneNumber };

            var pets = await _repository.GetUserPets(userId);
            managmentPresentation.Pets = new List<PetViewModel>();
            if (pets.Any())
            {
                var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                var route = urlHelper.Content("~/Content/img/photos/" + managmentPresentation.User.Name);
                foreach (var pet in pets)
                {
                    var photoUrl = "";
                    if (!string.IsNullOrEmpty(pet.PhotoURL))
                    {
                        photoUrl = Path.Combine(route, pet.PhotoURL);
                    }
                    managmentPresentation.Pets.Add(new PetViewModel
                    {
                        petName = pet.Name,
                        petBreed = pet.Breed,
                        petId = pet.PetID,
                        petPhotoURL = photoUrl,
                        petAge = pet.Age,
                        petColor = pet.Color,
                        petGender = pet.Gender,
                        petType = pet.Type
                    });
                }
            }
            return managmentPresentation;
        }

        public async Task<PetViewModel> GetPetInformation(int petId)
        {
            var petInformation = await _repository.GetPetById(petId);
            var petViewModel = new PetViewModel
            {
                petName = petInformation.Name,
                petLastName = petInformation.LastName,
                petAge = petInformation.Age,
                petPhotoURL = petInformation.PhotoURL,
                petBreed = petInformation.Breed,
                petColor = petInformation.Color,
                petGender = petInformation.Gender,
                petType = petInformation.Type,
                petId = petId,
                OwnerId =  petInformation.OwnerId
            };
            return petViewModel;
        }

        public async Task<bool> EditPetInformation(PetViewModel viewModel, HttpPostedFileBase photo, string route)
        {
            var originalModel = await _repository.GetPetById(viewModel.petId);
            var modelToUpdate = new PetModel
            {
                Name = viewModel.petName,
                LastName = viewModel.petLastName,
                Breed = viewModel.petBreed,
                Color = viewModel.petColor,
                Gender = viewModel.petGender,
                Type = viewModel.petType,
                Age = viewModel.petAge,
                PetID = viewModel.petId,
                OwnerId = viewModel.OwnerId,
                PhotoURL = viewModel.petPhotoURL
            };

            if(photo != null)
            {
                if ((photo.ContentType.Equals("image/jpeg") || photo.ContentType.Equals("image/png")) && photo.ContentLength <= 2000000)
                {
                    if (!string.IsNullOrEmpty(originalModel.PhotoURL))
                    {
                        var originalPhoto = Path.Combine(route, originalModel.PhotoURL);
                        if (File.Exists(originalPhoto))
                        {
                            File.Delete(originalPhoto);
                        }
                    }
                    string photoName = Path.GetFileName(photo.FileName);
                    string path = Path.Combine(route, photoName);

                    photo.SaveAs(path);
                    modelToUpdate.PhotoURL = viewModel.petPhotoURL;
                }
            }
            return await _repository.UpdatePetInformation(originalModel, modelToUpdate);
        }

        public async Task<object> RequestAppointment(RequestAppointmentViewModel viewModel)
        {
            bool isTaken = false;
            var appointmentsOnTheSameDate = await _repository.GetAppointmentsByDate(viewModel.Date);
            if(appointmentsOnTheSameDate.Count < 6)
            {
                foreach (var appointment in appointmentsOnTheSameDate)
                {
                    if (appointment.StartTime.Equals(viewModel.Hour))
                    {
                        isTaken = true;
                    }
                }
                if (isTaken)
                {
                    var availableHours = viewModel.Hours;
                    foreach (var appointment in appointmentsOnTheSameDate)
                    {
                        var remove = availableHours.Where(o => o.Value.Equals(appointment.StartTime)).FirstOrDefault();
                        availableHours.Remove(remove);
                    }
                    viewModel.Hours = availableHours;
                    return viewModel;
                }
                else
                {
                    var newAppointment = new AppointmentModel
                    {
                        ServiceID = viewModel.ServiceId,
                        PetID = viewModel.petId,
                        StartTime = viewModel.Hour,
                        Date = viewModel.Date,
                    };
                    var success = await _repository.BookAppointment(newAppointment);
                    if (success)
                    {
                        return "Date Booked";
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
            else
            {
                return "Date Not Available";
            }
        }

        public async Task<bool> SuscribePet(PetViewModel viewModel, HttpPostedFileBase photo, DirectoryInfo route)
        {
            PetModel model = new PetModel
            {
                Name = viewModel.petName,
                LastName = viewModel.petLastName,
                Age = viewModel.petAge,
                Type = viewModel.petType,
                Gender = viewModel.petGender,
                Breed = viewModel.petBreed,
                Color = viewModel.petColor,
                OwnerId = viewModel.OwnerId
            };

            if(photo != null)
            {
                if((photo.ContentType.Equals("image/jpeg") || photo.ContentType.Equals("image/png")) && photo.ContentLength <= 2000000)
                {
                    string photoName = Path.GetFileName(photo.FileName);
                    string path = Path.Combine(route.FullName, photoName);

                    photo.SaveAs(path);
                    model.PhotoURL = viewModel.petPhotoURL;
                }
            }
            return await  _repository.AddPet(model);
        }

    }   
}