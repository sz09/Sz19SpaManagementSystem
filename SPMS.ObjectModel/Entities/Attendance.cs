namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Attendance
    {
        public long Id { get; set; }

        public long StaffId { get; set; }

        public DateTime Time { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
