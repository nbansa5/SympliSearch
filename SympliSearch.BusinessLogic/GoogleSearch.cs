using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace SympliSearch.BusinessLogic
{
    public class GoogleSearch : ISearch
    {
        public async Task<string> Search(string queryString, string url)
        {
            IList<int> searchPlaces = new List<int>();
            var htmlWeb = new HtmlWeb();
            var query = $"https://www.google.com/search?q={queryString}&num=99";
            var doc = await htmlWeb.LoadFromWebAsync(query);
            var index = 1;
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//div[@class='kCrYT']/a"))
            {
                string href = link.GetAttributeValue("href", string.Empty);
                if (href.Contains(url))
                {
                    searchPlaces.Add(index);
                }
                index++;
            }
            var rank = string.Join(",", searchPlaces.Select(n => n.ToString()).ToArray());
            if (string.IsNullOrEmpty(rank))
            {
                return "0";
            }
            return rank;
        }
    }
}
