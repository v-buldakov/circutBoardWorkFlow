using System.ComponentModel.DataAnnotations;

namespace circutBoardWorkFlow.Models
{
    public class CircuitBoard
    {
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public Status Status { get; set; }
        public ICollection<HistoryRecord> History { get; set; } = Array.Empty<HistoryRecord>();

        public static CircuitBoard ConvertFromEntity(Entity.CircuitBoard board)
        {
            return new CircuitBoard
            {
                Id = board.Id,
                Name = board.Name,
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
                Name = board.Name,
                Created = board.Created,
                Updated = board.Updated,
                Status = board.Status,
            };
            
            if (board.History != null && board.History.Count > 0)
            {
                entity.HistoryRecords = HistoryRecord.ConvertToEntity(board.History, entity);
            }

            return entity;
        }
    }
}
