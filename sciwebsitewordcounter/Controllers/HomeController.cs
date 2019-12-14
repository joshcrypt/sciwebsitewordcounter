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
                var sitewords = content.GroupBy(x => x).OrderByDescending(x => x.Count());
                var wordcount = new List<WordCount>();
                foreach (var siteword in sitewords)
                {
                    Debug.WriteLine("{0} {1}", siteword.Key, siteword.Count());
                    wordcount.Add(new WordCount
                    {
                        SiteKey = siteword.Key,
                        SiteCount = siteword.Count().ToString()
                    });
                }
                Debug.WriteLine(wordcount.Count);
                TempData["wordcountdata"] = wordcount;
            }
            //return View("WordCount", wordcount);
            //return RedirectToAction("WordCount");
            return View("WordCount");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void WordCountFunction(string KeyNo, string KeyCount)
        {
            wordcount = new List<WordCount>()
            {
                new WordCount()
                {
                    SiteKey = KeyNo,
                    SiteCount = KeyCount
                }
            };
        }
        //Clears html data
        public static string ClearTags(string rhtml)
        {
            //Removes HTML Tags
            string clearedhtml = Regex.Replace(rhtml, @"<script[^>]*>[\s\S]*?</script>|<[^>]+>| ", " ").Trim();
            string kawaida = Regex.Replace(clearedhtml, @"\s{2,}", " ");
            return kawaida;
        }
        public void GetURL(string link)
        {
            //get site's data
            using (WebClient site = new WebClient())
            {
                Debug.WriteLine(link);
                string removehtml = site.DownloadString(link).ToLower();
                //call function to clear html tags
                removehtml = ClearTags(removehtml);
                List<string> content = removehtml.Split(' ').ToList();
                //return only letters
                var getAlphabetonly = new Regex(@"^[A-z]+$");
                content = content.Where(f => getAlphabetonly.IsMatch(f)).ToList();
                var sitewords = content.GroupBy(x => x).OrderByDescending(x => x.Count());
                var wordcount = new List<WordCount>();
                foreach (var siteword in sitewords)
                {
                    wordcount.Add(new WordCount
                    {
                        SiteKey = siteword.Key,
                        SiteCount = siteword.Count().ToString()
                    });
                }
                /*for(int i =0; i < sitewords.Count(); i++)
                    {
                        wordcount.Add(new WordCount() {
                            siteKey[i] = KeyNo[i],
                            siteCount[i] = KeyCount[i]
                        });
                    }*/
                /*foreach (var siteword in sitewords)
                {
                    string siteKey = siteword.Key;
                    string siteCount = siteword.Count().ToString();
                    
                    WordCountFunction(siteKey, siteCount);
                    countnumber++;
                    
                }*/
            }
        }
    }
}
