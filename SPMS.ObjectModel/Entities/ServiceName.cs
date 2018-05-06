namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceName")]
    public partial class ServiceName
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public virtual Service Service { get; set; }
    }
}
