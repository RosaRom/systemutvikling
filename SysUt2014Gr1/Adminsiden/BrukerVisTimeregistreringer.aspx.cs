using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class BrukerVisTimeregistreringer : System.Web.UI.Page
    {
        private int userID;
        private string session;

        private bool showActive = true;
        string query = "";        

        private DBConnect db = new DBConnect();
        private DataTable dt = new DataTable();
        private DataTable dtBacklog = new DataTable();
        private DataTable dtTaskName = new DataTable();

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
            session = (string)Session["userLoggedIn"];

            if (session == "teamMember")
            {
                userID = Convert.ToInt16(Session["userID"]);
                lbWhatIsShowing.Text = "Aktive timeregistreringer";
                Populate();
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }

        }
        
        public void Populate()
        {
            // bestemmer om aktive eller inaktive timereg. skal hentes ut
            if (showActive)
            {
                query = String.Format("SELECT * FROM TimeSheet WHERE userID = {0} AND state = 1 ORDER BY start DESC", userID);
            }
            else
            {
                query = String.Format("SELECT * FROM TimeSheet WHERE userID = {0} AND state = 0 ORDER BY start DESC", userID);
            }

            dt = db.getAll(query);
            ViewState["table"] = dt;

            gvTaskList.DataSource = dt;
            gvTaskList.DataBind();

                        
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int prioritet = Convert.ToInt32(dt.Rows[i]["state"]);

                switch (prioritet)
                {
                    case 0:
                        gvTaskList.Rows[i].Cells[5].Text = "Inaktiv";                        
                        break;
                    case 1:
                        gvTaskList.Rows[i].Cells[5].Text = "Aktiv";
                        break;                    
                }
            }            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string backlogQuery = string.Format("SELECT productBacklogID FROM Task WHERE taskID = {0}", dt.Rows[i]["taskID"].ToString());
                dtBacklog = db.getAll(backlogQuery);
                
                gvTaskList.Rows[i].Cells[0].Text = dtBacklog.Rows[0]["productBacklogID"].ToString();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string taskNameQuery = string.Format("SELECT taskName FROM Task WHERE taskID = {0}", dt.Rows[i]["taskID"].ToString());
                dtTaskName = db.getAll(taskNameQuery);

                gvTaskList.Rows[i].Cells[1].Text = dtTaskName.Rows[0]["taskName"].ToString();
            }
        }

        protected void gvTaskList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string updateQuery = "";

            // kjører om en "deaktiver"-knapp blir trykket på
            if (e.CommandName == "deaktiver" && showActive)
            {                
                int timeID = Convert.ToInt32(dt.Rows[index]["timeID"].ToString());                
                updateQuery = String.Format("UPDATE TimeSheet SET state = 0, permissionState = 1 WHERE timeID = {0}", timeID);
/*                
                if (Convert.ToInt32(dt.Rows[index]["state"]) == 0)
                {
                    updateQuery = String.Format("UPDATE Task SET state = 0 WHERE timeID = {0}", timeID);
                }
                else
                {
                    updateQuery = String.Format("UPDATE Task SET state = 1 WHERE timeID = {0}", timeID);
                }
*/
                db.InsertDeleteUpdate(updateQuery);
                Populate();
            }
        }

        protected void btShowActiveRegistrations_Click(object sender, EventArgs e)
        {
            showActive = true;
            lbWhatIsShowing.Text = "Aktive timeregistreringer";
            Populate();
        }

        protected void btShowInactiveRegistrations_Click(object sender, EventArgs e)
        {
            showActive = false;
            lbWhatIsShowing.Text = "Inaktive timeregistreringer";
            Populate();
        }
    }
}