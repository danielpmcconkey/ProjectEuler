using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerProblems.Lib.DAL.Models
{
    [Index(nameof(id))]
    [Table("run")]
    public class Run
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column("problem", TypeName = "integer")]
        public int problem { get; set; }
        [Column("durationms", TypeName = "numeric(10,4)")]
        public double duration { get; set; }        
    }
}
