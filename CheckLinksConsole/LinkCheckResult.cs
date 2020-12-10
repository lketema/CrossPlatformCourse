
namespace CheckLinksConsole
{
    public class LinkCheckerResult
    {
        public bool IsMissing => (LinkStatus == "ERROR" || LinkStatus == "NotFound") ? true : false;
        public string LinkName{get; set;}
        public string LinkStatus{get; set;}

        public string Problem{get; set;}
        public LinkCheckerResult(string link)
        {
            LinkName = link;
        }

    }
}