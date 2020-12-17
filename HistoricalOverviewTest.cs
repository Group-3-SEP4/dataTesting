using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest {
    public class HistoricalOverviewTest {
        [Test]
        public void TestConnectionToEndPoint() {
            string validFrom = "2020-11-27";
            string validTo = "2020-12-05";
            
            var url = "https://localhost:5001/HistoricalOverview?deviceEUI=0004A30B00219CB5&validFrom=" + validFrom + "&validTo=" + validTo;
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
        public async Task TestGetHistoricalOverviewNotNull()
        {
            string validFrom = "2020-11-27";
            string validTo = "2020-12-05";
            
            var url = "https://localhost:5001/HistoricalOverview?deviceEUI=0004A30B00219CB5&validFrom=" + validFrom + "&validTo=" + validTo;
            
            using (var client = new HttpClient()) {
                using (HttpResponseMessage responseMessage = await client.GetAsync(url)) {
                    using (HttpContent content = responseMessage.Content) {
                        var data = await content.ReadAsStringAsync();
                        var jObject = JObject.Parse(data)["detailedCo2List"].ToString();
                        var jArray = JArray.Parse(jObject);
                        var result = JObject.Parse(jArray[0].ToString())["value"].ToString();
                        Assert.IsNotNull(result);
                    }
                }
            }
        }
        
        [Test]
        public async Task TestGetHistoricalOverviewIsNotEmpty() {
            string validFrom = "2020-11-27";
            string validTo = "2020-12-05";
            
            var url = "https://localhost:5001/HistoricalOverview?deviceEUI=0004A30B00219CB5&validFrom=" + validFrom + "&validTo=" + validTo;
            
            using (var client = new HttpClient()) {
                using (HttpResponseMessage responseMessage = await client.GetAsync(url)) {
                    using (HttpContent content = responseMessage.Content) {
                        var data = await content.ReadAsStringAsync();
                        var jObject = JObject.Parse(data)["detailedCo2List"].ToString();
                        var jArray = JArray.Parse(jObject);
                        var result = JObject.Parse(jArray[0].ToString())["value"].ToString();
                        Assert.IsNotEmpty(result);
                    }
                }
            }
        }
        
        [Test]
        public async Task TestGetHistoricalOverviewIsNotNullNorZero() {
            string validFrom = "2020-11-27";
            string validTo = "2020-12-05";
            
            var URL = "https://localhost:5001/HistoricalOverview?deviceEUI=0004A30B00219CB5&validFrom=" + validFrom + "&validTo=" + validTo;

            using (var client = new HttpClient()) {
                using (HttpResponseMessage responseMessage = await client.GetAsync(URL)) {
                    using (HttpContent content = responseMessage.Content) {
                        var data = await content.ReadAsStringAsync();
                        var jObject = JObject.Parse(data)["detailedCo2List"].ToString();
                        var jArray = JArray.Parse(jObject);
                        var result = JObject.Parse(jArray[0].ToString())["value"].ToString();
                        if (result == null || result == "0") Assert.Fail();
                    }
                }
            }
        }
    }
}