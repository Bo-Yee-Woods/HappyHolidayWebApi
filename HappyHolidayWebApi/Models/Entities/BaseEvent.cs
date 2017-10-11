using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Entities
{
    public class EventBase
    {
        public EventBase()
        {
            Enrollments = new HashSet<Enrollment>();
            Activities = new HashSet<Activity>();
            Status = EventStatus.Pending;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public int TargetGroupSize { get; set; }
        
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }


        public int OwnerUserId { get; set; }

        [Required]
        [ForeignKey("OwnerUserId")]
        public User Owner { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }

    public static class EventStatus
    {
        public const string Pending = "Pending";
        public const string Comfirmed = "Comfirmed";
        public const string Completed = "Completed";
    }

}
