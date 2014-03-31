using Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class EditTask : System.Web.UI.Page
    {
        private DBConnect db;
        private string query, userQuery, saveQuery;
        private DataTable dataTable = new DataTable();
        private DataTable userTable = new DataTable();
        private int taskID = 12;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();

            if (!Page.IsPostBack)
            {
                taskID = 12; // Convert.ToInt32(Request.QueryString["taskID"]);
                Query();
            }         
        }
        private void Query()
        {
            query = String.Format("SELECT * FROM Task WHERE taskID = '{0}'", taskID);
            dataTable = db.getAll(query);

            userQuery = "SELECT * FROM User WHERE aktiv = 1";
            userTable = db.getAll(userQuery);
            try
            {
                tbTaskName.Text = dataTable.Rows[0]["taskName"].ToString();
                tbDescription.Text = dataTable.Rows[0]["description"].ToString();
                tbAllocatedTime.Text = dataTable.Rows[0]["hoursAllocated"].ToString();
                //endring her medfører rapportinnsending //NB!! Må legges til
                tbPriority.Text = dataTable.Rows[0]["priority"].ToString();

                ddlAddUser.DataTextField = "username";
                ddlAddUser.DataValueField = "userID";
                ddlAddUser.DataSource = userTable;
                ddlAddUser.DataBind();

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
 
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
           saveQuery = String.Format("UPDATE Task SET taskName = '{0}', description = '{1}', priority = {2}, hoursAllocated = {3} WHERE taskID = {4}",
               tbTaskName.Text, tbDescription.Text, tbPriority.Text, tbAllocatedTime.Text, taskID);
            db.InsertDeleteUpdate(saveQuery);
        }
        /*
        private void GetTasks()
        {
            query = String.Format("SELECT * FROM Task WHERE taskID = '{0}'", taskID);
            taskName.DataSource = db.getAll(query);
            taskName.DataValueField = "taskID";
            taskName.DataTextField = "taskName";
            taskName.Items.Insert(0, new ListItem("<Velg task>", "0"));
            taskName.DataBind();
        }*/
    }
}