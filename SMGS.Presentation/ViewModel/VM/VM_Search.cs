using System.Collections.Generic;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Search
    {
        public VM_Search()
        {
            this.ListAddress = new List<VM_Address>();
            this.ListBed = new List<VM_Bed>();
            this.ListCustomer = new List<VM_Customer>();
            this.ListEmployee = new List<VM_Staff>();
            this.ListService = new List<VM_Service>();
            this.ListStock = new List<VM_Stock>();
        }
        public List<VM_Address> ListAddress { get; set; }
        public List<VM_Bed> ListBed { get; set; }
        public List<VM_Customer> ListCustomer { get; set; }
        public List<VM_Service> ListService { get; set; }
        public List<VM_Staff> ListEmployee { get; set; }
        public List<VM_Stock> ListStock { get; set; }
        public int SearchResultsInSearch { get; set; }
    }
}