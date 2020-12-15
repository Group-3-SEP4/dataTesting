using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest
{
    [TestClass]
    public class NightOverviewTest
    {
        [Test]
        public void TestConnectionToEndPoint()
        {
            var url = "https://localhost:5001/NightOverview/Today?deviceEUI=0004A30B00219CAC";
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
        public async Task TestGetTodaysNightOverviewNotNull() 
        {
            String URL = "https://localhost:5001/NightOverview/Today?deviceEUI=0004A30B00219CB5";
            
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(URL))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var jArray = JArray.Parse(data);
                        var result = JObject.Parse(jArray[0].ToString())["humiMin"].ToString();
                        Assert.IsNotNull(result);
                    }
                }
            }
    
        }
        
        [Test]
        public async Task TestGetTodaysNightOverviewIsNotEmpty() 
        {
            String URL = "https://localhost:5001/NightOverview/Today?deviceEUI=0004A30B00219CB5";
            
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(URL))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var jArray = JArray.Parse(data);
                        // Min Humidity
                        var result = JObject.Parse(jArray[0].ToString())["humiMax"].ToString();
                        Assert.IsNotEmpty(result);
                        if (result == null || result == "0") Assert.Fail();

                    }
                }
            }
    
        }
        
        [Test]
        public async Task TestGetTodaysNightOverviewIsNotNullNorZero() 
        {
            String URL = "https://localhost:5001/NightOverview/Today?deviceEUI=0004A30B00219CB5";
            
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(URL))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var jArray = JArray.Parse(data);
                        // Min Humidity
                        var result = JObject.Parse(jArray[0].ToString())["tempAvg"].ToString();
                        if (result == null || result == "0") Assert.Fail();

                    }
                }
            }
    
        }
        
    }
}