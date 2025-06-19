using circutBoardWorkFlow.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace circutBoardWorkFlow.Controllers
{
    [ApiController]
    public class CircuitBoardController : ControllerBase
    {
        private readonly CircuitBoardService _circuitBoardService;

        public CircuitBoardController(CircuitBoardService circuitBoardService)
        {
            _circuitBoardService = circuitBoardService;
        }

        [HttpPost]
        public async Task<Result<CircuitBoard>> Create(CircuitBoard newBoard) => await _circuitBoardService.Create(newBoard);

        [HttpPatch]
        public async Task<Result<CircuitBoard>> Update(uint boardId, [FromBody]CircuitBoard board) => await _circuitBoardService.Update(boardId, board);

        [HttpGet]
        public async Task<Result<ICollection<HistoryRecord>>> GetHistory(uint boardId) => await _circuitBoardService.GetHistory(boardId);

    }
}
