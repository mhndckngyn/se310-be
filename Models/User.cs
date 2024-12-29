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
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime Updatedat { get; set; }

    [Column("currencyid")]
    public int Currencyid { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("User")]
    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    [ForeignKey("Currencyid")]
    [InverseProperty("Users")]
    public virtual Currency Currency { get; set; } = null!;
}
