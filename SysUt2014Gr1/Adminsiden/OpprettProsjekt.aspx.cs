using Adminsiden;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class OpprettProsjekt : System.Web.UI.Page
    {
        private DBConnect db;

        //private int textAreaCounter = 300;
        private int webClientTeamID;

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
                db = new DBConnect();
                calendarDateTo.StartDate = DateTime.Now;
                if (!IsPostBack)
                {
                    tb_dateFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    tb_dateTo.Text = DateTime.Today.ToString("dd-MM-yyyy");
                    getTeams(); //Binder teams fra databasen til DropDownList ddl_Team
                    getMainProjects(); //Binder hovedprosjekt til DropDownList ddl_Hovedprosjekt
                }
                else
                {
                    if (ViewState["teamID"] != null)
                        webClientTeamID = (int)ViewState["teamID"];
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
           
        }

        private void getTeams()
        {
            string query = "SELECT * FROM Team";
            ddl_Team.DataSource = db.getAll(query);
            ddl_Team.DataTextField = "teamName";
            ddl_Team.DataValueField = "teamID";
            ddl_Team.Items.Insert(0, new ListItem("<Velg team>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddl_Team.DataBind();
        }

        private void getMainProjects()
        {
            //M� nok endre litt p� sp�rring etter hvert som vi f�r kontroll p� projectState
            string query = "SELECT projectName, projectID FROM Project WHERE parentProjectID = 0";
            ddl_Hovedprosjekt.DataSource = db.getAll(query);
            ddl_Hovedprosjekt.DataTextField = "projectName";
            ddl_Hovedprosjekt.DataValueField = "projectID";
            //F�rste valg i DropDownList, hvor teamID er satt til 0, kj�rer en sjekk i ModalPopup_ShowTeam-event
            ddl_Hovedprosjekt.Items.Insert(0, new ListItem("<Velg Hovedprosjekt>", "0"));
            ddl_Hovedprosjekt.DataBind();
        }

        #region Events
        protected void ModalPopup_ShowTeam(object sender, EventArgs e)
        {
            //Hvis bruker har glemt � velge team fra dropdownlist
            if (webClientTeamID == 0)
            {
                lbl_warning.Visible = true;
            }
            else
            {   //Setter Popupheader lik navnet p� teamet som er valgt i dropdownlist
                string labelQuery = "SELECT teamName FROM Team WHERE teamID =" + webClientTeamID;
                DataTable dt = new DataTable();
                dt = db.getAll(labelQuery);
                string headerText = Convert.ToString(dt.Rows[0]["teamName"]);
                lbl_teamPopupHeader.Text = headerText;

                //Binder s� teammedlemmene til et gridview i popupmodulen
                string query = "SELECT firstname, surname, groupName FROM User, UserGroup WHERE UserGroup.groupID = User.groupID AND teamID =" + webClientTeamID;
                gv_selectedTeam.DataSource = db.getAll(query);
                gv_selectedTeam.DataBind();
                ModalPopupExtender_Team.Show();
            }
        }

        protected void ddl_Team_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_warning.Visible = false;
            webClientTeamID = Convert.ToInt32(ddl_Team.SelectedValue);
            ViewState["teamID"] = webClientTeamID;
        }

        #endregion

        protected void btn_AddMainTask_Click(object sender, EventArgs e)
        {

        }

        protected void btn_CreateMainTask_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            string query = String.Format("INSERT INTO Project (projectName, projectDescription, projectState, parentProjectID, teamID) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                        tb_projectName.Text, "Beskrivelse", 1, 0, 16); //team og beskrivelse hardkodet
                 
                
            db.InsertDeleteUpdate(query);

            lblMessageOK.ForeColor = Color.Green;
            lblMessageOK.Text = "Prosjekt endret, OK!";
        }
    }
}