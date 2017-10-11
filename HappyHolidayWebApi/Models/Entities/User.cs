using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Entities
{
    public class User
    {
        public User()
        {
            EventsOwning = new HashSet<EventBase>();
            ProposeActivities = new HashSet<Activity>(); 
            Enrollments = new HashSet<Enrollment>();
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public string DisplayAs { get; set; }        

        public string AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<EventBase> EventsOwning { get; set; }

        public virtual ICollection<Activity> ProposeActivities { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public override string ToString()
        {
            return DisplayAs ?? Email;
        }
    }

}
