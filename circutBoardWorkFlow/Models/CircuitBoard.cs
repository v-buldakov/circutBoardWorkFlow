namespace circutBoardWorkFlow.Models
{
    public class CircuitBoard
    {
        public uint Id { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public Status Status { get; set; }
        public ICollection<HistoryRecord> History { get; set; } = Array.Empty<HistoryRecord>();

        public static CircuitBoard ConvertFromEntity(Entity.CircuitBoard board)
        {
            return new CircuitBoard
            {
                Id = board.Id,
                Created = board.Created,
                Updated = board.Updated,
                History = HistoryRecord.ConvertFromEntity(board.HistoryRecords),
                Status = board.Status
            };
        }

        public static Entity.CircuitBoard ConvertToEntity(CircuitBoard board)
        {
            Entity.CircuitBoard entity = new Entity.CircuitBoard
            {
                Id = board.Id,
                Created = board.Created,
                Updated = board.Updated,
                Status = board.Status,
            };

            entity.HistoryRecords = HistoryRecord.ConvertToEntity(board.History, entity);

            return entity;
        }
    }
}
