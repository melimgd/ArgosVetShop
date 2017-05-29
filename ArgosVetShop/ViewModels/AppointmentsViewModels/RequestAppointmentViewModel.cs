using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArgosVetShop.ViewModels.AppointmentsViewModels
{
    public class RequestAppointmentViewModel
    {
        private string _date = "";
        private List<SelectListItem> _hours = null;
        [Required]
        public int petId { get; set; }
        [Required(ErrorMessage = "Selection is needed")]
        [Display(Name = "Pick Your Service")]
        public int ServiceId { get; set; }
        public List<SelectListItem> SelectItems
        {
            get {
                return new List<SelectListItem>() {
                    new SelectListItem { Value = "", Text="Select your service" ,Selected = true, Disabled = true},
                    new SelectListItem { Value = "1" , Text = "Veterinary consultation" },
                    new SelectListItem { Value = "2" , Text = "Haircut and Bath" },
                    new SelectListItem { Value = "3" , Text = "Haircut" },
                    new SelectListItem { Value = "4" , Text = "Bath" },
                    new SelectListItem { Value = "5" , Text = "Flea Bath" },

                };
            }
        }
        [Required]
        [Display(Name = "Pick Your Date")]
        public string Date
        {
            get
            {
                if (string.IsNullOrEmpty(_date))
                {
                    return DateTime.Now.Date.ToString("d MMMM, yyyy");
                }
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        [Required(ErrorMessage = "Selection is needed")]
        [Display(Name = "Pick Your Hour")]
        public string Hour { get; set; }
        public List<SelectListItem> Hours
        {
            get
            {
                if (_hours == null)
                {
                    return new List<SelectListItem>()
                    {
                        new SelectListItem { Value = null, Text="Pick your hour", Disabled = true, Selected = true },
                        new SelectListItem { Value = "10:00AM",Text ="10:00am - 11:00am" },
                        new SelectListItem { Value = "11:00AM",Text ="11:00am - 12:00am" },
                        new SelectListItem { Value = "12:00AM",Text ="12:00pm - 1:00pm" },
                        new SelectListItem { Value = "3:00PM",Text ="3:00pm - 4:00pm" },
                        new SelectListItem { Value = "4:00PM",Text ="4:00pm - 5:00pm" },
                        new SelectListItem { Value = "5:00PM",Text ="5:00pm - 6:00pm" },

                    };
                }
                return _hours;
                
            }
            set
            {
                _hours = value;
            }
        }

    }
}