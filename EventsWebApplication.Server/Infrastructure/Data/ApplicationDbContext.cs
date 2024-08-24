using EventsWebApplication.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Server.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка автоинкремента для полей Id
            modelBuilder.Entity<Event>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            // автоинкремент не нужен в промежуточной таблице
            modelBuilder.Entity<EventParticipant>()
        .Property(ep => ep.UserId)
        .ValueGeneratedNever();

            modelBuilder.Entity<EventParticipant>()
                .Property(ep => ep.EventId)
                .ValueGeneratedNever();

            // Many-To-Many
            modelBuilder.Entity<EventParticipant>()
                .HasKey(uer => new { uer.UserId, uer.EventId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(uer => uer.User)
                .WithMany(u => u.Events)
                .HasForeignKey(uer => uer.UserId);

            modelBuilder.Entity<EventParticipant>()
                .HasOne(uer => uer.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(uer => uer.EventId);

        }

    }
}
