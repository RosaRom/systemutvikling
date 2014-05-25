using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// TL_godkjenning_av_timeregistreringer.aspx.cs av Renate Karlsen
/// SysUt14Gr1 - Systemutvikling - Vår 2014
/// *************************************************************************************************************************
/// Denne klassen sjekker om noen brukere har permissionState 1. Dersom dette er tilfelle må denne godkjenning registreres
/// av Teamleder. Klassen henter ut diverse informasjon som blir lagt til i en gridView. Teamleder velger da om dette skal
/// godkjennes eller ikke. Dersom Teamleder godkjenner skifter permissionState til 0 og registreringen er godkjent
/// Dersom teamleder ikke godkjenner registreringen skifter permissionState til 2 og registreringen ble ikke godkjent.
/// *************************************************************************************************************************

namespace Adminsiden
{
    public partial class TL_godkjenning_av_timeregistreringer : System.Web.UI.Page
    {

        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        // Session
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
            FillGridView(); 
        }
        /// <summary>
        ///  GridView fylles opp med start/slutt dato, brukernavn, oppgave navn, arbeidsplass og beskrivelse
        /// </summary>

        public void FillGridView()
        {
            int projectID = Convert.ToInt32(Session["projectID"]);

            string query = String.Format("SELECT timeID, start, stop, username, taskName, workplace, Task.description, priority FROM User, TimeSheet, Task, Workplace WHERE User.userID = TimeSheet.userID AND TimeSheet.projectID = " + projectID + " AND TimeSheet.taskID = Task.taskID AND TimeSheet.workplaceID = Workplace.workplaceID AND TimeSheet.permissionState = 1");
            
            dt = db.getAll(query);
            ViewState["table"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            // Bytter prioriteten som er en int i DB med beskrivelsene: Høy, Mid, Lav
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int prioritet = Convert.ToInt32(dt.Rows[i]["priority"]);

                switch (prioritet)
                {
                    case 1:
                        GridView1.Rows[i].Cells[6].Text = "Høy";
                        break;
                    case 2:
                        GridView1.Rows[i].Cells[6].Text = "Mid";
                        break;
                    case 3:
                        GridView1.Rows[i].Cells[6].Text = "Lav";
                        break;
                }
            }
        }
        /// <summary>
        ///  Event som registrerer om "Godkjenn" eller "Ikke godkjenn" - knapper blir trykker på i GridView
        ///  Dersom en av knappene blir trykket på, oppdateres permissionState i DB. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
         {
             int index = Convert.ToInt32(e.CommandArgument.ToString());

             // Kjører om "Godkjenn" - knappen blir trykket på
             if (e.CommandName == "godkjent")
             {
                 int timeID = Convert.ToInt32(dt.Rows[index]["timeID"].ToString());
                 string query = String.Format("UPDATE TimeSheet SET permissionState = 2 WHERE timeID = {0}", timeID);
                 db.InsertDeleteUpdate(query);
                 FillGridView();
               
                 
             }
             // Kjører om "Ikke godkjenn" - knapp blir trykket på
             if (e.CommandName == "ikkeGodkjent")
             {
                 int timeID = Convert.ToInt32(dt.Rows[index]["timeID"].ToString());
                 string query = String.Format("UPDATE TimeSheet SET permissionState = 0 WHERE timeID = {0}", timeID);
                 db.InsertDeleteUpdate(query);
                 FillGridView();
              
             }
          
         }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}