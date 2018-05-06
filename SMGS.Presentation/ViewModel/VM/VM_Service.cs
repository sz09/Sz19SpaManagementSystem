namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Service
    {
        public int Id { get; set; }
        public string ServiceCode { get; set; }
        public decimal Cost { get; set; }
        public VM_Time TimeCost { get; set; }
        public string Name { get; set; }
        public bool IsInUse { get; set; }
    }

    public class VM_Time
    {
        public VM_Time()
        {

        }
        public VM_Time(int hour, int minute)
        {
            this.Hour = hour;
            this.minute = minute;
        }
        public int Hour { get; set; }
        public int minute { get; set; }
    }
}