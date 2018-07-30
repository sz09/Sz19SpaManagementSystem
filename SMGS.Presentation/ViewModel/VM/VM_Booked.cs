using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Booked
    {

        public long Id { get; set; }

        public DateTime? Time { get; set; }

        public decimal TotalCost { get; set; }

        public string StaffName { get; set; }
        
        public string BedName { get; set; }

        public DateTime? PeriodFrom { get; set; }

        public DateTime? PeriodTo { get; set; }
        

        public string CusomerName { get; set; }
        public string Services { get; set; }
        public string Description { get; set; }
    }
}