using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace spendo_be.Models;

[Table("Transfer")]
public partial class Transfer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime Updatedat { get; set; }

    [Column("sourceaccountid")]
    public int Sourceaccountid { get; set; }

    [Column("targetaccountid")]
    public int Targetaccountid { get; set; }

    [Column("categoryid")]
    public int? Categoryid { get; set; }

    [ForeignKey("Categoryid")]
    [InverseProperty("Transfers")]
    public virtual Category? Category { get; set; }

    [ForeignKey("Sourceaccountid")]
    [InverseProperty("TransferSourceaccounts")]
    public virtual Account Sourceaccount { get; set; } = null!;

    [ForeignKey("Targetaccountid")]
    [InverseProperty("TransferTargetaccounts")]
    public virtual Account Targetaccount { get; set; } = null!;
}
