using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Timers;

namespace SensorCloud
{
    public class SensorCloud : Interface.ISensorCloud
    {    
        //Datenbankverbindung
        private string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=TempSensor;Integrated Security=true;";

        public void getTemperature()
        {
            // wirklich viel steht hier ned x_X
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


        public void ReadTemperature()
        {
            AddValues newAdd = new AddValues();
            newAdd.Start();
        }

    //    private void WorkThread()
    //    {
    //        // ~* Timer Starten *~
    //        aTimer = new System.Timers.Timer(10000);
    //        aTimer.Elapsed += new ElapsedEventHandler(AddTemperature);

    //        //Intervall auf 2 Minuten setzen (120000)
    //        aTimer.Interval = 120000;
    //        aTimer.Enabled = true;
    //        GC.KeepAlive(aTimer);
    //    }     

    //    //Werte zur Datenbank hinzufügen
    //    private void AddTemperature(object source, ElapsedEventArgs e)
    //    {
    //        //Zufallswert "Temperatur" erstellen
    //        Random Rnd = new Random();
    //        int Wert = Rnd.Next(-50,50);

    //        //String für SQL
    //        string cmdInsert = "INSERT INTO TempSensor (Temperatur, Datum) VALUES (@Temperatur, CURRENT_TIMESTAMP)";

    //        try
    //        {
    //            using (SqlConnection db = new SqlConnection(strCon))
    //            {
    //                db.Open();
    //                // SQL STMT vorbereiten
    //                SqlCommand cmd = new SqlCommand(cmdInsert, db);
    //                // Parameter setzen
    //                cmd.Parameters.AddWithValue("@Temperatur", Wert);
    //                // Ausführen, rows enthält die Anzahl der betroffenen Zeilen
    //                int rows = cmd.ExecuteNonQuery();
    //                Console.WriteLine("~* Temperatur Wert der Datenbank hinzugefügt. *~");
    //                db.Close();
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Console.WriteLine("Connection to SensorCloud_DB failed");
    //        }
    //    }
    }
}
