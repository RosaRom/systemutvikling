using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Adminsiden
{
    /// <summary>
    /// PANyHovedTask.aspx.cs av Kristian Alm
    /// SysUt14Gr1 - SystemUtvikling - Vår 2014
    /// Ari lagde søkefunksjonen og sorteringsfunksjonen for siden.
    /// En enkel side som oppretter en kategori til et valgt prosjekt
    /// </summary>
    public partial class PANyHovedtask : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private int projectID;

        /// <summary>
        /// Sjekker på session hvilken type bruker det er som er logget inn.
        /// Er en standard metode vi har i alle klasser, setter masterpage for en gitt brukertype.
        /// Da hver bruker har tilgang til litt forskjellige sider trenger de hver sin meny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Ved oppstart av siden kjøres metoden som setter en automatisk produckt backlog id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    ProductBacklogID();
                }

            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
           
        }

        /// <summary>
        /// Her hentes all informasjon ut fra siden og lagrer det i databasen.
        /// ProjectID henter fra session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLagreHovedtask_Click(object sender, EventArgs e)
        {
            projectID = Convert.ToInt16(Session["projectID"]);

            try{
                string kategoriNavn = hovedtaskNavn.Text;
                string productBacklogId = id.Text;
                string beskrivelse = txtBeskrivelse.Text;

                string query = String.Format("INSERT INTO TaskCategory VALUES(NULL, '{0}', '{1}', {2}, NULL, '{3}')", kategoriNavn, beskrivelse, projectID, productBacklogId);
                db.InsertDeleteUpdate(query);

                lbBeskjed.Text = "Ny kategori lagt til";
                ResetForm();
                ProductBacklogID();
            } 
            catch(Exception ex)
            {
                lbBeskjed.Text = "Noe gikk galt: " + ex.Message;
            }
            
        }

        /// <summary>
        /// Ved lagring fjernes all tekst
        /// </summary>
        private void ResetForm()
        {
            hovedtaskNavn.Text = "";
            txtBeskrivelse.Text = "";
        }

        /// <summary>
        /// Teller antall eksisterende kategorier i prosjektet og gir en
        /// product backlog id etter antall + 1.
        /// </summary>
        private void ProductBacklogID()
        {
            projectID = Convert.ToInt16(Session["projectID"]);

            string query = "SELECT COUNT(*) FROM TaskCategory WHERE projectID = " + projectID;
            int count = db.Count(query);

            id.Text = Convert.ToString(count + 1);
        }

        /// <summary>
        /// En snarvei til å opprette nye tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNyTask_Click(object sender, EventArgs e)
        {
            Response.Redirect("PALeggTilTasks.aspx");
        }
    }
}