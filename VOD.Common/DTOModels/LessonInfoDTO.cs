﻿namespace VOD.Common.DTOModels
{
    public class LessonInfoDTO
    {
        public int LessonNumber { get; set; }
        public int NumberOfLessons { get; set; }
        public int PreviousVideoId { get; set; }
        public int NextVideoId { get; set; }
        public string NextVideoTitle { get; set; }
        public string NextVideoThumbnail { get; set; }
    }
}
