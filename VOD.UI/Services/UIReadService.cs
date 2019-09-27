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
        #endregion

    }
}
