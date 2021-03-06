﻿using Adminsiden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace Adminsiden
{
    public partial class Bruker : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// Bruker.apsx.cs av Tommy Langhelle
        /// SysUt14Gr1 - Systemutvikling - Vår 2014
        /// 
        /// Timeregistreringssiden for brukerkontoer med status "bruker"
        /// 
        /// </summary>
        private DBConnect db = new DBConnect();
        private int TaskID;
        private int WorkplaceID;
        List<String> projectList = new List<string>();

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

            if (session == "teamMember")
            {
                if(Page.IsPostBack && ddl_hour_from.SelectedValue != "00")
                {
                    fillTimeToSelectDLL();
                    GetTasks();
                    
                }
                if (!Page.IsPostBack)
                {
                    
                    getWorkplace();
                    fillTimeSelectDDL();
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
           
        }
        private void fillTimeSelectDDL() //Fyller dropdownlister med timer og minutter
        {
            if (ddl_hour_from.Items.Count == 0) //Fyller timer og minutter i "fra" dropdowns
            {
                for (int i = 0; i < 10; i++)
                {
                    ddl_hour_from.Items.Add("0" + i);
                }
                for (int i = 10; i < 24; i++)
                {
                    ddl_hour_from.Items.Add("" + i);
                }
            }
            if (ddl_min_from.Items.Count == 0)
            {
                for (int i = 0; i < 10; i += 15)
                {
                    ddl_min_from.Items.Add("0" + i);
                }
                for (int i = 15; i < 60; i += 15)
                {
                    ddl_min_from.Items.Add("" + i);
                }
            }
        }

        /// <summary>
        /// Denne metoden blir kjørt først når både starttime og minutter er valgt
        /// </summary>
        private void fillTimeToSelectDLL()
        {

            int from = 1;
            int hourTo = 0;
            int minTo = 0;

            if (ddl_hour_to.Items.Count != 0) //tar vare på registrerte til-klokkeslett
            {
                from = Convert.ToInt16(ddl_hour_from.SelectedValue);
                hourTo = Convert.ToInt16(ddl_hour_to.SelectedValue);
                minTo = Convert.ToInt16(ddl_min_to.SelectedValue);
                
                ddl_min_to.Items.Clear();
                ddl_hour_to.Items.Clear();
            }

            int hourFrom = Convert.ToInt16(ddl_hour_from.Text);

            int i;

            for (i = hourFrom; i < 10; i++) //Fyller timer dropdown
            {
                ddl_hour_to.Items.Add("0" + i);
            }
            for (int k = i; k >= 9 && k < 24; k++)
            {
                ddl_hour_to.Items.Add("" + k);
            }

            for (int j = 0; j < 10; j += 15) //Fyller minutter dropdown
            {
                ddl_min_to.Items.Add("0" + j);
            }
            for (int j = 15; j < 60; j += 15)
            {
                ddl_min_to.Items.Add("" + j);
            }

            if (from >= hourTo) //bestemmer om registrert til-tid kan brukes, eller om dette blir ugyldig med den nye ny fra-tiden

            {
                ddl_hour_to.SelectedValue = ddl_hour_from.SelectedValue;
                ddl_min_to.SelectedValue = ddl_min_from.SelectedValue;
            }
            else
            {
                if(hourTo < 10)
                    ddl_hour_to.SelectedValue = Convert.ToString("0" + hourTo);
                else
                    ddl_hour_to.SelectedValue = Convert.ToString(hourTo);

                if(minTo < 10)
                    ddl_min_to.SelectedValue = Convert.ToString("0" + minTo);
                else
                    ddl_min_to.SelectedValue = Convert.ToString(minTo);
            }
        }
        private void GetTasks() //Fyller dropdown med tasks

        {
            DataTable dt = new DataTable();
            int phaseID = 0;
            int projectID = Convert.ToInt16(Session["projectID"]);
            string query1 = "SELECT * FROM Fase WHERE projectID =" + projectID;
            dt = db.getAll(query1);

            DateTime dateFrom = Convert.ToDateTime(TB_Date.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dt.Rows[i][3]) <= dateFrom && Convert.ToDateTime(dt.Rows[i][4]) >= dateFrom)
                {
                    phaseID = Convert.ToInt16(dt.Rows[i][0]);
                }
            }

            string query = "SELECT * FROM Task WHERE phaseID =" + phaseID;
            taskName.DataSource = db.getAll(query);
            taskName.DataValueField = "taskID";
            taskName.DataTextField = "taskName";
            taskName.Items.Insert(0, new ListItem("<Velg task>", "0"));
            taskName.DataBind();
        }
        private void getWorkplace() //Fyller dropdown med plasser å jobbe fra
        {
            string query = "SELECT * FROM Workplace";
            workPlace.DataSource = db.getAll(query);
            workPlace.DataValueField = "workplaceID";
            workPlace.DataTextField = "workplace";
            workPlace.Items.Insert(0, new ListItem("<Velg arbeidsplass>", "0"));
            workPlace.DataBind();
        }

        protected void taskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskID = Convert.ToInt32(taskName.SelectedValue);
        }
        protected void workPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            WorkplaceID = Convert.ToInt32(workPlace.SelectedValue);
        }
        /// <summary>
        /// Metoden blir kjørt når "ok" knappen trykkes, og info fra alle felter blir formatert og sjekket.
        /// Om all input er gyldig blir informasjon sendt til databasen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string userDescription = TxtArea_userComment.Text;
            TaskID = Convert.ToInt32(taskName.SelectedValue);

            int tidFra_min = Convert.ToInt32(ddl_min_from.SelectedItem.ToString());
            int tidFra_hour = Convert.ToInt32(ddl_hour_from.SelectedItem.ToString());
            DateTime dateFrom = Convert.ToDateTime(TB_Date.Text);
            TimeSpan timespanFrom = new TimeSpan(tidFra_hour, tidFra_min, 0);
            dateFrom = dateFrom.Add(timespanFrom);

            string dateFromFormated = dateFrom.ToString("yyyy-MM-dd HH:mm:ss");

            int tidTil_min = Convert.ToInt32(ddl_min_to.SelectedItem.ToString());
            int tidTil_hour = Convert.ToInt32(ddl_hour_to.SelectedItem.ToString());
            DateTime dateTo = Convert.ToDateTime(TB_Date.Text);
            TimeSpan timespanTo = new TimeSpan(tidTil_hour, tidTil_min, 0);
            dateTo = dateTo.Add(timespanTo);

            string dateToFormated = dateTo.ToString("yyyy-MM-dd HH:mm:ss");

            int projectID = Convert.ToInt32(Session["projectID"]);
            int userID = Convert.ToInt32(Session["userID"]);
            int state = 1;

            double antallTimer = Convert.ToInt16(ddl_hour_to.SelectedValue) - Convert.ToInt16(ddl_hour_from.SelectedValue);
            int antallMinutt = Convert.ToInt16(ddl_min_to.SelectedValue) - Convert.ToInt16(ddl_min_from.SelectedValue);
            double antallMinuttDouble = 0;

            switch(antallMinutt)
            {
                case 00:
                    antallMinuttDouble = 0;
                    break;
                case 15:
                    antallMinuttDouble = 0.25;
                    break;
                case 30:
                    antallMinuttDouble = 0.50;
                    break;
                case 45:
                    antallMinuttDouble = 0.75;
                    break;
            }
            double tidBrukt = antallTimer + antallMinuttDouble;

            //Sjekker at alle felter er fylt ut.
            if (dateFromFormated != null && dateToFormated != null && userID != 0 && TaskID != 0 && WorkplaceID != 0 && projectID != 0)
            {
                int permissionState;
                DataTable dt = new DataTable();

                dt = db.getAll("SELECT hoursUsed, hoursAllocated FROM Task where taskID=" + TaskID);

                double hoursAllocated = Convert.ToDouble(dt.Rows[0]["hoursAllocated"].ToString());
                double hoursUsed = Convert.ToDouble(dt.Rows[0]["hoursUsed"].ToString());

                //Sjekker at fasen tasken ligger under har nok ledige timer til å foreta registreringen.
                if (hoursUsed + tidBrukt < hoursAllocated)
                {
                    if (dateFrom > DateTime.Now.AddDays(1) || dateFrom < DateTime.Now.AddDays(-1))
                    {
                        permissionState = 1;
                        label_result.Text = "Du har sendt prøvd å registrere timer utenfor +- 24t. Timeantallet er under godkjenning";
                        label_result.Visible = true;
                    }

                    else
                    {
                        permissionState = 2;
                        label_result.Text = "Registreringen ble fullført";
                        label_result.Visible = true;
                    }

                    db.InsertTimeSheet(dateFromFormated, dateToFormated, userID, TaskID, userDescription, WorkplaceID, state, projectID, permissionState);
                    db.InsertDeleteUpdate("UPDATE Task SET hoursUsed = hoursUsed + " + tidBrukt + " WHERE taskID = " + TaskID);
                }
                else
                { 
                    label_result.Text = "Maks " + (hoursAllocated - hoursUsed) + " timer kan registreres på denne tasken";
                    label_result.Visible = true;
                }            
            }
            else
            {
                label_result.Text = "Noe gikk gale, vennligst prøv igjen";
                label_result.Visible = true;
            }
        }
    }
}