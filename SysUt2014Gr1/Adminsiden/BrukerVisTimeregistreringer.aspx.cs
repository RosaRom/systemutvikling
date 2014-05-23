using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

///
/// BrukerVisTimeregistreringer.aspx.cs av Henning Fredriksen
/// SysUt14Gr1 - Systemutvikling - Vår 2014
///
/// Lar en bruker se sine egne timerregistreringer, og kan deaktivere dem om han ønsker.
/// Bruker kan også bytte mellom å se aktive/deaktiverte timeregistreringer.
/// 

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

        /// <summary>
        /// sjekker om det er en bruker som er logget inn, hvis ja populater den formen, hvis nei redirectes bruker til login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        
        /// <summary>
        /// fyller lista av timeregisteringer for bruker, showActive bool bestemmer om aktive eller inaktive timeregistreringer skal vises
        /// </summary>
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



            // bytter ut state som er en int i db med en beskrivelse (inaktiv/aktiv)            
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

            // bytter ut taskID i kolonne 0 med BacklogID (istedet for å gjøre dette direkte i query tidligere)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string backlogQuery = string.Format("SELECT productBacklogID FROM Task WHERE taskID = {0}", dt.Rows[i]["taskID"].ToString());
                dtBacklog = db.getAll(backlogQuery);
                
                gvTaskList.Rows[i].Cells[0].Text = dtBacklog.Rows[0]["productBacklogID"].ToString();
            }

            // bytter ut taskID i kolonne 1 med Tasknavn (istedet for å gjøre dette direkte i query tidligere)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string taskNameQuery = string.Format("SELECT taskName FROM Task WHERE taskID = {0}", dt.Rows[i]["taskID"].ToString());
                dtTaskName = db.getAll(taskNameQuery);

                gvTaskList.Rows[i].Cells[1].Text = dtTaskName.Rows[0]["taskName"].ToString();
            }
        }
        

        /// <summary>
        /// event som registerer om en deaktiver-knapp blir trykket på og setter state til 0 i db for den timereg.,
        /// noe som indikerer at den er inaktiv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTaskList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string updateQuery = "";

            // kjører om en "deaktiver"-knapp blir trykket på
            if (e.CommandName == "deaktiver" && showActive == true)
            {                
                int timeID = Convert.ToInt32(dt.Rows[index]["timeID"].ToString());                
                updateQuery = String.Format("UPDATE TimeSheet SET state = 0 WHERE timeID = {0}", timeID);              

                db.InsertDeleteUpdate(updateQuery);
                Populate();
            }
        }

        /// <summary>
        /// Knapp som bytter showActive til true (vise aktive timereg.) og gjør knapperekka med deaktiver-knapper synlig
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btShowActiveRegistrations_Click(object sender, EventArgs e)
        {
            showActive = true;
            lbWhatIsShowing.Text = "Aktive timeregistreringer";
            gvTaskList.Columns[6].Visible = true;
            Populate();
        }

        /// <summary>
        /// Knapp som bytter showActive til false (vise inaktive timereg.) og skjuler knapperekka med deaktiver-knapper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btShowInactiveRegistrations_Click(object sender, EventArgs e)
        {
            showActive = false;
            lbWhatIsShowing.Text = "Inaktive timeregistreringer";
            gvTaskList.Columns[6].Visible = false;
            Populate();
        }
    }
}