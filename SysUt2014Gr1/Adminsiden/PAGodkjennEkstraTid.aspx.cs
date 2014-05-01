using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class PAGodkjennEkstraTid : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void stuff()
        {
            string query = String.Format("SELECT * FROM Task WHERE extraHours != null");
            dt = db.getAll(query);


        }
    }
}