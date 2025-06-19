using circutBoardWorkFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace circutBoardWorkFlow
{
    public class CircuitBoardService
    {
        // при необходимости можем вынести конфигурирование словаря в конфиг файл
        private readonly Dictionary<Status, IReadOnlyCollection<Status>> availableStatusChange = new()
        {
            {Status.Registration, new [] {Status.ComponentInstallment } },
            {Status.ComponentInstallment,new[] { Status.QualityAssurance } },
            {Status.QualityAssurance, new[] {Status.Repair, Status.Package } },
            {Status.Repair, new[] {Status.QualityAssurance } }
        };

        private readonly CircuitBoardContext _circuitBoardContext;

        public CircuitBoardService(CircuitBoardContext circuitBoardContext)
        {
            _circuitBoardContext = circuitBoardContext;
        }

        public async Task<Result<CircuitBoard>> Create(CircuitBoard newBoard)
        {
            var board = new CircuitBoard
            {
                Status = Status.Registration,
                Created = DateTimeOffset.Now
            };

            var result = CircuitBoard.ConvertToEntity(board);
            _circuitBoardContext.Add(result);

            await _circuitBoardContext.SaveChangesAsync();

            return Result<CircuitBoard>.Success(CircuitBoard.ConvertFromEntity(result));
        }

        public async Task<Result<CircuitBoard>> Update(uint id, CircuitBoard newBoard)
        {
            var board = await _circuitBoardContext.CircuitBoards.FindAsync(id);

            if(board is null)
                return Result<CircuitBoard>.Failure(new(StatusCodes.Status404NotFound, "Not found", $"Board with {id} not found"));


            if (!availableStatusChange.TryGetValue(board.Status, out var nextStatus))
                return Result<CircuitBoard>.Failure(new(StatusCodes.Status400BadRequest, "Bad request", "Not supported status"));

            if(!nextStatus.Contains(newBoard.Status))
                return Result<CircuitBoard>.Failure(new(StatusCodes.Status400BadRequest, "Bad request", $"Cant change status from {board.Status} to {newBoard.Status}"));

            _circuitBoardContext.HistoryRecords.Add(new Models.Entity.HistoryRecord
            {
                Board = board,
                BoardId = board.Id,
                OldStatus = board.Status,
                NewStatus = newBoard.Status,
                Updated = DateTimeOffset.Now
            });
            board.Status = newBoard.Status;
            board.Updated = DateTimeOffset.Now;
            await _circuitBoardContext.SaveChangesAsync();

            return Result<CircuitBoard>.Success(CircuitBoard.ConvertFromEntity(board));
        }

        public async Task<Result<ICollection<HistoryRecord>>> GetHistory(uint id)
        {
            var historyRecords = await _circuitBoardContext.HistoryRecords.AsNoTracking().Where(record => record.BoardId == id).ToArrayAsync();

            if(historyRecords is null)
                return Result<ICollection<HistoryRecord>>.Success(Array.Empty<HistoryRecord>());

            return Result<ICollection<HistoryRecord>>.Success(HistoryRecord.ConvertFromEntity(historyRecords));
        }
    }
}
