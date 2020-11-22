using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Data;
using todo.Model;

namespace todo.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext context;
        public TodoController(TodoContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public ActionResult<List<TodoModel>> GetAll() => context.Todo.ToList();
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> GetById(long id)
        {
            var todo = await context.Todo.FindAsync(id);
            if (todo is null) return BadRequest();
            return todo;
        }
        [HttpPost]
        public async Task<IActionResult> Create(TodoModel todo)
        {
            context.Todo.Add(todo);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TodoModel todo)
        {
            if(id != todo.Id) return BadRequest();
            context.Entry(todo).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete ("{id}")]
        public async Task<ActionResult<TodoModel>> Delete(long id)
        {
            var todo = await context.Todo.FindAsync(id);
            if(todo is null) return BadRequest();
            context.Todo.Remove(todo);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}