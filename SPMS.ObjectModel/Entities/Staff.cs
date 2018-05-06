namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Attendances = new HashSet<Attendance>();
            DetailsBills = new HashSet<DetailsBill>();
            Salaries = new HashSet<Salary>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public string StaffCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastMiddle { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [StringLength(20)]
        public string IdentifierNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        public bool IsInUse { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Summary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailsBill> DetailsBills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
