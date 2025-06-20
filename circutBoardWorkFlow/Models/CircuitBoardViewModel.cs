using System.ComponentModel.DataAnnotations;

namespace circutBoardWorkFlow.Models
{
    public class CircuitBoardViewModel
    {
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public Status Status { get; set; }
        public ICollection<HistoryRecordViewModel> History { get; set; } = Array.Empty<HistoryRecordViewModel>();

        public static CircuitBoardViewModel ConvertFromEntity(Entity.CircuitBoard board)
        {
            return new CircuitBoardViewModel
            {
                Id = board.Id,
                Name = board.Name,
                Created = board.Created,
                Updated = board.Updated,
                History = HistoryRecordViewModel.ConvertFromEntity(board.HistoryRecords),
                Status = board.Status
            };
        }

        public static Entity.CircuitBoard ConvertToEntity(CircuitBoardViewModel board)
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
                entity.HistoryRecords = HistoryRecordViewModel.ConvertToEntity(board.History, entity);
            }

            return entity;
        }
    }
}
