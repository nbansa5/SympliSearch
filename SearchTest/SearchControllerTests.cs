using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SympliSearch;
using SympliSearch.BusinessLogic;
using SympliSearch.Controllers;
using System.Threading.Tasks;

namespace SearchTest
{
    public class SearchControllerTests
    {
        private Mock<ISearch> searchMock;
        private SearchController controller;
        private Mock<ILogger<SearchController>> loggerMock;
        private Mock<ISearchFactory> searchFactoryMock;
        private Mock<IMemoryCache> memoryCacheMock;
        private MemoryCache cache;
        [SetUp]
        public void Setup()
        {
            searchMock = new Mock<ISearch>();            
            loggerMock = new Mock<ILogger<SearchController>>();
            searchFactoryMock = new Mock<ISearchFactory>();            
            memoryCacheMock = new Mock<IMemoryCache>();
            cache = new MemoryCache(new MemoryCacheOptions());           
        }


        [Test]
        public async Task SeachController_Get_With_GoogleSearchObject_return_appropriate_value()
        {
            searchMock.Setup(p => p.Search("test", "hjhkj")).Returns(Task.FromResult("0"));
            searchFactoryMock.Setup(p => p.GetSearchProvider(SearchEnum.Google)).Returns(new GoogleSearch());
            controller = new SearchController(loggerMock.Object, cache, searchFactoryMock.Object);
            var result = await controller.Get("test", "hjhkj", SearchEnum.Google);
            Assert.AreEqual("0", ((string[])((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)[0]);
            object test;
            Assert.IsTrue(cache.TryGetValue("test_hjhkj_google", out test));
            Assert.AreEqual(test, "0");
        }

        [Test]
        public async Task SeachController_Get_With_BingSearchObject_return_appropriate_value()
        {
            searchMock.Setup(p => p.Search("test", "hjhkj")).Returns(Task.FromResult("0"));
            searchFactoryMock.Setup(p => p.GetSearchProvider(SearchEnum.Bing)).Returns(new BingSearch());
            controller = new SearchController(loggerMock.Object, cache, searchFactoryMock.Object);
            var result = await controller.Get("test", "hjhkj", SearchEnum.Bing);
            Assert.AreEqual("0", ((string[])((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)[0]);
            object test;
            Assert.IsTrue(cache.TryGetValue("test_hjhkj_bing", out test));
            Assert.AreEqual(test, "0");
        }

        [Test]
        public void SeachController_SearchAllProviders__return_allProvider()
        {
            searchMock.Setup(p => p.Search("test", "hjhkj")).Returns(Task.FromResult("0"));
            searchFactoryMock.Setup(p => p.GetSearchProvider(SearchEnum.Bing)).Returns(new BingSearch());
            controller = new SearchController(loggerMock.Object, cache, searchFactoryMock.Object);
            var result = controller.GetAllProviders();
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
        }
    }
}