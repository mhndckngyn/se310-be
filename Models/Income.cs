﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace spendo_be.Models;

[Table("Income")]
public partial class Income
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    public decimal Amount { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    public int AccountId { get; set; }

    public int? CategoryId { get; set; }
}
