using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_NewBooking
    {
        public VM_NewBooking()
        {
            this.Customers = new List<VM_Customer>();
            this.Services = new List<VM_Service>();
        }
        public int BedId { get; set; }
        public string BedName { get; set; }
        public List<VM_Customer> Customers { get; set; }
        public List<VM_Service> Services { get; set; }
        public string  Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}