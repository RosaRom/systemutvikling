﻿using Adminsiden;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    /// <summary>
    /// 
    /// Prosjektvalg.apsx.cs av Tommy Langhelle
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Denne siden er den første som blir vist etter login, og her velger du prosjekt du vil registrere timer på, se grafer og fremgang over, eller redigere.
    /// </summary>
    /// 
    public partial class Prosjektvalg : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        string session;

        protected void Page_Load(object sender, EventArgs e)
        {

            session = (string)Session["userLoggedIn"];

            if (session == "teamMember" || session == "teamLeader" || session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    GetProject();
                }        
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }
        }
        /// <summary>
        /// Fyller Gridview med prosjekter fra databasen
        /// </summary>
        private void GetProject()
        {
            session = (string)Session["userLoggedIn"];

            DataTable dt = new DataTable();
            string query = "SELECT projectID, projectName, projectDescription FROM Project";
            dt = db.getAll(query);
            if(dt.Rows.Count == 0)
            {
                if (session == "teamMember" || session == "teamLeader")
                {
                    Label_ingenProsjekt.Text = "Det er ingen prosjekter å registrere timer på.";
                }
                else
                {
                    Server.Transfer("OpprettProsjekt.aspx", true);
                }
            }
            else
            {
                GridViewProject.DataSource = dt;
                GridViewProject.DataBind();
            }
            
        }

        /// <summary>
        /// Sender deg videre til riktig side avhengig av inlogget brukers status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int projectID = Convert.ToInt32(GridViewProject.Rows[index].Cells[1].Text);
            string projectNavn = Convert.ToString(GridViewProject.Rows[index].Cells[2].Text);
            Session["projectID"] = projectID;
            Session["projectNavn"] = projectNavn;
            

            if (session == "teamMember")
                Server.Transfer("Bruker.aspx", true);

            else if (session == "projectManager")
                Server.Transfer("PAAdministrerBrukere.aspx", true);

            else
                Server.Transfer("Teamleder.aspx", true);
        }

        protected void GridViewProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
  
    }
}