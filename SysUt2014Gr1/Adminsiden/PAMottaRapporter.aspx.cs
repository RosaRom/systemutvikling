using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Adminsiden
{
    public partial class PAMottaRapporter : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable table = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    VisNyeKlager();
                    VisNyeRapporter();
                    TellNye();
                }

            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
          
        }
        
        /// <summary>
        /// Henter ut bare nye klager
        /// </summary>
        private void VisNyeKlager()
        {
            string query = "SELECT deviationID, deviationTitle, timeAndDate FROM deviationReport WHERE deviationType = 1 AND deviationState = 0 ORDER BY timeAndDate DESC";
            gvKlager.DataSource = db.AdminGetAllUsers(query);
            gvKlager.DataBind();
        }
        
        /// <summary>
        /// Henter ut bare nye rapporter
        /// </summary>
        private void VisNyeRapporter()
        {
            string query = "SELECT deviationID, deviationTitle, timeAndDate FROM deviationReport WHERE deviationType = 0 AND deviationState = 0 ORDER BY timeAndDate DESC";
            gvRapporter.DataSource = db.AdminGetAllUsers(query);
            gvRapporter.DataBind();
        }

        /// <summary>
        /// Henter ut alle klagene
        /// </summary>
        private void VisAlleKlager()
        {
            string query = "SELECT deviationID, deviationTitle, timeAndDate FROM deviationReport WHERE deviationType = 1 ORDER BY timeAndDate DESC";
            gvKlager.DataSource = db.AdminGetAllUsers(query);
            gvKlager.DataBind();
        }

        /// <summary>
        /// henter ut alle rapportene
        /// </summary>
        private void VisAlleRapporter()
        {
            string query = "SELECT deviationID, deviationTitle, timeAndDate FROM deviationReport WHERE deviationType = 0 ORDER BY timeAndDate DESC";
            gvRapporter.DataSource = db.AdminGetAllUsers(query);
            gvRapporter.DataBind();
        }

        /// <summary>
        /// Teller antall nye rapporter og oppdaterer labelene
        /// </summary>
        private void TellNye()
        {
            string queryKlager = "SELECT COUNT(*) FROM deviationReport WHERE deviationType = 1 AND deviationState = 0";
            string queryRapporter = "SELECT COUNT(*) FROM deviationReport WHERE deviationType = 0 AND deviationState = 0";

            if (db.Count(queryKlager) == 1)
                lbAntallNyeKlager.Text = db.Count(queryKlager) + " ny";
            else
                lbAntallNyeKlager.Text = db.Count(queryKlager) + " nye";

            if (db.Count(queryRapporter) == 1)
                lbAntallNyeRapporter.Text = db.Count(queryRapporter) + " ny";
            else
                lbAntallNyeRapporter.Text = db.Count(queryRapporter) + " nye";
        }

        /// <summary>
        /// Henter ut beskrivelsen til en rapport og legger teksten til i tekstboksen
        /// </summary>
        /// <param name="_id"></param>
        private void HentBeskrivelse(string _id)
        {
            string query = "SELECT deviationDescription FROM deviationReport WHERE deviationID = " + _id;
            table = db.AdminGetAllUsers(query);
            informasjon.Text = table.Rows[0]["deviationDescription"].ToString();
        }

        /// <summary>
        /// Oppdaterer rapporten som blir lest som lest i databasen, oppdaterer også antall nye rapporter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKlager_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvKlager.DataKeys[e.RowIndex]["deviationID"].ToString();

            string query = "UPDATE deviationReport SET deviationState = 1 WHERE deviationID = " + id;
            db.InsertDeleteUpdate(query);
            HentBeskrivelse(id);
            this.TellNye();
        }

        /// <summary>
        /// Oppdaterer rapporten som blir lest som lest i databasen, oppdaterer også antall nye rapporter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRapporter_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvRapporter.DataKeys[e.RowIndex]["deviationID"].ToString();

            string query = "UPDATE deviationReport SET deviationState = 1 WHERE deviationID = " + id;
            db.InsertDeleteUpdate(query);
            HentBeskrivelse(id);
            this.TellNye();
        }

        /// <summary>
        /// Metoder for de 4 knappene som er på siden, tar i bruk hver sin metode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNyeRapporter_Click(object sender, EventArgs e)
        {
            this.VisNyeRapporter();
        }

        protected void btnAlleRapporter_Click(object sender, EventArgs e)
        {
            this.VisAlleRapporter();
        }

        protected void btnNyeKlager_Click(object sender, EventArgs e)
        {
            this.VisNyeKlager();
        }

        protected void btnAlleKlager_Click(object sender, EventArgs e)
        {
            this.VisAlleKlager();
        }
    }
}