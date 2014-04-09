using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class VisFase : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private int phaseID = 1; // hardcoded phase id, needs to be referred
        private int projectID = 1; // hardcoded phase id, needs to be referred        
        private DataTable dataTable = new DataTable();
        private DataTable hourTable = new DataTable();
        private DataTable listTable = new DataTable();
        private DataTable countTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateBasicInfo();
            PopulateHoursAndFinishedTasks();
            FillGridView();
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