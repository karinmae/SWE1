using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer;

namespace UnitTest
{
    [TestClass]
    public class ParseRequest_long
    {
        [TestMethod]
        public void ParseRequest_longdata()
        {
            string data = "GET /getTemperature/2013/12/03 HTTP/1.1";
            string expected = "GET";
            Request rq = new Request(data);
            rq.parseRequest(data);
            String[] tokens = data.Split(' ');
            string actual = tokens[0].ToUpper();
            Assert.AreEqual(expected, actual, "Stimmt");


        }
    }
}
