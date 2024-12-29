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
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Period { get; set; }

    public decimal Current { get; set; }

    public decimal Limit { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    public int? CategoryId { get; set; }

    public int UserId { get; set; }
}
