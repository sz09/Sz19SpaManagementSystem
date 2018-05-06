namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Address
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string AddressNumberNoAndStreet { get; set; }

        public virtual VM_District District { get; set; }
    }
}
