using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class BrukerBeOmExtraTimer : System.Web.UI.Page
    {
        int userID = 44; // hardkodet, trenger session
        int taskID = 0;

        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateFaseValg();
        }

        public void Populate()
        {
            if (Convert.ToInt32(ddlTaskValg.SelectedValue.ToString()) != 0)
            {
                string query = string.Format("SELECT * FROM Task WHERE taskID = {0}", taskID);
                dt = db.getAll(query);
                tbEkstraTimer.Text = dt.Rows[0]["hoursExtra"].ToString();
                string allocated = dt.Rows[0]["hoursAllocated"].ToString();
                string used = dt.Rows[0]["hoursUsed"].ToString();
                lbValgtTaskInfo.Text = string.Format("Valgt task har {0} brukte / {1} allokerte timer.", used, allocated);
            }
        }

        // populates combobox with task selection
        public void PopulateFaseValg()
        {            
            string query = String.Format("SELECT * FROM Task WHERE phaseID IN (SELECT phaseID FROM Fase WHERE projectID IN (SELECT projectID FROM Project WHERE teamID IN (SELECT teamID FROM User WHERE userID = {0})))", userID);
            if (ddlTaskValg.Items.Count == 0)
            {
                ddlTaskValg.DataSource = db.getAll(query);
                ddlTaskValg.DataTextField = "taskName";
                ddlTaskValg.DataValueField = "taskID";
                ddlTaskValg.Items.Insert(0, new ListItem("<Velg task>", "0"));
                ddlTaskValg.DataBind();
            }
        }

        protected void ddlTaskValg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTaskValg.SelectedValue.ToString()) != 0)
            {
                taskID = Convert.ToInt32(ddlTaskValg.SelectedValue.ToString());
                Populate();
            }
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlTaskValg.SelectedValue.ToString()) != 0)
            {
                int ekstraTimer = Convert.ToInt32(tbEkstraTimer.Text.ToString());
                string query = string.Format("UPDATE Task SET hoursExtra = {0} WHERE taskID = {1}", ekstraTimer, Convert.ToInt32(ddlTaskValg.SelectedValue.ToString()));
                db.InsertDeleteUpdate(query);
                lbCommitStatus.Text = string.Format("Forespørsel om {0} ekstra timer er registrert. Avventer godkjenning.", ekstraTimer.ToString());
            }
            else
            {
                lbCommitStatus.Text = "Velg task først.";
            }
        }
    }
}