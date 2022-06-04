using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EulerProblems.Lib.DAL.Models
{
    [Index(nameof(id))]
    [Table("baseline")]
    public class Baseline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column("averagedurationms", TypeName = "numeric(10,4)")]
        public double averageDuration { get; set; }
        [Column("percentile90durationms", TypeName = "numeric(10,4)")]
        public double percentile90Duration { get; set; }
    }
}
