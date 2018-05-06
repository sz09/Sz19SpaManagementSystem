namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NotificationDetail
    {
        public long Id { get; set; }

        public long NotificationId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Details { get; set; }

        public virtual Notification Notification { get; set; }
    }
}
