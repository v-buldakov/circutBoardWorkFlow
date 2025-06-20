using circutBoardWorkFlow.Models;
using circutBoardWorkFlow.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace circutBoardWorkFlow
{
    public class CircuitBoardContext : DbContext
    {
        public CircuitBoardContext(DbContextOptions options) : base(options)
        {
        }

        protected CircuitBoardContext()
        {
        }

        public DbSet<CircuitBoard> CircuitBoards { get; set; }
        public DbSet<HistoryRecord> HistoryRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CircuitBoard>()
            .Property(e => e.Status)
            .HasConversion(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status), v)
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
