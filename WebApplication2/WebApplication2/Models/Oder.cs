namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Oder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oder()
        {
            OdersDetails = new HashSet<OdersDetail>();
        }

        public int OderID { get; set; }

        public DateTime? CreateDay { get; set; }

        public int? UserID { get; set; }

        public decimal? Total { get; set; }

        public int? Status { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OdersDetail> OdersDetails { get; set; }
    }
}
