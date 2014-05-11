using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Collections;

namespace Adminsiden
{
    public partial class NyttProsjekt : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();        
        private DateTime startDate = new DateTime();
        private DateTime endDate = new DateTime();
        //private bool datesOK = false;
        private List<String> taskCategoryIDs = new List<String>(); // lagrer ingenting
        private List<String> taskCategories = new List<String>();

        private int projectID;
        private int taskCategoriCounter = 0;

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
            if (!Page.IsPostBack)
            {
                GetTeams();
                GetParentProjects();
                GetTaskCategories();
            }
        }

        private void GetTeams()
        {
            string query = "SELECT * FROM Team";
            ddlTeam.DataSource = db.getAll(query);
            ddlTeam.DataTextField = "teamName";
            ddlTeam.DataValueField = "teamID";
            ddlTeam.Items.Insert(0, new ListItem("<Velg team>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddlTeam.DataBind();
        }

        private void GetParentProjects()
        {            
            string query = "SELECT projectName, projectID FROM Project WHERE parentProjectID = 0 AND projectState = 1";
            ddlSubProject.DataSource = db.getAll(query);
            ddlSubProject.DataTextField = "projectName";
            ddlSubProject.DataValueField = "projectID";
            ddlSubProject.Items.Insert(0, new ListItem("<Velg hovedprosjekt>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddlSubProject.DataBind();
        }

        private void GetTaskCategories()
        {
            string query = "SELECT * FROM TaskCategory";
            ddlTaskCategory.DataSource = db.getAll(query);
            ddlTaskCategory.DataTextField = "taskCategoryName";
            ddlTaskCategory.DataValueField = "taskCategoryID";
            ddlTaskCategory.Items.Insert(0, new ListItem("<Velg hovedtask>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddlTaskCategory.DataBind();
        }
        
        // event som oppdaterer endDate-boksen når startdato , antall faser og faselengde er fylt ut
        private void UpdateProjectEndDate()
        {
            if (tbSelectNumberOfPhases.Text != "" && tbSelectNumberOfDaysPerPhase.Text != "" && tbStartDate.Text != "")
            {
                startDate = Convert.ToDateTime(tbStartDate.Text);    
                endDate = startDate.AddDays(Convert.ToInt32(tbSelectNumberOfPhases.Text) * Convert.ToInt32(tbSelectNumberOfDaysPerPhase.Text) - 1);
                tbEndDate.Text = endDate.ToString("yyyy-MM-dd");

                ViewState["dateOK"] = true;
                //datesOK = true;
            }
        }

        private void PopulateTaskCategoryListBox(List<String> _list)
        {
            taskCategoryList.DataSource = _list;
            taskCategoryList.DataBind();
        }

        // legger til hovedtask
        // && (tbTaskCategoryPhase.Text != "" && Convert.ToInt32(tbTaskCategoryPhase.Text.ToString()) <= Convert.ToInt32(tbSelectNumberOfPhases.Text.ToString()))
        protected void btnAddTaskCategory_Click(object sender, EventArgs e)
        {
            if (ddlTaskCategory.SelectedIndex != 0)
            {                
//                taskCat.Add(Convert.ToInt32(ddlTaskCategory.SelectedValue));
                DataTable dt = new DataTable();
                string taskCategoryListQuery = string.Format("SELECT taskCategoryID, taskCategoryName, productBacklogID FROM TaskCategory WHERE taskCategoryID = {0}", ddlTaskCategory.SelectedValue);
                dt = db.getAll(taskCategoryListQuery);

//                if (taskCategoriIDs.Contains(dt.Rows[0]["taskCategoryID"].ToString()) != true)
//                {
                    taskCategories.Add(dt.Rows[0]["productBacklogID"] + " " + dt.Rows[0]["taskCategoryName"]);
                    PopulateTaskCategoryListBox(taskCategories);
                    taskCategoryIDs.Add(dt.Rows[0]["taskCategoryID"] + " ");
                    taskCategoriCounter += 1;
                    lbTaskCategoryError.Text = "";
/*                }
                else
                {
                    lbTaskCategoryError.ForeColor = Color.Red;
                    lbTaskCategoryError.Text = "Du har allerede lagt til denne hovedtasken";
                }
*/
            }
        }

        // lagrer prosjektet i db
        protected void btnCreateProject_Click(object sender, EventArgs e)
        {
            string query;
            
            if (ddlSubProject.SelectedIndex != 0)
            {
                query = string.Format("INSERT INTO Project (projectName, projectDescription, projectState, parentProjectID, teamID, latestProject)" +
                    " VALUES ('{0}', '{1}', {2}, {3}, {4}, {5})", tbProjectName.Text, tbDescription.Text, 0, ddlSubProject.SelectedValue, ddlTeam.SelectedValue, 1);
            }
            else
            {
                query = string.Format("INSERT INTO Project (projectName, projectDescription, projectState, teamID, latestProject) VALUES ('{0}', '{1}', {2}, {3}, {4})", tbProjectName.Text, tbDescription.Text, 0, ddlTeam.SelectedValue, 1);
            }
            
            if (tbProjectName.Text != "" && (bool)ViewState["dateOK"] == true && ddlTeam.SelectedIndex != 0)
            {
                DataTable dt = new DataTable();
                db.InsertDeleteUpdate(query);
                string latestProjectQuery = String.Format("SELECT projectID FROM Project WHERE latestProject = 1");
                dt = db.getAll(latestProjectQuery);
                projectID = Convert.ToInt32(dt.Rows[0]["projectID"]);

                string resetLatestProjectQuery = string.Format("UPDATE Project SET latestProject = 0  WHERE projectID = {0}", projectID);
                db.InsertDeleteUpdate(resetLatestProjectQuery);

                // kjører metodene nedenfor
                AddPhases();

                if (taskCategoriCounter > 0)
                {
                    AddTaskCategories();
                }

                lbError.ForeColor = Color.Black;
                lbError.Text = "Prosjekt lagt til, men er inaktivt. Du aktiverer det under 'Rediger Prosjekt'.";
            }
            else
            {
                lbError.ForeColor = Color.Red;
                lbError.Text = "Prosjektet må ha navn, startdato, antall faser og hvor lange fasene skal være. I tillegg må et team velges.";
            }
        }

        // legger til fasene i db
        private void AddPhases()
        {
            startDate = Convert.ToDateTime(tbStartDate.Text);
            DateTime phaseStartDate = new DateTime();
            DateTime phaseEndDate = new DateTime();

            for (int i = 0; i < Convert.ToInt32(tbSelectNumberOfPhases.Text); i++)
            {
                int fase = i;
                if (i == 0)
                {
                    phaseStartDate = startDate;
                    phaseEndDate = phaseStartDate.AddDays(Convert.ToInt32(tbSelectNumberOfDaysPerPhase.Text) - 1);
                }
                else
                {
                    phaseStartDate = startDate.AddDays(Convert.ToInt32(tbSelectNumberOfDaysPerPhase.Text) * (fase));
                    phaseEndDate = phaseStartDate.AddDays(Convert.ToInt32(tbSelectNumberOfDaysPerPhase.Text) - 1);
                }

                string phaseQuery = string.Format("INSERT INTO Fase (phaseName, phaseDescription, phaseFromDate, phaseToDate, projectID)" +
                    " VALUES ('{0}', '{1}', '{2}', '{3}', {4})", "Fase " + fase, "Beskrivelse for sprint " + fase, phaseStartDate.ToString("yyyy-MM-dd"), phaseEndDate.ToString("yyyy-MM-dd"), projectID);
                db.InsertDeleteUpdate(phaseQuery);
            }
        }

        // legger til hovedtasks i db ( FUNKER IKKE FORELØPIG, pga taskCategoriIDs[i]-lista )
        private void AddTaskCategories()
        {
            
            for (int i = 0; i < taskCategoriCounter; i++)
            {
                string taskCategoriesQuery = string.Format("UPDATE TaskCategory SET projectID = {0}" +
                    " WHERE taskCategoryID = {1}", projectID, taskCategoryIDs[i]);

                db.InsertDeleteUpdate(taskCategoriesQuery);
            }
        }

        protected void tbSelectNumberOfPhases_TextChanged(object sender, EventArgs e)
        {
            UpdateProjectEndDate();
        }

        protected void tbSelectNumberOfDaysPerPhase_TextChanged(object sender, EventArgs e)
        {
            UpdateProjectEndDate();
        }

        protected void tbStartDate_TextChanged(object sender, EventArgs e)
        {
            UpdateProjectEndDate();
        }

    }
}
