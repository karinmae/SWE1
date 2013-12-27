using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.IO;

namespace Esoterik
{
    public class Esoterik : IPlugin
    {
        private NetworkStream stream;
        private string strCon = @"Data Source=(local);" + "Initial Catalog=TempSensor; Integrated Security=true;";
        public void start()
        {
            Console.WriteLine("Esoterik Plugin loaded");
        }

        public void handleRequest(Url url, NetworkStream clientStream)
        {
            stream = clientStream;
            Url newUrl = new Url();
            newUrl = (Url)url;
            string pluginName = newUrl.getPluginName();
            string[] filenameSplit;
            string MagicName;

            if (pluginName == "Esoterik")
            {
                Console.WriteLine("{0}: handleRequest", pluginName);
                filenameSplit = newUrl.getSplitUrl();
                if (filenameSplit.Length == 4)
                {
                    string temp = filenameSplit[1] + filenameSplit[3];
                    string temp2 = temp.ToLower();
                    MagicName = temp2 + filenameSplit[2];
                   Berechnen(MagicName);
                }
            }
        }

        private void Berechnen(string MagicName)
        {
            int MagicNumber = 0;
            for (int i = 0; i < MagicName.Length; i++)
            {
                MagicNumber += MagicName[i];
            }
            int MagicResult = MagicNumber % 10;
            Console.WriteLine("Your Number: {0}", MagicResult);
            StreamWriter sw = new StreamWriter(stream); 
            //SQL-Connection
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                    SqlCommand cmdSelect = new SqlCommand("SELECT id, Name, Beschreibung FROM Esoterik WHERE [id] = @MagicResult Order by id;", db);
                    cmdSelect.Parameters.AddWithValue("@MagicResult", MagicResult);
                    
                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        sw.WriteLine("HTTP/1.1 200 OK");
                        sw.WriteLine("connection: close");
                        sw.WriteLine("content-type: text/html");
                        sw.WriteLine();
                        sw.WriteLine("<html><body>");
                        sw.WriteLine("<h1>");
                        while (rd.Read())
                        {
                            sw.WriteLine("Dein Name: {0}", rd["Name"]);
                            sw.WriteLine("</br>{0}", rd["Beschreibung"]);
                        }
                        // DataReader schließen 
                        sw.WriteLine("</h1>");
                        sw.WriteLine("</body></html>");
                        sw.Flush();
                        rd.Close();
                    }

                    // Verbindung schließen 
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection to Esoterik failed {0}", ex);
            }
        }

        public string getName()
        {
            return "Esoterik";
        }
    }
}
