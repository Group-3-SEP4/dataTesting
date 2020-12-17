using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest {
    public class MesurementsTest {
        [Test]
        public void TestConnectionToEndPoint() {
            var url = "https://localhost:5001/measurements?deviceEUI=0004A30B00219CAC";
            try {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)myRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) {
                    Console.Write(string.Format("{0} Available", url));
                }else {
                    Assert.Warn(string.Format("{0} Returned, but with status: {1}", 
                        url, response.StatusDescription));
                }
            }catch (Exception ex) {
                Console.Write(string.Format("{0} unavailable: {1}", url, ex.Message));
                Assert.Fail();
            }
        }

        [Test]
        public async Task TestGetCo2() {
            String URL = "https://localhost:5001/Measurements?deviceEUI=0004A30B00219CAC";
            
            using (var client = new HttpClient()) {
                using (HttpResponseMessage responseMessage = await client.GetAsync(URL)) {
                    using (HttpContent content = responseMessage.Content) {
                        var data = await content.ReadAsStringAsync();
                        try {
                            var result = JObject.Parse(data)["measurementId"].ToString();
                            if(!(int.Parse(result) > 0))
                                Assert.Fail();
                        }catch (Exception e)
                        {
                            Assert.Fail();
                        }
                    }
                }
            }
        }
    }
}
