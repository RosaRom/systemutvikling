using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

///
/// VisFase.aspx.cs av Henning Fredriksen
/// SysUt14Gr1 - Systemutvikling - Vår 2014
///
/// Lar deg velge blandt fasene på innlogget prosjekt og viser fram informasjon om valgt fase, noe som inkluderer:
/// navn, beskrivelse, fra-til dato, totale timer brukt/allokert, ferdige tasks, uferdige tasks, oversikt over tasks
/// og en burn-up chart for den fasen.
/// 


namespace Adminsiden
{
    public partial class VisFase : System.Web.UI.Page
    {
        private int phaseID;
        private int projectID;

        private DBConnect db = new DBConnect();
        private DataTable dataTable = new DataTable();
        private DataTable hourTable = new DataTable();
        private DataTable listTable = new DataTable();
        private DataTable countTable = new DataTable();
        private DataTable chartTable = new DataTable();
        private DataTable phaseDateToFromTable = new DataTable();
        private DataTable yAxis2Table = new DataTable();

        private Legend legend = new Legend("Legend");

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
        /// Sjekker om bruker er logget inn som bruker, teamleder eller prosjektansvarlig via session når formen loades,
        /// og kjører metodene som genererer / henter fra informasjon til feltene i .aspx. Redirecter til login hvis
        /// det ikke er en gyldig brukertype.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            string session = (string)Session["userLoggedIn"];

            if (session == "teamMember" || session == "teamLeader" || session == "projectManager")
            {
                if (!Page.IsPostBack)
                {

                    PopulateFaseValg();
                    ddlFaseValg.SelectedIndex = 0;
                    phaseID = Convert.ToInt32(ddlFaseValg.SelectedValue);
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            }         
        }

        /// <summary>
        /// Fyller dropdownbox til fasevalg, og populater første fase i lista
        /// </summary>
         public void PopulateFaseValg()
        {


            projectID = Convert.ToInt16(Session["projectID"]);


            string query = String.Format("SELECT * FROM Fase WHERE projectID = {0}", projectID);
            ddlFaseValg.DataSource = db.getAll(query);
            ddlFaseValg.DataTextField = "phaseName";
            ddlFaseValg.DataValueField = "PhaseID";
            ddlFaseValg.DataBind();
            PopulateBasicInfo();
            PopulateHoursAndFinishedTasks();
            FillGridView();
            PopulateChart();            
        }

        /// <summary>
        /// Repopulater feltene i form når en annen fase blir valgt i dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateBasicInfo();
            PopulateHoursAndFinishedTasks();
            FillGridView();
            PopulateChart();
        }
        
        /// <summary>
        /// Genererer burn-up chart for valgt fase
        /// </summary>
        public void PopulateChart()
        {            
            double usedHours = 0;
            double allocatedHours = 0;
            DateTime dateFrom = new DateTime();
            DateTime dateTo = new DateTime();

            phaseID = Convert.ToInt32(ddlFaseValg.SelectedValue);
            projectID = Convert.ToInt16(Session["projectID"]);

            // chart-properties
            this.phaseChart.ChartAreas["ChartArea1"].AxisX.Interval = 1; // gir hver dag en label
            this.phaseChart.Series["Brukte timer"].BorderWidth = 3; // forandrer bredden til "brukte timer" linja
            this.phaseChart.Series["Allokerte timer"].BorderWidth = 3; // forandrer bredden til "allokerte timer" linja
            this.phaseChart.Series["Allokerte timer"].Color = System.Drawing.Color.Red; // setter fargen til "allokerte timer" linja til rød
            this.phaseChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45; // setter vinkelen til labels
            this.phaseChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            if (this.phaseChart.Legends.Count == 0)
            {
                this.phaseChart.Legends.Add(legend); // legger et Legend-objekt til chart
            }
            this.phaseChart.Legends["Legend"].Enabled = true; // 
            projectID = Convert.ToInt16(Session["projectID"]);

            // queries + datatables
            string query = String.Format("SELECT * FROM TimeSheet WHERE taskID IN (SELECT taskID FROM Task WHERE phaseID = {0}) AND state = 1", phaseID);
            chartTable = db.getAll(query);
            string query2 = String.Format("SELECT phaseFromDate, phaseToDate FROM Fase WHERE phaseID = {0}", phaseID);
            phaseDateToFromTable = db.getAll(query2);
            string query3 = String.Format("SELECT hoursAllocated FROM Task WHERE phaseID = {0}", phaseID);
            yAxis2Table = db.getAll(query3);

            // sum av allokerte timer for alle tasks under valgt fase
            for (int i = 0; i < yAxis2Table.Rows.Count; i++)
            {
                allocatedHours += Convert.ToDouble(yAxis2Table.Rows[i]["hoursAllocated"]);
            }

            // finner startdato og sluttdato for valgt fase
            var startDate = Convert.ToDateTime(phaseDateToFromTable.Rows[0]["phaseFromDate"]);
            var endDate = Convert.ToDateTime(phaseDateToFromTable.Rows[0]["phaseToDate"]);

            // genererer en liste av DateTime objects, fra startDate til endDate av valgt fase, en for hver dag
            List<DateTime> range = Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(i => startDate.AddDays(i))
                .ToList();

            // populater grafen dynamisk, med et antall datapunkter lik antall dager i valgt fase, og legger dem til grafen.
            // den ytre løkka går igjennom hver dag/datapunkt og den indre går igjennom alle tasks på den dagen.
            foreach (var d in range) // går igjennom hver dag i fasen
            {
                for (int i = 0; i < chartTable.Rows.Count; i++) // går igjennom alle timeregistreringene
                {
                    if (DateTime.Compare(d.Date, Convert.ToDateTime(chartTable.Rows[i]["start"]).Date) == 1)
                    {
                        dateFrom = Convert.ToDateTime(chartTable.Rows[i]["start"]);
                        dateTo = Convert.ToDateTime(chartTable.Rows[i]["stop"]);
                        usedHours += (dateTo - dateFrom).Hours + ((double)(dateTo - dateFrom).Minutes * 1 / 60);
                    }
                }
                this.phaseChart.Series["Brukte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), usedHours); // legger til et punkt på "brukte timer" linja
                this.phaseChart.Series["Allokerte timer"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), allocatedHours); // legger til en punkt på "allokerte timer" linja
                usedHours = 0.0;
            }
        }

        /// <summary>
        /// populater lbPhaseName, lbDateFrom, lbDateTo, lbDescription
        /// </summary>
        public void PopulateBasicInfo()
        {
            phaseID = Convert.ToInt32(ddlFaseValg.SelectedValue);
            projectID = Convert.ToInt16(Session["projectID"]);

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

        /// <summary>
        /// Populater lbHoursUsed, lbHoursAllocated, lbFinishedTaskNum and lbUnfinishedTaskNum
        /// </summary>
        public void PopulateHoursAndFinishedTasks()
        {
            phaseID = Convert.ToInt32(ddlFaseValg.SelectedValue);
            projectID = Convert.ToInt16(Session["projectID"]);

            String query = String.Format("SELECT * FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0}", phaseID, projectID);
            String countRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state != 0", phaseID, projectID);
            String countCompletedRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state = 2", phaseID, projectID);

            // teller antall rader i resultatsettet
            int countRows = 0;
            countTable = db.getAll(countRowsQuery);
            countRows += Convert.ToInt32(countTable.Rows[0]["COUNT(*)"]);

            // sum av alle brukte timer for alle tasks under denne fasen
            hourTable = db.getAll(query);            
            int hoursUsed = 0;            

            for (int i = 0; i < countRows; i++)
            {
                hoursUsed += Convert.ToInt32(hourTable.Rows[i]["hoursUsed"]);                  
            }  
            
            lbHoursUsed.Text = hoursUsed.ToString();

            // sum av alle allokerte timer for alle tasks under denne fasen
            int hoursAllocated = 0;

            for (int i = 0; i < countRows; i++)
            {
                hoursAllocated += Convert.ToInt32(hourTable.Rows[i]["hoursAllocated"]);
            }

            lbHoursAllocated.Text = hoursAllocated.ToString();

            // teller rader med Task.state = 2, altså en task som er merket som ferdig
            int completedRows = 0;
            countTable = db.getAll(countCompletedRowsQuery);
            
            completedRows += Convert.ToInt32(countTable.Rows[0]["COUNT(*)"]);
            lbFinishedTaskNum.Text = completedRows.ToString();

            lbUnfinishedTaskNum.Text = (countRows - completedRows).ToString();
        }

        /// <summary>
        /// Fyller gridview med en liste av alle tasks under valgt fase
        /// </summary>
        public void FillGridView()
        {
            phaseID = Convert.ToInt32(ddlFaseValg.SelectedValue);
            projectID = Convert.ToInt16(Session["projectID"]);

            string queryActive = String.Format("SELECT productBacklogID \"BacklogID\", taskName \"Tasknavn\", priority \"Prioritet\", description \"Beskrivelse\"," +
                " hoursUsed \"Brukte timer\", hoursAllocated \"Allokerte timer\", state \"Status\"" +
                " FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state != 0", phaseID, projectID);
            
            listTable = db.AdminGetAllUsers(queryActive);
            ViewState["table"] = listTable;

            gvTaskList.DataSource = listTable;
            gvTaskList.DataBind();

            // teller rader som skal være med i gridview
            String countRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state != 0", phaseID, projectID);

            int countRows = 0;
            countTable = db.getAll(countRowsQuery);
            countRows += Convert.ToInt32(countTable.Rows[0]["COUNT(*)"]);            

            String prio1 = "Høy";
            String prio2 = "Mid";
            String prio3 = "Lav";

            // bytter ut prioritet som er en int i db med en beskrivelse (høy/mid/lav)
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
        
            // bytter ut state som er en int i db med en beskrivelse (inaktiv/aktiv/ferdig), samt fargekoder cellen
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