using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.UI.Services
{
    public interface IUIReadService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(string userId);
        Task<Course> GetCourseAsync(string userId, int courseId);
    }
}
