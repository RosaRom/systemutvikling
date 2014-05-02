using Adminsiden;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    /**Har metode SetProductBacklogID nederst, men får ikke brukt den for å sette korrekt backlogID. Slik
     * det er nå, blir ID satt fra textbox **/
    public partial class PAEditTask : System.Web.UI.Page
    {
        private DBConnect db;
        private int prosjektID = 2; //bare satt en verdi
        private string query, userQuery, saveQuery, taskQuery, backlogQuery;
        private DataTable dataTable = new DataTable();
        private DataTable userTable = new DataTable();
        private DataTable taskTable = new DataTable();
        private DataTable backlogTable = new DataTable();
        private string backlogID;        

        private int taskID = 12; //bare satt en verdi

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                db = new DBConnect();

                if (!Page.IsPostBack)
                {
                    taskID = 12;
                    Query();
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
        }
        private void Query()
        {
            query = String.Format("SELECT * FROM Task WHERE taskID = '{0}'", taskID);
            dataTable = db.getAll(query);

            userQuery = "SELECT * FROM User WHERE aktiv = 1";
            userTable = db.getAll(userQuery);
            taskQuery = "SELECT * FROM Task";
            taskTable = db.getAll(taskQuery);
            taskTable.Rows.InsertAt(taskTable.NewRow(), 0); //setter inn tom rad øverst
            //her blir alle verdier i textbokser satt
            try
            {
                tbTaskName.Text = dataTable.Rows[0]["taskName"].ToString();
                tbDescription.Text = dataTable.Rows[0]["description"].ToString();
                tbAllocatedTime.Text = dataTable.Rows[0]["hoursAllocated"].ToString();                
                tbPhase.Text = dataTable.Rows[0]["phaseID"].ToString();
                tbState.Text = dataTable.Rows[0]["state"].ToString();                
                tbPriority.Text = dataTable.Rows[0]["priority"].ToString();                              

                ddlAddUser.DataTextField = "username";
                ddlAddUser.DataValueField = "userID";
                ddlAddUser.DataSource = userTable;
                ddlAddUser.DataBind();

                ddlDependency.DataTextField = "taskName";
                ddlDependency.DataValueField = "taskID";
                ddlDependency.DataSource = taskTable;
                ddlDependency.DataBind();

                ddlParentTask.DataTextField = "taskName";
                ddlParentTask.DataValueField = "taskID";
                ddlParentTask.DataSource = taskTable;
                ddlParentTask.DataBind();

                string backlogStart = dataTable.Rows[0]["productBacklogID"].ToString();
                tbBacklog.Text = dataTable.Rows[0]["productBacklogID"].ToString();
                if (tbBacklog.Text != backlogStart)
                    SetProductBacklogID(true, dataTable.Rows[0]["taskID"].ToString());


            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }

        }

        /**
         * Sender inn oppdaterte verdier **/
        protected void btnSave_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            dataTable = db.getAll(String.Format("SELECT * FROM Task WHERE taskID = {0}", taskID));
            int hoursExtra = Convert.ToInt32(dataTable.Rows[0]["hoursExtra"].ToString());
            int temp2 = Convert.ToInt32(dataTable.Rows[0]["hoursAllocated"].ToString());
            int temp1 = Convert.ToInt32(tbAllocatedTime.Text);
            
            if (temp1 == temp2)
            {
                saveQuery = String.Format("UPDATE Task SET taskName = '{0}', description = '{1}', priority = {2}, state = {3}, hoursAllocated = {4}, phaseID ={5}, productBacklogID = '{6}', hoursExtra = {8}  WHERE taskID = {7}",
                tbTaskName.Text, tbDescription.Text, tbPriority.Text, tbState.Text, tbAllocatedTime.Text, tbPhase.Text, tbBacklog.Text, taskID, hoursExtra);
                db.InsertDeleteUpdate(saveQuery);
            }
            else
            {
                int temp3 = temp1 - temp2;

                saveQuery = String.Format("UPDATE Task SET taskName = '{0}', description = '{1}', priority = {2}, state = {3}, hoursAllocated = {4}, phaseID ={5}, productBacklogID = '{6}', hoursExtra = {8} WHERE taskID = {7}",
                tbTaskName.Text, tbDescription.Text, tbPriority.Text, tbState.Text, temp2, tbPhase.Text, tbBacklog.Text, taskID, temp3);
                db.InsertDeleteUpdate(saveQuery);

                // her kan en deviationrapport om at allocatedHours har blitt forandret genereres
                // temp1 = nye allokerte timer
                // temp2 = orginale allokerte timer
                // temp3 = forskjellen mellom dem (150 nytt estimat - 100 orginlt estimat = 50 ekstra timer)

                string queryDeviationReport = String.Format("INSERT INTO deviationReport VALUES(null, 'Timeforandring på task', 'Timeantallet på task: \"{0}\" forandres fra {1} timer til {2} timer', 0, 0)", tbTaskName.Text, temp2, temp1);
                db.InsertDeleteUpdate(queryDeviationReport);
            }
        }

        private void SetProductBacklogID(Boolean subTask, String _id)
        {
            string id = _id;
            string query = "SELECT productBacklogID FROM TaskCategory WHERE projectID = " + prosjektID + " AND taskCategoryID = 13"; //dummy
            string queryCount = "SELECT COUNT(*) FROM Task WHERE taskCategoryID = 13 AND LENGTH(productBacklogID) = 3";

            backlogTable = db.AdminGetAllUsers(query);
            int count = db.Count(queryCount) + 1;

            backlogID = backlogTable.Rows[0]["productBacklogID"].ToString() + "." + count;

            if (ddlParentTask.SelectedValue.ToString().Equals(""))
                subTask = false;

            //denne slår inn om den skal være en subtask av en annen task under samme kategori
            if (subTask)
            {
                string parentID = ddlParentTask.SelectedValue.ToString();
                backlogQuery = "SELECT productBacklogID FROM Task WHERE taskID = " + parentID;
                backlogTable = db.AdminGetAllUsers(query);

                queryCount = String.Format("SELECT COUNT(*) FROM Task WHERE productBacklogID LIKE '{0}%'", backlogTable.Rows[0]["productBacklogID"].ToString());
                count = db.Count(queryCount);

                backlogID = backlogTable.Rows[0]["productBacklogID"].ToString() + "." + count;
            }
            tbBacklog.Text = backlogID;
        }

    }
}