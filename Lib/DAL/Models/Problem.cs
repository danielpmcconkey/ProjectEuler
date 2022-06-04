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
        [Column("id", TypeName="integer")]        
        public int id { get; set; }
        [Column("name", TypeName = "varhcar(250)")]
        public string name { get; set; } = null!;
        [Column("solution", TypeName = "varchar(250)")]
        public string solution { get; set; } = null!;
    }
}
