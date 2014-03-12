using Admin;
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
        private string projectID;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();
            FillProjectList();

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
            projectList.Text = projectList.SelectedValue;
            Session["valg"] =  projectList.Text;

            Response.Redirect("editProject.aspx");
        }

        protected void projectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            projectID = projectList.Text;// Convert.ToInt32(projectList.SelectedValue);
            

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
           // projectID = Convert.ToInt32(projectList.SelectedValue);

        }

    }
}