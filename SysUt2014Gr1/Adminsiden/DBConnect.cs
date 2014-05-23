using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace Adminsiden
{
    /// <summary>
    /// DBConnet.cs skrevet litt av alle sammen 
    /// SysUt14Gr1 - SystemUtvikling - Vår 2014
    /// Denne klassen står for all kontakt mot databasen
    /// </summary>
    public class DBConnect
    {
        private MySqlConnection connection;
        private MySqlDataAdapter adapter;
        private DataTable dataTable;
        private string server;
        private string database;
        private string uid;
        private string password;

        /// <summary>
        /// Constructor for klassen
        /// </summary>
        public DBConnect()
        {
            this.Initialize();
        }

        /// <summary>
        /// Her opprettes den en connection
        /// </summary>
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

        /// <summary>
        /// Kobler til databasen
        /// </summary>
        /// <returns></returns>
        private bool OpenConnection()
        {
            connection.Open();
            return true;
        }

        /// <summary>
        /// Stenger tilkoblingen mot databasen
        /// </summary>
        /// <returns></returns>
        private bool CloseConnection()
        {
            connection.Close();
            return true;
        }

        /// <summary>
        /// Her hentes ut data basert på en query og returnerer en datatable.
        /// Navnet kan være misvisende, lagde den i forbindelse med Adminsiden.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gjør det samme som metoden over.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Teller antall kolonner og returnere antallet
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int Count(string query)
        {
            int count = 0;

            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return count;
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
        
        /// <summary>
        /// Gjør oppgaver som Update, Insert og Delete basert på hva query inneholder.
        /// </summary>
        /// <param name="query"></param>
        public void InsertDeleteUpdate(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try {
                cmd.ExecuteNonQuery();    
                }
                 catch(InvalidOperationException e){
                     Console.WriteLine("An error occurred: '{0}'", e);
                 }
                this.CloseConnection();
            }
        }
        
        /// <summary>
        /// Brukes for å oppdatere timesheet
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_stop"></param>
        /// <param name="_userID"></param>
        /// <param name="_taskID"></param>
        /// <param name="_description"></param>
        /// <param name="_workplaceID"></param>
        /// <param name="_state"></param>
        /// <param name="_projectID"></param>
        /// <param name="_permissionState"></param>
        public void InsertTimeSheet(string _start, string _stop, int _userID, int _taskID, string _description, int _workplaceID, int _state, int _projectID, int _permissionState)
        {
            string query = "INSERT INTO TimeSheet (start, stop, userID, taskID, description, workplaceID, state, projectID, permissionState) VALUES('" + _start + "', '" + _stop + "', '" + _userID + "', '" + _taskID + "', '" + _description + "', '" + _workplaceID + "', '" + _state + "', '" + _projectID +"','" + _permissionState + "')";
                
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
               
                this.CloseConnection();
            }
        }
    }
}