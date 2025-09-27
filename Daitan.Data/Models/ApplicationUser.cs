﻿

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? CompanyName { get; set; } 
        public double? EmissionFactor { get; set; }
    }
}
