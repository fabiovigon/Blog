using Blog.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.BusinessManager.interfaces
{
    public interface IAdminBusinessManager
    {
        Task<IndexViewModel> GetAdminDashBoard(ClaimsPrincipal claimsPrincipal);
    }
}
