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
    public partial class editProject : System.Web.UI.Page
    {
        private DBConnect db;
        private string query;
        private string listRef;
        private DataTable dataTable;


        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();
            listRef = (String)Session["valg"];
            Refresh();
        }

        private void EditProject()
        {
            /*
            query = String.Format("UPDATE Project SET projectName = '{0}', projectDescription = '{1}', projectState = '{2}', parentProjectID = '{3}' WHERE projectID = '{4}')",
                tbProjectName.Text, tbProjectDescription.Text, "1", "0", "2");
             * */

             query = String.Format("UPDATE Project SET projectName = '{0}' WHERE projectID = '{1}'",
                tbProjectName.Text, "2");
            db.InsertDeleteUpdate(query);

        }
        private void Refresh()
        {
            query = "SELECT * FROM Project WHERE projectID = 2 ";// +listRef;
            dataTable = db.getAll(query);

            tbProjectName.Text = dataTable.Rows[0]["projectName"].ToString();
            tbProjectDescription.Text = dataTable.Rows[0]["projectDescription"].ToString();

        }

        protected void btnUpdateQuery_Click(object sender, EventArgs e)
        {
            EditProject();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}