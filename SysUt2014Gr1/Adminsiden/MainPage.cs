using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login;

namespace Login
{
    public partial class MainPage : Form
    {
        private DBConnect db = new DBConnect();
        private BindingList<User> list;


        private int userID;
        private string surname;
        private string firstname;
        private string username;
        private int phone;
        private string mail;

        public MainPage()
        {
            InitializeComponent();
            list = db.Select();
            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string tempID = dataGridView1.Rows[e.RowIndex].Cells["ID"].EditedFormattedValue.ToString();
            userID = Convert.ToInt32(tempID);
            firstname = dataGridView1.Rows[e.RowIndex].Cells["Firstname"].EditedFormattedValue.ToString();
            surname = dataGridView1.Rows[e.RowIndex].Cells["Surname"].EditedFormattedValue.ToString();
            username = dataGridView1.Rows[e.RowIndex].Cells["Username"].EditedFormattedValue.ToString();
            string tempPhone = dataGridView1.Rows[e.RowIndex].Cells["Phone"].EditedFormattedValue.ToString();
            phone = Convert.ToInt32(tempPhone);
        }
    }
}
