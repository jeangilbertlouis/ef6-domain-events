using MediatR;
using Microsoft.EntityFrameworkCore;
using Webhost.Domain;

namespace Webhost.Db;

public class PersonContext : DbContext
{
    private readonly IMediator _mediator;
    
    public PersonContext(DbContextOptions<PersonContext> options, IMediator mediator):base(options)
    {
        _mediator = mediator;
    }
    
    protected PersonContext(DbContextOptions options, IMediator mediator):base(options)
    {
        _mediator = mediator;
    }
    
    public DbSet<Person> Persons { get; set; }
    public DbSet<ReadModelStore> ReadModelStores { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().Property(s=>s.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Person>().Ignore(s=>s.DomainEvents);
        
        modelBuilder.Entity<ReadModelStore>().HasKey(s=>s.Id);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var response = await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
        return response;
    }

    public override int SaveChanges()
    {
        var response = base.SaveChanges();
        _dispatchDomainEvents().GetAwaiter().GetResult();
        return response;
    }
    
    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<Person>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (var entityDomainEvent in events)
                await _mediator.Publish(entityDomainEvent);
        }
    }
}