using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin;
using System.Data;

namespace Adminsiden
{
    public partial class PALeggTilTasks : System.Web.UI.Page
    {
        private int prosjektID = 2;            //hentes fra forrige side
        private DBConnect db = new DBConnect();
        private DataTable table = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillMainTasks();
            }
        }

        //lagrer en ny task i databasen
        protected void BtnLagreTask_Click(object sender, EventArgs e)
        {
            
        }

        //henter ut alle tasks som tilhører en valgt hovedtask, blir oppdatert hver gang hovedtask endres
        private void FillTasks()
        {
            string queryTask = "SELECT taskID, taskName FROM Task WHERE TaskCategoryID = " + DropDownMainTask.SelectedValue.ToString();

            table = db.AdminGetAllUsers(queryTask);
            table.Rows.InsertAt(table.NewRow(), 0);

            DropDownSubTask.DataSource = table;
            DropDownSubTask.DataBind();
        }

        //kjøres ved oppstart av siden, henter ut alle hovedtasks til en gitt projectID
        private void FillMainTasks()
        {
            string queryMainTask = "SELECT taskCategoryID, taskCategoryName FROM TaskCategory WHERE projectID = " + prosjektID;
            table = db.AdminGetAllUsers(queryMainTask);

            DropDownMainTask.DataSource = table;
            DropDownMainTask.DataBind();

            FillTasks();
        }

        //kjøres ved hver forandring av hovedtask, fyller opp tilhørende tasks på nytt
        protected void DropDownMainTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTasks();
        }
    }
}