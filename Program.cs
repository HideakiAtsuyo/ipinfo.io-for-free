using Leaf.xNet;
using System;

namespace IpInfoDotIoForFree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("IP: ");
            string ip = Console.ReadLine();

            using (HttpRequest req = new HttpRequest())
            {
                req.IgnoreProtocolErrors = true;
                req.Proxy = HttpProxyClient.Parse("209.141.62.66:1337"); //Use Proxy To Avoid The Rate Limit(put a proxy list don't be dumb huh :()
                req.AddHeader("Referer", "https://ipinfo.io");
                req.AddHeader("User-Agent", "HelloWorld");

                var res = req.Get(String.Format("https://ipinfo.io/widget/{0}", ip));
                if (res.StatusCode == HttpStatusCode.TooManyRequests)
                    Console.WriteLine("Rate Limited");
                else
                    Console.WriteLine(res.ToString());
                req.Close();
            }
            Console.ReadLine();
        }
    }
}
