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
            try
            {
                string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=TempSensor;Integrated Security=true;";
                SqlConnection con = new SqlConnection(strCon);
                Console.WriteLine("Connected to TempSensor");
                SqlCommand cmdSelect = new SqlCommand("SELECT * FROM TempSensor", con);
                SqlCommand cmdInsert = new SqlCommand("INSERT INTO TempSensor (Wert) VALUES(@Wert)", con);
                cmdInsert.Parameters.AddWithValue("@Wert", 100);
                cmdInsert.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmdSelect;
                DataTable tbl = new DataTable();
                da.Fill(tbl);

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    DataRow row = tbl.Rows[i];
                    Console.WriteLine("{0} ", row[0]);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection to TempSensor failed");
            }
        }
    }
}
