namespace circutBoardWorkFlow.Models.Entity
{
    public class CircuitBoard : Entity<uint>
    {
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public Status Status { get; set; }
        public ICollection<HistoryRecord>? HistoryRecords { get; set; }
    }
}
