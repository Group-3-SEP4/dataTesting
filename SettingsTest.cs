using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
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

        //TODO will need changing after adding user to get specific device/room co2 
        [Test]
        public void TestGetSettings() {
            using (var client = new HttpClient()) {
                String URL = "https://localhost:5001/Settings";
                var response = client.GetStringAsync(URL);
                Assert.IsTrue(!response.Equals("[]"));
            }
        }
    }
}
