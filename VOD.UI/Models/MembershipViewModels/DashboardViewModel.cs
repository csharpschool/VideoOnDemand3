﻿using System.Collections.Generic;
using VOD.Common.DTOModels;

namespace VOD.UI.Models.MembershipViewModels
{
    public class DashboardViewModel
    {
        public List<List<CourseDTO>> Courses { get; set; }
    }
}
