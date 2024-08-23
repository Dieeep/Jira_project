using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;
using ToDoApp.Data.Context;

namespace ToDoApp.Api.Controllers
{
    [Route("api/task-item")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ToDoContext _context;
        public ValuesController(ToDoContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<TaskItem>> GetAsynk()
        {
            var items = await _context.Tasks.ToListAsync();
            return Ok(items);
        }
    }
}
