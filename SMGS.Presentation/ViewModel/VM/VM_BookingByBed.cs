using System.Collections.Generic;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_BookingByBed
    {
        public VM_BookingByBed()
        {
            this.Bookings = new List<VM_BookingByBedInformationRow>();
        }
        public int BedId { get; set; }
        public List<VM_BookingByBedInformationRow> Bookings { get; set; }
    }
}