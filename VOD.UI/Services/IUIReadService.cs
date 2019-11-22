using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.DTOModels;
using VOD.Common.Entities;

namespace VOD.UI.Services
{
    public interface IUIReadService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(string userId);
        Task<Course> GetCourseAsync(string userId, int courseId);
        Task<IEnumerable<Video>> GetVideosAsync(string userId, int moduleId = default(int));
        Task<Video> GetVideoAsync(string userId, int videoId);
        Task<Comment> GetCommentAsync(int commentId);
        Task<IEnumerable<Comment>> GetCommentsAsync(int courseId = 0);
        Task AddCommentAsync(Comment comment);
    }
}
