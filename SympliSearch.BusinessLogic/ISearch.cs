using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SympliSearch.BusinessLogic
{
    public interface ISearch
    {
        Task<string> Search(string queryString, string url);
    }
}
