using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class ViewProjectArchive : System.Web.UI.Page
    {

        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

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
                Populate();
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }
        }

        public void Populate()
        {
            string query = "SELECT projectID, projectName \"Prosjektnavn\", projectDescription \"Beskrivelse\", projectState \"State\"," +
              " parentProjectID \"Foreldre-ID\", teamID FROM Project WHERE projectState = 2";

            dt = db.getAll(query);
            ViewState["table"] = dt;

            gvTaskList.DataSource = dt;
            gvTaskList.DataBind();

        }

        protected void gvTaskList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            //kjører når "Gjør aktiv" blir trykket for en item
            if (e.CommandName == "aktiver")
            {
                int projectID = Convert.ToInt32(dt.Rows[index]["projectID"].ToString());
                string query = String.Format("UPDATE Project SET projectState = 1 WHERE projectID = {0}",  projectID);
                db.InsertDeleteUpdate(query);
                Populate();        
            }
        }
    }
}