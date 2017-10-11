using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Entities
{
    public class Vote
    {
        public Vote()
        {
            IsNetural = false;
            IsUpVote = false;
        }

        public int Id { get; set; }

        public bool IsNetural { get; set; } 

        public bool IsUpVote { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
