namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Service")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            DetailsBills = new HashSet<DetailsBill>();
            ServiceDesciptions = new HashSet<ServiceDesciption>();
            ServiceNames = new HashSet<ServiceName>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string ServiceCode { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        public int TimeCost { get; set; }

        public bool IsInUse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailsBill> DetailsBills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceDesciption> ServiceDesciptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceName> ServiceNames { get; set; }
    }
}
