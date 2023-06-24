using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Transflo.Assessment.Core.Domain;

namespace Transflo.Assessment.Infrastructure.Persistence
{
    internal class TransfloDBContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public TransfloDBContext(DbContextOptions<TransfloDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
