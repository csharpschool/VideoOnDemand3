using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using VOD.Common.DTOModels;
using VOD.Common.Entities;

namespace VOD.UI.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("comments")]
    public class CommentTagHelper : TagHelper
    {
        #region Properties
        public IEnumerable<CommentDTO> Data { get; set; } = new List<CommentDTO>();
        StringBuilder result = new StringBuilder();
        #endregion

        private string MediaTag(int id, string title, string body, string avatarUrl, int childCount, bool isChild = false)
        {
            string childClass = isChild ? "" : "mt-3";
            string hasChildren = childCount == 0 ? "" : 
                $"<span class='small font-italic small-left-margin'>({childCount} comments)</span>";
            
            return 
                $"<div class='media {childClass}' id='{id}'>" +
                    $"<img src='{avatarUrl}' class='mr-3' alt='Avatar'>" +
                    $"<div class='media-body'>" +
                    $"<h5 class='mt-0'>{title}" +
                    $"<span><button class='btn btn-link'>Reply</button>{hasChildren}</h5></span>" +
                    $"<div>{body}</div><div id='{id}' class='hide'><input style='width:93%;'/><button class='btn btn-success btn-sm'>Save</button></div></div></div>";
        }

        private string RecursiveComments(IEnumerable<CommentDTO> comments)
        {

            foreach (var parent in comments)
            {
                result.Append($"<li>{MediaTag(parent.Id, parent.Title, parent.Body, parent.AvatarUrl, parent.ChildComments.Count)}");
                if (parent.ChildComments.Count > 0) result.Append("<ul>");

                foreach (var child in parent.ChildComments)
                {
                    result.Append($"<li>{MediaTag(child.Id, child.Title, child.Body, child.AvatarUrl, child.ChildComments.Count)}");
                    if (child.ChildComments.Count > 0)
                    {
                        result.Append("<ul>");
                        RecursiveComments(child.ChildComments);
                        result.Append("</ul>");
                    }
                    result.Append("</li>");
                }

                if (parent.ChildComments.Count > 0) result.Append("</ul>");
                result.Append("</li>");
            }

            return result.ToString();
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Changes <btn> tag to <a> tag when rendered
            var html = RecursiveComments(Data);
            output.TagName = "ul";
            //var html = RecursiveMediaComments(Data);
            //output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(html);

            base.Process(context, output);

        }
    }
}
