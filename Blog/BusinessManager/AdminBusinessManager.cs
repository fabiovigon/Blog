using Blog.BusinessManager.interfaces;
using Blog.Data.Models;
using Blog.Models.AdminViewModels;
using Blog.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.BusinessManager
{
    public class AdminBusinessManager : IAdminBusinessManager
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AdminBusinessManager(UserManager<ApplicationUser> userManager,
                                    IPostService postService,
                                    IUserService userService,
                                    IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _postService = postService;
            _userService = userService;
            _webHostEnviroment = webHostEnvironment;
        }
        public async Task<IndexViewModel> GetAdminDashBoard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Posts = _postService.GetPosts(applicationUser)
            };
        }

        public async Task<AboutViewModel> GetAboutViewModel(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new AboutViewModel
            {
                SubHeader = applicationUser.SubHeader,
                Content = applicationUser.AboutContent
            };
        }

        public async Task UpdateAbout(AboutViewModel aboutViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            applicationUser.SubHeader = aboutViewModel.SubHeader;
            applicationUser.SubHeader = aboutViewModel.Content;

            if (aboutViewModel.HeaderImage != null)
            {
                string webRootPath = _webHostEnviroment.WebRootPath;
                string pathToImage = $@"{webRootPath}\UserFiles\Users\{applicationUser.Id}\HeaderImage.jpg";

                EnsureFolder(pathToImage);

                using (var fileStream = new FileStream(pathToImage, FileMode.Create))
                {
                    await aboutViewModel.HeaderImage.CopyToAsync(fileStream);
                }

            }

            await _userService.Update(applicationUser);
        }
        private void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }
    }
}
