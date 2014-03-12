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
            string query = "SELECT userID, CONCAT (firstname, ' ',  surname) AS FullName FROM User";
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
            string query = "UPDATE User SET groupID = 1 AND teamID=" + Convert.ToInt16(ViewState["teamID"]) + " WHERE userID=" + Convert.ToInt16(ViewState["userID"]);
            db.InsertDeleteUpdate(query);
        }

        protected void btn_addTeamleader_Click(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(ddl_users.SelectedValue);
            string query = "UPDATE User SET groupID = 2 AND teamID=" + teamID + " WHERE userID=" + userID;
            db.InsertDeleteUpdate(query);
            ViewState["userID"] = userID;
        }

        protected void btn_selectTeam_Click(object sender, EventArgs e)
        {
            teamID = Convert.ToInt32(ddl_selectTeam.SelectedValue);
            fillGridView();
            ViewState["teamID"] = teamID;
        }
    }
}