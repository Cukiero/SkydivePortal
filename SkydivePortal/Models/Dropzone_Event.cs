using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkydivePortal.Models
{
    public class Dropzone_Event
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        public int DropzoneId { get; set; }
        public Dropzone Dropzone { get; set; }
    }
}
