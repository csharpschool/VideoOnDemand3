using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.Entities;
using VOD.Database.Services;

namespace VOD.UI.Services
{
    public class UIReadService : IUIReadService
    {
        #region Properties
        private readonly IDbReadService _db;
        #endregion

        #region Constructor
        public UIReadService(IDbReadService db)
        {
            _db = db;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Course>> GetCoursesAsync(string userId)
        {
            _db.Include<UserCourse>();
            var userCourses = await _db.GetAsync<UserCourse>(uc => uc.UserId.Equals(userId));

            return userCourses.Select(c => c.Course);
        }
        public async Task<Course> GetCourseAsync(string userId, int courseId)
        {
            _db.Include<Course, Module>();
            var userCourse = await _db.SingleAsync<UserCourse>(c =>
                c.UserId.Equals(userId) && c.CourseId.Equals(courseId));

            if (userCourse == null) return default;
            
            return userCourse.Course;
        }

        public async Task<IEnumerable<Video>> GetVideosAsync(string userId, int moduleId = 0)
        {
            _db.Include<Video>();

            var module = await _db.SingleAsync<Module>(m =>
                m.Id.Equals(moduleId));
            if (module == null) return default(List<Video>);

            var userCourse = await _db.SingleAsync<UserCourse>(uc =>
                uc.UserId.Equals(userId) &&
                uc.CourseId.Equals(module.CourseId));
            if (userCourse == null) return default(List<Video>);

            return module.Videos;
        }
        #endregion

    }
}
