﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Identity
{
    public class AppUser:IdentityUser
    {
        public string Name {  get; set; }=string.Empty;
    }
}
