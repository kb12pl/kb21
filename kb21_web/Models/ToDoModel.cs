using Microsoft.EntityFrameworkCore;

namespace kb21_web.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        
    }

    public class ToDoContext:DbContext
    {
        DbSet<ToDo> ToDo { get; set; }
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) {}
    }
}
