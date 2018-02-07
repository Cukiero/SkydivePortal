using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.ProfileViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<UserJump> UserJumps { get; set; }

        public UserJump UserJump { get; set; }
}
}
