using System;
using System.Collections.Generic;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Bed
    {
        public VM_Bed()
        {
            this.TimePeriod = new List<VM_TimePeriod>();
        }
        public int Id { get; set; }
        public string BedCode { get; set; }
        public string Name { get; set; }

        public List<VM_TimePeriod> TimePeriod { get; set; }


        public bool CheckDateTimeInPeriod(DateTime dateTime)
        {
            foreach (var item in this.TimePeriod)
            {
                if (item.From <= dateTime && dateTime <= item.To)
                    return true;
            }
            return false;
        }
    }
}