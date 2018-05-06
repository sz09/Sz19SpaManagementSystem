namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceDesciption
    {
        public long Id { get; set; }

        public int ServiceId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Description { get; set; }

        public byte DescriptionForTypeId { get; set; }

        public virtual DescriptionForType DescriptionForType { get; set; }

        public virtual Service Service { get; set; }
    }
}
