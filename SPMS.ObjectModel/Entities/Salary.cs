namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Salary")]
    public partial class Salary
    {
        public long Id { get; set; }

        public long StaffId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Time { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TimePay { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalSalary { get; set; }

        public bool IsPaid { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Description { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
