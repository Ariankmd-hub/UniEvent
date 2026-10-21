using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniEvent.Domain.Entities;
// i changed the name space here . 
namespace UniEvent.Persistence

{
    public class UniEventDbContext : DbContext
    {
      
        public UniEventDbContext(DbContextOptions<UniEventDbContext> options) : base(options)
        { }
        public DbSet<Event> Events { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }
    }
}
