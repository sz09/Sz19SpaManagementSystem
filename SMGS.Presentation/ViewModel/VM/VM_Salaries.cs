using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Salaries
    {
        public VM_Salaries()
        {
            this.Salaries = new List<VM_SalaryInfo>();
        }
        public DateTime Time{ get; set; }
        public List<VM_SalaryInfo> Salaries { get; set; }
    }
    

    public class VM_SalaryInfo
    {
        public long Id { get; set; }
        public string FirstName { get; set; }

        public string LastMiddle { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName.Trim() + " " + this.LastMiddle.Trim();
            }
        }
        public string EmpCode { get; set; }
        public decimal Salary { get; set; }
        public DateTime TimePaid { get; set; }
        public string Description { get; set; }

        public string CurrencyCode { get; set; }
    }
    public class VM_SalariesInMonth
    {
        public VM_SalariesInMonth()
        {
            this.MonthSalaries = new List<VM_Salaries>();
        }
        public List<VM_Salaries> MonthSalaries { get; set; }
    }
}