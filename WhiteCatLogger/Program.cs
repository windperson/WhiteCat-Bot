using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Titanium.Web.Proxy;
using Titanium.Web.Proxy.Models;

namespace WhiteCatLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            WCatLogger.ReloadKeys();

            var proxyServer = new ProxyServer();

            proxyServer.BeforeRequest += WCatLogger.OnRequest;
            proxyServer.BeforeResponse += WCatLogger.OnResponse;
            //proxyServer.ServerCertificateValidationCallback += WCatLogger.OnCertificateValidation;
            //proxyServer.ClientCertificateSelectionCallback += WCatLogger.OnCertificateSelection;


            var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, false)
            {
                // ExcludedHttpsHostNameRegex = new List<string>() { "google.com", "dropbox.com" }
            };


            proxyServer.AddEndPoint(explicitEndPoint);
            proxyServer.Start();


            foreach (var endPoint in proxyServer.ProxyEndPoints)
                Console.WriteLine("Listening on '{0}' endpoint at Ip {1} and port: {2} ",
                    endPoint.GetType().Name, endPoint.IpAddress, endPoint.Port);

            //Only explicit proxies can be set as system proxy!
            //proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            //proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);

            while (!Console.ReadLine().Equals("exit")) ;

            //Unsubscribe & Quit
            proxyServer.BeforeRequest -= WCatLogger.OnRequest;
            proxyServer.BeforeResponse -= WCatLogger.OnResponse;
            proxyServer.DisableAllSystemProxies();
            proxyServer.Stop();
        }
    }
}
