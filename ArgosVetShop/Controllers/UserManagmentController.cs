using ArgosVetShop.ViewModels.AppointmentsViewModels;
using ArgosVetShop.ViewModels.ManagmentViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArgosVetShop.Controllers
{
    public class UserManagmentController:BaseController
    {
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Managment()
        {
            if (TempData["success"] != null)
            {
                ViewBag.Success = "Your Pet has been suscribed";
            }
            if (TempData["successEdit"] != null)
            {
                ViewBag.Success = "Your Pet information has been updated";
            }
            if (TempData["successBooked"] != null)
            {
                ViewBag.Success = "Your appointment has been booked";
            }
            var viewModel = await UserService.GetUserInformation(User.Identity.GetUserId());
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult AddPet()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> AddPet(PetViewModel viewModel,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                viewModel.OwnerId = User.Identity.GetUserId();
                var userName = User.Identity.Name;
                var route = Directory.CreateDirectory(Server.MapPath("~/Content/img/photos/" + userName));            
                var success = await UserService.SuscribePet(viewModel, photo, route);
                if(success){
                    TempData["success"] = "success";
                    return RedirectToAction("Managment");
                }
                else
                {
                    ViewBag.Failed = "An error occurred please contact the administrator";
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> EditPet(int petId)
        {
            var viewModel = await UserService.GetPetInformation(petId);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> EditPet(PetViewModel viewModel, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var route = Server.MapPath("~/Content/img/photos/" + userName);
                var success = await UserService.EditPetInformation(viewModel, photo, route);
                if (success)
                {
                    TempData["successEdit"] = "success";
                    return RedirectToAction("Managment");
                }
                else
                {
                    ViewBag.Failed = "An error ocurred during update";
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult RequestAppointment(int petId)
        { 
            var viewModel = new RequestAppointmentViewModel();
            viewModel.petId = petId;
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles ="User")]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> RequestAppointment(RequestAppointmentViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel.ServiceId > 0)
            {
                var outCome = await UserService.RequestAppointment(viewModel);
                if(outCome.GetType() == typeof(string))
                {
                    var code = outCome as string;
                    switch (code)
                    {
                        case "Date Booked":
                            TempData["successBooked"] = "success";
                            return RedirectToAction("Managment");
                        case "Date Not Available":
                            ViewBag.DateNotAvailable = "No appointments available for this date";
                            return View(viewModel);
                        case "Error":
                            ViewBag.Failed = "An error has ocurred pleas ";
                            return View(viewModel);
                    }
                }
                else if(outCome.GetType() == typeof(RequestAppointmentViewModel))
                {
                    var updatedViewModel = outCome as RequestAppointmentViewModel;
                    ViewBag.HourTaken = "The Hour has been already taken";
                    return View(updatedViewModel);
                }
            }
            if(viewModel.ServiceId <= 0)
            {
                ViewBag.PickService = "Selection is needed";
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<ActionResult> PetRecord(int petId)
        {
            var viewModel = await UserService.GetPetInformation(petId);
            if (!string.IsNullOrEmpty(viewModel.petPhotoURL))
            {
                var image = viewModel.petPhotoURL;
                var userName = User.Identity.Name;
                var route = UrlHelper.GenerateContentUrl("~/Content/img/photos/" + userName, HttpContext);
                viewModel.petPhotoURL = Path.Combine(route, image);
            }
            return View(viewModel);
        }

    }
}