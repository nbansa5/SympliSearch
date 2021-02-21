using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SympliSearch.BusinessLogic;

namespace SympliSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;

        private readonly IMemoryCache _memoryCache;

        private readonly ISearchFactory _searchFactory;

        public SearchController(ILogger<SearchController> logger, IMemoryCache memoryCache, ISearchFactory searchFactory)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _searchFactory = searchFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string queryString, string url, SearchEnum searchProvicer)
        {
            if (string.IsNullOrEmpty(queryString) || string.IsNullOrEmpty(url))
            {
                return BadRequest("QueryString or Url should not be empty.");
            }
            var cacheKey = queryString.ToLower() + "_" + url.ToLower() + "_" + searchProvicer.ToString().ToLower();
            // TODO : Ideally will use distributed caching like REDIS provided by AWS ElastiCache instead of in memory cache.
            if (!_memoryCache.TryGetValue(cacheKey, out string rank))
            {
                rank = await _searchFactory.GetSearchProvider(searchProvicer).Search(queryString, url);

                var cacheExpirationOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(1)
                };
                _memoryCache.Set(cacheKey, rank, cacheExpirationOption);
            }
            return Ok(rank.Split(','));
        }

        [HttpGet]
        [Route("SearchAllProviders")]
        public async Task<IActionResult> SearchAllProviders(string queryString, string url)
        {
            if (string.IsNullOrEmpty(queryString) || string.IsNullOrEmpty(url))
            {
                return BadRequest("QueryString or Url should not be empty.");
            }
            List<string> rankings = new List<string>();
            foreach (int i in Enum.GetValues(typeof(SearchEnum)))
            {
                var provider = Enum.GetName(typeof(SearchEnum), i);
                Enum.TryParse(provider, out SearchEnum myProvider);
                var cacheKey = queryString.ToLower() + "_" + url.ToLower() + "_" + provider.ToString().ToLower();
                // TODO : Ideally will use distributed caching like REDIS provided by AWS ElastiCache instead of in memory cache.
                if (!_memoryCache.TryGetValue(cacheKey, out string rank))
                {
                    rank = await _searchFactory.GetSearchProvider(myProvider).Search(queryString, url);

                    var cacheExpirationOption = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(1)
                    };
                    _memoryCache.Set(cacheKey, rank, cacheExpirationOption);
                }
                rankings.Add(provider.ToString() + " : " + rank);
            }            
            return Ok(rankings);
        }

        [HttpGet]
        [Route("GetAllProviders")]
        public IActionResult GetAllProviders()
        {
            List<string> providers = new List<string>();
            foreach (int i in Enum.GetValues(typeof(SearchEnum)))
            {
                providers.Add(Enum.GetName(typeof(SearchEnum), i));

            }
            return Ok(providers);
        }
    }

}
