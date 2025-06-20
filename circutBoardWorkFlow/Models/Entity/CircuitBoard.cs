using System.ComponentModel.DataAnnotations;

namespace circutBoardWorkFlow.Models.Entity
{
    public class CircuitBoard : Entity<uint>
    {
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public Status Status { get; set; }
        public ICollection<HistoryRecord>? HistoryRecords { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
