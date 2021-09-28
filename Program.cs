using Leaf.xNet;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IpInfoDotIoForFree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("IP: ");
            string ip = Console.ReadLine();
            Console.Clear();
            using (HttpRequest req = new HttpRequest())
            {
                req.IgnoreProtocolErrors = true;
                req.Proxy = HttpProxyClient.Parse("190.107.224.150:3128"); //Use Proxy To Avoid The Rate Limit(put a proxy list don't be dumb huh :()
                req.AddHeader("Referer", "https://ipinfo.io");
                req.AddHeader("User-Agent", "HelloWorld");

                var res = req.Get(String.Format("https://ipinfo.io/widget/{0}", ip));
                if (res.StatusCode == HttpStatusCode.TooManyRequests)
                    Console.WriteLine("Rate Limited");
                else
                {
                    JObject json = JObject.Parse(res.ToString());
                    Console.WriteLine(String.Format("Checked IP: {0}\nASN Name: {1}\nCompany Name: {2}\nPrivacy: VPN: {3} | Proxy: {4} | Tor: {5} | Hosting: {6}\nAbuse Email: {7}\nDomains Count: {8}", json["ip"], json["asn"]["name"], json["company"]["name"], json["privacy"]["vpn"], json["privacy"]["proxy"], json["privacy"]["tor"], json["privacy"]["hosting"], json["abuse"]["email"], json["domains"]["total"]));
                }
                    
                req.Close();
            }
            Console.ReadLine();
        }
    }
}
