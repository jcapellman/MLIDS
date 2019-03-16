using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using jcIDS.web.DAL.Tables;
using jcIDS.web.DAL.Tables.Base;

using Microsoft.EntityFrameworkCore;

namespace jcIDS.web.DAL
{
    public class IDSContext : DbContext
    {
        public DbSet<Devices> Devices { get; set; }

        public DbSet<Packets> Packets { get; set; }

        public IDSContext(DbContextOptions options) : base(options) { }

        private bool BaseModifiyLogic()
        {
            var changeSet = ChangeTracker.Entries<BaseTable>();

            if (changeSet == null)
            {
                return false;
            }

            foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
            {
                entry.Entity.Modified = DateTimeOffset.Now;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTimeOffset.Now;
                        entry.Entity.Active = true;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.Active = false;
                        break;
                }
            }

            return true;
        }

        public override int SaveChanges() => !BaseModifiyLogic() ? 0 : base.SaveChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (!BaseModifiyLogic())
            {
                return 0;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=jcIDS;Trusted_Connection=True;");
        }
    }
}