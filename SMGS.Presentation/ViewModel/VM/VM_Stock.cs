using System.Collections.Generic;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Stock
    {
        public long Id { get; set; }
        public decimal Cost { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
    }
    public class VM_ListStock
    {
        public VM_ListStock()
        {
            this.Stocks = new List<VM_Stock>();
            this.ThisPage = 0;
            this.TotalPages = 0;
        }
        public int ThisPage { get; set; }
        public int TotalPages { get; set; }
        public List<VM_Stock> Stocks { get; set; }
    }
}