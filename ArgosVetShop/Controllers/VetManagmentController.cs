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
            var todaysAppointments = await VetService.GetTodayAppoinments();
            return View();
        }
    }
}