using System;
using System.Net.Http;
using OAuth;
using System.Net;
using System.Text;
namespace Next.Adapters
{
    public class TwitterAdapter
    {
        public static string TweeterCall(string url,string consumerKey,string consumerSecret)
        {
            OAuthRequest client = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                RequestUrl = url,
                Version = "1.0a",
                Realm = "twitter.com"
            };

            string auth = client.GetAuthorizationHeader();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(client.RequestUrl);
            request.Headers.Add("Authorization", auth);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var encoding = Encoding.UTF8;
            var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding);
            string responseText = reader.ReadToEnd();
            reader.Dispose();
            return responseText;
        }

    }
}

