using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Next.Adapters;
using Microsoft.Extensions.Configuration;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Next.Controllers
{

    public class TwitterController : Controller
    {
        IConfiguration configuration;
        public TwitterController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }


        public IActionResult Index()
        {
            return View();
        }

        public string LinuxTweets()
        {
            string url = configuration.GetSection("Twitter").GetSection("url").Value;
            string consumer_key = configuration.GetSection("Twitter").GetSection("consumer_key").Value;
            string consumer_secret = configuration.GetSection("Twitter").GetSection("consumer_secret").Value;
            return TwitterAdapter.TweeterCall(url, consumer_key, consumer_secret);
        }
    }
}
