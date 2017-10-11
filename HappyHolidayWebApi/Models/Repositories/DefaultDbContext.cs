using HappyHolidayWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HappyHolidayWebApi.Models.Repositories
{
    public class DefaultDbContext : AbstractDbcontext
    {

        public DefaultDbContext(DbContextOptions options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string DefaultConnectionString = "Server=tcp:db-sever.database.windows.net,1433;Initial Catalog=Free32MB;Persist Security Info=False;User ID=baoyu;Password=boyeewoods@1993;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //    optionsBuilder.UseSqlServer(DefaultConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FriendShip>()
                .HasOne(f => f.Self)
                .WithMany()
                .HasForeignKey(f => f.SelfId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendShip>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.BaseEvent)
                .WithMany(e => e.Activities)
                .HasForeignKey(a => a.BaseEventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Activity)
                .WithMany(a => a.Votes)
                .HasForeignKey(v => v.ActivityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventBase>()
                .HasOne(e => e.Owner)
                .WithMany(u => u.EventsOwning)
                .HasForeignKey(e => e.OwnerUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.EventEnrolled)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(e => e.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.UserEnrolled)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Proposer)
                .WithMany(u => u.ProposeActivities)
                .HasForeignKey(e => e.ProposerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
