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
    /// <summary>
    /// EditTask.aspx.cs av Tord-Marius Fredriksen
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Klassen brukes til å endre et eksisterende task. Teamleder og Prosjektansvarlig har tilgang 
    /// til denne siden.
    /// </summary>
    public partial class EditTask : System.Web.UI.Page
    {
        private DBConnect db;
        private string query, userQuery, saveQuery, taskQuery;
        private DataTable dataTable = new DataTable();
        private DataTable userTable = new DataTable();
        private DataTable taskTable = new DataTable();
        private DataTable backlogTable = new DataTable();
        private string backlogID;
        private int taskID;

        /// <summary>
        /// Metode som kjøres først av alle for å sjekke hvilken masterpage som skal brukes,
        /// alt etter hvilken brukertype som er logget inn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            String userLoggedIn = (String)Session["userLoggedIn"];

            if (userLoggedIn == "teamMember")
                this.MasterPageFile = "~/Masterpages/Bruker.Master";

            else if (userLoggedIn == "teamLeader")
                this.MasterPageFile = "~/Masterpages/Teamleder.Master";

            else if (userLoggedIn == "admin")
                this.MasterPageFile = "~/Masterpages/Admin.Master";

            else
                this.MasterPageFile = "~/Masterpages/Prosjektansvarlig.Master";
        }
        /// <summary>
        /// Kjøres i det nettsiden lastes inn, og gir bare tilgang til brukere som skal ha det.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "teamLeader" || session == "projectManager")
            {
                db = new DBConnect();

                if (!Page.IsPostBack)
                {
                    taskID = Convert.ToInt16(Session["taskID"]);
                    Query();
                }

                if (ViewState["name"] != null)
                {
                    backlogID = (string)ViewState["backlogID"];
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }      
        }
        /// <summary>
        /// Fyller ut data i tekstbokser og lister.
        /// </summary>
        private void Query()
        {
            taskID = Convert.ToInt16(Session["taskID"]);

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
                //endring her medfører rapportinnsending //NB!! Må legges til
                tbPriority.Text = dataTable.Rows[0]["priority"].ToString();

                ddlAddUser.DataTextField = "username";
                ddlAddUser.DataValueField = "userID";
                ddlAddUser.DataSource = userTable;
                ddlAddUser.DataBind();

                ddlDependency.DataTextField = "taskName";
                ddlDependency.DataValueField = "taskID";
                ddlDependency.DataSource = taskTable;
                ddlDependency.DataBind();

                string backlogStart = dataTable.Rows[0]["productBacklogID"].ToString();
                tbBacklog.Text = dataTable.Rows[0]["productBacklogID"].ToString();

                ViewState["taskID"] = dataTable.Rows[0]["taskID"].ToString();

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
 
        }

        /// <summary>
        /// Lagrer oppdaterte verdier i databasen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SetProductBacklogID(true, (string)ViewState["taskID"]);

           saveQuery = String.Format("UPDATE Task SET taskName = '{0}', description = '{1}', priority = {2}, state = {3}, hoursAllocated = {4}, phaseID ={5}, productBacklogID = {6} WHERE taskID = {7}",
               tbTaskName.Text, tbDescription.Text, tbPriority.Text, tbState.Text, tbAllocatedTime.Text, tbPhase.Text, (string)ViewState["backlogID"], Convert.ToInt16(Session["taskID"]));
            db.InsertDeleteUpdate(saveQuery);
        }

        /// <summary>
        /// Denne klassen skal brukes til å sette korrekt BacklogID, men er ikke i dirft grunnet dårlig tid.
        /// </summary>
        /// <param name="subTask"></param>
        /// <param name="_id"></param>
        private void SetProductBacklogID(Boolean subTask, String _id)
        {
            string id = _id;
            taskID = Convert.ToInt16(Session["taskID"]);
            int projectID = Convert.ToInt16(Session["projectID"]);

            string query = "SELECT productBacklogID FROM TaskCategory WHERE projectID = " + projectID + " AND taskCategoryID = 4"; //dummy
            string queryCount = "SELECT COUNT(*) FROM Task WHERE taskCategoryID = 13 AND LENGTH(productBacklogID) = 3";

            backlogTable = db.AdminGetAllUsers(query);
            int count = db.Count(queryCount) + 1;

            backlogID = backlogTable.Rows[0]["productBacklogID"].ToString() + "." + count;

//            if (ddlParentTask.SelectedValue.ToString().Equals(""))
                subTask = false;

            //denne slår inn om den skal være en subtask av en annen task under samme kategori
            if (subTask)
            {
//                string parentID = ddlParentTask.SelectedValue.ToString();
//                backlogQuery = "SELECT productBacklogID FROM Task WHERE taskID = " + parentID;
                backlogTable = db.AdminGetAllUsers(query);

                queryCount = String.Format("SELECT COUNT(*) FROM Task WHERE productBacklogID LIKE '{0}%'", backlogTable.Rows[0]["productBacklogID"].ToString());
                count = db.Count(queryCount);

                backlogID = backlogTable.Rows[0]["productBacklogID"].ToString() + "." + count;

                ViewState["backlogID"] = backlogID;

            }
            tbBacklog.Text = backlogID;
        }

    }
}