using DeskAspMvc.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
            modelBuilder.Entity<Location>
                (
                    entity =>
                    {
                        entity.ToTable("Location");
                        entity.HasKey(x => x.Id);
                    }
                );

            modelBuilder.Entity<Desk>(
                entity =>
                {

                    entity.HasKey(x => x.Id);
                    entity.ToTable("Desk");
                    entity
                    .HasOne(x => x.Location)
                    .WithMany(x => x.Desks)
                    .HasForeignKey(x => x.LocationKey);
                }
            );

            modelBuilder.Entity<Reservation>(
                entity =>
                {
                    entity.ToTable("Reservation");
                    entity.HasKey(x => x.Id);
                    entity.HasOne(x => x.Desk)
                    .WithMany(x => x.Reservations)
                    .HasForeignKey(x => x.DeskId);
                    entity.HasMany(x => x.Dates)
                    .WithMany(x => x.Reservations);
                }
            );
            modelBuilder.Entity<MyDate>(
                entity =>
                {
                    entity.ToTable("MyDate");
                    entity.HasKey(x => x.Id);
                }
                );
        }
        public DbSet<Desk> desks { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<MyDate> mydates { get; set; }
    }
}