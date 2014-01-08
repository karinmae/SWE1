using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interface; 

namespace UnitTest
{
    [TestClass]
    public class SaticFile_File
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Filedoesnotexist()
        {
            string file = "bla.jpg";

            var staticfile = new StaticFiles();
            staticfile.set_filename(file);
            PrivateObject privateStatic = new PrivateObject(staticfile);

            privateStatic.Invoke("SendFile");



        }
    }
}
