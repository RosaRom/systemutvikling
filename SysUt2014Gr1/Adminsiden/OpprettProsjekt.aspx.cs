using Admin;
using System;
using System.Collections.Generic;
using System.Data;
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
        private int descriptionMaxLength;
        private int webClientTeamID;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();
            if (!IsPostBack)
            {
                tb_dateFrom.Text = DateTime.Today.ToString("dd.MM.yyyy");
                tb_dateTo.Text = DateTime.Today.ToString("dd.MM.yyyy");
                getTeams(); //Binder teams fra databasen til DropDownList ddl_Team
                getMainProjects(); //Binder hovedprosjekt til DropDownList ddl_Hovedprosjekt
                getColumnLengths(); //Henter maxlength på kolonner i databasen der det er relevant
            }
            else
            {
                if (ViewState["teamID"] != null)
                    webClientTeamID = (int)ViewState["teamID"];
                //Hadde problem med å få TextArea countern til å holde på verdien ved server postback
                //Neste steg: assigne max length til en variabel ved en sql spørring opp mot kolonnen i databasen så man slipper å hardkode 300
                counter.InnerText = Convert.ToString(descriptionMaxLength - TextArea_ProjectDescription.InnerText.Length);
            }
        }

        private void getColumnLengths()
        {
            /*select COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH 
from information_schema.columns
where table_schema = DATABASE() AND   -- name of your database
      table_name = 'turno' AND        -- name of your table
      COLUMN_NAME = 'nombreTurno'     -- name of the column
            string descriptionQuery = "SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH AS maxLength FROM information_chema.columns"
                + " WHERE table_schema = DATABASE() AND table_name = 'Project' AND COLUMN_NAME = 'projectDescription'";
            DataTable dtDescription = new DataTable();
            dtDescription = db.getAll(descriptionQuery);
            descriptionMaxLength = Convert.ToInt32(dtDescription.Rows[0]["maxLength"]);
            lbl_warning.Text = Convert.ToString(descriptionMaxLength);
            lbl_warning.Visible = true;*/
        }
        private void getTeams()
        {
            string query = "SELECT * FROM Team";
            ddl_Team.DataSource = db.getAll(query);
            ddl_Team.DataTextField = "teamName";
            ddl_Team.DataValueField = "teamID";
            ddl_Team.Items.Insert(0, new ListItem("<Velg team>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddl_Team.DataBind();
        }

        private void getMainProjects()
        {
            //Må nok endre litt på spørring etter hvert som vi får kontroll på projectState
            string query = "SELECT projectName, projectID FROM Project WHERE parentProjectID = 0";
            ddl_Hovedprosjekt.DataSource = db.getAll(query);
            ddl_Hovedprosjekt.DataTextField = "projectName";
            ddl_Hovedprosjekt.DataValueField = "projectID";
            //Første valg i DropDownList, hvor teamID er satt til 0, kjører en sjekk i ModalPopup_ShowTeam-event
            ddl_Hovedprosjekt.Items.Insert(0, new ListItem("<Velg Hovedprosjekt>", "0"));
            ddl_Hovedprosjekt.DataBind();
        }

        #region Events
        protected void ModalPopup_ShowTeam(object sender, EventArgs e)
        {
            //Hvis bruker har glemt å velge team fra dropdownlist
            if (webClientTeamID == 0)
            {
                lbl_warning.Visible = true;
            }
            else
            {   //Setter Popupheader lik navnet på teamet som er valgt i dropdownlist
                string labelQuery = "SELECT teamName FROM Team WHERE teamID =" + webClientTeamID;
                DataTable dt = new DataTable();
                dt = db.getAll(labelQuery);
                string headerText = Convert.ToString(dt.Rows[0]["teamName"]);
                lbl_teamPopupHeader.Text = headerText;

                //Binder så teammedlemmene til et gridview i popupmodulen
                string query = "SELECT firstname, surname, groupName FROM User, UserGroup WHERE UserGroup.groupID = User.groupID AND teamID =" + webClientTeamID;
                gv_selectedTeam.DataSource = db.getAll(query);
                gv_selectedTeam.DataBind();
                ModalPopupExtender_Team.Show();
            }
        }

        protected void ddl_Team_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_warning.Visible = false;
            webClientTeamID = Convert.ToInt32(ddl_Team.SelectedValue);
            ViewState["teamID"] = webClientTeamID;
        }

        #endregion
    }
}