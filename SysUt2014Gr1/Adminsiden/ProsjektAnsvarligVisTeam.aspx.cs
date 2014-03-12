using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin;

namespace Adminsiden
{
    public partial class ProsjektAnsvarligVisTeam : System.Web.UI.Page
    {
        DBConnect db;
        int prosjektId = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();

            if (!Page.IsPostBack)
            {
                GetTeam();
            }
        }

        protected void GridViewTeam_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void GetTeam()
        {
            string query = "SELECT userID, firstname, surname, groupName FROM User, UserGroup, Team, Project WHERE User.groupID = UserGroup.groupID AND User.teamID = Team.teamID AND Team.teamID = Project.teamID AND ";
            
            GridViewTeam.DataSource = db.AdminGetAllUsers(query);
            GridViewTeam.DataBind();
        }
    }
}