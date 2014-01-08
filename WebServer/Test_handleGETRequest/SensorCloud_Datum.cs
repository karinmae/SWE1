//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using WebServer;
//using WebLibrary;
//using Interface;
//using System.Net.Sockets;

//namespace UnitTest
//{
//    [TestClass]
//    public class SensorCloud_Datum
//    {
//        [TestMethod]
//        public void Datum_richtig()
//        {

//            //TcpClient tcpClient = new TcpClient();
//            //NetworkStream stream = tcpClient.GetStream();
//            NetworkStream stream; 
           
//            Url u = new Url();

//            string[] split = { "getTemperature", "06", "01", "2014" };
//            u.setPluginName("getTemperature");

//            string expected = "06.01.2014";

//            var sensor = new SensorCloud();

//            sensor.handleRequest(u, stream);

//            string actual = sensor.Datum;

//            Assert.AreEqual(expected, actual, "Datum richtig");

//        }
//    }
//}
