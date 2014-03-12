using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


using System.Web; 

using System.Data.SqlClient; 
using System.Configuration; 
using System.Security; 


namespace Login
{
    public partial class LoginForm : Form
    {
        private MySqlConnection mySQLconnection;
        private string server;
        private string database;
        private string userID;
        private string password;
        private MainPage mainpage;

        public LoginForm()
        {
            InitializeComponent();
            server = "kark.hin.no";
            database = "gruppe1";
            userID = "gruppe1";
            password = "gruppe.123";

            string mySQLconnectionString;
            mySQLconnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UserID=" + userID + ";" + "Password=" + password + ";";
            mySQLconnection = new MySqlConnection(mySQLconnectionString);
          
        }
        private bool OpenConnection()
        {
            try
            {
                mySQLconnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Kan ikke kontakte serveren.  Kontakt administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Ugyldig brukernavn/passord, prøv på nytt");
                        break;
                }
                return false;
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(Login.Properties.Resources.morildDataLogo);
            LogoPictureBox.Image = bitmap;
            this.LogoPictureBox.Image = (Image)bitmap;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
          //  mySQLconnection.Open();
                  
            /*
                MySqlCommand mySQLcommand = new MySqlCommand("SELECT * FROM SUser WHERE username = @username AND password = @password");

                if (this.OpenConnection() == true) 
                {
                    mySQLcommand.Parameters.AddWithValue("username", UsernameTextBox.Text);
                    mySQLcommand.Parameters.AddWithValue("password", PasswordTextBox.Text);
                    MySqlDataReader dataReader = mySQLcommand.ExecuteReader();
                    while(dataReader.Read())
                    {
                        mainpage = new MainPage();
                        mainpage.Show();
                    }
                    dataReader.Close();
                }
            //mainpage = new MainPage();
           // mainpage.Show();
             * */

            DBConnect db = new DBConnect();
            DataTable dataTable = db.GetUser(PasswordTextBox.Text, UsernameTextBox.Text);
            if (dataTable != null)
            {
                mainpage = new MainPage();
                mainpage.Show();
                //MessageBox.Show(dataTable.ToString());
            }
            else MessageBox.Show("Noe gikk galt");

            }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            
        }
    
    }
}
