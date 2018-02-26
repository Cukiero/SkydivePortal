using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SkydivePortal.Models
{
    public enum Role
    {
        Master, Admin, Moderator, None
    }
    public class ApplicationRole
    {
        public int Id { get; set; }
        public Role Name { get; set; }
        public int? DropzoneId { get; set; }
        public Dropzone Dropzone { get; set; }
}
}
