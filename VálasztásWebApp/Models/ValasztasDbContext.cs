using Microsoft.EntityFrameworkCore;

namespace VálasztásWebApp.Models
{
    public class ValasztasDbContext:DbContext
    {
        public ValasztasDbContext(DbContextOptions<ValasztasDbContext> option) : base(option) 
        { 
        
        
        }

        public DbSet<Jelolt> JeloltekListaja { get; set; }
        public DbSet<Part> Partok {  get; set; }
    }
}
