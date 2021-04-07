using Blog.BusinessManager.interfaces;
using Blog.Data.Models;
using Blog.Models.BlogViewModels;
using Blog.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.BusinessManager
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;

        public BlogBusinessManager(UserManager<ApplicationUser> userManager,
                                    IBlogService blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }

        public async Task<Blogger> CreateBlog(CreateBlogViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blogger blog = createBlogViewModel.Blog;
            blog.Creator = await _userManager.GetUserAsync(claimsPrincipal);
            blog.CreatedOn = DateTime.Now;

            return await _blogService.Add(blog);

        }
    }
}
