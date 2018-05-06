using System.ComponentModel.DataAnnotations;
using System;
namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Salary
    {
        public long Id { get; set; }
        public string FirstName { get; set; }

        public string LastMiddle { get; set; }
        public string FullName { 
            get 
            {
                return this.FirstName.Trim() + " " + this.LastMiddle.Trim();
            }
        }
        public string EmpCode { get; set; }
        public decimal Salary { get; set; }
    }
}