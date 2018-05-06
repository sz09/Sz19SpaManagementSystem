namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockPackage")]
    public partial class StockPackage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockPackage()
        {
            StockPackageDetails = new HashSet<StockPackageDetail>();
        }

        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAdd { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockPackageDetail> StockPackageDetails { get; set; }
    }
}
