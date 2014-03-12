using Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class OpprettTeam : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private int teamID;
        private int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getUsers();
                getTeams();
                fillGridView();
            }
            else
            {
                if (ViewState["teamID"] != null)
                teamID = (int)ViewState["teamID"];
            }
        }
        private void getTeams()
        {
            string query = "SELECT * FROM Team";
            ddl_selectTeam.DataSource = db.getAll(query);
            ddl_selectTeam.DataTextField = "teamName";
            ddl_selectTeam.DataValueField = "teamID";
            ddl_selectTeam.Items.Insert(0, new ListItem("<Velg team>", "0"));
            ddl_selectTeam.DataBind();
        }
        private void getUsers()
        {
            string query = "SELECT userID, CONCAT (firstname, ' ',  surname) AS FullName FROM User WHERE teamID NOT LIKE " + teamID;
            ddl_users.DataSource = db.getAll(query);
            ddl_users.DataTextField = "FullName";
            ddl_users.DataValueField = "userID";
            ddl_users.Items.Insert(0, new ListItem("<Velg bruker>", "0"));
            ddl_users.DataBind(); 
        }
        private void fillGridView()
        {
            string query = "SELECT firstname, surname, groupName FROM User, UserGroup WHERE UserGroup.groupID = User.groupID AND teamID =" + teamID;
            GridView1.DataSource = db.getAll(query);
            GridView1.DataBind();
        }

        protected void btn_addUser_Click(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(ddl_users.SelectedValue);
            string query = "UPDATE User SET groupID = 1, teamID=" + teamID + " WHERE userID=" + userID;
            db.InsertDeleteUpdate(query);
            fillGridView();

            ddl_users.Items.Clear();
            getUsers();
        }

        protected void btn_addTeamleader_Click(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(ddl_users.SelectedValue);
            string query = "UPDATE User SET groupID = 2, teamID=" + teamID + " WHERE userID=" + userID;
            db.InsertDeleteUpdate(query);
            fillGridView();

            ddl_users.Items.Clear();
            getUsers();
        }

        protected void btn_selectTeam_Click(object sender, EventArgs e)
        {
            teamID = Convert.ToInt32(ddl_selectTeam.SelectedValue);
            fillGridView();
            ViewState["teamID"] = teamID;

            ddl_users.Items.Clear();
            getUsers();
        }

        protected void btn_opprett_Click(object sender, EventArgs e)
        {
            btn_abort.Visible = true;
            tb_newTeam.Visible = true;
            btn_createTeam.Visible = true;
            btn_opprett.Visible = false;
        }

        protected void btn_createTeam_Click(object sender, EventArgs e)
        {
            string newTeam = tb_newTeam.Text;
            string query = "INSERT INTO Team (teamName) VALUES ('" + newTeam + "')";
            db.InsertDeleteUpdate(query);

            ddl_selectTeam.Items.Clear();
            getTeams();

            tb_newTeam.Text = "";
            btn_abort.Visible = false;
            tb_newTeam.Visible = false;
            btn_createTeam.Visible = false;
            btn_opprett.Visible = true;

        }

        protected void btn_abort_Click(object sender, EventArgs e)
        {
            tb_newTeam.Text = "";
            btn_abort.Visible = false;
            tb_newTeam.Visible = false;
            btn_createTeam.Visible = false;
            btn_opprett.Visible = true;
        }
    }
}