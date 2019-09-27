﻿using System.ComponentModel.DataAnnotations;

namespace VOD.Common.Entities
{
    public class Download
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(80), Required]
        public string Title { get; set; }
        [MaxLength(1024)]
        public string Url { get; set; }

        // Side-step from 3rd normal form for easier
        // access to a video’s course and module
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public Module Module { get; set; }
        public Course Course { get; set; }
    }
}
