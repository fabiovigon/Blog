using Blog.BusinessManager.interfaces;
using Blog.Data.Models;
using Blog.Models.AdminViewModels;
using Blog.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.BusinessManager
{
    public class AdminBusinessManager : IAdminBusinessManager
    {
        private UserManager<ApplicationUser> _userManager;
        private IBlogService _blogService;

        public AdminBusinessManager(UserManager<ApplicationUser> userManager,
                                    IBlogService blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }
        public async Task<IndexViewModel> GetAdminDashBoard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Blogs = _blogService.GetBlogs(applicationUser)
            };
        }
    }
}
