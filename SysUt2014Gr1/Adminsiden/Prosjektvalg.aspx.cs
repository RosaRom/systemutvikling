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
    public partial class Prosjektvalg : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetProject();
            }        
        }
        private void GetProject()
        {
            
            string query = "SELECT projectID, projectName, projectDescription FROM Project";
            GridViewProject.DataSource = db.getAll(query);
            GridViewProject.DataBind();
        }
        protected void GridViewProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int projectID = Convert.ToInt32(GridViewProject.Rows[index].Cells[1].Text);
            int userID = 2;
            Session["projectID"] = projectID;
            Session["userID"] = userID;
            Server.Transfer("Bruker.aspx", true);
        }
  
    }
}