
using System.ComponentModel.DataAnnotations;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Staff
    {
        public VM_Staff()
        {
            
        }

        public long Id { get; set; }
        public string StaffCode { get; set; }
        public string FirstName { get; set; }
        public string LastMiddle { get; set; }
        public string FullName 
        { 
            get 
            { 
                return this.FirstName.Trim() + " " + this.LastMiddle.Trim(); 
            }
        }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Salary { get; set; }
        public virtual VM_ContactInformation ContactInformation { get; set; }
    }
}