using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace WebServiceTest
{
    [TestClass]
    public class UnitTestTemperatureAPI
    {
        [Test]
        public void TestConnection()
        {
            UnitTestConnection unitTestConnection = new UnitTestConnection();
            unitTestConnection.TestConnection("Temperature");
        }

        [Test]
        public void TestGetTemperature()
        {
            using (var client = new HttpClient())
            {
                String URL = "https://localhost:5001/Temperature";
                var response = client.GetStringAsync(URL);
                Assert.IsTrue(Int16.Parse(response.Result) == 32767);
            }
        }
    }
}