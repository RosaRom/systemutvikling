using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class PAGodkjennEkstraTid : System.Web.UI.Page
    {
        private int projectID;
        
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

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
                    Populate();
                }
            else
                {
                    Server.Transfer("Login.aspx", true);
                } 
            }

        public void Populate()
        {
            projectID = Convert.ToInt16(Session["projectID"]);

            string query = String.Format("SELECT productBacklogID \"BacklogID\", taskName \"Tasknavn\", priority \"Prioritet\", description \"Beskrivelse\"," +
                " hoursUsed \"Brukte timer\", hoursAllocated \"Allokerte timer\", hoursExtra \"Ekstra timer\", taskID, hoursAllocated, hoursExtra" + 
                " FROM Task WHERE hoursExtra != 0 AND phaseID IN (SELECT phaseID FROM Fase WHERE projectID = {0})", projectID);
            
            dt = db.getAll(query);
            ViewState["table"] = dt;

            gvTaskList.DataSource = dt;
            gvTaskList.DataBind();
                                           
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int prioritet = Convert.ToInt32(dt.Rows[i]["Prioritet"]);

                switch (prioritet)
                {
                    case 1:
                        gvTaskList.Rows[i].Cells[2].Text = "Høy";
                        break;
                    case 2:
                        gvTaskList.Rows[i].Cells[2].Text = "Mid";
                        break;
                    case 3:
                        gvTaskList.Rows[i].Cells[2].Text = "Lav";
                        break;
                }
            }
        }

        protected void gvTaskList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            
            // kjører om en "Godkjenn"-knapp blir trykket på
            if (e.CommandName == "godkjenn")
            {
                int taskID = Convert.ToInt32(dt.Rows[index]["taskID"].ToString());
                int hoursAllocated = Convert.ToInt32(dt.Rows[index]["hoursAllocated"].ToString());                
                int hoursExtra = Convert.ToInt32(dt.Rows[index]["hoursExtra"].ToString());
                int newHoursAllocated = hoursAllocated + hoursExtra;
                string query = String.Format("UPDATE Task SET hoursAllocated = {0}, hoursExtra = 0  WHERE taskID = {1}", newHoursAllocated, taskID);
                db.InsertDeleteUpdate(query);
                Populate();
            }

            // kjører om en "Ikke godkjenn"-knapp blir trykket på
            if (e.CommandName == "ikkegodkjenn")
            {
                int taskID = Convert.ToInt32(dt.Rows[index]["taskID"].ToString());
                string query = String.Format("UPDATE Task SET hoursExtra = 0 WHERE taskID = {0}", taskID);
                db.InsertDeleteUpdate(query);
                Populate();
 
                // send report til TL om at ønskede timer ikke ble godkjent
            }            
        }
    }
}