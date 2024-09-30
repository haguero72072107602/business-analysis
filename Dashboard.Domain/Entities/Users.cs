using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Dashboard.Domain.Common;
using Dashboard.Domain.Entities.Common;

namespace Dashboard.Domain.Entities;

[Table("Users")]
public class Users : BaseEntity 
{
    [MaxLength(100)]
    [Required]
    public string UserName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Password { get; set; }

    [NotMapped]
    public string ConfirmedPswd { get; set; }

    [MaxLength(12)]
    [Required]
    public string Role { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }
}