using HappyHolidayWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Repositories
{
    public abstract class AbstractDbcontext: DbContext
    {
        public AbstractDbcontext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FriendShip> FriendShips { get; set; }
        public DbSet<EventBase> Events { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Vote> Votes { get; set; }

    }
}
