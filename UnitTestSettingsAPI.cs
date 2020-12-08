using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest
{
    [TestClass]
    public class UnitTestSettingsApi
    {
        
        [Test]
        public async Task TestGetSettings()
        {
            String URL = "https://localhost:5001/Settings?deviceEUI=0004A30B00219CAC";
            
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(URL))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var result = JObject.Parse(data)["settingsId"].ToString();
                        Assert.IsNotNull(result);
                    }
                }
            }
        }
        
        [Test]
        public async Task TestGetSettingsID()
        {
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
    }
}
