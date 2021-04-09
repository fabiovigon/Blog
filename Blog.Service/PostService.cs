using Blog.Data;
using Blog.Data.Models;
using Blog.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace post.Service
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PostService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
       
        public Post GetPost(int postId)
        {
            return _applicationDbContext.Posts.FirstOrDefault(post => post.Id == postId);
        }

        public IEnumerable<Post> GetPosts(string searchString)
        {
            return _applicationDbContext.Posts
                .OrderByDescending(post => post.UpdatedOn)
                .Include(post => post.Creator)
                .Include(post => post.Comments)
                .Where(post => post.Title.Contains(searchString) || post.Content.Contains(searchString));
        }

        public IEnumerable<Post> GetPosts(ApplicationUser applicationUser)
        {
            return _applicationDbContext.Posts
                .Include(post => post.Creator)
                .Include(post => post.Approver)
                .Include(post => post.Comments)
                .Where(post => post.Creator == applicationUser);
        }

        public async Task<Post> Add(Post post)
        {
            _applicationDbContext.Add(post);
            await _applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post> Update(Post post)
        {
            _applicationDbContext.Update(post);
            await _applicationDbContext.SaveChangesAsync();
            return post;
        }
    }
}
