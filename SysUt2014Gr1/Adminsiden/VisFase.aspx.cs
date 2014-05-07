using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace Adminsiden
{
    public partial class VisFase : System.Web.UI.Page
    {
        private int phaseID = 1; // hardcoded phase id, needs to be referred
        private int projectID = 1; // hardcoded phase id, needs to be referred

        private DBConnect db = new DBConnect();
        private DataTable dataTable = new DataTable();
        private DataTable hourTable = new DataTable();
        private DataTable listTable = new DataTable();
        private DataTable countTable = new DataTable();
        private DataTable chartTable = new DataTable();
        private DataTable phaseDateToFromTable = new DataTable();
        private DataTable yAxis2Table = new DataTable();

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

            if (session == "teamMember" || session == "teamLeader" || session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
//                    PopulateBasicInfo();
//                    PopulateHoursAndFinishedTasks();
//                    FillGridView();
//                    PopulateChart();
                    PopulateFaseValg();
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
         
        }

        // populates combobox with phase selection
        public void PopulateFaseValg()
        {
            string query = String.Format("SELECT * FROM Fase WHERE projectID = {0}", projectID);
            ddlFaseValg.DataSource = db.getAll(query);
            ddlFaseValg.DataTextField = "phaseName";
            ddlFaseValg.DataValueField = "PhaseID";
            ddlFaseValg.Items.Insert(0, new ListItem("<Velg fase>", "0"));
            ddlFaseValg.DataBind();            
        }

        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlFaseValg.SelectedValue) != 0)
                phaseID = Convert.ToInt32(ddlFaseValg.SelectedValue);

            PopulateBasicInfo();
            PopulateHoursAndFinishedTasks();
            FillGridView();
            PopulateChart();
        }
        
        public void PopulateChart()
        {            
            double usedHours = 0;
            double allocatedHours = 0;
            DateTime dateFrom = new DateTime();
            DateTime dateTo = new DateTime();

            // chart-properties
            this.phaseChart.ChartAreas["ChartArea1"].AxisX.Interval = 1; // makes sure each point has a label
            this.phaseChart.Series["Brukte timer"].BorderWidth = 3; // changes the width of the burn-up-line
            this.phaseChart.Series["Allokerte timer"].BorderWidth = 3; // changes the width of the burn-up-ceiling
            this.phaseChart.Series["Allokerte timer"].Color = System.Drawing.Color.Red; // changes the color of the burn-up-ceiling to red
            this.phaseChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;
            this.phaseChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            this.phaseChart.Legends.Add(new Legend("Legend"));
            this.phaseChart.Legends["Legend"].Enabled = true;

            //queries + datatables
            string query = String.Format("SELECT * FROM TimeSheet WHERE projectID = {0} AND state = 1", projectID);
            chartTable = db.getAll(query);
            string query2 = String.Format("SELECT phaseFromDate, phaseToDate FROM Fase WHERE phaseID = {0}", phaseID);
            phaseDateToFromTable = db.getAll(query2);
            string query3 = String.Format("SELECT hoursAllocated FROM Task WHERE phaseID = {0}", phaseID);
            yAxis2Table = db.getAll(query3);

            // counts allocated hours from all the tasks from this phase
            for (int i = 0; i < yAxis2Table.Rows.Count; i++)
            {
                allocatedHours += Convert.ToDouble(yAxis2Table.Rows[i]["hoursAllocated"]);
            }

            // gets startdate and enddate of the phase
            var startDate = Convert.ToDateTime(phaseDateToFromTable.Rows[0]["phaseFromDate"]);
            var endDate = Convert.ToDateTime(phaseDateToFromTable.Rows[0]["phaseToDate"]);

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
                this.phaseChart.Series["Brukte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), usedHours);
                this.phaseChart.Series["Allokerte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), allocatedHours);
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

        // fills lbPhaseName, lbDateFrom, lbDateTo, lbDescription
        public void PopulateBasicInfo()
        {            
            String query = String.Format("SELECT * FROM Fase WHERE phaseID = '{0}'", phaseID);
            dataTable = db.getAll(query);
            String phaseName = Convert.ToString(dataTable.Rows[0]["phaseName"]);            
            DateTime dateFrom = Convert.ToDateTime(dataTable.Rows[0]["phaseFromDate"]);
            DateTime dateTo = Convert.ToDateTime(dataTable.Rows[0]["phaseToDate"]);
            String description = Convert.ToString(dataTable.Rows[0]["phaseDescription"]);            
            
            lbPhaseName.Text = phaseName.ToUpper();            
            lbDateFrom.Text = dateFrom.ToShortDateString();
            lbDateTo.Text = dateTo.ToShortDateString();
            lbDescription.Text = description;
        }

        // fills lbHoursUsed, lbHoursAllocated, lbFinishedTaskNum and lbUnfinishedTaskNum
        public void PopulateHoursAndFinishedTasks()
        {                    
            String query = String.Format("SELECT * FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0}", phaseID, projectID);
            String countRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state != 0", phaseID, projectID);
            String countCompletedRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state = 2", phaseID, projectID);

            // counts returned # of rows selected, not including tasks with Task.state = 0
            int countRows = 0;
            countTable = db.getAll(countRowsQuery);
            countRows += Convert.ToInt32(countTable.Rows[0]["COUNT(*)"]);            
            
            // counts used hours for all selected tasks from db
            hourTable = db.getAll(query);            
            int hoursUsed = 0;            

            for (int i = 0; i < countRows; i++)
            {
                hoursUsed += Convert.ToInt32(hourTable.Rows[i]["hoursUsed"]);                  
            }  
            
            lbHoursUsed.Text = hoursUsed.ToString();

            // counts allocated hours for all selected tasks from db
            int hoursAllocated = 0;

            for (int i = 0; i < countRows; i++)
            {
                hoursAllocated += Convert.ToInt32(hourTable.Rows[i]["hoursAllocated"]);
            }

            lbHoursAllocated.Text = hoursAllocated.ToString();

            // counts rows with Task.state = 2, meaning a task marked as completed
            int completedRows = 0;
            countTable = db.getAll(countCompletedRowsQuery);
            
            completedRows += Convert.ToInt32(countTable.Rows[0]["COUNT(*)"]);
            lbFinishedTaskNum.Text = completedRows.ToString();

            lbUnfinishedTaskNum.Text = (countRows - completedRows).ToString();
        }

        public void FillGridView()
        {
            string queryActive = String.Format("SELECT productBacklogID \"BacklogID\", taskName \"Tasknavn\", priority \"Prioritet\", description \"Beskrivelse\"," +
                " hoursUsed \"Brukte timer\", hoursAllocated \"Allokerte timer\", state \"Status\"" +
                " FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state != 0", phaseID, projectID);
            
            listTable = db.AdminGetAllUsers(queryActive);
            ViewState["table"] = listTable;

            gvTaskList.DataSource = listTable;
            gvTaskList.DataBind();

            // replaces the int in Task.state
            String countRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state != 0", phaseID, projectID);

            int countRows = 0;
            countTable = db.getAll(countRowsQuery);
            countRows += Convert.ToInt32(countTable.Rows[0]["COUNT(*)"]);            

            String prio1 = "Høy";
            String prio2 = "Mid";
            String prio3 = "Lav";

            for (int i = 0; i < countRows; i++)
            {
                int prioritet = Convert.ToInt32(listTable.Rows[i]["Prioritet"]);

                switch (prioritet)
                {
                    case 1:
                        gvTaskList.Rows[i].Cells[2].Text = prio1;
                        break;
                    case 2:
                        gvTaskList.Rows[i].Cells[2].Text = prio2;
                        break;
                    case 3:
                        gvTaskList.Rows[i].Cells[2].Text = prio3;
                        break;
                }
            }
        

            for (int i = 0; i < countRows; i++)
            {
                int status = Convert.ToInt32(listTable.Rows[i]["Status"]);

                switch (status)
                {
                    case 0:
                        gvTaskList.Rows[i].Cells[6].BackColor = System.Drawing.Color.White;
                        gvTaskList.Rows[i].Cells[6].Text = "Inaktiv";
                        break;
                    case 1:
                        gvTaskList.Rows[i].Cells[6].BackColor = System.Drawing.Color.Yellow;
                        gvTaskList.Rows[i].Cells[6].Text = "Aktiv";
                        break;
                    case 2:
                        gvTaskList.Rows[i].Cells[6].BackColor = System.Drawing.Color.LawnGreen;
                        gvTaskList.Rows[i].Cells[6].Text = "Ferdig";
                        break;
                }
            } 
        }
    }
}