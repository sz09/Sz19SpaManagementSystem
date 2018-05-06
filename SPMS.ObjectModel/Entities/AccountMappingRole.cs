namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountMappingRole")]
    public partial class AccountMappingRole
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public int RoleId { get; set; }

        public virtual Account Account { get; set; }

        public virtual AccountFor AccountFor { get; set; }
    }
}
