using NUnit.Framework;
using CheckLinksConsole;
using System.Linq;
namespace CheckLinksConsoleTests
{
    public class CheckLinksTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LinkWithoutBeginnerTest()
        {  
            var links = LinkChecker.GetLinks("<a href=\"google.com\"><\\a>");
            Assert.AreEqual(links.ToList().Count(), 0);
        }

        [Test]
        public void LinkWithBeginnerTest()
        {  
            var links = LinkChecker.GetLinks("<a href=\"https:\\\\google.com\"><\\a>");
            Assert.AreEqual(links.ToList().Count(), 1);
        }
    }
}