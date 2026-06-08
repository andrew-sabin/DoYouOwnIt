using AngleSharp.Html.Parser;
using System.Collections.Generic;

namespace DoYouOwnIt.Client.Helpers
{
    public class HTMLImageExtractor
    {
        public static List<string> ExtractImageUrls(string htmlContent)
        {
            var imageUrls = new List<string>();

            if (string.IsNullOrWhiteSpace(htmlContent))
                return imageUrls;

            var parser = new HtmlParser();
            var document = parser.ParseDocument(htmlContent);
            var imgElements = document.QuerySelectorAll("img");

            foreach(var img in imgElements)
            {
                var src = img.GetAttribute("src");
                if (!string.IsNullOrEmpty(src))
                {
                    imageUrls.Add(src);
                }
            }
            return imageUrls;
        }

        public static List<string> ExtractImageAlts(string htmlContent)
        {
            var imgAlts = new List<string>();

            if (string.IsNullOrEmpty(htmlContent))
                return imgAlts;

            var parser = new HtmlParser();
            var document = parser.ParseDocument(htmlContent);
            var imgElements = document.QuerySelectorAll("img");

            foreach (var img in imgElements)
            {
                var src = img.GetAttribute("alt");
                if (!string.IsNullOrEmpty(src))
                {
                    imgAlts.Add(src);
                }
            }
            return imgAlts;
        }
    }
}
