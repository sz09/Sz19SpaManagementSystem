namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_ContactInformation
    {
        public long Id { get; set; }
        public int AddressId { get; set; }
        public int EAdressId { get; set; }
        public int ContactForId { get; set; }
        public int ContactTypeId { get; set; }
        public long PersonId { get; set; }
        public bool IsInUse { get; set; }

        public virtual VM_Address Address { get; set; }
        public virtual VM_ContactFor ContactFor { get; set; }
        public virtual VM_ContactType ContactType { get; set; }
        public virtual VM_EAddress EAddress { get; set; }
    }
}