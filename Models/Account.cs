using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace spendo_be.Models;

[Table("Account")]
public partial class Account
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public decimal Balance { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }
}