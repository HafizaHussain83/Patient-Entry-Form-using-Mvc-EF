using Microsoft.EntityFrameworkCore;
using MVCStore.Models;

namespace MVCStore.Services
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions options): base(options)
        { 


        }
        public DbSet<Product> products { get; set; }
    }
    
    
}
