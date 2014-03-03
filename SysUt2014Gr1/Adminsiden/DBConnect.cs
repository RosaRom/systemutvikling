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

        public DBConnect()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            server = "kark.hin.no";
            database = "gruppe1";
            uid = "gruppe1";
            password = "gruppe.123";
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

        public DataTable AdminGetAllUsers(string query)
        {
            try
            {
                if (this.OpenConnection() == true)
                {
                    dataTable = new DataTable();
                    adapter = new MySqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string message = "Klarte ikke hente data fra databasen!";
                message += ex.Message;
                throw new Exception(message);
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public DataTable getAll(string query)
        {
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

        public int GetTeamGroupId(string query)
        {
            int teamId = 0;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                teamId = (Int32) cmd.ExecuteScalar();

                this.CloseConnection();
            }
            return teamId;
        }


       /* public void InsertTimeSheet(string _start, string _stop, int _userID, int _taskID, string _description, int _workplaceID, int _active, int _projectID)
        {
            //string query = "INSERT INTO TimeSheet (start, stop, userID, taskID, description, workplaceID, active, projectID) VALUES('" + first_name + "', '" + second_name + "')"
                
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }*/
    }
}