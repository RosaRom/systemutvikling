using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Adminsiden
{
    public partial class PARedigerFase : System.Web.UI.Page
    {
        private int projectID;
        private DataTable table = new DataTable();
        private DataTable projectNameTable = new DataTable();
        private DBConnect db = new DBConnect();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    projectID = Convert.ToInt16(Session["projectID"]);
                    GetAllPhases();
                }
                else
                {
                    projectID = Convert.ToInt16(Session["projectID"]);
                    table = (DataTable)ViewState["table"];
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }
        }

        /// <summary>
        /// Her lastet informasjonen om alle faser inn og navnene legges til i en dropdownliste
        /// </summary>
        private void GetAllPhases()
        {
            string query = "SELECT * FROM Fase WHERE projectID = " + projectID + " ORDER BY phaseToDate ASC";
            table = db.AdminGetAllUsers(query);
            //table.Rows.InsertAt(table.NewRow(), 0);
            ViewState["table"] = table;
            velgFase.DataSource = table;
            velgFase.DataBind();
        }

        /// <summary>
        /// Her legges all informasjon om en fase inn i tekstfeltene de tilhører
        /// </summary>
        private void PopulateFields()
        {
            int index = velgFase.SelectedIndex;
            tbPAPhasename.Text = table.Rows[index]["phaseName"].ToString();
            tbPADateFrom.Text = Convert.ToDateTime(table.Rows[index]["phaseFromDate"]).ToString("yyyy-MM-dd");
            tbPADateTo.Text = Convert.ToDateTime(table.Rows[index]["phaseToDate"]).ToString("yyyy-MM-dd");
            tbPADescription.Text = table.Rows[index]["phaseDescription"].ToString();
        }

        /// <summary>
        /// Fjerner all tekst fra tekstfeltene
        /// </summary>
        private void ResetFields()
        {
            tbPAPhasename.Text = "";
            tbPADateFrom.Text = "";
            tbPADateTo.Text = "";
            tbPADescription.Text = "";
            lbPAError.Text = "";
        }

        /// <summary>
        /// Metode som kjøres når det blir byttet fase i dropdownlisten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VelgFase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (velgFase.SelectedIndex == 0)
                ResetFields();
            else
            {
                PopulateFields();
                lbPAError.Text = "";
            }
        }

        /// <summary>
        /// Alt blir hentet ut fra tekstfeltene og alle forandringer gjort på fasen blir lagret 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (velgFase.SelectedIndex != 0)
            {
                int index = velgFase.SelectedIndex;
                string query = String.Format("UPDATE Fase SET phaseName = '{0}', phaseDescription = '{1}', phaseFromDate = '{2}', phaseToDate = '{3}' WHERE phaseID = {4}", tbPAPhasename.Text, tbPADescription.Text, tbPADateFrom.Text, tbPADateTo.Text, velgFase.SelectedValue.ToString());

                if (Convert.ToDateTime(tbPADateFrom.Text) > Convert.ToDateTime(tbPADateTo.Text))
                {
                    lbPAError.Text = "Til dato må være satt senere enn fra dato for en fase";
                }
                else
                {
                    try
                    {
                        //Her sjekkes det om sluttdatoen på siste fase i prosjektet er forandret, om den er det blir det lagd en rapport
                        if (Convert.ToDateTime(tbPADateTo.Text) != Convert.ToDateTime(table.Rows[index]["phaseToDate"]) && index == velgFase.Items.Count - 1)
                        {
                            string projectNameQuery = "SELECT projectName from Project where projectID = " + table.Rows[index]["projectID"].ToString();
                            projectNameTable = db.AdminGetAllUsers(projectNameQuery);

                            string deviationReport = String.Format("INSERT INTO deviationReport VALUES(null, 'sluttdato prosjekt forandret', 'Sluttdatoen på prosjektet \"{0}\" har blitt forandret fra {1} til {2}.', 0, 0, now())", projectNameTable.Rows[0]["projectName"].ToString(), Convert.ToDateTime(table.Rows[index]["phaseToDate"]).ToString("yyyy-MM-dd"), tbPADateTo.Text);
                            db.InsertDeleteUpdate(deviationReport);
                        }
                        db.InsertDeleteUpdate(query);
                        lbPAError.Text = ("Fasen er oppdatert");
                    
                        GetAllPhases();
                    }
                    catch(Exception ex)
                    {
                        lbPAError.Text = "Feil ved oppdatering: " + ex.Message;
                    }
                }
            }
        }
    }
}