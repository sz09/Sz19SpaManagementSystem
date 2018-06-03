using System.ComponentModel.DataAnnotations;

namespace SMGS.Presentation.ViewModel.VM
{
    public class VM_ServiceName
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int LanguageId { get; set; }
    }
}