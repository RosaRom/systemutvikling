using Adminsiden;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden   
{
    /// <summary>
    /// EditProject.aspx.cs av Tord-Marius Fredriksen
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Klassen brukes til å endre et eksisterende prosjekt. Teamleder og Prosjektansvarlig har tilgang 
    /// til denne siden.
    /// </summary>
    public partial class editProject : System.Web.UI.Page
    {
        private DBConnect db; 
        private DataTable table = new DataTable();
        private DataTable tableProjectNames = new DataTable();
        private DataTable tableTeamNames = new DataTable();
        private int projectID;
        private string queryUpdate;
        /// <summary>
        /// Metode som kjøres først av alle for å sjekke hvilken masterpage som skal brukes,
        /// alt etter hvilken brukertype som er logget inn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            String userLoggedIn = (String)Session["userLoggedIn"];

            if (userLoggedIn == "teamMember")
                this.MasterPageFile = "~/Masterpages/Bruker.Master";

            else if (userLoggedIn == "teamLeader")
                this.MasterPageFile = "~/Masterpages/Teamleder.Master";

            else if (userLoggedIn == "admin")
                this.MasterPageFile = "~/Masterpages/Admin.Master";

            else
                this.MasterPageFile = "~/Masterpages/Prosjektansvarlig.Master";
        }
        /// <summary>
        /// Kjøres i det nettsiden lastes inn, og gir bare tilgang til brukere som skal ha det.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();
            projectID = Convert.ToInt16(Session["projectID"]);

            string session = (string)Session["userLoggedIn"];

            if (session == "teamLeader" || session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    GetProjectDetails();
                }
                else
                {
                    table = (DataTable)ViewState["table"];
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }
          
        }
        /// <summary>
        /// Lister ut gitt prosjekt fra databasetabellen
        /// </summary>
        private void GetProjectDetails()
        {
            string queryProject = "SELECT * FROM Project WHERE projectID = " + projectID;
            table = db.AdminGetAllUsers(queryProject);
            ViewState["table"] = table;

            tbProjectName.Text = table.Rows[0]["projectName"].ToString();
            tbProjectDescription.Text = table.Rows[0]["projectDescription"].ToString();
            dropDownState.Items.FindByValue(table.Rows[0]["projectState"].ToString()).Selected = true;

            string queryProjectNames = "SELECT projectID, projectName FROM Project";
            tableProjectNames = db.AdminGetAllUsers(queryProjectNames);
            tableProjectNames.Rows.InsertAt(tableProjectNames.NewRow(), 0);
            ddlSubProject.DataSource = tableProjectNames;
            ddlSubProject.DataBind();

            if (!table.Rows[0]["parentProjectID"].ToString().Equals(""))
            {
                if (Convert.ToInt32(table.Rows[0]["parentProjectID"].ToString()) != 0)
                    ddlSubProject.Items.FindByValue(table.Rows[0]["parentProjectID"].ToString()).Selected = true;
            }

            string queryTeams = "SELECT teamName, teamID FROM Team";
            tableTeamNames = db.AdminGetAllUsers(queryTeams);
            ddlTeam.DataSource = tableTeamNames;
            ddlTeam.DataBind();

            if (!table.Rows[0]["teamID"].ToString().Equals(""))
            {
                ddlTeam.Items.FindByValue(table.Rows[0]["teamID"].ToString()).Selected = true;
            }
        }
        /// <summary>
        /// Kjøres ved lagring av endringer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubProject.SelectedIndex == 0)
                    queryUpdate = String.Format("UPDATE Project SET projectName = '{0}', projectDescription = '{1}', projectState = {2}, parentProjectID = 0, teamID = {3} WHERE projectID = {4}", tbProjectName.Text, tbProjectDescription.Text, dropDownState.SelectedValue, ddlTeam.SelectedValue, table.Rows[0]["projectID"].ToString());
                else
                    queryUpdate = String.Format("UPDATE Project SET projectName = '{0}', projectDescription = '{1}', projectState = {2}, parentProjectID = {3}, teamID = {4} WHERE projectID = {5}", tbProjectName.Text, tbProjectDescription.Text, dropDownState.SelectedValue, ddlSubProject.SelectedValue, ddlTeam.SelectedValue, table.Rows[0]["projectID"].ToString());

                db.InsertDeleteUpdate(queryUpdate);
                lblMessageOK.Text = "Prosjektet er oppdatert";
            }
            catch (Exception ex)
            {
                lblMessageOK.Text = "Noe gikk galt: " + ex.Message;
            }
        }
        
    }
}