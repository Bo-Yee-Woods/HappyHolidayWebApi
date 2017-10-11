using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Entities
{
    public class Activity
    {
        public Activity()
        {
            Votes = new HashSet<Vote>();
            StartDateTime = DateTime.MinValue;
            EndDateTime = DateTime.MaxValue;
        }

        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }


        public int BaseEventId { get; set; }

        public virtual EventBase BaseEvent { get; set; }

        public int ProposerId { get; set; }

        public virtual User Proposer { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
