﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.GoogleAuth.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }    
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
