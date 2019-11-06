using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VOD.Common.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; } = null;
        public int CourseId { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Text { get; set; }

        [MaxLength(1024)]
        public string AvatarUrl { get; set; }

        public Comment ParentComment { get; set; } = default;
        public List<Comment> ChildComments { get; set; } = new List<Comment>();
    }
}
