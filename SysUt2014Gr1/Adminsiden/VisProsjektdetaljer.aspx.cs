using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class VisTeam : System.Web.UI.Page
    {

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



        // har to problemer: 1) Viser ikke startdato på chart, kun dato for søndagen. 2) brukte timer er 1 dag forskjøvet
        public void PopulateChart()
        {
            double usedHours = 0;
            double allocatedHours = 0;
            DateTime dateFrom = new DateTime();
            DateTime dateTo = new DateTime();

            // chart-properties
            this.projectChart.ChartAreas["ChartArea1"].AxisX.Interval = 7; // makes sure each week has a label
            this.projectChart.ChartAreas["ChartArea1"].AxisY.Name = "test";
            this.projectChart.Series["Brukte timer"].BorderWidth = 3; // changes the width of the burn-up-line
            this.projectChart.Series["Allokerte timer"].BorderWidth = 3; // changes the width of the burn-up-ceiling
            this.projectChart.Series["Allokerte timer"].Color = System.Drawing.Color.Red; // changes the color of the burn-up-ceiling to red
            this.projectChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;
            this.projectChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            this.projectChart.Legends.Add(new Legend("Legend"));
            this.projectChart.Legends["Legend"].Enabled = true;

            //queries + datatable-assignment
            string query = String.Format("SELECT * FROM TimeSheet WHERE projectID = 1");
            chartTable = db.getAll(query);
            string query2 = String.Format("SELECT phaseFromDate, phaseToDate FROM Fase WHERE projectID = 1");
            phaseDateToFromTable = db.getAll(query2);
            string query3 = String.Format("SELECT hoursAllocated FROM Task WHERE phaseID IN (SELECT phaseID FROM Fase WHERE projectID = 1)");
            yAxis2Table = db.getAll(query3);

            // counts allocated hours from all the tasks from this project
            for (int i = 0; i < yAxis2Table.Rows.Count; i++)
            {
                allocatedHours += Convert.ToDouble(yAxis2Table.Rows[i]["hoursAllocated"]);
            }

            // gets startdate and enddate of the project
            var startDate = Convert.ToDateTime(phaseDateToFromTable.Rows[0]["phaseFromDate"]);
            var endDate = Convert.ToDateTime(phaseDateToFromTable.Rows[phaseDateToFromTable.Rows.Count - 1]["phaseToDate"]);

            // generates a list of DateTime objects, from startDate - endDate of the phase
            List<DateTime> range = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(i => startDate.AddDays(i))
                .ToList();

            // populates the x-axis dynamically, with a number of datapoints equal to the length of the phase (in days,
            // with how many hours are spent on tasks on that day
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
                //                DateTime temp = new DateTime();
                //                temp = d.AddDays(-1);
                this.projectChart.Series["Brukte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), usedHours);
                this.projectChart.Series["Allokerte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), allocatedHours);
                usedHours = 0;
            }
            /*           
                       this.phaseChart.Series["y-akse"].Points.AddXY("punkt1", 20);
                       this.phaseChart.Series["y-akse2"].Points.AddXY("punkt1", 59);

                       this.phaseChart.Series["y-akse"].Points.AddXY("punkt2", 30);
                       this.phaseChart.Series["y-akse2"].Points.AddXY("punkt2", 69);

                       this.phaseChart.Series["y-akse"].Points.AddXY("punkt3", 45);
                       this.phaseChart.Series["y-akse2"].Points.AddXY("punkt3", 76);
           */
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int projectID = Convert.ToInt16(Session["projectID"]);

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
    }
}