using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest
{
    
    public class UnitTestConnection
    {
        public void TestConnection(string url)
        {
            using (var client = new HttpClient())
            {
                String URL = "https://localhost:5001/" + url;
                var response = client.GetStringAsync(URL);
                string result = response.ToString();
                Assert.IsTrue(result != null);
            }
        }
    }
}