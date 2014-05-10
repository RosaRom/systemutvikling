using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class TL_godkjenning_av_timeregistreringer : System.Web.UI.Page
    {

        // Prosjekt skal automatisk stå under godkjenning state 1
        // Hvis tl godkjenner så blir state forandret til 2
        // Dersom TL ikke godkjenner skal det stå state 0

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
            FillGridView(); 
        }

        private void FillDates()
        {
            //   string query = String.Format("SELECT 
        }
        public void FillGridView()
        {
            string query = String.Format("SELECT start \"start\", stop \"stop\", username \"username\", taskName \"taskName\", workplace \"workplace\", Task.description \"description\", priority \"priority\"" +
            "FROM User, TimeSheet, Task, Workplace WHERE User.userID = TimeSheet.userID AND TimeSheet.taskID = Task.taskID AND TimeSheet.workplaceID = Workplace.workplaceID AND Task.taskID IN (SELECT taskID FROM TimeSheet WHERE permissionState = 1)");
            
            dt = db.getAll(query);
            ViewState["table"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int prioritet = Convert.ToInt32(dt.Rows[i]["priority"]);

                switch (prioritet)
                {
                    case 1:
                        GridView1.Rows[i].Cells[6].Text = "Høy";
                        break;
                    case 2:
                        GridView1.Rows[i].Cells[6].Text = "Mid";
                        break;
                    case 3:
                        GridView1.Rows[i].Cells[6].Text = "Lav";
                        break;
                }
            }
        }

         protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
         {
             int index = Convert.ToInt32(e.CommandArgument.ToString());

             if (e.CommandName == "godkjent")
             { 
                
             
             }

             if (e.CommandName == "ikkeGodkjent")
             { 
                
             
             }
          
         }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}