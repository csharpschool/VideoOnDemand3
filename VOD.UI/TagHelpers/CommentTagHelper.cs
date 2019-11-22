using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using VOD.Common.DTOModels;
using VOD.Common.Extensions;

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

        // Needed for rendering a partial view
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        #endregion

        // Needed for rendering a partial view
        public CommentTagHelper(IHtmlHelper htmlHelper)
        {
            html = htmlHelper;
        }

        private async Task<string> RecursiveComments(IEnumerable<CommentDTO> comments)
        {
            foreach (var parent in comments)
            {
                var parentMediaHtml = await html.PartialAsync("_MediaPartial", parent);
                result.Append($"<li>{parentMediaHtml.ToHtml()}");
                if (parent.ChildComments.Count > 0) result.Append("<ul>");

                foreach (var child in parent.ChildComments)
                {
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
}
