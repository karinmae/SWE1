using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Timers;
using WebLibrary;

namespace Interface
{
    public class SensorCloud : IPlugin
    {    
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

        public void handleRequest(Url url)
        {
            Console.WriteLine("SensorCloud: handleRequest");
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
