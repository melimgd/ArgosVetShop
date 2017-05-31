using ArgosVetShop.ViewModels.AppointmentsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgosVetShop.ViewModels.ManagmentViewModels
{
    public class VetManagmentViewModel
    {
        public List<AppointmentViewModel> TodayAppointments { get; set; }
        public List<AppointmentViewModel> FutureAppoitments { get; set; }
        public List<AppointmentViewModel> PastAppointments { get; set; }
    }
}