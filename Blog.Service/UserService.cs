using Blog.Data;
using Blog.Data.Models;
using Blog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public ApplicationUser Get(string id)
        {
            return _applicationDbContext.Users
                .FirstOrDefault(user => user.Id == id);
        }

        public async Task<ApplicationUser> Update(ApplicationUser applicationUser)
        {
            _applicationDbContext.Update(applicationUser);
            await _applicationDbContext.SaveChangesAsync();
            return applicationUser;
        }
    }
}
