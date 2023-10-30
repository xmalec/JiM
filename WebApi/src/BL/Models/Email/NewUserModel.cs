﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.Email
{
    public class NewUserModel : EmailBodyModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Link { get; set; }
        public string Password { get; set; }
    }
}
