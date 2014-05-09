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
        private int TaskID;
        private int WorkplaceID;
        private string userType = "Teamleder";

        private int projectID;
        private int userID;

        List<String> projectList = new List<string>();

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


            if (session == "teamLeader")
            {
                if (Page.IsPostBack && ddl_hour_from.SelectedValue != "00")
                {
                    fillTimeToSelectDLL();
                }
                if (!Page.IsPostBack)
                {
                    GetTasks();
                    getWorkplace();
                    fillTimeSelectDDL();
                    PopulateTeams();
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
            
        }
        //Fyller dropdownlister med timer og minutter
        private void fillTimeSelectDDL()
        {
            //Fyller timer og minutter i "fra" dropdowns
            if (ddl_hour_from.Items.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    ddl_hour_from.Items.Add("0" + i);
                }
                for (int i = 10; i < 24; i++)
                {
                    ddl_hour_from.Items.Add("" + i);
                }
            }
            if (ddl_min_from.Items.Count == 0)
            {
                for (int i = 0; i < 10; i += 15)
                {
                    ddl_min_from.Items.Add("0" + i);
                }
                for (int i = 15; i < 60; i += 15)
                {
                    ddl_min_from.Items.Add("" + i);
                }
            }
        }

        private void fillTimeToSelectDLL()
        {
            //Denne metoden blir kjørt først når både starttime og minutter er valgt

            int from = 1;
            int hourTo = 0;
            int minTo = 0;

            //tar vare på registrerte til-klokkeslett
            if (ddl_hour_to.Items.Count != 0)
            {
                from = Convert.ToInt16(ddl_hour_from.SelectedValue);
                hourTo = Convert.ToInt16(ddl_hour_to.SelectedValue);
                minTo = Convert.ToInt16(ddl_min_to.SelectedValue);

                ddl_min_to.Items.Clear();
                ddl_hour_to.Items.Clear();
            }

            int hourFrom = Convert.ToInt16(ddl_hour_from.Text);

            //Fyller timer dropdown
            int i;

            for (i = hourFrom; i < 10; i++)
            {
                ddl_hour_to.Items.Add("0" + i);
            }
            for (int k = i; k >= 9 && k < 24; k++)
            {
                ddl_hour_to.Items.Add("" + k);
            }

            //Fyller minutter dropdown
            for (int j = 0; j < 10; j += 15)
            {
                ddl_min_to.Items.Add("0" + j);
            }
            for (int j = 15; j < 60; j += 15)
            {
                ddl_min_to.Items.Add("" + j);
            }

            /*bestemmer om registeret til-tid kan brukes, 
             eller om dette blir ugyldig med den nye ny fra-tiden*/
            if (from >= hourTo)
            {
                ddl_hour_to.SelectedValue = ddl_hour_from.SelectedValue;
                ddl_min_to.SelectedValue = ddl_min_from.SelectedValue;
            }
            else
            {
                if (hourTo < 10)
                    ddl_hour_to.SelectedValue = Convert.ToString("0" + hourTo);
                else
                    ddl_hour_to.SelectedValue = Convert.ToString(hourTo);

                if (minTo < 10)
                    ddl_min_to.SelectedValue = Convert.ToString("0" + minTo);
                else
                    ddl_min_to.SelectedValue = Convert.ToString(minTo);
            }
        }
        //Fyller dropdown med tasks
        private void GetTasks()
        {
            DataTable dt = new DataTable();
            int phaseID = 0;
            int projectID = Convert.ToInt16(Session["projectID"]);
            string query1 = "SELECT * FROM Fase WHERE projectID =" + projectID;
            dt = db.getAll(query1);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dt.Rows[i][3]) < DateTime.Now && Convert.ToDateTime(dt.Rows[i][4]) > DateTime.Now)
                {
                    phaseID = Convert.ToInt16(dt.Rows[i][0]);
                }
            }

            string query = "SELECT * FROM Task WHERE phaseID =" + phaseID;
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

            projectID = Convert.ToInt32(Session["projectID"]);
            userID = Convert.ToInt32(Session["userID"]);
            int state = 1;

            if (dateFromFormated != null && dateToFormated != null && userID != 0 && TaskID != 0 && WorkplaceID != 0 && projectID != 0)
            {
                int permissionState;

                if (dateFrom > DateTime.Now.AddDays(1) || dateFrom < DateTime.Now.AddDays(-1))
                    permissionState = 1;

                else
                    permissionState = 2;

                db.InsertTimeSheet(dateFromFormated, dateToFormated, userID, TaskID, userDescription, WorkplaceID, state, projectID, permissionState);
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
        protected void Button2_Click(object sender, EventArgs e)
        {

            Server.Transfer("TL_godkjenning_av_timeregistreringer.aspx", true);
        }
    }
}