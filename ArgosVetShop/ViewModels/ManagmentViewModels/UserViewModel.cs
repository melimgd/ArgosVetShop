﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgosVetShop.ViewModels.ManagmentViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; internal set; }
    }
}