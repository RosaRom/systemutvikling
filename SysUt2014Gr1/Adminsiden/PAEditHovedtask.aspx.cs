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
        private int taskCategoryID = 1; // hardkodet, må byttes ut

        private DBConnect db = new DBConnect();
        private DataTable dt = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    PopulateFields();
                }   
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
            tbTaskCategoryName.Text = dt.Rows[0]["taskCategoryName"].ToString();
            taTaskCategoryDesc.Text = dt.Rows[0]["taskCategoryDescription"].ToString();            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (taTaskCategoryDesc.Text != "" && tbTaskCategoryName.Text != "")
            {
                String desc = taTaskCategoryDesc.Text;
                String name = tbTaskCategoryName.Text;
                string query = String.Format("UPDATE TaskCategory SET TaskCategoryDescription = \"{1}\", TaskCategoryName =\"{2}\" WHERE TaskCategoryID = {0}", taskCategoryID, desc, name);
                db.InsertDeleteUpdate(query);
            }
            else
                lbError.Text = "Feltene kan ikke være tomme.";
        }
    }
}