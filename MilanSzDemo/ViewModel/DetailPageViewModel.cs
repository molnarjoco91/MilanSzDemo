using MilanSzDemo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MilanSzDemo.ViewModel
{
    public class DetailPageViewModel : INotifyPropertyChanged
    {
        private New article;

        public Rss Rss { get; set; }

        public New New
        {
            get
            {
                return article;
            }
            set
            {
                if (value != this.article)
                {
                    this.article = value;
                }
            }
        }

        public DetailPageViewModel()
        {
            this.New = new New();
        }

        public void SetRss(Rss rss)
        {
            this.Rss = rss;
            DownloadNew(rss.Url);
        }

        private async void DownloadNew(string url)
        {
            string line;
            using (var wc = new HttpClient())
            {
                var response = await wc.GetAsync(url);
                var html = await response.Content.ReadAsStreamAsync();
                var stream = new StreamReader(html);
                line = stream.ReadToEnd();
            }
            ParseHtml(line);
        }

        private void ParseHtml(string html)
        {
            HtmlAgilityPack.HtmlDocument DocToParse = new HtmlAgilityPack.HtmlDocument();
            DocToParse.LoadHtml(html);
            var root = DocToParse.DocumentNode;

            var p = root.Descendants()
                        .Where(n => n.GetAttributeValue("class", "").Equals("cikk-torzs"))
                        .Single();

            New.Content = p.InnerText;            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
