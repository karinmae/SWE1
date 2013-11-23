using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SensorCloud
{
    public class SensorCloud : Interface.ISensorCloud
    {    
        //Datenbankverbindung
        private string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=TempSensor;Integrated Security=true;";
        
        public void Register()
        {
            //~* ja, also wirklich viel steht hier ned x_X
            Console.WriteLine("SensorCloud Plugin Loaded");
            ReadTempValue();

        }
        
        //Auslesen der Datenbank
        private void ReadTempValue()
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                 {    
                    db.Open();
                    Console.WriteLine("Connected to SensorCloud");
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

        //Thread zum generieren der Werte
        public void ReadTemperature()
        {
            Console.WriteLine("Start reading Temperature...");
            AddTemperature();
        }

        //Werte zur Datenbank hinzufügen
        private void AddTemperature()
        {
            //Zufallswert "Temperatur" erstellen
            Random Rnd = new Random();
            int Wert = Rnd.Next(-50,50);

            //String für SQL
            string cmdInsert = "INSERT INTO TempSensor (Temperatur, Datum) VALUES (@Temperatur, CURRENT_TIMESTAMP)";

            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    // SQL STMT vorbereiten
                    SqlCommand cmd = new SqlCommand(cmdInsert, db);
                    // Parameter setzen
                    cmd.Parameters.AddWithValue("@Temperatur", Wert);
                    // Ausführen, rows enthält die Anzahl der betroffenen Zeilen
                    int rows = cmd.ExecuteNonQuery();
                    db.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Connection to SensorCloud failed");
            }
        }
    }
}
