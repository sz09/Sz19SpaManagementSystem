namespace SMGS.Presentation.ViewModel.VM
{
    using System.Collections.Generic;

    public class VM_ListStaff
    {
        public VM_ListStaff()
        {
            this.AllStaff = new List<VM_Staff>();
        }
        public List<VM_Staff> AllStaff { get; set; }
        public int ThisPage { get; set; }
        public int TotalPages { get; set; }
    }
}