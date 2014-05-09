using Adminsiden;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class editProject : System.Web.UI.Page
    {
        private DBConnect db;
        private string query;
        private DataTable dataTable = new DataTable();
        private int projectID;
        private string projectDescription;
        private string name;
        private string state;

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
            db = new DBConnect();

            string session = (string)Session["userLoggedIn"];

            if (session == "teamLeader" || session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    state = tbState.Text;
                    name = tbProjectName.Text;
                    projectDescription = tbProjectDescription.Text;
                    projectID = Convert.ToInt16(Session["projectID"]);
                    Refresh();
                }
                else
                {
                    if (ViewState["name"] != null)
                    {
                        name = (string)ViewState["name"];
                        projectDescription = (string)ViewState["projectDescription"];
                    }
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }
          
        }
        private void EditProject()
        {
            /*
            query = String.Format("UPDATE Project SET projectName = '{0}', projectDescription = '{1}', projectState = '{2}', parentProjectID = '{3}' WHERE projectID = '{4}')",
                tbProjectName.Text, tbProjectDescription.Text, "1", "0", "2");
             * */
            name = tbProjectName.Text;
            projectID = Convert.ToInt16(Session["projectID"]);
            projectDescription = tbProjectDescription.Text;
            state = tbState.Text;


             query = String.Format("UPDATE Project SET projectName = '{0}', projectDescription = '{1}', projectState = '{2}' WHERE projectID = '{3}'",
                name, projectDescription, state, projectID);
            db.InsertDeleteUpdate(query);
            lblMessageOK.ForeColor = Color.Green;
            lblMessageOK.Text = "Prosjekt endret, OK!";

        }
        private void Refresh()
        {
            query = String.Format("SELECT * FROM Project WHERE projectID = '{0}'", projectID);
            dataTable = db.getAll(query);

            try
            {
                tbProjectName.Text = dataTable.Rows[0]["projectName"].ToString();
                tbProjectDescription.Text = dataTable.Rows[0]["projectDescription"].ToString();
                ViewState["name"] = dataTable.Rows[0]["projectName"].ToString();
                tbState.Text = dataTable.Rows[0]["projectState"].ToString();
                ViewState["description"] = dataTable.Rows[0]["projectDescription"].ToString();
                tbState.Text = dataTable.Rows[0]["projectState"].ToString();
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
 
        }

        protected void btnUpdateQuery_Click(object sender, EventArgs e)
        {
            EditProject();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}