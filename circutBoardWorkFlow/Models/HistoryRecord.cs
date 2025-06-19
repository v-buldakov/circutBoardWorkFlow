namespace circutBoardWorkFlow.Models
{
    public class HistoryRecord
    {
        public uint Id { get; set; }
        public Status OldStatus { get; set; }
        public Status NewStatus { get; set; }
        public DateTimeOffset Updated { get; set; }

        static public ICollection<HistoryRecord> ConvertFromEntity(ICollection<Entity.HistoryRecord> records)
        {
            return [.. records.Select(CreateNewRecord)];
        }

        static public ICollection<Entity.HistoryRecord> ConvertToEntity(ICollection<HistoryRecord> records, Entity.CircuitBoard board)
        {
            return [.. records.Select(record => CreateNewEntity(board, record))];
        }

        private static HistoryRecord CreateNewRecord(Entity.HistoryRecord record)
        {
            return new HistoryRecord
            {
                Id = record.Id,
                OldStatus = record.OldStatus,
                NewStatus = record.NewStatus,
                Updated = record.Updated
            };
        }

        private static Entity.HistoryRecord CreateNewEntity(Entity.CircuitBoard board, HistoryRecord record)
        {
            return new Entity.HistoryRecord 
            {
                Id = record.Id,
                OldStatus = record.OldStatus,
                NewStatus = record.NewStatus,
                Updated = record.Updated,
                Board = board,
                BoardId = board.Id
            };
        }
    }
}
