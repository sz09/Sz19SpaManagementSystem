namespace SPMS.ObjectModel.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactInformation")]
    public partial class ContactInformation
    {
        public long Id { get; set; }

        public int AddressId { get; set; }

        public int EAdressId { get; set; }

        public int ContactForId { get; set; }

        public int ContactTypeId { get; set; }

        public long PersonId { get; set; }

        public bool IsInUse { get; set; }

        public virtual Address Address { get; set; }

        public virtual ContactFor ContactFor { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual EAddress EAddress { get; set; }
    }
}
