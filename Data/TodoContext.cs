using Microsoft.EntityFrameworkCore;
using todo.Model;

namespace todo.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        public DbSet<TodoModel> Todo { get; set; }
    }
}