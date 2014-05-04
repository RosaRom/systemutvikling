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
    public partial class Prosjektvalg : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        string session;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            String userLoggedIn = (String)Session["userLoggedIn"];

            if(userLoggedIn == "teamMember")
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

            session = (string)Session["userLoggedIn"];

            if (session == "teamMember" || session == "teamLeader" || session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    GetProject();
                }        
            }
            else
            {
                Server.Transfer("Login.aspx", true);
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
            Session["projectID"] = projectID;

            if (session == "teamMember" || session == "projectManager")
                Server.Transfer("Bruker.aspx", true);

            else
                Server.Transfer("Teamleder.aspx", true);
        }
  
    }
}