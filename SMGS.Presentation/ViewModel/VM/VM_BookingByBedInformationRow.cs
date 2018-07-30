using System.Collections.Generic;
using System;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_BookingByBedInformationRow
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal  Cost { get; set; }
        public bool IsPaid { get; set; }
    }
}