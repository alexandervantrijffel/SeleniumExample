using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Structura.GuiTests.SeleniumHelpers
{
    public class ExtendedRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        private readonly Uri _remoteHost;

        public ExtendedRemoteWebDriver(Uri remoteHost, ICapabilities capabilities, TimeSpan commandTimeout)
            : base(remoteHost, capabilities, commandTimeout)
        {
            _remoteHost = remoteHost;
        }

        public string GetNodeHost()
        {
            var result = "[UNKNOWN HOST]";
            var uri = new Uri(string.Format("http://{0}:{1}/grid/api/testsession?session={2}", _remoteHost.Host, _remoteHost.Port, SessionId));

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var httpResponse = (HttpWebResponse)request.GetResponse())
            {
                var stream = httpResponse.GetResponseStream();
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        result = JObject.Parse(reader.ReadToEnd()).SelectToken("proxyId").ToString();
                    }
                }
            }
            return result;
        }

        //this will allow screenshots to be taken from the remote browser
        public Screenshot GetScreenshot()
        {
            return new Screenshot(Execute(DriverCommand.Screenshot, null).Value.ToString());
        }
    }
}