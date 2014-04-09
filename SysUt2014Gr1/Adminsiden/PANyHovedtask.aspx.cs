using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Adminsiden
{
    public partial class PANyHovedtask : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private string projectID = "2";                       //hentes fra tidligere side, muligens cookie

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillDropDownFase();
                ProductBacklogID();
            }
        }

        private void FillDropDownFase()
        {
            string query = "SELECT phaseName, phaseID FROM Fase WHERE projectID = " + projectID;
            DataTable table = new DataTable();

            try
            {
                table = db.getAll(query);
                DropDownFase.DataSource = table;
                DropDownFase.DataBind();
            }
            catch (Exception ex)
            {
                lbBeskjed.Text = "Noe gikk galt: " + ex.Message;
            }
        }

        protected void btnLagreHovedtask_Click(object sender, EventArgs e)
        {
            try{
                string kategoriNavn = hovedtaskNavn.Text;
                string productBacklogId = id.Text;
                string beskrivelse = txtBeskrivelse.Text;
                string fase = DropDownFase.SelectedValue;

                string query = String.Format("INSERT INTO TaskCategory VALUES(null, '{0}', '{1}', {2}, {3}, '{4}')", kategoriNavn, beskrivelse, projectID, fase, productBacklogId);
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

        private void ResetForm()
        {
            hovedtaskNavn.Text = "";
            txtBeskrivelse.Text = "";
        }

        private void ProductBacklogID()
        {
            string query = "SELECT COUNT(*) FROM TaskCategory WHERE projectID = " + projectID;
            int count = db.Count(query);

            id.Text = Convert.ToString(count + 1);
        }

        protected void BtnNyTask_Click(object sender, EventArgs e)
        {
            Response.Redirect("PALeggTilTasks.aspx");
        }
    }
}