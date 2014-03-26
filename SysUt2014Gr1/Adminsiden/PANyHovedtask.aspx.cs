using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin;


namespace Adminsiden
{
    public partial class PANyHovedtask : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private string projectID = "2";                       //hentes fra tidligere side, muligens cookie

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }
        }

        private void FillDropDownFase()
        {
            string query = "SELECT ";
        } 
    }
}