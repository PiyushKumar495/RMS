using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RailwayManagement.Models
{
    public partial class RailwayDBContext : DbContext
    {
        public RailwayDBContext()
        {
        }

        public RailwayDBContext(DbContextOptions<RailwayDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookings> Bookings { get; set; }
        public virtual DbSet<Passengers> Passengers { get; set; }
        public virtual DbSet<Trains> Trains { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-COGPE35;Database=RailwayDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookings>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK__Bookings__73951AEDDC2799F4");

                entity.HasIndex(e => e.Pnrnumber)
                    .HasName("UQ__Bookings__A03A5D41045DFD99")
                    .IsUnique();

                entity.Property(e => e.BookingDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BookingStatus).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Train)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TrainId)
                    .HasConstraintName("FK_Booking_Train");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Booking_User");
            });

            modelBuilder.Entity<Passengers>(entity =>
            {
                entity.HasKey(e => e.PassengerId)
                    .HasName("PK__Passenge__88915FB088959D53");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_Passenger_Booking");
            });

            modelBuilder.Entity<Trains>(entity =>
            {
                entity.HasKey(e => e.TrainId)
                    .HasName("PK__Trains__8ED2723A4E9D5167");

                entity.Property(e => e.DestinationStation).IsUnicode(false);

                entity.Property(e => e.SourceStation).IsUnicode(false);

                entity.Property(e => e.TrainName).IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C6A739665");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__A9D105341A9F3D85")
                    .IsUnique();

                entity.Property(e => e.Role).HasDefaultValueSql("('User')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
