using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest {
    [TestClass]
    public class SettingsTest {
        [Test]
        public void TestConnectionToEndPoint() {
            var url = "https://localhost:5001/Settings?deviceEUI=0004A30B00219CAC";
            try {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)myRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) {
                    Console.Write(string.Format("{0} Available", url));
                }else {
                    Assert.Warn(string.Format("{0} Returned, but with status: {1}", 
                        url, response.StatusDescription));
                }
            }
            catch (Exception ex) {
                Console.Write(string.Format("{0} unavailable: {1}", url, ex.Message));
                Assert.Fail();
            }
        }

        [Test]
        public async Task TestGetSettings() {
            String URL = "https://localhost:5001/Settings?deviceEUI=0004A30B00219CAC";
            
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(URL))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var result = JObject.Parse(data)["settingsId"].ToString();
                        Assert.AreEqual("4", result);
                    }
                }
            }
        }
        
        [Test]
        public async Task TestPostSettings() {
            string contentPost = "{\"settingsId\":6,\"lastUpdated\":\"2020-12-08T14:52:43.723\",\"temperatureSetpoint\":18.0,\"ppmMin\":400,\"ppmMax\":5000,\"sentToDevice\":null}";
            var buffer = System.Text.Encoding.UTF8.GetBytes(contentPost);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            String URL = "https://localhost:5001/Settings?deviceEUI=0004A30B00219CAC";
            
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.PostAsync(URL, byteContent))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var result = JObject.Parse(data)["settingsId"].ToString();
                        Assert.AreEqual("6", result);
                    }
                }
            }
        }
    }
}
