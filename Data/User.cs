﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Data
{
    public class User : IdentityUser
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }

    }
}