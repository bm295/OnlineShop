using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Framework
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        public int? ParentId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Order { get; set; }

        public bool? Status { get; set; }
    }
}
