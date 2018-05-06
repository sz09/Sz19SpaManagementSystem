namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailsBill")]
    public partial class DetailsBill
    {
        public long Id { get; set; }

        public long BillId { get; set; }

        public int ServiceId { get; set; }

        public long StaffId { get; set; }

        public virtual Bills Bill { get; set; }

        public virtual Service Service { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
