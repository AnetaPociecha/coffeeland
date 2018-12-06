using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace Coffeeland
{
    public class DatabaseQueryProcessor
    {
        public const String connectionString = "datasource=localhost;Initial Catalog=books;port=3306;username=root;password=";

        public void Insert(String command)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand sqlCommand = new MySqlCommand(command, connection);
                sqlCommand.ExecuteReader();
                Console.WriteLine("Saved");

                connection.Close();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public void Select(String query)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(string.Format("tytul = {0}, autor = {1}", dr["tytul"].ToString(), dr["autor"].ToString()));
                }
                connection.Close();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}