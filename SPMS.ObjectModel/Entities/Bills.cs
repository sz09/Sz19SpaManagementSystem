namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bills
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bills()
        {
            DetailsBills = new HashSet<DetailsBill>();
        }

        public long Id { get; set; }

        public DateTime? Time { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalCost { get; set; }

        public long StaffId { get; set; }

        public bool IsPaid { get; set; }
        [Required]
        public int BedId { get; set; }

        public DateTime? PeriodFrom { get; set; }

        public DateTime? PeriodTo { get; set; }

        public DateTime? TimePaid { get; set; }

        [Required]
        public long CustomerId { get; set; }

        public virtual Bed Bed { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailsBill> DetailsBills { get; set; }
    }
}
