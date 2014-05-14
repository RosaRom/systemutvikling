using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class BootstrapTL : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            DataTable dt = new DataTable();
            string phaseName = null;
            int phaseID = 0;
            int projectID = Convert.ToInt16(Session["projectID"]);
            string query = "SELECT * FROM Fase WHERE projectID =" + projectID;
            dt = db.getAll(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dt.Rows[i][3]) <= DateTime.Today && Convert.ToDateTime(dt.Rows[i][4]) >= DateTime.Today)
                {
                    phaseName = Convert.ToString(dt.Rows[i][1]);
                    phaseID = Convert.ToInt16(dt.Rows[i][0]);
                }
            }
            Session["phaseID"] = phaseID;
            Label_prosjekt.Text = (string)Session["projectNavn"];
            Label_fase.Text = phaseName;
        }
    }
}