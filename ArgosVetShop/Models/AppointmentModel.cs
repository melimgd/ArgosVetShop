using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArgosVetShop.Models
{
    public class AppointmentModel
    {
        [Key]
        public int AppoinmentID { get; set; }
        [ForeignKey("Pet")]
        public int PetID { get; set; }
        [ForeignKey("Service")]
        public int ServiceID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string Diagnostic { get; set; }
        public string Observations { get; set; }

        public virtual PetModel Pet { get; set; }
        public virtual ServiceModel Service { get; set; }
    }
}