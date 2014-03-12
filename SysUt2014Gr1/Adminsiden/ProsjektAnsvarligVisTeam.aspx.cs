using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin;
using System.Data;

namespace Adminsiden
{
    public partial class ProsjektAnsvarligVisTeam : System.Web.UI.Page
    {
        DBConnect db;
        int prosjektId = 2;                         //skal egentlig hentes fra valgt prosjekt på forrige side
        DataTable table;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();
            table = new DataTable();

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
            string query = String.Format("SELECT userID, firstname, surname, groupName, teamName FROM User, Team, Project, UserGroup WHERE User.groupID = UserGroup.groupID AND User.teamID = Team.teamID AND Project.teamID = Team.teamID AND projectID = {0} ORDER BY groupName DESC", prosjektId);
            table = db.AdminGetAllUsers(query);

            GridViewTeam.DataSource = table;
            GridViewTeam.DataBind();

            if (GridViewTeam.Rows[0].Cells[4].Text != null)
            {
                teamNavn.Text = GridViewTeam.Rows[0].Cells[4].Text;
            }
            else
                teamNavn.Text = "Ingen team på dette prosjektet";
        }
    }
}