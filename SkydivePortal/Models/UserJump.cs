using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models
{
    public class UserJump
    {
        public int Id { get; set; }
        public int Number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? Height { get; set; }
        public string Plane { get; set; }
        public string Video { get; set; }
        public string Note { get; set; }
        public int? DropzoneId { get; set; }
        public Dropzone Dropzone { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Parachute { get; set; }

    }
}
