using Blog.BusinessManager.interfaces;
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
        public HomeController(IPostBusinessManager postBusinessManager)
        {
            _postBusinessManager = postBusinessManager;
        }

        public IActionResult Index(string searchString, int? page)
        {
            return View(_postBusinessManager.GetIndexViewModel(searchString, page));
        }
    }
}
