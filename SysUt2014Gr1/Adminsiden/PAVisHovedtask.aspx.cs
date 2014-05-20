using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

///
/// PAVisHovedTask.aspx.cs av Henning Fredriksen
/// SysUt14Gr1 - Systemutvikling - Vår 2014
///
/// Enkel form som lar prosjektansvarlig vise navn og beskrivelse for hovedtask
/// 

namespace Adminsiden
{
    public partial class PAVisHovedtask : System.Web.UI.Page
    {
//        private int taskCategoryID = 19; // hardkodet, må byttes ut
        int projectID;
        
        private DBConnect db = new DBConnect();
        private DataTable dt = new DataTable();

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
        /// Sjekker om bruker er logget inn som prosjektansvarlig via session når formen loades,
        /// kjører så metoden som fyller dropdownlista med hovedkategorier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                GetTaskCategories();
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
        }

        /// <summary>
        /// Fyller dropdownliste med taskkategorier
        /// </summary>
        private void GetTaskCategories()
        {
            projectID = Convert.ToInt16(Session["projectID"]);
            string query = string.Format("SELECT * FROM TaskCategory WHERE projectID = {0}", projectID);
            ddlTaskCategory.DataSource = db.getAll(query);
            ddlTaskCategory.DataTextField = "taskCategoryName";
            ddlTaskCategory.DataValueField = "taskCategoryID";
//            ddlTaskCategory.Items.Insert(0, new ListItem("<Velg hovedtask>", "0")); //OBS! AppendDataBoundItems="true" i asp-kodene om dette skal funke!
            ddlTaskCategory.DataBind();
            PopulateFields();
        }

        /// <summary>
        /// Fyller label/tekstboks med navn og beskrivelse av valgt hovedtask
        /// </summary>
        public void PopulateFields()
        {
            projectID = Convert.ToInt16(Session["projectID"]);
            string query = String.Format("SELECT * FROM TaskCategory WHERE taskCategoryID = {0} AND projectID = {1}", ddlTaskCategory.SelectedValue, projectID);
            dt = db.getAll(query);
            lbTaskCategoryName.Text = dt.Rows[0]["taskCategoryName"].ToString();
            taTaskCategoryDesc.Text = dt.Rows[0]["taskCategoryDescription"].ToString();           
        }

        /// <summary>
        /// Fyller label/tekstboks med navn og beskrivelse av valgt hovedtask når hovedtask blir valgt i dropdownliste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTaskCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFields();
        }
    }
}