using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SympliSearch.BusinessLogic
{
    public class BingSearch : ISearch
    {
        public async Task<string> Search(string queryString, string url)
        {
            IList<int> searchPlaces = new List<int>();
            var htmlWeb = new HtmlWeb();
            var query = $"https://www.bing.com/search?q={queryString}&count=100";
            var doc = await htmlWeb.LoadFromWebAsync(query);
            var results = doc.DocumentNode.SelectNodes("//li[@class='b_algo']");

            if (results != null)
            {
                var index = 1;
                foreach (var result in results)
                {
                    if (result.Element("h2") != null)
                    {
                        var refNode = result.Element("h2").Element("a");
                        if (refNode.Attributes["href"].Value.Contains(url))
                        {
                            searchPlaces.Add(index);
                        }
                        index++;
                    }
                }
            }
            var rank =  string.Join(",", searchPlaces.Select(n => n.ToString()).ToArray());
            if (string.IsNullOrEmpty(rank))
            {
                return "0";
            }
            return rank;
        }
    }
}
