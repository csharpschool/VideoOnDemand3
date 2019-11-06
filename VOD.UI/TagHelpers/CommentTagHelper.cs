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
                $"<span class='small font-italic' style='position:absolute; right:25px;'>({childCount} comments)</span>";
            
            return 
                $"<div class='media {childClass}' style='margin-top:5px !important;' id='{id}'>" +
                    $"<img src='{avatarUrl}' class='mr-3' alt='Avatar'>" +
                    $"<div class='media-body'>" +
                    $"<h5 class='mt-0'>{title}{hasChildren}</h5>" +
                    $"{body}";
        }

        private string RecursiveMediaComments(IEnumerable<CommentDTO> comments)
        {
            foreach (var parent in comments)
            {
                result.Append(MediaTag(parent.Id, parent.Title, parent.Body, parent.AvatarUrl, parent.ChildComments.Count));

                foreach (var child in parent.ChildComments)
                {
                    result.Append(MediaTag(child.Id, child.Title, child.Body, child.AvatarUrl, child.ChildComments.Count, true));

                    if (child.ChildComments.Count > 0)
                        RecursiveMediaComments(child.ChildComments);

                    result.Append("</div></div>");
                }

                result.Append("</div></div>");
            }

            return result.ToString();
        }
        private string RecursiveComments(IEnumerable<CommentDTO> comments)
        {

            foreach (var parent in comments)
            {
                result.Append($"<li style='list-style-type: none;'>{MediaTag(parent.Id, parent.Title, parent.Body, parent.AvatarUrl, parent.ChildComments.Count)}</li>");
                if (parent.ChildComments.Count > 0) result.Append("<ul>");

                foreach (var child in parent.ChildComments)
                {
                    result.Append($"<li style='list-style-type: none;'>{MediaTag(child.Id, child.Title, child.Body, child.AvatarUrl, child.ChildComments.Count)}</li>");
                    if (child.ChildComments.Count > 0)
                    {
                        result.Append("<ul>");
                        RecursiveComments(child.ChildComments);
                        result.Append("</ul>");
                    }
                }

                if (parent.ChildComments.Count > 0) result.Append("</ul>");
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
