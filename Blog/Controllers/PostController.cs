﻿using Blog.BusinessManager.interfaces;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostBusinessManager _postBusinessManager;

        public PostController(IPostBusinessManager postBusinessManager)
        {
            _postBusinessManager = postBusinessManager;
        }
        [Route("Post/{id}"),AllowAnonymous]
        public async Task<IActionResult> Index(int? id)
        {
            var actionResult = await _postBusinessManager.GetPostViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        public async Task<IActionResult> Edit (int? id)
        {
            var actionResult = await _postBusinessManager.GetEditViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createBlogViewModel)
        {
            await _postBusinessManager.CreatePost(createBlogViewModel, User);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var actionResult = await _postBusinessManager.UpdatePost(editViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.Post.Id });

            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Comment(PostViewModel postViewModel)
        {
            var actionResult = await _postBusinessManager.CreateComment(postViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Index", new { postViewModel.Post.Id });

            return actionResult.Result;
        }
    }
}
