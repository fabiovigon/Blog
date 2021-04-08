using Blog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Service.Interfaces
{
    public interface IBlogService
    {
        Blogger GetBlog(int blogId);
        Task<Blogger> Add(Blogger blog);
        Task<Blogger> Update(Blogger blog);
        IEnumerable<Blogger> GetBlogs(ApplicationUser applicationUser);
    }
}
