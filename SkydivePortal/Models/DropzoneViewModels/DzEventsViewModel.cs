using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkydivePortal.Models.DropzoneViewModels
{
    public class DzEventsViewModel
    {
        public Dropzone_Event newEvent { get; set; }
        public IEnumerable<Dropzone_Event> Dropzone_Events { get; set; }
        
        public int NextEventId { get; set; }
    }
}
