using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;
using System.Threading.Tasks;

namespace ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] Board board)
        {
            if (board == null)
            {
                return BadRequest("Board cannot be null.");
            }

            var createdBoard = await _boardService.CreateBoardAsync(board);
            return CreatedAtAction(nameof(GetBoardById), new { id = createdBoard.Id }, createdBoard);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardById(int id)
        {
            var board = await _boardService.GetBoardByIdAsync(id);
            if (board == null)
            {
                return NotFound($"Board with ID {id} not found.");
            }

            return Ok(board);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            var boards = await _boardService.GetAllBoardsAsync();
            return Ok(boards);
        }
    }
}
