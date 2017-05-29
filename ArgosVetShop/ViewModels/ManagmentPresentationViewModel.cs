using ArgosVetShop.ViewModels.ManagmentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgosVetShop.ViewModels
{
    public class ManagmentPresentationViewModel
    {
        public UserViewModel User { get; set;}
        public List<PetViewModel> Pets { get; set; }
    }
}