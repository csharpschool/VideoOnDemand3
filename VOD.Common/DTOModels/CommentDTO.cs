using System;
using System.Collections.Generic;
using System.Text;

namespace VOD.Common.DTOModels
{
    public class CommentDTO
    {
        public int? ParentId { get; set; } = null;
        public string Title { get; set; }
        public string Text { get; set; }
        public string AvatarUrl { get; set; }

        public List<CommentDTO> ChildComments { get; set; } = new List<CommentDTO>();

    }
}
