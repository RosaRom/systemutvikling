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

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();
            FillProjectList();

        }

        private void FillProjectList()
        {
            query = "SELECT ProjectName FROM Project WHERE projectState = 1";
            dataTable = db.getAll(query);
            projectList.DataValueField = "projectName";
            projectList.DataSource = dataTable;
            projectList.DataBind();

        }

        protected void btnEditProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("editProject.aspx");
        }
    }
}