namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockName")]
    public partial class StockName
    {
        public long Id { get; set; }

        public long StockId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int LanguageId { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
