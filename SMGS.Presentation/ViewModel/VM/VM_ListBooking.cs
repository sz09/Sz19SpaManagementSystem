using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_ListBooking
    {
        public VM_ListBooking()
        {
            this.Bookings = new List<VM_Booking>();
            this.Beds = new List<VM_Bed>();
        }
        public IEnumerable<VM_Booking> Bookings { get; set; }
        public IEnumerable<VM_Bed> Beds { get; set; }
    }
}