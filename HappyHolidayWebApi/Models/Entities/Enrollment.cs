using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public EventBase EventEnrolled { get; set; }
        
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserEnrolled { get; set; }
    }
}
