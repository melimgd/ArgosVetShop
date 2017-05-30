using ArgosVetShop.ViewModels.ManagmentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgosVetShop.ViewModels.AppointmentsViewModels
{
    public class AppointmentViewModel
    {
        public string Date { get; set; }
        public string Diagnostic { get; set; }
        public string Observations { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }

        public PetViewModel Pet { get; set; }
        public string OwnerId { get; internal set; }
        public object OwnerUserName { get; internal set; }
    }
}