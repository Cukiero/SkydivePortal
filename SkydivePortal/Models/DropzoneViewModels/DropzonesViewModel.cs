using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.DropzoneViewModels
{
    public class DropzonesViewModel
    {
        public IEnumerable<Dropzone> Dropzones { get; set; }
        public IEnumerable<Region> Regions { get; set; }
    }
}
