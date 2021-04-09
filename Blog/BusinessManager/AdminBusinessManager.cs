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
        private IPostService _postService;

        public AdminBusinessManager(UserManager<ApplicationUser> userManager,
                                    IPostService postService)
        {
            _userManager = userManager;
            _postService = postService;
        }
        public async Task<IndexViewModel> GetAdminDashBoard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Posts = _postService.GetPosts(applicationUser)
            };
        }
    }
}
