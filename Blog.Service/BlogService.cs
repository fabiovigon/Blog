using Blog.Data;
using Blog.Data.Models;
using Blog.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BlogService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
       
        public Blogger GetBlog(int blogId)
        {
            return _applicationDbContext.Blogs.FirstOrDefault(blog => blog.Id == blogId);
        }

        public IEnumerable<Blogger> GetBlogs(ApplicationUser applicationUser)
        {
            return _applicationDbContext.Blogs
                .Include(blog => blog.Creator)
                .Include(blog => blog.Approver)
                .Include(blog => blog.Posts)
                .Where(Blog => Blog.Creator == applicationUser);
        }

        public async Task<Blogger> Add(Blogger blog)
        {
            _applicationDbContext.Add(blog);
            await _applicationDbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<Blogger> Update(Blogger blog)
        {
            _applicationDbContext.Update(blog);
            await _applicationDbContext.SaveChangesAsync();
            return blog;
        }
    }
}
