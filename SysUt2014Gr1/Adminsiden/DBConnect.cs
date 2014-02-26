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
        /*
        //Metode for å sende filtrert DataTable, sql spørring manipuleres i Admin.aspx.cs
        public DataTable FilterGridView(MySqlCommand sqlCmd)
        {
            try
            {
                if (this.OpenConnection() == true) //Åpner connection
            {
                sqlCmd.Connection = this.connection; //Får inn en sql command uten connection property, gir den riktig database connection her

                dataTable = new DataTable();
                MySqlDataAdapter filterAdapter = new MySqlDataAdapter(sqlCmd); //Lager en data adapter for å få all data fra databasespørringen og for å sette det inn i en data table
                filterAdapter.Fill(dataTable); //Fyller datatable med data fra SQL connection   
            }
            return dataTable;
            }
            catch (System.Data.SqlClient.SqlException ex) //Om noe skulle gå galt
            {
                string feilmelding = "Kunne ikke filtrere brukere!";
                feilmelding += ex.Message;
                throw new Exception(feilmelding);
            }
            finally
            {
                this.CloseConnection(); //Lukker connection
            }
            
        }
         * */
    }
}