using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_EmpSalaryPaying
    {
        public VM_EmpSalaryPaying()
        {
            this.ListEmpPaying = new List<VM_SalaryPaying>();
        }
        public List<VM_SalaryPaying> ListEmpPaying { get; set; }
        public int ToltalPages { get; set; }
        public int ThisPage { get; set; }
    }
}