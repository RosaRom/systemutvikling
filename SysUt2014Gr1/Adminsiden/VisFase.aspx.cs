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

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateBasicInfo();
            PopulateHoursAndFinishedTasks();
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
            
            lbPhaseName.Text = phaseName;
            lbDateFrom.Text = dateFrom.ToShortDateString();
            lbDateTo.Text = dateTo.ToShortDateString();
            lbDescription.Text = description;
        }

        // fills lbHoursUsed and lbHoursAllocated
        public void PopulateHoursAndFinishedTasks()
        {                    
            String query = String.Format("SELECT * FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0}", phaseID, projectID);
            String countRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state != 0", phaseID, projectID);
            String countCompletedRowsQuery = String.Format("SELECT COUNT(*) FROM Task WHERE Task.phaseID IN (SELECT phaseID from Fase WHERE Fase.projectID = {1}) AND Task.PhaseID = {0} AND Task.state = 2", phaseID, projectID);

            // counts returned # of rows selected, not including tasks with Task.state = 0
            int countRows = 0;
            hourTable = db.getAll(countRowsQuery);
            countRows += Convert.ToInt32(hourTable.Rows[0]["COUNT(*)"]);            
            
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

            // counts rows with Task.state = 2, meaning it is completed
            int completedRows = 0;
            hourTable = db.getAll(countCompletedRowsQuery);
            
            completedRows += Convert.ToInt32(hourTable.Rows[0]["COUNT(*)"]);
            lbFinishedTaskNum.Text = completedRows.ToString();

            lbUnfinishedTaskNum.Text = (countRows - completedRows).ToString();
        }
    }
}