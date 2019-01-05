using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Coffeeland.Database
{
    class Connector
    {
        //const String connectionString = "datasource=localhost;Initial Catalog=coffeeland;port=3306;username=root;password=;Convert Zero Datetime=True";
        const String connectionString = "datasource=localhost;Initial Catalog=coffeeland_test;port=3306;username=root;password=;Convert Zero Datetime=True";
        const String dbScriptPath = @"D:\kurwamac\coffeeland\src\Coffeeland\Coffeeland\Database\db.sql";

        internal bool Erase()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
            
                MySqlScript script = new MySqlScript(connection, File.ReadAllText(dbScriptPath));
                script.Delimiter = ";";
                script.Execute();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }


        public bool ExecuteCommand(String command)  // for insert, update, delete
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand sqlCommand = new MySqlCommand(command, connection);
                sqlCommand.ExecuteReader();

                connection.Close();
                return true;

            }catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }


        public DataTable ExecuteQuery(String query) // for select
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                connection.Close();

                return dt;

            }catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
