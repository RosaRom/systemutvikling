using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Adminsiden;


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
            }
        }

        private void FillDropDownFase()
        {
            string query = "SELECT phaseName, phaseID FROM Fase WHERE projectID = " + projectID;
            DataTable table = new DataTable();

            try
            {
                table = db.getAll(query);
                DropDownFraFase.DataSource = table;
                DropDownTilFase.DataSource = table;

                DropDownFraFase.DataBind();
                DropDownTilFase.DataBind();
            }
            catch (Exception ex)
            {
                lbBeskjed.Text = "Noe gikk galt: " + ex.Message;
            }
        } 
    }
}