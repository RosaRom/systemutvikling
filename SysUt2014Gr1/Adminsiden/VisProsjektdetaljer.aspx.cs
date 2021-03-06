﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

///
/// VisProsjektdetaljer.aspx.cs av Tommy Langhelle (PopulateChart() av Henning Fredriksen)
/// SysUt14Gr1 - Systemutvikling - Vår 2014
///
/// PopulateChart() genererer en burn-up chart for aktivt prosjekt.
/// 

namespace Adminsiden
{
    public partial class VisTeam : System.Web.UI.Page
    {
        int projectID;
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();
        DataTable dt_users = new DataTable();
        DataTable dt_tasks = new DataTable();

        // datatables for chart
        DataTable chartTable = new DataTable();
        DataTable phaseDateToFromTable = new DataTable();
        DataTable yAxis2Table = new DataTable();

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
        /// Genererer burn-up chart for aktivt prosjekt
        /// </summary>
        public void PopulateChart()
        {
            double usedHours = 0;
            double allocatedHours = 0;
            DateTime dateFrom = new DateTime();
            DateTime dateTo = new DateTime();

            // chart-properties
            this.projectChart.ChartAreas["ChartArea1"].AxisX.Interval = 7; // // gir hver uke en label
            this.projectChart.ChartAreas["ChartArea1"].AxisY.Name = "test";
            this.projectChart.Series["Brukte timer"].BorderWidth = 3; // forandrer bredden til "brukte timer" linja
            this.projectChart.Series["Allokerte timer"].BorderWidth = 3; // forandrer bredden til "allokerte timer" linja
            this.projectChart.Series["Allokerte timer"].Color = System.Drawing.Color.Red; // setter fargen til "allokerte timer" linja til rød
            this.projectChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45; // setter vinkelen til labels
            this.projectChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            this.projectChart.Legends.Add(new Legend("Legend"));  // legger et Legend-objekt til chart
            this.projectChart.Legends["Legend"].Enabled = true;

            //queries + datatable-assignment
            string query = String.Format("SELECT * FROM TimeSheet WHERE projectID = {0}", projectID);
            chartTable = db.getAll(query);
            string query2 = String.Format("SELECT phaseFromDate, phaseToDate FROM Fase WHERE projectID = {0}", projectID);
            phaseDateToFromTable = db.getAll(query2);
            string query3 = String.Format("SELECT hoursAllocated FROM Task WHERE phaseID IN (SELECT phaseID FROM Fase WHERE projectID = {0})", projectID);
            yAxis2Table = db.getAll(query3);

            // sum av allokerte timer for alle tasks under aktivt prosjekt
            for (int i = 0; i < yAxis2Table.Rows.Count; i++)
            {
                allocatedHours += Convert.ToDouble(yAxis2Table.Rows[i]["hoursAllocated"]);
            }

            // finner startdato og sluttdato for aktivt prosjekt ut ifra fasedatoene
            DataView dv = phaseDateToFromTable.DefaultView;
            dv.Sort = "phaseFromDate ASC";
            DataTable sortedDT = dv.ToTable();
            var startDate = Convert.ToDateTime(sortedDT.Rows[0]["phaseFromDate"]);
            dv.Sort = "phaseToDate ASC";
            sortedDT = dv.ToTable();
            var endDate = Convert.ToDateTime(sortedDT.Rows[sortedDT.Rows.Count - 1]["phaseToDate"]);

            /// genererer en liste av DateTime objects, fra startDate til endDate av aktivt prosjekt, en for hver dag
            List<DateTime> range = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(i => startDate.AddDays(i))
                .ToList();

            // populater grafen dynamisk, med et antall datapunkter lik antall dager i aktivt prosjekt, og legger dem til grafen.
            // den ytre løkka går igjennom hver dag/datapunkt og den indre går igjennom alle tasks på den dagen.
            foreach (var d in range)
            {
                for (int i = 0; i < chartTable.Rows.Count; i++)
                {
                    if (DateTime.Compare(d, Convert.ToDateTime(chartTable.Rows[i]["start"])) == 1)
                    {
                        dateFrom = Convert.ToDateTime(chartTable.Rows[i]["start"]);
                        dateTo = Convert.ToDateTime(chartTable.Rows[i]["stop"]);
                        usedHours += (dateTo - dateFrom).Hours + ((double)(dateTo - dateFrom).Minutes * 1 / 60);
                    }
                }

                this.projectChart.Series["Brukte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), usedHours);
                this.projectChart.Series["Allokerte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), allocatedHours);
                usedHours = 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            projectID = Convert.ToInt16(Session["projectID"]);

            getFromToDateProject();

            string session = (string)Session["userLoggedIn"];

            if (session == "teamMember" || session == "teamLeader" || session == "projectManager")
            {
                  PopulateChart(); // populates the chart            

            string query = "SELECT * FROM Project, Team WHERE Project.projectID = " + projectID + " AND Team.teamID = Project.teamID";
            dt = db.getAll(query);
            int teamID = Convert.ToInt16(dt.Rows[0]["teamID"]);
            int phaseID = Convert.ToInt16(Session["phaseID"]);
            //string query1 = "SELECT taskID, taskName, hoursUsed, HoursAllocated FROM Task WHERE TaskCategoryID IN (SELECT TaskCategoryID FROM TaskCategory WHERE ProjectID = " + projectID + ")";
            string query3 = "SELECT taskID, taskName, hoursUsed, HoursAllocated FROM Task WHERE phaseID =" + phaseID;
            dt_tasks = db.getAll(query3);

            string query2 = "SELECT  CONCAT (firstname, ' ',  surname) AS FullName FROM User WHERE teamID =" + teamID;
            dt_users = db.getAll(query2);

            if (dt != null && dt.Rows.Count > 0)
            {
                string projectname = Convert.ToString(dt.Rows[0]["projectName"]);
                string description = Convert.ToString(dt.Rows[0]["ProjectDescription"]);
                string teamName = Convert.ToString(dt.Rows[0]["teamName"]);

                Label_navn.Text = projectname;
                tb_desc.Text = description;
                Label_team.Text = teamName;

                ListView_team.DataSource = dt_users;
                ListView_team.DataBind();

                Listview_task.DataSource = dt_tasks;
                Listview_task.DataBind();
            }
            else
            {
                Label_warning.Text = "Noe har gått gale, vennligst velg prosjekt igjen.";
            }
        
    
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
        }
        void getFromToDateProject()
        {
            DataTable dt = new DataTable();
            int projectID = Convert.ToInt16(Session["projectID"]);
            string query = "SELECT * FROM Fase WHERE projectID =" + projectID;
            dt = db.getAll(query);

            DateTime LavesteDato = DateTime.Now;
            DateTime HøyesteDato= DateTime.Now;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dt.Rows[i][3]) < LavesteDato)
                {
                    LavesteDato = Convert.ToDateTime(dt.Rows[i][3]);
                }
                if(Convert.ToDateTime(dt.Rows[i][4]) > HøyesteDato)
                {
                    HøyesteDato = Convert.ToDateTime(dt.Rows[i][4]);
                }
            }


           Label_tidFra.Text = LavesteDato.ToString("dd-MMMM-yyyy");
           Label_tilTid.Text = HøyesteDato.ToString("dd-MMMM-yyyy");
            
        }
    }
}