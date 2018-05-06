using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_AnEmpSalaries
    {
        public VM_Salary Emp_Salary { get; set; }

        public VM_EmpSalaryPaying SalaryPaying { get; set; }

        public int History { get; set; }
    }
}