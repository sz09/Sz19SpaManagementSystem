using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_NewBooking
    {
        public int BedId { get; set; }
        public string BedName { get; set; }
        public List<VM_Service> Services { get; set; }
        public string  Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}