using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkydivePortal.Models
{
    public class Dropzone
    {
        public int Id { get; set; }
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
