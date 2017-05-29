using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArgosVetShop.Models
{
    public class ServiceModel
    {
        [Key]
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal Price { get; set; }
        public ICollection<AppointmentModel> Appointments { get; set; }
    }
}