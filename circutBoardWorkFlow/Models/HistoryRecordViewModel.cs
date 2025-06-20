namespace circutBoardWorkFlow.Models
{
    public class HistoryRecordViewModel
    {
        public long Id { get; set; }
        public Status OldStatus { get; set; }
        public Status NewStatus { get; set; }
        public DateTimeOffset Updated { get; set; }

        static public ICollection<HistoryRecordViewModel> ConvertFromEntity(ICollection<Entity.HistoryRecord>? records)
        {
            if (records == null || records.Count == 0)
                return [];
            return [.. records.Select(CreateNewRecord)];
        }

        static public ICollection<Entity.HistoryRecord> ConvertToEntity(ICollection<HistoryRecordViewModel> records, Entity.CircuitBoard board)
        {
            return [.. records.Select(record => CreateNewEntity(board, record))];
        }

        private static HistoryRecordViewModel CreateNewRecord(Entity.HistoryRecord record)
        {
            return new HistoryRecordViewModel
            {
                Id = record.Id,
                OldStatus = record.OldStatus,
                NewStatus = record.NewStatus,
                Updated = record.Updated
            };
        }

        private static Entity.HistoryRecord CreateNewEntity(Entity.CircuitBoard board, HistoryRecordViewModel record)
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
