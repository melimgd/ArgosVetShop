using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArgosVetShop.ViewModels.AppointmentsViewModels
{
    public class AddAppointmentInformationViewModel
    {
        public int AppointmentId { get; set; }
        [Required]
        public string Diagnostic { get; set; }
        [Required]
        public string Observation { get; set; }
    }
}