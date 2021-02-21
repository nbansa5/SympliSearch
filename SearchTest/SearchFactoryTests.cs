using NUnit.Framework;
using SympliSearch;
using SympliSearch.BusinessLogic;

namespace SearchTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SearchFactory_When_Called_With_Google_Should_Return_GoogleSearchObject()
        {
            ISearch search = new SearchFactory().GetSearchProvider(SearchEnum.Google);
            Assert.IsTrue(search is GoogleSearch);
        }

        [Test]
        public void SearchFactory_When_Called_With_Bing_Should_Return_BingObject()
        {
            ISearch search = new SearchFactory().GetSearchProvider(SearchEnum.Bing);
            Assert.IsTrue(search is BingSearch);
        }
    }
}