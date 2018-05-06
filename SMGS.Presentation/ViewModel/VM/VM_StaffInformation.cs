namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_StaffInformation
    {
        public long Id { get; set; }
        public string StaffCode { get; set; }
        public string FirstName { get; set; }
        public string LastMiddle { get; set; }
        public string FullName
        {
            get { return this.FirstName.Trim() + " " + this.LastMiddle.Trim(); }
        }

        public string Image { get; set; }
        public string IdentifierNumber { get; set; }
        public decimal Salary { get; set; }
        public bool IsInUse { get; set; }
        public string Summary { get; set; }
        public virtual VM_ContactInformation ContactInformation { get; set; }
    }
}