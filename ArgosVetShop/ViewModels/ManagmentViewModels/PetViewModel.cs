using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArgosVetShop.ViewModels.ManagmentViewModels
{
    public class PetViewModel
    {
        [Required]
        [Display(Name = "Age")]
        [RegularExpression(@"^\d{1,2}$", ErrorMessage = "Invalid Age format")]
        public int petAge { get; set; }
        [Display(Name = "Breed")]
        [StringLength(40, ErrorMessage = "Maximun length 40 characters")]
        public string petBreed { get; set; }
        [Display(Name = "Color")]
        [StringLength(40, ErrorMessage = "Maximun length 40 characters")]
        public string petColor { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public string petGender { get; set; }
        public int petId { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(40,ErrorMessage = "Maximun length 40 characters")]
        public string petName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(40, ErrorMessage = "Maximun length 40 characters")]
        public string petLastName { get; set; }
        [Required]
        [Display(Name = "Type")]
        [StringLength(40, ErrorMessage = "Maximun length 40 characters")]
        public string petType { get; set; }
        public string petPhotoURL { get; set; }
        public string OwnerId { get; set; }
        public bool IsMale { get { return string.Equals("Male", petGender); } }
        public bool IsFemale { get { return string.Equals("Female", petGender); } }
    }
}