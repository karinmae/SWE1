using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer;

namespace UnitTest
{
    [TestClass]
    public class ParseRequest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ParseRequest_shortdata()
        {
            string data = "GET /getTemperature/2013/12/03";
            Request rq = new Request(data);
            rq.parseRequest(data);
        }
    }
}
