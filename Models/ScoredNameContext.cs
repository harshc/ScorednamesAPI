using Microsoft.EntityFrameworkCore;

namespace indexProcessorWeb.Models
{
    public class ScoredNameContext : DbContext
    {
        public ScoredNameContext(DbContextOptions<ScoredNameContext> options) : base(options)
        {

        }

        public DbSet<ScoredName> ScoredNames { get; set; }
    }
}
