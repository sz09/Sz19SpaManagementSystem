using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Book
    {

        public long Id { get; set; }

        public DateTime? Time { get; set; }

        public decimal TotalCost { get; set; }

        public long StaffId { get; set; }
        
        public int BedId { get; set; }

        public DateTime? PeriodFrom { get; set; }

        public DateTime? PeriodTo { get; set; }
        

        public long CustomerId { get; set; }
    }
}