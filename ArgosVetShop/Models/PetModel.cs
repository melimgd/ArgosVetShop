using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArgosVetShop.Models
{
    public class PetModel
    {
        [Key]
        public int PetID { get; set; }
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return Name + "" + LastName; } }
        public string Gender { get; set; }
        public string Color { get; set; }
        public int Age { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string PhotoURL { get; set; }
        
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<AppointmentModel> Appointments { get; set; }
    }
}