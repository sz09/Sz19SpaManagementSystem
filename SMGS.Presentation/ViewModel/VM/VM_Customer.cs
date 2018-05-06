namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Customer
    {
        public long Id { get; set; }
        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastMiddle;
            }
        }
        public string LastMiddle { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
    }
}