using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        private readonly IHtmlHelper html;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        #endregion

        public CommentTagHelper(IHtmlHelper htmlHelper)
        {
            html = htmlHelper;
        }

        private string MediaTag(int id, int courseId, string title, string body, string avatarUrl, int childCount, bool isChild = false)
        {
            string childClass = isChild ? "" : "mt-3";
            string hasChildren = childCount == 0 ? "" : 
                $"<button class='btn btn-link media-replies font-italic small-left-margin'>({childCount} comments)</button>";
            
            return 
                $"<div class='media {childClass}' id='{id}'>" +
                    $"<img src='{avatarUrl}' class='mr-3' alt='Avatar'>" +
                    $"<div class='media-body'>" +
                    $"<h5 class='mt-0'>{title}" +
                    $"<span><button class='btn btn-link media-reply'>Reply</button>{hasChildren}</span></h5>" +
                    $"<div>{body}</div>" +
                    $"<div class='hide media-input'>" +
                    $"<form action='/Membership/CreateComment' data-ajax='true' data-ajax-update='#comments'>" +
                        $"<input type='hidden' id='parentId' value='{id}'/>" +
                        $"<input type='hidden' id='courseId' value='{courseId}'/>" +
                        $"<ul>" +
                            $"<li><span>Title:</span><input class='media-comment-input-title'/></li>" +
                            $"<li><span>Body:</span><input class='media-comment-input-body'/></li>" +
                            $"<li><button type='submit' class='btn btn-success btn-sm media-save'>Save</button></li>" +
                        $"</ul>" +
                    $"</form>" +
                    $"</div></div></div>";
        }

        private async Task<string> RecursiveComments(IEnumerable<CommentDTO> comments)
        {
            foreach (var parent in comments)
            {
                //await html.PartialAsync("", parent);
                //result.Append($"<li>{MediaTag(parent.Id, parent.CourseId, parent.Title, parent.Body, parent.AvatarUrl, parent.ChildComments.Count)}");
                var parentMediaHtml = await html.PartialAsync("_MediaPartial", parent);
                result.Append($"<li>{parentMediaHtml.ToHtml()}");
                if (parent.ChildComments.Count > 0) result.Append("<ul>");

                foreach (var child in parent.ChildComments)
                {
                    //result.Append($"<li>{MediaTag(child.Id, child.CourseId, child.Title, child.Body, child.AvatarUrl, child.ChildComments.Count)}");
                    var childMediaHtml = await html.PartialAsync("_MediaPartial", child);
                    result.Append($"<li>{childMediaHtml.ToHtml()}");
                    if (child.ChildComments.Count > 0)
                    {
                        result.Append("<ul>");
                        await RecursiveComments(child.ChildComments);
                        result.Append("</ul>");
                    }
                    result.Append("</li>");
                }

                if (parent.ChildComments.Count > 0) result.Append("</ul>");
                result.Append("</li>");
            }

            return result.ToString();
        }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Needed for rendering a partial view
            (html as IViewContextAware).Contextualize(ViewContext);

            // Creates comments recursively as parent/child
            var htmlOutput = await RecursiveComments(Data);

            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(htmlOutput);

            base.Process(context, output);

        }
    }

    public static class HtmlExtensions 
    {
        public static string ToHtml(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
