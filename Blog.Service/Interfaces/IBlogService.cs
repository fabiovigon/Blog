using Blog.Data.Models;
using System.Threading.Tasks;

namespace Blog.Service.Interfaces
{
    public interface IBlogService
    {
        Task<Blogger> Add(Blogger blog);
    }
}
