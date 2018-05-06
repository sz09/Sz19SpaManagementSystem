namespace SMGS.Presentation.ViewModel.VM
{
    using System.ComponentModel.DataAnnotations;

    public class VM_EAddress
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public long StaffId { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([A-Za-z0-9_.\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$")]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string NumberPhone { get; set; }
        [Required]
        [MaxLength(100)]
        public string Website { get; set; }
    }
}
