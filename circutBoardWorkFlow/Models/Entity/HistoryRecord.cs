namespace circutBoardWorkFlow.Models.Entity
{
    public class HistoryRecord: Entity<uint>
    {
        public Status OldStatus { get; set; }
        public Status NewStatus { get; set; }
        public DateTimeOffset Updated { get; set; }

        public uint BoardId { get; set; }
        public required CircuitBoard Board { get; set; }
    }
}
