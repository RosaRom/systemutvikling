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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillTimeSelectDDL();
                fromCalendar.Visible = false;
                tb_dateFrom.Text = DateTime.Today.ToString("dd/MM/yyyy");
                tb_dateTo.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }

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

        protected void fromCalendar_SelectionChanged(object sender, EventArgs e)
        {
            tb_dateFrom.Text = Convert.ToDateTime(fromCalendar.SelectedDate, CultureInfo.GetCultureInfo("nb-NO")).ToString("dd/MM/yyyy");
            fromCalendar.Visible = false;
        }

        protected void ib_fromCalendar_Click(object sender, ImageClickEventArgs e)
        {
            if (fromCalendar.Visible == false)
            {
                fromCalendar.Visible = true;
            }
            else fromCalendar.Visible = false;
        }
    }
}