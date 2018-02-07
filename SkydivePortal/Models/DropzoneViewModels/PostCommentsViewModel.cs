using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.DropzoneViewModels
{
    public class PostCommentsViewModel
    {
        public IEnumerable<Dropzone_User_Post_Comment> Dropzone_User_Post_Comments { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
