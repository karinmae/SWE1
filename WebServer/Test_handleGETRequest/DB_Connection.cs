using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interface;

namespace UnitTest
{
    [TestClass]
    public class DB_Connection
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Connection()
        {
            string strCon = @"Data Source="";" + "Initial Catalog=TempSensor;Integrated Security=true;";

            var SensorCloud = new SensorCloud();
            SensorCloud.set_Connection(strCon);
            PrivateObject privateSensor = new PrivateObject(SensorCloud);

            privateSensor.Invoke("ReadTempValue");
        }
    }
}
