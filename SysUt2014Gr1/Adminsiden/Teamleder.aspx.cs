using Adminsiden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace Adminsiden
{
    public partial class Teamleder : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private int active = 0; //Denne må sittes under login, noe som ikke er skrevet enda. Hardkodet inntil videre.
        private int TaskID;
        private int WorkplaceID;
        private int userID = 19; // denne refererer til hvilken bruker som er logget inn (er hardkodet til 19, Ari for øyeblikket)
        private string userType = "Teamleder";

        List<String> projectList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetTasks();
                getWorkplace();
                fillTimeSelectDDL();
                PopulateTeams();                
            }
        }
        //Fyller dropdownlister med timer og minutter
        private void fillTimeSelectDDL()
        {
            if (ddl_hour_from.Items.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    ddl_hour_from.Items.Add("0" + i);
                    ddl_hour_to.Items.Add("0" + i);
                }
                for (int i = 10; i < 24; i++)
                {
                    ddl_hour_from.Items.Add("" + i);
                    ddl_hour_to.Items.Add("" + i);
                }
            }
            if (ddl_min_from.Items.Count == 0)
            {
                for (int i = 0; i < 10; i += 15)
                {
                    ddl_min_from.Items.Add("0" + i);
                    ddl_min_to.Items.Add("0" + i);
                }
                for (int i = 15; i < 60; i += 15)
                {
                    ddl_min_from.Items.Add("" + i);
                    ddl_min_to.Items.Add("" + i);
                }
            }
        }
        //Fyller dropdown med tasks
        private void GetTasks()
        {
            string query = "SELECT * FROM Task";
            taskName.DataSource = db.getAll(query);
            taskName.DataValueField = "taskID";
            taskName.DataTextField = "taskName";
            taskName.Items.Insert(0, new ListItem("<Velg task>", "0"));
            taskName.DataBind();
        }
        //Fyller dropdown med plasser å jobbe fra
        private void getWorkplace()
        {
            string query = "SELECT * FROM Workplace";
            workPlace.DataSource = db.getAll(query);
            workPlace.DataValueField = "workplaceID";
            workPlace.DataTextField = "workplace";
            workPlace.Items.Insert(0, new ListItem("<Velg arbeidsplass>", "0"));
            workPlace.DataBind();
        }

        protected void taskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskID = Convert.ToInt32(taskName.SelectedValue);
        }
        protected void workPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            WorkplaceID = Convert.ToInt32(workPlace.SelectedValue);
        }
        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string userDescription = TxtArea_userComment.Text;

            int tidFra_min = Convert.ToInt32(ddl_min_from.SelectedItem.ToString());
            int tidFra_hour = Convert.ToInt32(ddl_hour_from.SelectedItem.ToString());
            DateTime dateFrom = Convert.ToDateTime(TB_Date.Text);
            TimeSpan timespanFrom = new TimeSpan(tidFra_hour, tidFra_min, 0);
            dateFrom = dateFrom.Add(timespanFrom);

            string dateFromFormated = dateFrom.ToString("yyyy-MM-dd HH:mm:ss");

            int tidTil_min = Convert.ToInt32(ddl_min_to.SelectedItem.ToString());
            int tidTil_hour = Convert.ToInt32(ddl_hour_to.SelectedItem.ToString());
            DateTime dateTo = Convert.ToDateTime(TB_Date.Text);
            TimeSpan timespanTo = new TimeSpan(tidTil_hour, tidTil_min, 0);
            dateTo = dateTo.Add(timespanTo);

            string dateToFormated = dateTo.ToString("yyyy-MM-dd HH:mm:ss");

            int projectID = Convert.ToInt32(Session["projectID"]);
            int userID = Convert.ToInt32(Session["userID"]);

            if (dateFromFormated != null && dateToFormated != null && userID != 0 && TaskID != 0 && WorkplaceID != 0 && active != 0 && projectID != 0)
            {
                db.InsertTimeSheet(dateFromFormated, dateToFormated, userID, TaskID, userDescription, WorkplaceID, active, projectID);
                label_result.Text = "Registreringen ble fullført";
                label_result.Visible = true;
            }
            else
            {
                label_result.Text = "Noe gikk gale, vennligst prøv igjen";
                label_result.Visible = true;
            }
        }

        // populater ddlTeams med team innlogget bruker er teamleder av
        public void PopulateTeams()
        {
            string query = String.Format("SELECT * FROM Team WHERE teamID IN (SELECT teamID from User WHERE groupID IN (SELECT groupID from UserGroup WHERE groupName = \"{1}\") AND userID = {0})", userID, userType);
            ddlTeam.DataSource = db.getAll(query);
            ddlTeam.DataTextField = "teamName";
            ddlTeam.DataValueField = "teamID";
            ddlTeam.Items.Insert(0, new ListItem("<Velg team>", "0"));
            ddlTeam.DataBind();
        }

        // populater ddlBruker med brukere som tilhører valgt team etter at et er valgt fra ddlTeam
        public void PopulateTeamMembers()
        {
            string query = String.Format("SELECT *, CONCAT(firstname, \" \", surname) as name FROM User WHERE teamID = {0}", ddlTeam.SelectedValue);
            ddlBruker.DataSource = db.getAll(query);
            ddlBruker.DataTextField = "name";
            ddlBruker.DataValueField = "userID";
            ddlBruker.Items.Insert(0, new ListItem("<Velg bruker>", "0"));
            ddlBruker.DataBind();
        }

        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateTeamMembers();
        }

        // overfører teamlederstatus
        protected void Button1_Click(object sender, EventArgs e)
        {
            String query = String.Format("UPDATE User SET groupID = 1 WHERE userID = {0}", userID);
            db.InsertDeleteUpdate(query);

            query = String.Format("UPDATE User SET groupID = 2 WHERE userID = {0}", ddlBruker.SelectedValue);
            db.InsertDeleteUpdate(query);

            lbTeamlederTransferred.Text = "Teamlederstatus overført";

            // kode for å tvangsutlogge bruker her
        }
    }
}