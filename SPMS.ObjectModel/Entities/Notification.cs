namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Notification()
        {
            NotificationDetails = new HashSet<NotificationDetail>();
        }

        public long Id { get; set; }

        public long ForAccountId { get; set; }

        public DateTime Time { get; set; }

        [Required]
        [StringLength(200)]
        public string Summary { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Details { get; set; }

        public bool IsRead { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationDetail> NotificationDetails { get; set; }
    }
}
