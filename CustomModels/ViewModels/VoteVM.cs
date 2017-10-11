using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedModels.ViewModels
{
    public class VoteVM
    {
        public int Id { get; set; }

        [Required]
        public bool IsNetural { get; set; }

        [Required]
        public bool IsUpVote { get; set; }                

        [Required]
        public int UserId { get; set; }

        public string UserDisplayAs { get; set; }
    }
}
