using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SkydivePortal.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? RegionId { get; set; }
        public Region Region { get; set; }
        public string City { get; set; }
        public int? ParachuteId { get; set; }
        public Parachute Parachute { get; set; }


    }
}
