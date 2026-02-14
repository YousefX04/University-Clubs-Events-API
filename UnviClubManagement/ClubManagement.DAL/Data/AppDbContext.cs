using ClubManagement.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClubManagement.DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ClubMember> ClubMembers { get; set; }
        public DbSet<EventMember> EventMembers { get; set; }
        public DbSet<ClubUpdate> ClubUpdates { get; set; }
        public DbSet<EventUpdate> EventUpdates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClubMember>().HasKey(c => new { c.UserID, c.ClubID });
            modelBuilder.Entity<EventMember>().HasKey(e => new { e.UserID, e.EventID });
            modelBuilder.Entity<Club>().Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Event>().Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ClubMember>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.JoinedClubs) // or WithMany(u => u.ClubMembers)
                .HasForeignKey(cm => cm.UserID)
                .OnDelete(DeleteBehavior.Cascade); // No cascade

            modelBuilder.Entity<ClubMember>()
                .HasOne(cm => cm.Club)
                .WithMany(c => c.Members) // or WithMany(c => c.Members)
                .HasForeignKey(cm => cm.ClubID)
                .OnDelete(DeleteBehavior.Restrict); // No cascade

            modelBuilder.Entity<EventMember>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.JoinedEvents) // or WithMany(u => u.ClubMembers)
                .HasForeignKey(cm => cm.UserID)
                .OnDelete(DeleteBehavior.Cascade); // No cascade

            modelBuilder.Entity<EventMember>()
                .HasOne(cm => cm.Event)
                .WithMany(e => e.Members) // or WithMany(c => c.Members)
                .HasForeignKey(cm => cm.EventID)
                .OnDelete(DeleteBehavior.Restrict); // No cascade


            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "c9c7b6d1-4f57-4c6c-9b1c-2b8c5a7a6e3f",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "c9c7b6d1-4f57-4c6c-9b1c-2b8c5a7a6e3f"
                },
                new IdentityRole
                {
                    Id = "8e2d4a91-3b7e-4d9f-a6f2-5c0b9f3e2d44",
                    Name = "ClubLeader",
                    NormalizedName = "CLUBLEADER",
                    ConcurrencyStamp = "8e2d4a91-3b7e-4d9f-a6f2-5c0b9f3e2d44"
                },
                new IdentityRole
                {
                    Id = "1f6a2c8e-9d5b-47a3-b3d1-0b7c6e4f8a12",
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = "1f6a2c8e-9d5b-47a3-b3d1-0b7c6e4f8a12"
                }
            );


            modelBuilder.Entity<AppUser>().HasData(
                 new AppUser
                 {
                     Id = "11111111-1111-1111-1111-111111111111",
                     UserName = "admin",
                     NormalizedUserName = "ADMIN",
                     Email = "admin@gmail.com",
                     NormalizedEmail = "ADMIN@GMAIL.COM",
                     EmailConfirmed = true,
                     PasswordHash = "AQAAAAIAAYagAAAAEF1P9msprFt8rq8JmHf0YuwxJQywTlpRnnLxK43Vx977UuiosltxCAfgW/bvmCyiFg==", // OOoo11## Password
                     SecurityStamp = "seed-admin-security-stamp",
                     ConcurrencyStamp = "seed-admin-concurrency"
                 }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "11111111-1111-1111-1111-111111111111",
                    RoleId = "c9c7b6d1-4f57-4c6c-9b1c-2b8c5a7a6e3f"
                }
            );

            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     Id = 1,
                     AppUserId = "11111111-1111-1111-1111-111111111111"
                 }
            );
        }
    }
}
