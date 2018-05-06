using System.Collections.Generic;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_ListServices
    {

        public VM_ListServices()
        {
            this.ListServices = new List<VM_Service>();
        }

        public IEnumerable<VM_Service> ListServices { get; set; }
        public int ThisPage { get; set; }
        public int TotalPages { get; set; }
    }
}