using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adminsiden;
using System.Data;

namespace Adminsiden
{
    public partial class ProsjektAnsvarligVisTeam : System.Web.UI.Page
    {
        private DBConnect db;
        private int projectID;
        private DataTable table;
        private int userID;
        private string userType = "Teamleder";

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
                table = new DataTable();

                if (!Page.IsPostBack)
                {
                    GetTeam();
                    PopulateTeams();
                }

            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
           
        }

        //skal fjerne et medlem fra team/prosjekt
        protected void GridViewTeam_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        //henter ut all teammedlemmer som tilhører et gitt prosjekt
        private void GetTeam()
        {
            projectID = Convert.ToInt16(Session["projectID"]);

            string query = String.Format("SELECT userID, firstname, surname, groupName, teamName FROM User, Team, Project, UserGroup WHERE User.groupID = UserGroup.groupID AND User.teamID = Team.teamID AND Project.teamID = Team.teamID AND projectID = {0} ORDER BY groupName DESC", projectID);
            table = db.AdminGetAllUsers(query);

            GridViewTeam.DataSource = table;
            GridViewTeam.DataBind();

            if (GridViewTeam.Rows[0].Cells[4].Text != null)
            {
                teamNavn.Text = GridViewTeam.Rows[0].Cells[4].Text;
                getProjects();
            }
            else
                teamNavn.Text = "Ingen team på dette prosjektet";
        }

        //Henter ut all prosjekter som tilhører et gitt teamnavn
        private void getProjects()
        {
            string query = String.Format("SELECT projectID, projectName, projectDescription FROM Project WHERE Project.teamID = (SELECT Team.teamID FROM Team WHERE Team.teamName LIKE '{0}')", teamNavn.Text);
            table = db.AdminGetAllUsers(query);

            GridViewProject.DataSource = table;
            GridViewProject.DataBind();
        }

        private void DropDownTeamFill()
        {
            
        }

       
        // populater ddlTeams med team innlogget bruker er teamleder av
        public void PopulateTeams()
        {
//            userID = Convert.ToInt16(Session["userID"]);
            string query = String.Format("SELECT * FROM Team");
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
        protected void btnOK_Click(object sender, EventArgs e)
        {
            String query = String.Format("SELECT userID FROM User WHERE groupID = 2 AND teamID = {0}", ddlTeam.SelectedValue);
            DataTable dt = new DataTable();
            dt = db.getAll(query);
            userID = Convert.ToInt32(dt.Rows[0]["userID"]);
            
            query = String.Format("UPDATE User SET groupID = 1 WHERE userID = {0}", userID);
            db.InsertDeleteUpdate(query);

            query = String.Format("UPDATE User SET groupID = 2 WHERE userID = {0}", ddlBruker.SelectedValue);
            db.InsertDeleteUpdate(query);

            lbTeamlederTransferred.Text = "Teamlederstatus overført";                       
        }
    }
}