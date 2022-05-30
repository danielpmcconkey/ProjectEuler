using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerProblems.Lib.DAL.Models
{
    [Index(nameof(id))]
    [Table("problem")]
    public class Problem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("name", TypeName = "varchar(256)")]
        public string name { get; set; } = null!;
        [Column("solution", TypeName = "varchar(256)")]
        public string solution { get; set; } = null!;
    }
}
