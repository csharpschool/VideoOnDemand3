using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.Entities;
using VOD.Database.Services;

namespace VOD.UI.Services
{
    public class UIMockService : IUIReadService
    {
        #region Properties
        private readonly MockData _db = new MockData();
        #endregion

        #region Constructor
        public UIMockService()
        {
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Course>> GetCoursesAsync(string userId)
        {
            var courses = await Task.Run(() =>
            {
                var courses = _db.UserCourses
                .Where(uc => uc.UserId.Equals(userId))
                .Join(_db.Courses, uc => uc.CourseId, c => c.Id, (uc, c) => c)
                .ToList();

                return courses;
            });

            return courses;
        }
        public async Task<Course> GetCourseAsync(string userId, int courseId)
        {
            var course = await Task.Run(() => { 
                var userCourse = _db.UserCourses.Single(uc =>
                    uc.UserId.Equals(userId) && uc.CourseId.Equals(courseId));
                if (userCourse == null) return default;

                var course = _db.Courses.Single(c => c.Id.Equals(courseId));

                course.Modules = _db.Modules.Where(c => c.CourseId.Equals(course.Id)).ToList();
                course.Instructor = _db.Instructors.Single(i => i.Id.Equals(course.InstructorId));

                foreach (var module in course.Modules) {
                    module.Videos = _db.Videos.Where(v => v.ModuleId.Equals(module.Id)).ToList();
                    module.Downloads = _db.Downloads.Where(d => d.ModuleId.Equals(module.Id)).ToList();
                }

                return course;
            });

            return course;
        }
        public async Task<IEnumerable<Video>> GetVideosAsync(string userId, int moduleId = 0)
        {
            var videos = await Task.Run(() => { 
                var module = _db.Modules.Single(m => m.Id.Equals(moduleId));
                if (module == null) return default(List<Video>);

                var userCourse = _db.UserCourses.Single(uc =>
                    uc.UserId.Equals(userId) && uc.CourseId.Equals(module.CourseId));
                if (userCourse == null) return default(List<Video>);

                var videos = _db.Videos.Where(v =>
                    v.CourseId.Equals(userCourse.CourseId) &&
                    v.ModuleId.Equals(moduleId)).ToList();

                var course = _db.Courses.Single(m => m.Id.Equals(userCourse.CourseId));

                foreach (var video in videos)
                {
                    video.Course = course;
                    video.Module = module;
                }

                return videos;
            });

            return videos;
        }
        public async Task<Video> GetVideoAsync(string userId, int videoId)
        {
            var video = await Task.Run(() => {
                var video = _db.Videos.Single(v => v.Id.Equals(videoId));
                if (video == null) return default;

                var userCourse =  _db.UserCourses.Single(uc =>
                    uc.UserId.Equals(userId) && uc.CourseId.Equals(video.CourseId));
                if (userCourse == null) return default;

                var course = _db.Courses.Single(c => c.Id.Equals(userCourse.CourseId));
                course.Instructor = _db.Instructors.Single(i => i.Id.Equals(course.InstructorId));

                var module = _db.Modules.Single(m => m.Id.Equals(video.ModuleId));
                module.Videos = _db.Videos.Where(v => v.ModuleId.Equals(module.Id)).ToList();
                module.Downloads = _db.Downloads.Where(d => d.ModuleId.Equals(module.Id)).ToList();

                video.Course = course;
                video.Module = module;

                return video;
            });

            return video;
        }
        public async Task<Comment> GetCommentAsync(int commentId)
        {
            var comment = await Task.Run(() => {
                var comment = _db.Comments.Single(v => v.Id.Equals(commentId));
                if (comment == null) return default;

                return comment;
            });

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(int courseId)
        {
            var comments = await Task.Run(() => {
                var comments = _db.Comments.Where(c => c.CourseId.Equals(courseId));
                if (comments == null) return default;

                return comments;
            });

            return comments;
        }
        #endregion

    }
}
