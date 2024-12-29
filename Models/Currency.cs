using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace spendo_be.Models;

[Table("Currency")]
public partial class Currency
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    public string Code { get; set; } = null!;

    [StringLength(10)]
    public string Sign { get; set; } = null!;
}
