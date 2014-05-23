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
    /// EditTask.aspx.cs av Tord-Marius Fredriksen, skrevet om av Kristian Alm
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Klassen brukes til å endre et eksisterende task. Teamleder og Prosjektansvarlig har tilgang 
    /// til denne siden.
    /// </summary>
    public partial class EditTask : System.Web.UI.Page
    {
        private DBConnect db;
        private DataTable tableTask = new DataTable();
        private DataTable tableBacklogID = new DataTable();
        private DataTable tableCategory = new DataTable();
        private DataTable tableSubTasks = new DataTable();
        private int taskID;
        private int projectID;

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
                taskID = Convert.ToInt16(Session["taskID"]);

                if (!Page.IsPostBack)
                {
                    FillAllFields();
                }
                else
                {
                    tableTask = (DataTable)ViewState["table"];
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }      
        }

        /// <summary>
        /// Her hentes data om task ut og alle felt/dropdownlister fylles med informasjon
        /// </summary>
        private void FillAllFields()
        {
            FillMainTasks();
            FillDropDownFase();

            string query = "SELECT * FROM Task WHERE taskID = " + taskID;
            tableTask = db.AdminGetAllUsers(query);
            ViewState["table"] = tableTask;
            
            FillTasks(Convert.ToInt32(tableTask.Rows[0]["taskCategoryID"]));

            DropDownMainTask.SelectedValue = tableTask.Rows[0]["taskCategoryID"].ToString();
            taskNavn.Text = tableTask.Rows[0]["taskName"].ToString();
            timerAllokert.Text = tableTask.Rows[0]["hoursAllocated"].ToString();
            beskrivelse.Text = tableTask.Rows[0]["description"].ToString();
            pbID.Text = tableTask.Rows[0]["productBacklogID"].ToString();
            DropDownFase.SelectedValue = tableTask.Rows[0]["phaseID"].ToString();
            DropDownPrioritering.SelectedValue = tableTask.Rows[0]["priority"].ToString();

            if (!tableTask.Rows[0]["parentTaskID"].ToString().Equals(""))
                DropDownSubTask.SelectedValue = tableTask.Rows[0]["parentTaskID"].ToString();
        }

        /// <summary>
        /// henter ut alle tasks som tilhører en valgt kategori, blir oppdatert hver gang hovedtask endres
        /// </summary>
        private void FillTasks(int taskCategoryID)
        {
            string queryTask = "SELECT taskID, taskName FROM Task WHERE TaskCategoryID = " + taskCategoryID;

            tableSubTasks = db.AdminGetAllUsers(queryTask);
            tableSubTasks.Rows.InsertAt(tableSubTasks.NewRow(), 0);

            DropDownSubTask.DataSource = tableSubTasks;
            DropDownSubTask.DataBind();
        }

        /// <summary>
        /// Fyller dropdownlisten med alle kategorier til prosjektet
        /// </summary>
        private void FillMainTasks()
        {
            projectID = Convert.ToInt16(Session["projectID"]);

            string queryMainTask = "SELECT taskCategoryID, taskCategoryName FROM TaskCategory WHERE projectID = " + projectID;
            tableCategory = db.AdminGetAllUsers(queryMainTask);

            try
            {
                DropDownMainTask.DataSource = tableCategory;
                DropDownMainTask.DataBind();
            }
            catch (Exception ex)
            {
                beskjed.Text = "Noe gikk galt(FillMainTasks): " + ex.Message;
            }

        }

        /// <summary>
        /// henter ut alle faser til et gitt prosjekt
        /// </summary>
        private void FillDropDownFase()
        {
            projectID = Convert.ToInt16(Session["projectID"]);

            string query = "SELECT phaseName, phaseID FROM Fase WHERE projectID = " + projectID;
            DataTable table = new DataTable();

            try
            {
                table = db.getAll(query);
                DropDownFase.DataSource = table;
                DropDownFase.DataBind();
            }
            catch (Exception ex)
            {
                beskjed.Text = "Noe gikk galt: (Filldriodownfase)" + ex.Message;
            }
        }

        /// <summary>
        /// Utfører disse operasjonene hver gang kategori forandres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownMainTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTasks(Convert.ToInt32(tableTask.Rows[0]["taskCategoryID"]));
            SetProductBacklogID(false);
        }

        /// <summary>
        /// Fyller ut listen med tasks tilhørende samme kategori
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownSubTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProductBacklogID(true);
        }

        /// <summary>
        /// Når man trykker på lagre knappen sendes all informasjon om tasken inn til databasen.
        /// Noen verdier som kan være null i databasen må sjekkes mot stringverdi og settes til null om ikke annet står der.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    description = beskrivelse.Text;

                string priority = DropDownPrioritering.SelectedValue.ToString();
                string state = tableTask.Rows[0]["state"].ToString();

                string hoursAllocated;
                if (timerAllokert.Text.Equals(""))
                    hoursAllocated = "NULL";
                else
                    hoursAllocated = timerAllokert.Text;

                string parentTaskID;
                if (DropDownSubTask.SelectedValue.ToString().Equals(""))
                    parentTaskID = "NULL";
                else
                    parentTaskID = DropDownSubTask.SelectedValue.ToString();

                string phaseID = DropDownFase.SelectedValue.ToString();

                string productBacklogID = pbID.Text;

                string query = String.Format("UPDATE Task SET taskCategoryID = '{0}', taskName = '{1}', description = '{2}', priority = '{3}', state = '{4}', hoursUsed = '{5}', hoursAllocated = '{6}', parentTaskID = '{7}', phaseID = '{8}', productBacklogID = '{9}', hoursExtra = '{10}' WHERE taskID = '{11}'",
                    taskCategoryID, taskName, description, priority, state, tableTask.Rows[0]["hoursUsed"].ToString(), hoursAllocated, parentTaskID, phaseID, productBacklogID, tableTask.Rows[0]["hoursExtra"].ToString(), taskID);
                db.InsertDeleteUpdate(query);

                beskjed.Text = "Task er redigert";

            }
            catch (Exception ex)
            {
                beskjed.Text = "Noe gikk galt: " + ex.Message;                              //skriver ut beskjed om noe gikk galt under lagring av task
            }
        }

        /// <summary>
        /// Her lages en product backlog id automatisk basert på tidligere registrerte tasks og hvilken kategori den ligger under
        /// </summary>
        /// <param name="subTask">Settes til true/false basert på om den skal være en subtask eller ikke</param>
        private void SetProductBacklogID(Boolean subTask)
        {
            projectID = Convert.ToInt16(Session["projectID"]);
            try
            {
                string id = DropDownMainTask.SelectedValue.ToString();
                string query = "SELECT productBacklogID FROM TaskCategory WHERE projectID = " + projectID + " AND taskCategoryID = " + id;
                string queryCount = "SELECT COUNT(*) FROM Task WHERE taskCategoryID = " + id + " AND LENGTH(productBacklogID) = 3";

                tableBacklogID = db.AdminGetAllUsers(query);
                int count = db.Count(queryCount) + 1;

                string backlogID = tableBacklogID.Rows[0]["productBacklogID"].ToString() + "." + count;

                if (DropDownSubTask.SelectedValue.ToString().Equals(""))
                    subTask = false;

                //denne slår inn om den skal være en subtask av en annen task under samme kategori
                if (subTask)
                {
                    query = "SELECT productBacklogID FROM Task WHERE taskID = " + DropDownSubTask.SelectedValue.ToString();
                    tableBacklogID = db.AdminGetAllUsers(query);

                    queryCount = String.Format("SELECT COUNT(*) FROM Task WHERE productBacklogID LIKE '{0}%'", tableBacklogID.Rows[0]["productBacklogID"].ToString());
                    count = db.Count(queryCount);

                    backlogID = tableBacklogID.Rows[0]["productBacklogID"].ToString() + "." + count;
                }
                pbID.Text = backlogID;
            }
            catch (Exception ex)
            {
                beskjed.Text = "Noe gikk galt: " + ex.Message;                              //skriver ut beskjed om noe gikk galt under lagring av task
            }
        }
    }
}