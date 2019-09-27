using System;
using System.Collections.Generic;
using System.Text;

namespace VOD.Common.DTOModels
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string MarqueeImageUrl { get; set; }
        public string CourseImageUrl { get; set; }
    }
}
