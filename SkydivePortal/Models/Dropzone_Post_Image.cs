using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkydivePortal.Models
{
    public class Dropzone_Post_Image
    {
        public int Id { get; set; }
        public int Dropzone_PostId { get; set; }
        public Dropzone_Post Dropzone_Post { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
