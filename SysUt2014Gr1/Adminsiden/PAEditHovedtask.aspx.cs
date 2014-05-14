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
    public partial class PAEditHovedtask : System.Web.UI.Page
    {
//        private int taskCategoryID = 1; // hardkodet, må byttes ut
        int projectID;

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
                if (!Page.IsPostBack)
                {
                    GetTaskCategories();                    
                }   
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
//            ddlTaskCategory.Items.Insert(0, new ListItem("<Velg hovedtask>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddlTaskCategory.DataBind();
            PopulateFields();
        }

        public void PopulateFields()
        {
            projectID = Convert.ToInt16(Session["projectID"]);
            string query = String.Format("SELECT * FROM TaskCategory WHERE taskCategoryID = {0} AND projectID = {1}", ddlTaskCategory.SelectedValue, projectID);
            dt = db.getAll(query);
            tbTaskCategoryName.Text = dt.Rows[0]["taskCategoryName"].ToString();
            taTaskCategoryDesc.Text = dt.Rows[0]["taskCategoryDescription"].ToString();            
        }

        protected void ddlTaskCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFields();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (taTaskCategoryDesc.Text != "" && tbTaskCategoryName.Text != "")
            {
                String desc = taTaskCategoryDesc.Text;
                String name = tbTaskCategoryName.Text;
                string query = String.Format("UPDATE TaskCategory SET TaskCategoryDescription = \"{1}\", TaskCategoryName =\"{2}\" WHERE TaskCategoryID = {0}", ddlTaskCategory.SelectedValue, desc, name);
                db.InsertDeleteUpdate(query);
            }
            else
                lbError.Text = "Feltene kan ikke være tomme.";
        }
    }
}