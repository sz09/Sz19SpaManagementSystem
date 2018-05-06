
namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_Province
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string ProvinceName { get; set; }
        public virtual VM_Country Country { get; set; }
    }
}
