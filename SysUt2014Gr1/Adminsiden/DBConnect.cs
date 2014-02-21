using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace Admin
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private MySqlDataAdapter adapter;
        private DataTable dataTable;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string test;

        public DBConnect()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            server = "kark.hin.no";
            database = "KALM_DB1";
            uid = "kristiana";
            password = "kristiana123";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            connection.Open();
            return true;
        }

        private bool CloseConnection()
        {
            connection.Close();
            return true;
        }

        public DataTable AdminGetAllUsers()
        {
            string query = "SELECT * FROM test_brukere";

            if (this.OpenConnection() == true)
            {
                dataTable = new DataTable();
                adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(dataTable);
                this.CloseConnection();
            }
            return dataTable;
        }

        public void InsertDeleteUpdate(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
    }
}