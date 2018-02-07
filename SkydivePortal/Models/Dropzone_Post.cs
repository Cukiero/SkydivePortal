using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkydivePortal.Models
{
    public class Dropzone_Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int DropzoneId { get; set; }
        public Dropzone Dropzone { get; set; }
        public ICollection<Dropzone_Post_Image> Dropzone_Post_Images { get; set; }
    }
}
