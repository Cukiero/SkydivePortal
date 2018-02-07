using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.DropzoneViewModels
{
    public class DzPostsViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Dropzone Dropzone { get; set; }

        public Dropzone_Post NewPost { get; set; }

        public DzEventsViewModel DzEvents { get; set; }

        public IEnumerable<Dropzone_Post> Dropzone_Posts { get; set; }
    }
}
