
namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_District
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string DistrictName { get; set; }
        public virtual VM_Province Province { get; set; }
    
    }
}
