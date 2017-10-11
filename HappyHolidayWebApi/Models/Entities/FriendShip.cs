using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Entities
{
    public class FriendShip
    {
        public FriendShip()
        {
            StartAt = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime StartAt { get; set; }
        
        public int SelfId { get; set; }

        public User Self { get; set; }
        
        public int FriendId { get; set; }
        
        public User Friend { get; set; }
    }    
}
