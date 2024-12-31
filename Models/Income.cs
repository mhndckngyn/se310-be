using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace spendo_be.Models;

[Table("Income")]
public partial class Income
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
    
    [Column("date", TypeName = "timestamp without time zone")]
    public DateTime Date { get; set; }

    [Column("accountid")]
    public int Accountid { get; set; }

    [Column("categoryid")]
    public int? Categoryid { get; set; }

    [ForeignKey("Accountid")]
    [InverseProperty("Incomes")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("Categoryid")]
    [InverseProperty("Incomes")]
    public virtual Category? Category { get; set; }
}
