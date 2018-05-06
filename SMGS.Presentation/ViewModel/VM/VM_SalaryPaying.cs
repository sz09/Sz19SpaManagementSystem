using System;
using System.ComponentModel.DataAnnotations;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_SalaryPaying
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy MMM}")]
        public DateTime Time { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH.mm yyyy dd MMM}")]
        public DateTime? TimePay { get; set; }
        [DisplayFormat(DataFormatString="{0:C0}")]
        public decimal TotalSalary { get; set; }
        public int SumDays { get; set; }
        public bool IsPaid { get; set; }
        public string Description { get; set; }
    }
}