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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillMainTasks();
                FillDropDownFase();
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

                string query = String.Format("INSERT INTO Task VALUES(null, '{0}', '{1}', {2}, '{3}', '{4}', '{5}', {6}, {7}, {8}, {9})",
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

        private void SetProductBacklogID(Boolean subTask)
        {
            string id = DropDownMainTask.SelectedValue.ToString();
            string query = "SELECT productBacklogID FROM TaskCategory WHERE projectID = " + prosjektID + " AND taskCategoryID = " + id;
            string queryCount = "SELECT COUNT(*) FROM Task WHERE taskCategoryID = " + id;

            table = db.AdminGetAllUsers(query);
            int count = db.Count(queryCount) + 1;

            string backlogID = table.Rows[0]["productBacklogID"].ToString() + "." + count;

            if (DropDownSubTask.SelectedValue.ToString().Equals(""))
                subTask = false;

            if (subTask)
            {
                queryCount = "SELECT COUNT(*) FROM Task WHERE parentTaskID = " + DropDownSubTask.SelectedValue.ToString();
                count = db.Count(queryCount) + 1;
                backlogID += "." + count;
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