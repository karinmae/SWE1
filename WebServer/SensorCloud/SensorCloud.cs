using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Timers;
using WebLibrary;

namespace Interface
{
    public class SensorCloud : IPlugin
    {

        //private Response newResponse = new Response();
        private Url newUrl = new Url();
        private String pluginName;
        private NetworkStream clientStream;
        //Datenbankverbindung
        private string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=TempSensor;Integrated Security=true;";
        public void start()
        {
            // wirklich viel steht hier ned x_X
            ReadTempValue();
            ReadSensor ReadSensor = new ReadSensor();
            ReadSensor.Start();

        }

        public string getName()
        {
            return "SensorCloud";
        }

        public void handleRequest(Url url, NetworkStream stream)
        {
            newUrl = (Url)url;
           // DateTime Date = new DateTime();
            string Datum;
            clientStream = stream;
            //if (clientStream == null) throw new ArgumentNullException("stream");
            string[] split = newUrl.getSplitUrl();
            pluginName = newUrl.getPluginName();
            
            if (String.Compare(pluginName, "getTemperature") == 0)
            {
                //Console.WriteLine("{0}: handleRequest", pluginName);
                if (split.Length == 4)
                {
                    Datum = split[3] + '-' + split[2] + '-' + split[1];
                    //Date = DateTime.Parse(Datum, System.Globalization.CultureInfo.InvariantCulture);
                    //Console.WriteLine("Date: {0}", Date);
                    SearchTemp(Datum);
                }
                else
                {
                    Console.WriteLine("Too few Arguments");
                }
            }
         }


        //Auslesen der Datenbank
        private void ReadTempValue()
        {          
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                 {    
                    db.Open();
                    Console.WriteLine("---");
                    //SQL Statement zum auslesen
                    SqlCommand cmdSelect = new SqlCommand("SELECT Temperatur, Datum FROM TempSensor ORDER BY [Datum]", db);
                    //cmdSelect.Parameters.AddWithValue("@Date", "28.11.2013");

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {
                            Console.WriteLine("Temperatur: {0}°C \nDatum: {1}",
                            rd["Temperatur"], rd["Datum"]);
                            Console.WriteLine("----");
                        }
                        // DataReader schließen 
                        rd.Close();
                    }

                    // Verbindung schließen 
                    db.Close();
                    
            }

            }
            catch (Exception)
            {
                Console.WriteLine("Connection to SensorCloud_DB_Read failed");
            }
        }

        private void SearchTemp(string Date)
        {
            if (clientStream == null) throw new ArgumentNullException("stream");
            StreamWriter sw = new StreamWriter(clientStream);    
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    Console.WriteLine("---");
                    //SQL Statement zum auslesen
                    SqlCommand cmdSelect = new SqlCommand("SELECT Temperatur, Datum FROM TempSensor WHERE [Datum] = @Date Order by Datum;", db);
                    cmdSelect.Parameters.AddWithValue("@Date", Date);
                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {
                            Console.WriteLine("Temperatur: {0}°C \nDatum: {1}",
                            rd["Temperatur"], rd["Datum"]);
                            Console.WriteLine("----");
                        }
                        // DataReader schließen 
                        rd.Close();
                    }

                    // Verbindung schließen 
                    db.Close();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Connection to SensorCloud_DB failed");
            }
        }

    }
}
