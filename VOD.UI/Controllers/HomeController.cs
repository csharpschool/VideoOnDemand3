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

namespace VOD.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<VODUser> _signInManager;
        private readonly IDbReadService _db;

        public HomeController(ILogger<HomeController> logger, SignInManager<VODUser> signInMgr, IDbReadService db)
        {
            _logger = logger;
            _signInManager = signInMgr;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var result1 = await _db.GetAsync<Download>(); // Fetch all
            // Fetch all that matches the Lambda expression
            var result2 = await _db.GetAsync<Download>(d => d.ModuleId.Equals(1));

            var result3 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));

            var result4 = await _db.AnyAsync<Download>(d => d.ModuleId.Equals(1)); // True if a record is found

            var videos = new List<Video>();
            var convertedVideos = videos.ToSelectList<Video>("Id", "Title");

            _db.Include<Download>();
            var result5 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));

            _db.Include<Download>();
            _db.Include<Module, Course>();
            var result6 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));


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
