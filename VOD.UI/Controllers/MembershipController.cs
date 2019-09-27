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
using VOD.Common.DTOModels;
using VOD.UI.Models.MembershipViewModels;

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
        public async Task<IActionResult> Dashboard()
        {
            var courseDtoObjects = _mapper.Map<List<CourseDTO>>(await _db.GetCoursesAsync(_userId));
            var dashboardModel = new DashboardViewModel();
            dashboardModel.Courses = new List<List<CourseDTO>>();
            var noOfRows = courseDtoObjects.Count <= 3 ? 1 : courseDtoObjects.Count / 3;
            for (var i = 0; i < noOfRows; i++)
            {
                dashboardModel.Courses.Add(courseDtoObjects.Skip(i * 3).Take(3).ToList());
            }

            return View(dashboardModel);
        }

        [HttpGet]
        public async Task<IActionResult> Course(int id)
        {
            var course = await _db.GetCourseAsync(_userId, id);

            var courseDTO = _mapper.Map<CourseDTO>(course);
            var instructorDTO = _mapper.Map<InstructorDTO>(
                course.Instructor);
            var moduleDTOs = _mapper.Map<List<ModuleDTO>>(course.Modules);

            var courseModel = new CourseViewModel
            {
                Course = courseDTO,
                Instructor = instructorDTO,
                Modules = moduleDTOs
            };

            return View(courseModel);
        }

        [HttpGet]
        public async Task<IActionResult> Video(int id)
        {
            var video = await _db.GetVideoAsync(_userId, id);
            var videoDTO = _mapper.Map<VideoDTO>(video);
            var courseDTO = _mapper.Map<CourseDTO>(video.Course);
            var instructorDTO = _mapper.Map<InstructorDTO>(video.Course.Instructor);

            var videos = video.Module.Videos;
            var count = videos.Count();
            var index = videos.FindIndex(v => v.Id.Equals(id));

            var previous = videos.ElementAtOrDefault(index - 1);
            var previousId = previous == null ? 0 : previous.Id;

            var next = videos.ElementAtOrDefault(index + 1);
            var nextId = next == null ? 0 : next.Id;
            var nextTitle = next == null ? string.Empty : next.Title;
            var nextThumb = next == null ? string.Empty : next.Thumbnail;


            return View();
        }

    }
}