using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace Adminsiden
{
    /// <summary>
    /// PAMottaRapporter.aspx.cs av Kristian Alm
    /// SysUt14Gr1 - SystemUtvikling - Vår 2014
    /// Denne siden skal vise alle nye rapporter generert av systemet
    /// </summary>
    public partial class PAMottaRapporter : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable table = new DataTable();
        DataTable tempTable = new DataTable();

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
        /// Ved oppstart av siden kjøres de metodene nedenfor
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
                    SjekkFaser();
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
        /// <param name="_id">Henter ut beskrivelse basert på id som kommer inn</param>
        private void HentBeskrivelse(string _id)
        {
            string query = "SELECT deviationDescription FROM deviationReport WHERE deviationID = " + _id;
            table = db.AdminGetAllUsers(query);
            informasjon.Text = table.Rows[0]["deviationDescription"].ToString();
        }

        /// <summary>
        /// Automatisk generering av rapport ved endt fase, sjekker alle som er ferdig for mindre enn 3 dager siden.
        /// Sjekker om det allerede eksisterer en rapport, om ikke en finnes genereres en med alle uferdige tasks som var i fasen.
        /// </summary>
        private void SjekkFaser()
        {
            string query = "SELECT * FROM Fase";
            table = db.AdminGetAllUsers(query);

            foreach (DataRow row in table.Rows)
            {
                if ((DateTime.Now - Convert.ToDateTime(row["phaseToDate"].ToString())).TotalHours < 72 && (DateTime.Now - Convert.ToDateTime(row["phaseToDate"].ToString())).TotalHours > 1)
                {
                    string queryProsjekt = "SELECT projectName FROM Project WHERE projectID = " + row["projectID"].ToString();
                    tempTable = db.AdminGetAllUsers(queryProsjekt);

                    string sjekkOverskrift = row["phaseName"].ToString() + " i " + tempTable.Rows[0]["projectName"].ToString();
                    string querySjekkOverskrift = String.Format("SELECT COUNT(*) FROM deviationReport WHERE deviationTitle LIKE '{0}'", sjekkOverskrift);

                    if (db.Count(querySjekkOverskrift) == 0)
                    {
                        string uferdigeTasksQuery = String.Format("SELECT taskName FROM Task WHERE phaseID = {0} AND (state = 0 OR state = 1)", row["phaseID"]);
                        tempTable = db.AdminGetAllUsers(uferdigeTasksQuery);

                        StringBuilder tasks = new StringBuilder();
                        tasks.Append("Uferdige tasks er: ");

                        foreach (DataRow r in tempTable.Rows)
                        {
                            tasks.Append(r["taskName"].ToString() + ", ");
                        }

                        if (!tasks.ToString().Equals("Uferdige tasks er: "))
                        {
                            string queryNyRapport = String.Format("INSERT INTO deviationReport VALUES(null, '{0}', '{1}', 0, 0, now())", sjekkOverskrift, tasks.ToString());
                            db.InsertDeleteUpdate(queryNyRapport);
                        }
                    }
                }
            }
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