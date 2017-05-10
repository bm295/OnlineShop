using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Framework
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
