using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sciwebsitewordcounter.Models;
using System.Text.RegularExpressions;

namespace sciwebsitewordcounter.Controllers
{
    public class HomeController : Controller
    {
        public List<WordCount> wordcount;
        public List<string[]> sitewordcount;
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string getformurl)
        {
            using (WebClient site = new WebClient())
            {
                Debug.WriteLine(getformurl);
                string removehtml = site.DownloadString(getformurl).ToLower();
                //call function to clear html tags
                removehtml = ClearTags(removehtml);
                List<string> content = removehtml.Split(' ').ToList();
                //return only letters
                var getAlphabetonly = new Regex(@"^[A-z]+$");
                content = content.Where(f => getAlphabetonly.IsMatch(f)).ToList();
                //removes prepositions and words less than 2 characters
                string[] blockedprepositions = { "and", "for", "is", "on", "or", "to", "the", "a" };
                content = content.Where(x => x.Length > 2).Where(x => !blockedprepositions.Contains(x)).ToList();
                var sitewords = content.GroupBy(x => x).OrderByDescending(x => x.Count());
                var wordcount = new List<WordCount>();
                foreach (var siteword in sitewords)
                {
                    Debug.WriteLine("{0} {1}", siteword.Key, siteword.Count());
                    wordcount.Add(new WordCount
                    {
                        //Create Dictionary
                        SiteKey = siteword.Key,
                        SiteCount = siteword.Count().ToString()
                    });
                }
                Debug.WriteLine(wordcount.Count);
                TempData["wordcountdata"] = wordcount;
            }
            return View("WordCount");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Clears html data
        public static string ClearTags(string rhtml)
        {
            //Removes HTML Tags
            string clearedhtml = Regex.Replace(rhtml, @"<script[^>]*>[\s\S]*?</script>|<[^>]+>| ", " ").Trim();
            string removedhtml = Regex.Replace(clearedhtml, @"\s{2,}", " ");
            return removedhtml;
        }
    }
}
