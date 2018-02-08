using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkydivePortal.Models
{
    public class Dropzone_User_Post
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public ICollection<Dropzone_User_Post_Image> Dropzone_User_Post_Images { get; set; }
        public ICollection<Dropzone_User_Post_Comment> Dropzone_User_Post_Comments { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int DropzoneId { get; set; }
        public Dropzone Dropzone { get; set; }
    }
}
