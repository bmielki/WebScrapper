using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScrapper.Models
{
    public class UrlModel
    {
        public string url { get; set; }

        public bool validateUrl(UrlModel url)
        {
            Uri uriResult;
            return (Uri.TryCreate(url.url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
        }
    }

    public class ImageScrapperModel
    {
        public string url { get; set; }

        public List<ImageScrapperModel> getImages(UrlModel url)
        {
            var document = new HtmlWeb().Load(url.url);
            var srcs = document.DocumentNode.Descendants("img")
                                            .Select(e => e.GetAttributeValue("src", null))
                                            .Where(e => !String.IsNullOrEmpty(e))
                                            .Where(e => e.Substring(0, 4) == "http");

            List<ImageScrapperModel> list = new List<ImageScrapperModel>();
            foreach (var item in srcs)
            {
                ImageScrapperModel obj = new ImageScrapperModel();
                obj.url = item;
                list.Add(obj);
            }
            return list;
        }
    }

    public class WordScrapperModel
    {
        public string word { get; set; }
        public int count { get; set; }

        public List<WordScrapperModel> getWords(UrlModel url, string[] ignore = null)
        {
            ignore = ignore ?? new string[0];

            var document = new HtmlWeb().Load(url.url);

            string finaltext = "";

            //var ps = document.DocumentNode.Descendants("p");
            //foreach(var p in ps) { finaltext += " " + p.InnerText; }

            //var spans = document.DocumentNode.Descendants("span");
            //foreach (var span in spans) { finaltext += " " + span.InnerText; }

            //var h1s = document.DocumentNode.Descendants("h1");
            //foreach (var h1 in h1s) { finaltext += " " + h1.InnerText; }

            //var h2s = document.DocumentNode.Descendants("h2");
            //foreach (var h2 in h2s) { finaltext += " " + h2.InnerText; }

            //var h3s = document.DocumentNode.Descendants("h3");
            //foreach (var h3 in h3s) { finaltext += " " + h3.InnerText; }

            //var h4s = document.DocumentNode.Descendants("h4");
            //foreach (var h4 in h4s) { finaltext += " " + h4.InnerText; }

            //var h5s = document.DocumentNode.Descendants("h5");
            //foreach (var h5 in h5s) { finaltext += " " + h5.InnerText; }

            //var h6s = document.DocumentNode.Descendants("h6");
            //foreach (var h6 in h6s) { finaltext += " " + h6.InnerText; }


            var nodes = document.DocumentNode.SelectSingleNode("//body").DescendantsAndSelf();
            foreach (var node in nodes)
            {
                if (node.NodeType == HtmlNodeType.Text 
                    &&  node.ParentNode.Name != "script" )
                {
                    finaltext += " " + node.InnerText.ToLower();
                }
            }


            finaltext = new string(finaltext.Where(c => !char.IsPunctuation(c)).ToArray());
            var words = finaltext.Trim()
                                .Split(' ')
                                .GroupBy(x => x)
                                .Select(x => new
                                {
                                    KeyField = x.Key,
                                    Count = x.Count()
                                })
                                .OrderByDescending(x => x.Count);


            List<WordScrapperModel> list = new List<WordScrapperModel>();
            foreach (var word in words)
            {
                WordScrapperModel obj = new WordScrapperModel();
                obj.word = word.KeyField;
                obj.count = word.Count;

                if (obj.word.Length < 3) { continue; }
                if (ignore.Contains(obj.word)) { continue; }

                list.Add(obj);
            }
            return list.GetRange(0, 10);
        }
    }
}

