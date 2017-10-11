﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Models.Entities
{
    public class UserClaim
    {
        public int Id { get; set; }
       
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }

    }
}
