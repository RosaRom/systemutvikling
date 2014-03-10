using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class OpprettProsjekt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fillTimeSelectDDL();
        }
        private void fillTimeSelectDDL()
        {
            if (ddl_hour.Items.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    ddl_hour.Items.Add("0" + i);
                }
                for (int i = 10; i < 24; i++)
                {
                    ddl_hour.Items.Add("" + i);
                }
            }
            if (ddl_min.Items.Count == 0)
            {
                for (int i = 0; i < 10; i += 15)
                {
                    ddl_min.Items.Add("0" + i);
                }
                for (int i = 15; i < 60; i += 15)
                {
                    ddl_min.Items.Add("" + i);
                }
            }
        }
    }
}