namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockPackageDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public long StockPackageId { get; set; }

        public long StockId { get; set; }

        public int Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAdd { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual StockPackage StockPackage { get; set; }
    }
}
