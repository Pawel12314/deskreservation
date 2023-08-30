using DeskAspMvc.Models;
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
                        entity.HasKey(x => x.id);
                        // entity.HasMany(x => x.desks).WithOne(x => x.location).HasForeignKey(x=>x.locationKey);
                    }
                );

            modelBuilder.Entity<Desk>(
                entity =>
                {

                    entity.HasKey(x => x.id);
                    entity.ToTable("Desk");
                    entity
                    .HasOne(x => x.location)
                    .WithMany(x => x.desks)
                    .HasForeignKey(x => x.locationKey);
                    /*entity
                    .HasOne(x => x.reservation)
                    .WithOne(x => x.desk)
                    .HasForeignKey<Desk>(x => x.reservationId);
                    *///.HasForeignKey<Reservation>(x => x.deskId);
                    //.IsRequired(false);
                }
            );

            modelBuilder.Entity<Reservation>(
                entity =>
                {
                    entity.ToTable("Reservation");
                    entity.HasKey(x => x.id);
                    entity.HasOne(x => x.desk)
                    .WithOne(x => x.reservation)
                    .HasForeignKey<Reservation>(x => x.deskId);
                    //entity.HasOne(x => x.owner).WithMany(x => x.reservations).HasForeignKey(x => x.ownerId);
                    //entity.HasOne(x => x.location).WithMany(x => x.reservations).HasForeignKey(x => x.locationId);
                }
            );
        }
        //public DbSet<TestItem> TestITem { get; set; }
        public DbSet<Desk> desks { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        //public DbSet<ReservationItem> reservationItems { get; set; }

    }
}