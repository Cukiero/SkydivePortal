using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models
{
    public class Dropzone_User_Post_Image
    {
        public int Id { get; set; }
        public int Dropzone_User_PostId { get; set; }
        public Dropzone_User_Post Dropzone_User_Post { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }

    }
}
