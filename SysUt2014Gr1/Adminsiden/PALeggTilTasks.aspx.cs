using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Adminsiden
{
    public partial class PALeggTilTasks : System.Web.UI.Page
    {
        private int prosjektID = 2;                                                     //hentes fra forrige side
        private DBConnect db = new DBConnect();
        private DataTable table = new DataTable();
        private DataTable categoryTable = new DataTable();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    FillMainTasks();
                    FillDropDownFase();
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
         
        }

        //lagrer en ny task i databasen
        protected void BtnLagreTask_Click(object sender, EventArgs e)
        {
            try
            {
                string taskCategoryID = DropDownMainTask.SelectedValue.ToString();
                
                string taskName;
                if (taskNavn.Text.Equals(""))
                    throw new Exception("Task må ha et navn");                          //lager exception med en message om ikke navn er satt
                else
                    taskName = taskNavn.Text;

                string description;
                if (beskrivelse.Text.Equals(""))
                    description = "NULL";
                else
                    description = "'" + beskrivelse.Text + "'";

                string priority = DropDownPrioritering.SelectedValue.ToString();
                string state = "0";

                string hoursAllocated;
                if (timerAllokert.Text.Equals(""))
                    hoursAllocated = "NULL";
                else
                    hoursAllocated = "'" + timerAllokert.Text + "'";

                string parentTaskID;
                if (DropDownSubTask.SelectedValue.ToString().Equals(""))
                    parentTaskID = "NULL";
                else
                    parentTaskID = "'" + DropDownSubTask.SelectedValue.ToString() + "'";

                string phaseID = DropDownFase.SelectedValue.ToString();

                string productBacklogID = pbID.Text;

                string query = String.Format("INSERT INTO Task VALUES(null, '{0}', '{1}', {2}, '{3}', '{4}', '{5}', {6}, {7}, {8}, '{9}', null)",
                    taskCategoryID, taskName, description, priority, state, null, hoursAllocated, parentTaskID, phaseID, productBacklogID);
                db.InsertDeleteUpdate(query);

                beskjed.Text = "Ny task er lagt til";

                FillTasks();
                ResetForm();
            } catch (Exception ex)
            {
                beskjed.Text = "Noe gikk galt: " + ex.Message;                              //skriver ut beskjed om noe gikk galt under lagring av task
            }
            
        }

        protected void BtnNyKategori_Click(object sender, EventArgs e)
        {
            Response.Redirect("PANyHovedtask.aspx");
        }

        //henter ut alle tasks som tilhører en valgt hovedtask, blir oppdatert hver gang hovedtask endres
        private void FillTasks()
        {
            string queryTask = "SELECT taskID, taskName FROM Task WHERE TaskCategoryID = " + DropDownMainTask.SelectedValue.ToString();

            table = db.AdminGetAllUsers(queryTask);
            table.Rows.InsertAt(table.NewRow(), 0);

            DropDownSubTask.DataSource = table;
            DropDownSubTask.DataBind();
        }

        //kjøres ved oppstart av siden, henter ut alle hovedtasks til en gitt projectID
        private void FillMainTasks()
        {
            string queryMainTask = "SELECT taskCategoryID, taskCategoryName FROM TaskCategory WHERE projectID = " + prosjektID;
            table = db.AdminGetAllUsers(queryMainTask);

            DropDownMainTask.DataSource = table;
            DropDownMainTask.DataBind();

            SetProductBacklogID(false);
            FillTasks();
        }

        //kjøres ved hver forandring av hovedtask, fyller opp tilhørende tasks på nytt
        protected void DropDownMainTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTasks();
            beskjed.Text = "";
            ResetForm();
            SetProductBacklogID(false);
        }

        //kjøres ved hver forandring av hovedtask, fyller opp tilhørende tasks på nytt
        protected void DropDownSubTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProductBacklogID(true);
        }

        /// <summary>
        /// Her lages en product backlog id automatisk basert på tidligere registrerte tasks og hvilken kategori den ligger under
        /// </summary>
        /// <param name="subTask">Settes til true/false basert på om den skal være en subtask eller ikke</param>
        private void SetProductBacklogID(Boolean subTask)
        {
            string id = DropDownMainTask.SelectedValue.ToString();
            string query = "SELECT productBacklogID FROM TaskCategory WHERE projectID = " + prosjektID + " AND taskCategoryID = " + id;
            string queryCount = "SELECT COUNT(*) FROM Task WHERE taskCategoryID = " + id + " AND LENGTH(productBacklogID) = 3";

            table = db.AdminGetAllUsers(query);
            int count = db.Count(queryCount) + 1;

            string backlogID = table.Rows[0]["productBacklogID"].ToString() + "." + count;

            if (DropDownSubTask.SelectedValue.ToString().Equals(""))
                subTask = false;

            //denne slår inn om den skal være en subtask av en annen task under samme kategori
            if (subTask)
            {
                query = "SELECT productBacklogID FROM Task WHERE taskID = " + DropDownSubTask.SelectedValue.ToString();
                table = db.AdminGetAllUsers(query);

                queryCount = String.Format("SELECT COUNT(*) FROM Task WHERE productBacklogID LIKE '{0}%'", table.Rows[0]["productBacklogID"].ToString());
                count = db.Count(queryCount);

                backlogID = table.Rows[0]["productBacklogID"].ToString() + "." + count;
            }
            pbID.Text = backlogID;
        }

        //setter alle verdier tilbake til null
        private void ResetForm()
        {
            taskNavn.Text = "";
            beskrivelse.Text = "";
            DropDownPrioritering.SelectedIndex = 0;
            timerAllokert.Text = "";
            DropDownSubTask.SelectedIndex = 0;
        }

        //henter ut alle faser til et gitt prosjekt
        private void FillDropDownFase()
        {
            string query = "SELECT phaseName, phaseID FROM Fase WHERE projectID = " + prosjektID;
            DataTable table = new DataTable();

            try
            {
                table = db.getAll(query);
                DropDownFase.DataSource = table;
                DropDownFase.DataBind();
            }
            catch (Exception ex)
            {
                beskjed.Text = "Noe gikk galt: " + ex.Message;
            }
        }
    }
}