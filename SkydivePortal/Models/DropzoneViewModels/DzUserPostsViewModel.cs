using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.DropzoneViewModels
{
    public class DzUserPostsViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Dropzone Dropzone { get; set; }

        public Dropzone_User_Post NewPost { get; set; }

        public Dropzone_User_Post_Comment NewPostComment { get; set; }

        public DzEventsViewModel DzEvents { get; set; }

        public IEnumerable<Dropzone_User_Post> Dropzone_User_Posts { get; set; }
    }
}
