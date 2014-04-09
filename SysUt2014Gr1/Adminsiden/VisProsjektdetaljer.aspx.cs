using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class VisTeam : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();
        DataTable dt_users = new DataTable();
        DataTable dt_tasks = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //int projectID = Convert.ToInt16(Session["projectID"]);
            int projectID = 2;

            string query = "SELECT * FROM Project, Team WHERE Project.projectID = " + projectID + " AND Team.teamID = Project.teamID";
            dt = db.getAll(query);
            int teamID = Convert.ToInt16(dt.Rows[0]["teamID"]);

            string query1 = "SELECT taskID, taskName, hoursUsed, HoursAllocated FROM Task WHERE TaskCategoryID IN (SELECT TaskCategoryID FROM TaskCategory WHERE ProjectID = " + projectID + ")";
            dt_tasks = db.getAll(query1);

            string query2 = "SELECT  CONCAT (firstname, ' ',  surname) AS FullName FROM User WHERE teamID =" + teamID;
            dt_users = db.getAll(query2);

            if (dt != null && dt.Rows.Count > 0)
            {
                string projectname = Convert.ToString(dt.Rows[0]["projectName"]);
                string description = Convert.ToString(dt.Rows[0]["ProjectDescription"]);
                string teamName = Convert.ToString(dt.Rows[0]["teamName"]);

                Label_navn.Text = projectname;
                tb_desc.Text = description;
                Label_team.Text = teamName;

                ListView_team.DataSource = dt_users;
                ListView_team.DataBind();

                Listview_task.DataSource = dt_tasks;
                Listview_task.DataBind();
            }
            else
            {
                Label_warning.Text = "Noe har gått gale, vennligst velg prosjekt igjen.";
            }
        }
    }
}