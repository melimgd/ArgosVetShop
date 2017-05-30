using ArgosVetShop.Models;
using ArgosVetShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ArgosVetShop.ViewModels.AppointmentsViewModels;
using ArgosVetShop.ViewModels.ManagmentViewModels;
using System.Web.Mvc;
using System.IO;

namespace ArgosVetShop.Services
{
    public class VetManagmentService
    {
        private VetManagmentRepo _repository;

        public VetManagmentService(ApplicationDbContext dbContext, ApplicationUserManager userManager)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            _repository = new VetManagmentRepo(dbContext, userManager);
        }

        public async Task<List<AppointmentViewModel>> GetTodayAppoinments()
        {
            var appointments = await _repository.GetAllAppointments();
            var viewModel = new List<AppointmentViewModel>();
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            foreach (var appointment in appointments)
            {
                var date = DateTime.ParseExact(appointment.Date, "d MMM, yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (date.ToString("dd/MM/yyyy").Equals(DateTime.Now.ToString("dd/MM/yyyy")))
                {
                    var appointmentViewModel = new AppointmentViewModel();
                    appointmentViewModel.ServiceName = appointment.Service.ServiceName;
                    appointmentViewModel.ServiceDescription = appointment.Service.ServiceDescription;
                    appointmentViewModel.ServicePrice = appointment.Service.Price;
                    appointmentViewModel.Observations = appointment.Observations;
                    appointmentViewModel.Diagnostic = appointment.Diagnostic;

                    appointmentViewModel.Pet = new PetViewModel();
                    appointmentViewModel.Pet.petName = appointment.Pet.Name;
                    appointmentViewModel.Pet.petBreed = appointment.Pet.Breed;
                    appointmentViewModel.Pet.petGender = appointment.Pet.Gender;
                    appointmentViewModel.Pet.petPhotoURL = appointment.Pet.PhotoURL;

                    appointmentViewModel.OwnerId = appointment.Pet.OwnerId;
                    appointmentViewModel.OwnerUserName = await _repository.GetOwnerNameById(appointmentViewModel.OwnerId);

                    if (!string.IsNullOrEmpty(appointmentViewModel.Pet.petPhotoURL))
                    {
                        var image = appointmentViewModel.Pet.petPhotoURL;
                        var route = urlHelper.Content("~/Content/img/photos" + appointmentViewModel.OwnerUserName);
                        appointmentViewModel.Pet.petPhotoURL = Path.Combine(route,image);
                    }
                    viewModel.Add(appointmentViewModel);
                }
                
            }
            return viewModel;
        }
    }
}