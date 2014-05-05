using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

// det er ikke gjort stort med utseendet, siden formen er så enkel og vi trenger et uniformt utseende uansett

namespace Adminsiden
{
    public partial class PAVisHovedtask : System.Web.UI.Page
    {
        private int taskCategoryID = 1; // hardkodet, må byttes ut
        
        private DBConnect db = new DBConnect();
        private DataTable dt = new DataTable();

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
                PopulateFields();            

            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
        }

        public void PopulateFields()
        {
            string query = String.Format("SELECT * FROM TaskCategory WHERE taskCategoryID = {0}", taskCategoryID);
            dt = db.getAll(query);
            lbTaskCategoryName.Text = dt.Rows[0]["taskCategoryName"].ToString().ToUpper();
            taTaskCategoryDesc.Text = dt.Rows[0]["taskCategoryDescription"].ToString();
        }
    }
}