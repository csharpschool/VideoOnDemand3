using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using VOD.Common.Entities;
using VOD.UI.Models;
using VOD.Database.Services;
using VOD.Common.Extensions;
using VOD.UI.Services;

namespace VOD.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<VODUser> _signInManager;
        private readonly IUIReadService _db;

        public HomeController(ILogger<HomeController> logger, SignInManager<VODUser> signInMgr, IUIReadService db)
        {
            _logger = logger;
            _signInManager = signInMgr;
            _db = db;
        }

        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToPage("/Account/Login",
                    new { Area = "Identity" });

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
