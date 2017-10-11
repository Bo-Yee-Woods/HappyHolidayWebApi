using SharedModels.ViewModels;
using HappyHolidayWebApi.Models.Entities;
using HappyHolidayWebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Services
{
    public class UserManager
    {
        public readonly AbstractDbcontext db;

        public UserManager(AbstractDbcontext db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetUsersFriends(int userId)
        {
            return db.FriendShips.Where(f => f.SelfId == userId)
                                 .Select(f => f.Friend)
                                 .ToList();
        }

        public bool hasFriendshipBetween(int id, int friendId)
        {
            return db.FriendShips
                     .Where(f => (f.SelfId == id && f.FriendId == friendId) ||
                                 (f.SelfId == friendId && f.FriendId == f.SelfId))
                     .Count() > 0;
        }
    }
}
