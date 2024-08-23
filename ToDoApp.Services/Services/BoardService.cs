using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services
{
    public class BoardService : IBoardService
    {
        private readonly ToDoContext _context;

        public BoardService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<Board> CreateBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<Board> GetBoardByIdAsync(int boardId)
        {
            Console.WriteLine($"Searching for Board with ID: {boardId}");
            var board = await _context.Boards.FindAsync(boardId);
            if (board == null)
            {
                Console.WriteLine($"Board with ID: {boardId} not found.");
            }
            return board;
        }


        public async Task<IEnumerable<Board>> GetAllBoardsAsync()
        {
            return await _context.Boards.ToListAsync();
        }
    }

}
