using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Framework
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }

        [StringLength(20, ErrorMessage = "Maximum 20 characters")]
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Please enter category name")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("SEO Title")]
        public string Alias { get; set; }

        [DisplayName("Parent Menu")]
        public int? ParentId { get; set; }

        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = "Numeric only")]
        public int? Order { get; set; }

        public bool? Status { get; set; }
    }
}
