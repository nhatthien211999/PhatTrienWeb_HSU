namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OdersDetail")]
    public partial class OdersDetail
    {
        [Key]
        [Column(Order = 0)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OderID { get; set; }

        public int? Quantity { get; set; }

        public decimal? TotalPrice { get; set; }

        public virtual Oder Oder { get; set; }

        public virtual Product Product { get; set; }
    }
}
