using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models
{
    public class ApplicationUserRoles
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
    }
}
