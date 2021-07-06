namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OdersDetails = new HashSet<OdersDetail>();
        }

        public int ProductID { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public decimal? Price { get; set; }

        public decimal? PriceSale { get; set; }

        public int? Quantity { get; set; }

        public string Descriptions { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public bool? Status { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OdersDetail> OdersDetails { get; set; }
    }
}
