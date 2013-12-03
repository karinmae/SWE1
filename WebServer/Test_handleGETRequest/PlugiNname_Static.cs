using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer;
using System.Text.RegularExpressions;

namespace UnitTest
{
    [TestClass]
    public class PluginName_Static
    {
        [TestMethod]
        public void PluginName_true()
        {
            string http_url = "/StaticFile";
            string expected = "StaticFile";

            Request rq = new Request(http_url);
            rq.handleGETRequest();

            string[] split = Regex.Split(http_url, "/");
            string actual = split[1];
            Assert.AreEqual(expected, actual, "Pluginname richtig");


        }
    }
}
