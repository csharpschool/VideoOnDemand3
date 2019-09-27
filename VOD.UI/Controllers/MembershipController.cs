using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VOD.Common.Entities;
using VOD.UI.Services;

namespace VOD.UI.Controllers
{
    public class MembershipController : Controller
    {
        private readonly string _userId;
        private readonly IMapper _mapper;
        private readonly IUIReadService _db;

        public MembershipController(
        IHttpContextAccessor httpContextAccessor,
        UserManager<VODUser> userManager, IMapper mapper, IUIReadService db)
        {
            var user = httpContextAccessor.HttpContext.User;
            _userId = userManager.GetUserId(user);
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Course(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Video(int id)
        {
            return View();
        }

    }
}