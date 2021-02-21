using SympliSearch.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SympliSearch
{
    public class SearchFactory : ISearchFactory
    {
        public ISearch GetSearchProvider(SearchEnum searchEnum){
            switch (searchEnum) {
                case SearchEnum.Google:
                {
                    return new GoogleSearch();
                }
                case SearchEnum.Bing:
                {
                    return new BingSearch();
                }
                default:
                {
                    return null;
                }
            }   
        }
    }

    public enum SearchEnum{
        Google,
        Bing
    }
}
