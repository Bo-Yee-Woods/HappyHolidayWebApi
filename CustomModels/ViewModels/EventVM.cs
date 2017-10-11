using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModels.ViewModels
{
    public class EventVM
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(63)]
        public string Name { get; set; }
                
        public int? TargetGroupSize { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        [Required]
        public int OwnerUserId { get; set; }
    }
}
