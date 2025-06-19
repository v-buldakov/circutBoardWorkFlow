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
    }
}
