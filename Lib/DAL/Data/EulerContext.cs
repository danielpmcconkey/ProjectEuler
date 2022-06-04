using EulerProblems.Lib.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Json;

namespace EulerProblems.Lib.DAL.Data
{
    public class EulerContext: DbContext
    {
        public DbSet<Problem> Problems { get; set; } = null!;
        public DbSet<Baseline> Baselines { get; set; } = null!;
        public DbSet<Run> Runs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = Environment.GetEnvironmentVariable
                ("EulerDBConnection", EnvironmentVariableTarget.User);

            if (connectionString != null) optionsBuilder.UseNpgsql(connectionString);
            else throw new NullReferenceException("Connection string was null");
        }
    }
}
