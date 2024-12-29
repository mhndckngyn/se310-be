using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace spendo_be.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string? Password { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    public int CurrencyId { get; set; }
}
