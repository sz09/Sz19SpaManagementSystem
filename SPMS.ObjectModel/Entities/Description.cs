namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Description")]
    public partial class Description
    {
        public long Id { get; set; }

        public int KindToDescription { get; set; }

        [Column("Description", TypeName = "ntext")]
        [Required]
        public string Description1 { get; set; }

        public int LanguageId { get; set; }

        public int DescriptionFor { get; set; }

        public virtual DescriptionFor DescriptionFor1 { get; set; }

        public virtual Language Language { get; set; }
    }
}
