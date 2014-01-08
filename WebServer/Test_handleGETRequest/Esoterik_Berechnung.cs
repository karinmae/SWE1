using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServer;
using WebLibrary;
using Esoterik;




namespace UnitTest
{
    [TestClass]
    public class Esoterik_Berechnung
    {
        [TestMethod]
        public void Berechnung_esoterik()
        {
           
            string MagicName = "karinw1992";
            int expected = 5;

            var eso = new Esoterik.Esoterik();
            PrivateObject privateEso = new PrivateObject(eso);
           
            privateEso.Invoke("Berechnen", MagicName);

            int actual = eso.MagicResult;
            
            Assert.AreEqual(expected, actual, "Berechnung richtig");

        }
    }
}
