using kb21_web.Models;
using Microsoft.EntityFrameworkCore;

namespace kb21_web.Data
{
public class DataContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Todo> Todo { get; set; }

        public DbSet<Info> Info { get; set; }

        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) 
        { 

        }
    }
}
