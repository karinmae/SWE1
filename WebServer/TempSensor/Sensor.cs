using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SensorCloud
{
    public class Temp : IPlugin
    {
        //private string  strSQL = "SELECT * FROM TempSensor";
        
        public void Register()
        {
            //Console.WriteLine("Ich bin ein Plugin ^_^");

            Random Rnd = new Random();
            int Wert = Rnd.Next(50);
            InsertTempValue(Wert);

        }
        private void InsertTempValue(int Wert)
        {
            //try
            //{
                string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=TempSensor;Integrated Security=true;";
                using (SqlConnection db = new SqlConnection(strCon))
                 {    
                    db.Open();
                    Console.WriteLine("Connected to TempSensor");
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

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Connection to TempSensor failed");
            //}
        }
    }
}
