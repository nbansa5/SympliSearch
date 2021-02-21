using SympliSearch.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SympliSearch
{
    public interface ISearchFactory
    {
        ISearch GetSearchProvider(SearchEnum searchEnum);
    }
}
