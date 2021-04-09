using Blog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Service.Interfaces
{
    public interface IPostService
    {
        Post GetPost(int post);
        Task<Post> Add(Post post);
        Task<Post> Update(Post post);
        IEnumerable<Post> GetPosts(ApplicationUser applicationUser);
        IEnumerable<Post> GetPosts(string searchString);
    }
}
