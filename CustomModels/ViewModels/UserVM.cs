using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace SharedModels.ViewModels
{
    public class UserVM
    {
        public UserVM() { }

        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [MinLength(1)]
        public string DisplayAs { get; set; }

        public string AvatarUrl { get; set; }
    }


}
