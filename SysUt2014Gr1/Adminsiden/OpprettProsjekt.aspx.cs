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
        private DBConnect db;
        private int webClientTeamID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //tb_dateFrom.Attributes.Add("onkeydown", "DataField_KeyDown(this,'" + calendarDateFrom.ClientID + "')");
            //tb_dateTo.Attributes.Add("onkeydown", "DataField_KeyDown(this,'" + calendarDateTo.ClientID + "')");
            db = new DBConnect();
            if (!IsPostBack)
            {
                tb_dateFrom.Text = DateTime.Today.ToString("dd.MM.yyyy");
                tb_dateTo.Text = DateTime.Today.ToString("dd.MM.yyyy");
                getTeams(); //Binder teams fra databasen til DropDownList ddl_Team
                getMainProjects(); //Binder hovedprosjekt til DropDownList ddl_Hovedprosjekt
            }
            else
            {
                if (ViewState["teamID"] != null)
                    webClientTeamID = (int)ViewState["teamID"];
            }
        }

        private void getTeams()
        {
            string query = "SELECT * FROM Team";
            ddl_Team.DataSource = db.getAll(query);
            ddl_Team.DataTextField = "teamName";
            ddl_Team.DataValueField = "teamID";
            ddl_Team.Items.Insert(0, new ListItem("<Velg team>", "null")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddl_Team.DataBind();
        }

        private void getMainProjects()
        {
            //Må nok endre litt på spørring etter hvert som vi får kontroll på projectState
            string query = "SELECT projectName, projectID FROM Project WHERE parentProjectID = 0";
            ddl_Hovedprosjekt.DataSource = db.getAll(query);
            ddl_Hovedprosjekt.DataTextField = "projectName";
            ddl_Hovedprosjekt.DataValueField = "projectID";
            ddl_Hovedprosjekt.Items.Insert(0, new ListItem("<Velg Hovedprosjekt>", "0"));
            ddl_Hovedprosjekt.DataBind();
        }

        #region Events
        protected void ModalPopup_ShowTeam(object sender, EventArgs e)
        {
            string query = "SELECT firstname, surname, groupName FROM User, UserGroup WHERE UserGroup.groupID = User.groupID AND teamID =" + webClientTeamID;
            gv_selectedTeam.DataSource = db.getAll(query);
            gv_selectedTeam.DataBind();
            ModalPopupExtender_Team.Show();
        }

        protected void ddl_Team_SelectedIndexChanged(object sender, EventArgs e)
        {
            webClientTeamID = Convert.ToInt32(ddl_Team.SelectedValue);
            ViewState["teamID"] = webClientTeamID;
        }

        #endregion
    }
}