using Microsoft.AspNetCore.Html;
using System.IO;
using System.Text.Encodings.Web;

namespace VOD.Common.Extensions
{
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
