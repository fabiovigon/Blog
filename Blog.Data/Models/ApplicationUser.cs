using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FisrtName { get; set; }
        public string LastName { get; set; }
    }
}
