using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Framework;

[Table("Account")]
public class Account
{
    [StringLength(20)]
    public string UserName { get; set; } = string.Empty;

    [StringLength(50)]
    public string Password { get; set; } = string.Empty;
}
