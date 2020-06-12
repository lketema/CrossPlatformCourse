using System;
using System.Net.Http;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckLinksConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {          
            var site = "https://lketema.github.io/";
           // site = "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/";
            var client = new HttpClient();
            // the below dumps content of site
            var bodyReturned = client.GetStringAsync(site);            

            var links = LinkChecker.GetLinks(bodyReturned.Result);

            var result = LinkChecker.CheckLinks(links);
            LogToFile(result);

        }

        private static void LogToFile(IEnumerable<LinkCheckerResult> items)
        {
            var fileDirectory = "Report";
            var fileName = "report.txt";

            var current = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(current, fileDirectory, fileName);

            var directory = Path.GetDirectoryName(filePath);
            // ensure directory is created
            Directory.CreateDirectory(directory);
            Console.WriteLine($"File path {filePath}");
        
            using(var streamWritter = new StreamWriter(filePath))
            {              
                foreach(var item in items.OrderBy(l => !l.IsMissing))
                {
                    streamWritter.WriteLine($"{item.LinkStatus} - {item.LinkName}");
                }
            }
        }
    }
}
