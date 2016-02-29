using MilanSzDemo.Helper;
using MilanSzDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Web.Syndication;

namespace MilanSzDemo.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Rss> News { get; set; }

        public MainViewModel()
        {
            DownloadRss();
        }

        private async void DownloadRss()
        {
            News = new ObservableCollection<Rss>();
            HttpClient client = new HttpClient();
            string url = Config.RssUrl;
            string xml;
            using (var webClient = new HttpClient())
            {
                xml = await webClient.GetStringAsync(url);
            }

            XDocument document = XDocument.Parse(xml);
            List<Rss> rss = (from descendant in document.Descendants("item")
                             select new Rss()
                             {
                                 Description = descendant.Element("description").Value,
                                 Title = descendant.Element("title").Value,
                                 Url = descendant.Element("link").Value
                             }).ToList();
            foreach (var item in rss)
            {
                News.Add(item);
            }
        }
    }
}
