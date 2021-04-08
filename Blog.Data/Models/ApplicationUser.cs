using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FisrtName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
