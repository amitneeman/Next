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
        const string url = @"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=linux";
        const string authString = "oauth_consumer_key=\"dGlEBGTmOORa5L56dJuXdIQKK\",oauth_token=\"1159469034477367298-xv8gleoltiIRBjTXEuPt1kwU5Zj4Tf\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1565360089\",oauth_nonce=\"TDM0yHO6M49\",oauth_version=\"1.0\",oauth_signature=\"42y94WlrJ2%2Ba4M0bxDgyb%2Bmrq3w%3D\"";
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public string LinuxTweets()
        {
            return TwitterAdapter.TweeterCall(url, authString);
        }
    }
}
