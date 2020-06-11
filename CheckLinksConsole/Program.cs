using System;
using System.Net.Http;
using System.Linq;
namespace CheckLinksConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var site = "https://lketema.github.io/";
            var client = new HttpClient();
            // the below dumps content of site
            var bodyReturned = client.GetStringAsync(site);
            

            var links = LinkChecker.GetLinks(bodyReturned.Result);

            links.ToList().ForEach(Console.WriteLine);
        }
    }
}
