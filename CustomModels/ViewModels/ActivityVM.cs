using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModels.ViewModels
{
    public class ActivityVM
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int ProposerId { get; set; }

        [Required]
        [MaxLength(31)]
        public string Title { get; set; }                

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        
    }
}
