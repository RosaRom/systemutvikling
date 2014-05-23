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
            int projectID = Convert.ToInt32(Session["projectID"]);

            string query = String.Format("SELECT timeID, start, stop, username, taskName, workplace, Task.description, priority FROM User, TimeSheet, Task, Workplace WHERE User.userID = TimeSheet.userID AND TimeSheet.projectID = " + projectID + " AND TimeSheet.taskID = Task.taskID AND TimeSheet.workplaceID = Workplace.workplaceID AND TimeSheet.permissionState = 1");
            
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
                 int timeID = Convert.ToInt32(dt.Rows[index]["timeID"].ToString());
                 string query = String.Format("UPDATE TimeSheet SET permissionState = 2 WHERE timeID = {0}", timeID);
                 db.InsertDeleteUpdate(query);
                 FillGridView();
               
                 
             }

             if (e.CommandName == "ikkeGodkjent")
             {
                 int timeID = Convert.ToInt32(dt.Rows[index]["timeID"].ToString());
                 string query = String.Format("UPDATE TimeSheet SET permissionState = 0 WHERE timeID = {0}", timeID);
                 db.InsertDeleteUpdate(query);
                 FillGridView();
              
             }
          
         }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}