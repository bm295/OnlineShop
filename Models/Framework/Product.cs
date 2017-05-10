using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Framework
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(250)]
        public string Images { get; set; }

        public DateTime? CreatedDate { get; set; }

        public decimal? Price { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public bool? Status { get; set; }
    }
}
