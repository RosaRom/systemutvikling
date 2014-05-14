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
//        private int taskCategoryID = 19; // hardkodet, må byttes ut
        int projectID;
        
        private DBConnect db = new DBConnect();        

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
                GetTaskCategories();
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
        }

        private void GetTaskCategories()
        {
            projectID = Convert.ToInt16(Session["projectID"]);
            string query = string.Format("SELECT * FROM TaskCategory WHERE projectID = {0}", projectID);
            ddlTaskCategory.DataSource = db.getAll(query);
            ddlTaskCategory.DataTextField = "taskCategoryName";
            ddlTaskCategory.DataValueField = "taskCategoryID";
            ddlTaskCategory.Items.Insert(0, new ListItem("<Velg hovedtask>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddlTaskCategory.DataBind();
        }

        public void PopulateFields()
        {
            projectID = Convert.ToInt16(Session["projectID"]);
            string query = String.Format("SELECT * FROM TaskCategory WHERE taskCategoryID = {0} AND projectID = {1}", ddlTaskCategory.SelectedValue, projectID);
            DataTable dt = new DataTable();
            dt = db.getAll(query);
            lbTaskCategoryName.Text = dt.Rows[0]["taskCategoryName"].ToString();
            taTaskCategoryDesc.Text = dt.Rows[0]["taskCategoryDescription"].ToString();
        }

        protected void ddlTaskCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFields();
        }
    }
}