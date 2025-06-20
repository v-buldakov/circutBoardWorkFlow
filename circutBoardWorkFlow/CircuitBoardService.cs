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

        public async Task<Result<CircuitBoardViewModel>> Create(CircuitBoardViewModel newBoard)
        {
            var board = new CircuitBoardViewModel
            {
                Name = newBoard.Name,
                Status = Status.Registration,
                Created = DateTimeOffset.Now
            };

            var result = CircuitBoardViewModel.ConvertToEntity(board);
            _circuitBoardContext.Add(result);

            await _circuitBoardContext.SaveChangesAsync();

            return Result<CircuitBoardViewModel>.Success(CircuitBoardViewModel.ConvertFromEntity(result));
        }

        public async Task<Result<CircuitBoardViewModel>> Update(uint id, CircuitBoardViewModel newBoard)
        {
            var board = await _circuitBoardContext.CircuitBoards.FindAsync(id);

            if (board is null)
                return Result<CircuitBoardViewModel>.Failure(new(StatusCodes.Status404NotFound, "Not found", $"Board with {id} not found"));


            if (!availableStatusChange.TryGetValue(board.Status, out var nextStatus))
                return Result<CircuitBoardViewModel>.Failure(new(StatusCodes.Status400BadRequest, "Bad request", "Not supported status"));

            if (!nextStatus.Contains(newBoard.Status))
                return Result<CircuitBoardViewModel>.Failure(new(StatusCodes.Status400BadRequest, "Bad request", $"Cant change status from {board.Status} to {newBoard.Status}"));

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

            return Result<CircuitBoardViewModel>.Success(CircuitBoardViewModel.ConvertFromEntity(board));
        }

        public async Task<Result<ICollection<HistoryRecordViewModel>>> GetHistory(uint id)
        {
            var historyRecords = await _circuitBoardContext.HistoryRecords.AsNoTracking().Where(record => record.BoardId == id).ToArrayAsync();

            if (historyRecords is null)
                return Result<ICollection<HistoryRecordViewModel>>.Success(Array.Empty<HistoryRecordViewModel>());

            return Result<ICollection<HistoryRecordViewModel>>.Success(HistoryRecordViewModel.ConvertFromEntity(historyRecords));
        }
    }
}
