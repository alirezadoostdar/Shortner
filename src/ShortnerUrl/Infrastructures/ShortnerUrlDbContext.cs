using Microsoft.EntityFrameworkCore;

namespace ShortnerUrl.Infrastructures
{
    public class ShortnerUrlDbContext:DbContext
    {
        public ShortnerUrlDbContext(DbContextOptions<ShortnerUrlDbContext> options) : base(options) 
        { 

        }
    }
}
