using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class visTaskdetaljer : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            int taskID = 3;

            string query = "SELECT * FROM Task WHERE taskID = " + taskID;
            dt = db.getAll(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                string taskname = Convert.ToString(dt.Rows[0]["taskName"]);
                string description = Convert.ToString(dt.Rows[0]["description"]);
                int hoursUsed = Convert.ToInt16(dt.Rows[0]["hoursUsed"]);
                int hoursAllocated = Convert.ToInt16(dt.Rows[0]["hoursAllocated"]);
                int priority = Convert.ToInt16(dt.Rows[0]["priority"]);
                //progressbar1

                Label_navn.Text = taskname;
                tb_desc.Text = description;

                switch (priority)
                {
                    case 1: Label_prioritet.Text = "Høy";
                        break;
                    case 2: Label_prioritet.Text = "Middels";
                        break;
                    case 3: Label_prioritet.Text = "Lav";
                        break;
                    default: Label_prioritet.Text = "Feil i databasen, kontakt administrator";
                        break;
                }


            }
            else
            {
                //Label_warning.Text = "Noe har gått gale, vennligst velg prosjekt igjen.";
            }
        }
    }
}