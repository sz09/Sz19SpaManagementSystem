using System.Collections.Generic;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_ListSalary
    {
        public VM_ListSalary()
        {
            this.ListSalary = new List<VM_Salary>();
        }
        public List<VM_Salary> ListSalary { get; set; }
        public int ThisPage { get; set; }
        public int TotalPages { get; set; }
    }
}