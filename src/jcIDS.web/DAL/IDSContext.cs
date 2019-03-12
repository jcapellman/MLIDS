using jcIDS.web.DAL.Tables;

using Microsoft.EntityFrameworkCore;

namespace jcIDS.web.DAL
{
    public class IDSContext : DbContext
    {
        public DbSet<Devices> Devices { get; set; }

        public DbSet<Packets> Packets { get; set; }

        public IDSContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=jcIDS;Trusted_Connection=True;");
        }
    }
}