using Blog.BusinessManager.interfaces;
using Blog.Data.Models;
using Blog.Models.HomeViewModels;
using Blog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.BusinessManager
{
        public class HomeBusinessManager : IHomeBusinessManager
        {
            private readonly IPostService _postService;
            private readonly IUserService _userService;

            public HomeBusinessManager(
                IPostService postService,
                IUserService userService)
            {
                _postService = postService;
                _userService = userService;
            }

            public ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page)
            {
                if (authorId is null)
                    return new BadRequestResult();

                var applicationUser = _userService.Get(authorId);

                if (applicationUser is null)
                    return new NotFoundResult();

                int pageSize = 20;
                int pageNumber = page ?? 1;

                var posts = _postService.GetPosts(searchString ?? string.Empty)
                    .Where(post => post.Published && post.Creator == applicationUser);

                return new AuthorViewModel
                {
                    Author = applicationUser,
                    Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),
                    SearchString = searchString,
                    PageNumber = pageNumber
                };
            }
        }
    }
