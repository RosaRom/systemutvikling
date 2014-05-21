using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    /// <summary>
    /// PickTask.aspx.cs av Tord-Marius Fredriksen
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Brukeren velger task for å endre den.
    /// </summary>
    public partial class PickTask : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();
        DataTable dtTest = new DataTable();

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
                Populate();
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }
        }

        public void Populate()
        {
            int projectID = Convert.ToInt16(Session["projectID"]);

            string query = String.Format("SELECT taskID, taskCategoryID, taskName, description FROM Task WHERE phaseID IN (SELECT phaseID FROM Fase WHERE projectID IN (SELECT projectID from Project WHERE projectID = {0}))", projectID);

                dt = db.getAll(query);
                ViewState["table"] = dt;

                gvTaskList.DataSource = dt;
                gvTaskList.DataBind();
        }

        protected void gvTaskList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            //kjører når "Endre" blir trykket for en task
            if (e.CommandName == "endre")
            {
                int taskID = Convert.ToInt32(dt.Rows[index]["taskID"].ToString());

                Session["taskID"] = taskID;
                Server.Transfer("EditTask.aspx", true);
              
            }
        }
    }
}