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
        private int projectID;
            
           
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

        private void ResetForm()
        {
            hovedtaskNavn.Text = "";
            txtBeskrivelse.Text = "";
        }

        private void ProductBacklogID()
        {
            projectID = Convert.ToInt16(Session["projectID"]);

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