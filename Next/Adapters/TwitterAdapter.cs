using System;
using System.Net.Http;

namespace Next.Adapters
{
    public class TwitterAdapter
    {
        public static string TweeterCall(string url,string authString)
        {
            HttpResponseMessage data = null;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth",authString);
            try
            {
                data = client.GetAsync(@url).Result;
                string res = data.Content.ReadAsStringAsync().Result;
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "[]";
            }
        }

    }
}
