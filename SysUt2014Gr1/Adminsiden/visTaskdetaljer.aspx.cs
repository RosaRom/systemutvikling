﻿using System;
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
    /// visTaskdetaljer.apsx.cs av Tommy Langhelle
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// Her får du detaljert oversikt over valgt fase. start-slutt dato, tilhørende prosjekt, registrerte timer/allokerte timer etc.
    /// </summary>
    /// 

    public partial class visTaskdetaljer : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();
        DataTable dt_project = new DataTable();

        /// <summary>
        /// Riktig masterpage blir bestemt ut i fra hvilken status innlogget bruker har.
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "teamMember" || session == "teamLeader" || session == "projectManager") //Sjekker om du har riktig status, ellers blir du sendt til login skjerm.
            {
                if (!Page.IsPostBack)
                {
                    int taskID = Convert.ToInt16(Request.QueryString["taskID"]);
                    
                    if(taskID == 0)
                        taskID = Convert.ToInt16(Session["taskID"]);

                    string query = "SELECT * FROM Task, Fase WHERE taskID = " + taskID + " AND Fase.phaseID = Task.phaseID";
                    dt = db.getAll(query);

                    int projectID = Convert.ToInt16(Session["projectID"]);

                    string query1 = "SELECT projectName FROM Project WHERE projectID =" + projectID;
                    dt_project = db.getAll(query1);

                    if (dt != null && dt.Rows.Count > 0) //Sjekker at dt inneholder info om tasks, for så å sende info ut til siden.
                    {
                        string taskname = Convert.ToString(dt.Rows[0]["taskName"]);
                        string taskDescription = Convert.ToString(dt.Rows[0]["description"]);
                        int hoursUsed = Convert.ToInt16(dt.Rows[0]["hoursUsed"]);
                        int hoursAllocated = Convert.ToInt16(dt.Rows[0]["hoursAllocated"]);
                        int priority = Convert.ToInt16(dt.Rows[0]["priority"]);
                        string backlogID = Convert.ToString(dt.Rows[0]["productBacklogID"]);
                
                        int parentTaskID = 0;
                        if(!dt.Rows[0]["parentTaskID"].ToString().Equals(""))
                            parentTaskID = Convert.ToInt16(dt.Rows[0]["parentTaskID"]);

                        int status = Convert.ToInt16(dt.Rows[0]["state"]);

                        if (parentTaskID != 0)
                        {
                            Session["taskID"] = parentTaskID;
                            Link_task.Text = "" + parentTaskID;
                        }
                        else
                            Label_undertask.Text = "Ingen foreldretask";

                        Label_navn.Text = taskname;
                        tb_desc.Text = taskDescription;
                        Label_backlogID.Text = backlogID;                
                
                        Label_progress.Text = hoursUsed + " timer / " + hoursAllocated + " timer";

                        int tidsavvik = hoursAllocated - hoursUsed;
                        Label_tidsavvik.Text = "avvik fra estimert tid: " + tidsavvik + " timer";

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

                        switch(status)
                        {
                            case 0: Label_status.Text = "Inaktiv";
                                Label_status.BackColor = System.Drawing.Color.White;
                                break;
                            case 1: Label_status.Text = "Aktiv";
                                Label_status.BackColor = System.Drawing.Color.Yellow;
                                break;
                            case 2: Label_status.Text = "Ferdig";
                                Label_status.BackColor = System.Drawing.Color.Green;
                                break;
                            default: Label_status.Text = "test";
                                break;
                        }

                        //Finner info for aktuell fase og viser dette på siden.
                        string fasename = Convert.ToString(dt.Rows[0]["phaseName"]);
                        string faseDescription = Convert.ToString(dt.Rows[0]["phaseDescription"]);
                        DateTime faseFrom = Convert.ToDateTime(dt.Rows[0]["phaseFromDate"]);
                        DateTime faseTo = Convert.ToDateTime(dt.Rows[0]["phaseToDate"]);

                        String faseFromFormated = faseFrom.ToString("dd-MMMM-yyyy");
                        String faseToFormated = faseTo.ToString("dd-MMMM-yyyy");

                        Label_faseNavn.Text = fasename;
                        tb_faseDesc.Text = faseDescription;
                        Label_faseTid.Text = "<b>fra:</b> " + faseFromFormated + "<br /><b>Til: </b>" + faseToFormated;

                        //prosjekt info
                        string projectName = Convert.ToString(dt_project.Rows[0]["projectName"]);
                    }     

                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }

        }
    }
}