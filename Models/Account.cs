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
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("balance")]
    public decimal Balance { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime Updatedat { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [InverseProperty("Account")]
    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    [InverseProperty("Sourceaccount")]
    public virtual ICollection<Transfer> TransferSourceaccounts { get; set; } = new List<Transfer>();

    [InverseProperty("Targetaccount")]
    public virtual ICollection<Transfer> TransferTargetaccounts { get; set; } = new List<Transfer>();

    [ForeignKey("Userid")]
    [InverseProperty("Accounts")]
    public virtual User User { get; set; } = null!;
}
