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
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("code")]
    [StringLength(10)]
    public string Code { get; set; } = null!;

    [Column("sign")]
    [StringLength(10)]
    public string Sign { get; set; } = null!;

    [InverseProperty("Currency")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
