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
        #endregion

        /*
        <div class="media">
            <img src="..." class="mr-3" alt="...">
            <div class="media-body">
                <h5 class="mt-0">Media heading</h5>
                Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.

                <div class="media mt-3">
                    <a class="mr-3" href="#">
                        <img src="..." class="mr-3" alt="...">
                    </a>
                    <div class="media-body">
                        <h5 class="mt-0">Media heading</h5>
                        Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.

                        <div class="media mt-3">
                            <a class="mr-3" href="#">
                                <img src="..." class="mr-3" alt="...">
                            </a>
                            <div class="media-body">
                                <h5 class="mt-0">Media heading</h5>
                                Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        */
        StringBuilder result = new StringBuilder();
        private string RecursiveComments(IEnumerable<CommentDTO> comments)
        {
            foreach (var parent in comments)
            {
                result.Append(
                    $"<div class='media'>" +
                    $"<img src='/images/avatar.png' class='mr-3' alt='Avatar'>" +
                    $"<div class='media-body'>" +
                    $"<h5 class='mt-0'>{parent.Title}</h5>" +
                    $"{parent.Text}");

                //if (parent.ChildComments.Count > 0) result.Append("<div>");

                foreach (var child in parent.ChildComments)
                {
                    result.Append(
                        $"<div class='media mt-3'>" +
                        $"<img src='/images/avatar.png' class='mr-3' alt='Avatar'>" +
                        $"<div class='media-body'>" +
                        $"<h5 class='mt-0'>{child.Title}</h5>" +
                        $"{child.Text}");
                    if (child.ChildComments.Count > 0)
                    {
                        //result.Append("<div>");
                        RecursiveComments(child.ChildComments);
                        //result.Append("</div>");
                    }
                    result.Append("</div></div>");
                }

                //if (parent.ChildComments.Count > 0) result.Append("</div>");
                result.Append("</div></div>");
            }

            return result.ToString();
        }
        //private string RecursiveComments(IEnumerable<CommentDTO> comments)
        //{

        //    foreach (var parent in comments)
        //    {
        //        result.Append($"<li>{parent.Title}");
        //        if (parent.ChildComments.Count > 0) result.Append("<ul>");

        //        foreach (var child in parent.ChildComments)
        //        {
        //            result.Append($"<li>{child.Title}");
        //            if (child.ChildComments.Count > 0)
        //            {
        //                result.Append("<ul>");
        //                RecursiveComments(child.ChildComments);
        //                result.Append("</ul>");
        //            }
        //            result.Append("</li>");
        //        }

        //        if (parent.ChildComments.Count > 0) result.Append("</ul>");
        //        result.Append("</li>");
        //    }

        //    return result.ToString();
        //}

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Changes <btn> tag to <a> tag when rendered
            var html = RecursiveComments(Data);
            //output.TagName = "ul";
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(html);

            base.Process(context, output);

        }
    }
}
