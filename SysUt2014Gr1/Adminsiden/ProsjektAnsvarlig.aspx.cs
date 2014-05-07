using Adminsiden;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class ProsjektAnsvarlig : System.Web.UI.Page
    {
        string query;
        private DBConnect db;
        private DataTable dataTable;
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
                db = new DBConnect();

                if (!Page.IsPostBack)
                {
                    FillProjectList();
                }
                else
                {
                    if (ViewState["projectID"] != null)
                    {
                        projectID = (int)ViewState["projectID"];
                        Session["valgtID"] = projectID;
                    }

                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }

       
        }

        private void FillProjectList()
        {
            query = "SELECT * FROM Project WHERE projectState = 1";
            dataTable = db.getAll(query);
            projectList.DataTextField = "projectName";
            projectList.DataValueField = "projectID";
            projectList.DataSource = dataTable;
            projectList.DataBind();

        }

        protected void btnEditProject_Click(object sender, EventArgs e)
        {
            ViewState["projectID"] = Convert.ToInt32(projectList.SelectedValue);
            projectID = Convert.ToInt32(projectList.SelectedValue);
            Session["projectID"] = projectID;
            Server.Transfer("EditProject.aspx", true);
        }

        protected void projectList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        protected void btnArchiveProject_Click(object sender, EventArgs e)
        {
            projectID = Convert.ToInt32(projectList.SelectedValue);

            query = String.Format("UPDATE Project SET projectState = 0 WHERE projectID = '{0}'",
                Session["valgtID"]);
            db.InsertDeleteUpdate(query);

        }

        protected void btnNewProject_Click(object sender, EventArgs e)
        {
            Server.Transfer("OpprettProsjekt.aspx", true);
        }

        protected void btnShowArchive_Click(object sender, EventArgs e)
        {
            Server.Transfer("ViewProjectArchive.aspx", true);

        }

     

     
    }
}