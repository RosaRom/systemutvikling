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
        private int prosjektID = 0;            //hentes fra forrige side
        private DBConnect db = new DBConnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillTasks();
                FillMainTasks();
            }
            
            else
            {
                FillTasks();

                DropDownMainTask.DataSource = (DataTable)ViewState["dtMainTask"];
                DropDownMainTask.DataBind();
            }
        }

        protected void BtnLagreTask_Click(object sender, EventArgs e)
        {
            
        }

        private void FillTasks()
        {
            string queryTask = "SELECT taskID, taskName FROM Task";
            DataTable dtTask = new DataTable();
            
            dtTask = db.AdminGetAllUsers(queryTask);
            dtTask.Rows.InsertAt(dtTask.NewRow(), 0);

            DropDownSubTask.DataSource = dtTask;
            DropDownSubTask.DataBind();
        }

        private void FillMainTasks()
        {
            string queryMainTask = "SELECT taskCategoryID, taskCategoryName FROM TaskCategory";
            DataTable dtMainTask = new DataTable();
            dtMainTask = db.AdminGetAllUsers(queryMainTask);

            ViewState["dtMainTask"] = dtMainTask;

            DropDownMainTask.DataSource = dtMainTask;
            DropDownMainTask.DataBind();
        }
    }
}