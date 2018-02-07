using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.HomeViewModels
{
    public class PagePostsViewModel
    {
        public PagePost NewPost { get; set; }
        public IEnumerable<PagePost> PagePosts { get; set; }
    }
}
