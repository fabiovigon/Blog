﻿using Blog.BusinessManager.interfaces;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostBusinessManager _postBusinessManager;
        private readonly IHomeBusinessManager _homeBusinessManager;
        public HomeController(IPostBusinessManager postBusinessManager, 
                              IHomeBusinessManager homeBusinessManager)
        {
            _postBusinessManager = postBusinessManager;
            _homeBusinessManager = homeBusinessManager;
        }

        public IActionResult Index(string searchString, int? page)
        {
            return View(_postBusinessManager.GetIndexViewModel(searchString, page));
        }

        public IActionResult Author(string authorId, string searchString, int? page)
        {
            var actionResult = _homeBusinessManager.GetAuthorViewModel(authorId, searchString, page);
            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }
    }
}
