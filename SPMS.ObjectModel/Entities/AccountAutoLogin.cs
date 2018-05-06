namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountAutoLogin")]
    public partial class AccountAutoLogin
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Xs { get; set; }
    }
}
