using ArgosVetShop.ViewModels.AppointmentsViewModels;
using ArgosVetShop.ViewModels.ManagmentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArgosVetShop.Controllers
{
    public class VetManagmentController:BaseController
    {
        [HttpGet]
        [Authorize(Roles = "Veterinarian")]
        public async Task<ActionResult> AppointmentManagment()
        {
            if (TempData["success"] != null)
            {
                ViewBag.Success = "The appointment information was updated";
            }

            var viewModel = new VetManagmentViewModel();
            viewModel.TodayAppointments = await VetService.GetTodayAppoinments();
            viewModel.PastAppointments = await VetService.GetPastAppoitnments();
            viewModel.FutureAppoitments = await VetService.GetFutureAppoitnments();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Veterinarian")]
        public ActionResult AddInformation(int appointmentId)
        {
            var viewModel = new AddAppointmentInformationViewModel();
            viewModel.AppointmentId = appointmentId;
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Veterinarian")]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> AddInformation(AddAppointmentInformationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var success = await VetService.UpdateAppointmentInformation(viewModel);
                if (success)
                {
                    TempData["success"] = "success";
                    return RedirectToAction("AppointmentManagment");
                }
            }
            ViewBag.Failed = "Something went wrong with the update";
            return View(viewModel);
        }

    }
}