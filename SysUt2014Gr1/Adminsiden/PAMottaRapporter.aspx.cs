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
            if (!Page.IsPostBack)
            {
                VisNyeKlager();
                VisNyeRapporter();
                TellNye();
            }
        }

        private void VisNyeKlager()
        {
            string query = "SELECT deviationID, deviationTitle FROM deviationReport WHERE deviationType = 1 AND deviationState = 0";
            gvKlager.DataSource = db.AdminGetAllUsers(query);
            gvKlager.DataBind();
        }
        
        private void VisNyeRapporter()
        {
            string query = "SELECT deviationID, deviationTitle FROM deviationReport WHERE deviationType = 0 AND deviationState = 0";
            gvRapporter.DataSource = db.AdminGetAllUsers(query);
            gvRapporter.DataBind();
        }

        private void VisAlleKlager()
        {
            string query = "SELECT deviationID, deviationTitle FROM deviationReport WHERE deviationType = 1 ORDER BY deviationState DESC";
            gvKlager.DataSource = db.AdminGetAllUsers(query);
            gvKlager.DataBind();
        }

        private void VisAlleRapporter()
        {
            string query = "SELECT deviationID, deviationTitle FROM deviationReport WHERE deviationType = 0 ORDER BY deviationState DESC";
            gvRapporter.DataSource = db.AdminGetAllUsers(query);
            gvRapporter.DataBind();
        }

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


        private void HentBeskrivelse(string _id)
        {
            string query = "SELECT deviationDescription FROM deviationReport WHERE deviationID = " + _id;
            table = db.AdminGetAllUsers(query);
            informasjon.Text = table.Rows[0]["deviationDescription"].ToString();
        }

        protected void gvKlager_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvKlager.DataKeys[e.RowIndex]["deviationID"].ToString();

            string query = "UPDATE deviationReport SET deviationState = 1 WHERE deviationID = " + id;
            //db.InsertDeleteUpdate(query);
            HentBeskrivelse(id);
        }

        protected void gvRapporter_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvRapporter.DataKeys[e.RowIndex]["deviationID"].ToString();

            string query = "UPDATE deviationReport SET deviationState = 1 WHERE deviationID = " + id;
            //db.InsertDeleteUpdate(query);
            HentBeskrivelse(id);
        }
    }
}