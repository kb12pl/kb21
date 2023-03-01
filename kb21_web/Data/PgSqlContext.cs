using kb21_web.Models;
using Microsoft.EntityFrameworkCore;

namespace kb21_web.Data
{
public class PgSqlContext : DbContext
    {
        DbSet<ToDo> ToDo { get; set; }
        public PgSqlContext(DbContextOptions<PgSqlContext> options) 
            : base(options) 
        { 

        }
    }
}
