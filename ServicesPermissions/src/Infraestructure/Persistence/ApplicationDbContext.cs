using Application.Data;
using Domain.Permissions;
using Domain.PermissionTypes;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        
        private readonly IPublisher publisher;
        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base (options) 
        {
            this.publisher = publisher ?? throw new ArgumentNullException(nameof(options));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<PermissionType>().HasData(
                new PermissionType {Id = 1, Description = "Read"},
                new PermissionType { Id = 2, Description = "Write"}
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            IEnumerable<DomainEvent> domainEvents = ChangeTracker.Entries<AggregateRoot>()
                                            .Select(e => e.Entity)
                                            .Where(e => e.GetDomainEvents().Any())
                                            .SelectMany(e => e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (DomainEvent domainEvent in domainEvents)
            {
                await publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }

    }
}
