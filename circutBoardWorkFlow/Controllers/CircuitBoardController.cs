using circutBoardWorkFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace circutBoardWorkFlow.Controllers
{
    [ApiController]
    [Route("")]
    public class CircuitBoardController : ControllerBase
    {
        private readonly CircuitBoardService _circuitBoardService;

        public CircuitBoardController(CircuitBoardService circuitBoardService)
        {
            _circuitBoardService = circuitBoardService;
        }

        [HttpPost, Route("/create")]
        public async Task<Result<CircuitBoardViewModel>> Create(CircuitBoardViewModel newBoard) => await _circuitBoardService.Create(newBoard);

        [HttpPatch, Route("/update/{boardId}")]
        public async Task<Result<CircuitBoardViewModel>> Update(long boardId, [FromBody]CircuitBoardViewModel board) => await _circuitBoardService.Update(boardId, board);

        [HttpGet, Route("/history/{boardId}")]
        public async Task<Result<ICollection<HistoryRecordViewModel>>> GetHistory(long boardId) => await _circuitBoardService.GetHistory(boardId);

    }
}
