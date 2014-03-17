using Admin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class OpprettProsjekt : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_dateFrom.Text = DateTime.Today.ToString("dd/MM/yyyy");
                tb_dateTo.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }

        }
    }
}