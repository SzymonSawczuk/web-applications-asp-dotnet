using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab13.Model
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string ? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        [PersonalData]
        public string? Address { get; set; }
        [PersonalData]
        public string? City { get; set; }
        [PersonalData]
        public string? PostCode { get; set; }
        [PersonalData]
        public string? Country { get; set; }
        [PersonalData]
        public string? Voivodeship { get; set; }


    }
}
