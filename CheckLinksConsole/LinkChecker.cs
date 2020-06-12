using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace CheckLinksConsole
{
    public class LinkChecker
    {
        public static IEnumerable<string> GetLinks(string page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);
            var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty))
                .Where(l => !string.IsNullOrEmpty(l))
                .Where(l => l.StartsWith("http"));

            return links;
        }

        public static IEnumerable<LinkCheckerResult> CheckLinks(IEnumerable<string> links)
        {
            // this will wait on all links to be checked, waiting on all threads to complete
            var allResult = Task.WhenAll(links.Select(CheckLink));
            return allResult.Result;
        }

        private static async Task<LinkCheckerResult> CheckLink(string link)
        {
            var result = new LinkCheckerResult(link);
            using(var client = new HttpClient())
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Head, link);
                try
                {
                    var response = await client.SendAsync(httpRequest);
                    result.LinkStatus =response.StatusCode.ToString();
                }
                catch(HttpRequestException e)
                {
                    result.Problem = e.Message;
                    result.LinkStatus = "ERROR";
                }
            }

            return result;
        }        
    }
}