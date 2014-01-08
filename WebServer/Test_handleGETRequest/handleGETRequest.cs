using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer;

namespace UnitTest
{
    [TestClass]
    public class handleGETRequest
    {
        [TestMethod]
        public void handleGETRequest_Favicon()
        {
            String http_url = "favicon.ico";
            bool expected = false;

            Request newRequest = new Request(http_url);
            newRequest.handleGETRequest();

            bool actual = newRequest.favicon;
            Assert.AreEqual(expected, actual, "Stimmt nicht");
        }
    }
}
