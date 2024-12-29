using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace spendo_be.Models;

[Table("Budget")]
public partial class Budget
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("startdate")]
    public DateOnly Startdate { get; set; }

    [Column("enddate")]
    public DateOnly Enddate { get; set; }

    [Column("period")]
    public int Period { get; set; }

    [Column("current")]
    public decimal Current { get; set; }

    [Column("budgetlimit")]
    public decimal Budgetlimit { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime Updatedat { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("categoryid")]
    public int? Categoryid { get; set; }

    [ForeignKey("Categoryid")]
    [InverseProperty("Budgets")]
    public virtual Category? Category { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Budgets")]
    public virtual User User { get; set; } = null!;
}
