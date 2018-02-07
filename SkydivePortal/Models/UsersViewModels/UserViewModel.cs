using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.UsersViewModels
{
    public class UserViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<ApplicationUserRoles> ApplicationUserRoles { get; set; }
    }
}
