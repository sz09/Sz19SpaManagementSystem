namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BedName")]
    public partial class BedName
    {
        public int Id { get; set; }

        public int BedId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int LanguageId { get; set; }

        public virtual Bed Bed { get; set; }
    }
}
