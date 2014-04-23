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
    /**Har metode SetProductBacklogID nederst, men får ikke brukt den for å sette korrekt backlogID. Slik
     * det er nå, blir ID satt fra textbox **/
    public partial class EditTask : System.Web.UI.Page
    {
        private DBConnect db;
        private int prosjektID = 2; //bare satt en verdi
        private string query, userQuery, saveQuery, taskQuery, backlogQuery;
        private DataTable dataTable = new DataTable();
        private DataTable userTable = new DataTable();
        private DataTable taskTable = new DataTable();
        private DataTable backlogTable = new DataTable();
        private string backlogID;

        private int taskID = 2; //bare satt en verdi for debug

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();

            if (!Page.IsPostBack)
            {
                taskID = 2;
                Query();
            }

            if (ViewState["name"] != null)
            {
                backlogID = (string)ViewState["backlogID"];
            }
        }
        private void Query()
        {
            query = String.Format("SELECT * FROM Task WHERE taskID = '{0}'", taskID);
            dataTable = db.getAll(query);

            userQuery = "SELECT * FROM User WHERE aktiv = 1";
            userTable = db.getAll(userQuery);
            taskQuery = "SELECT * FROM Task";
            taskTable = db.getAll(taskQuery);     
            taskTable.Rows.InsertAt(taskTable.NewRow(), 0); //setter inn tom rad øverst
            //her blir alle verdier i textbokser satt
            try
            {
                tbTaskName.Text = dataTable.Rows[0]["taskName"].ToString();
                tbDescription.Text = dataTable.Rows[0]["description"].ToString();
                tbAllocatedTime.Text = dataTable.Rows[0]["hoursAllocated"].ToString();
                tbPhase.Text = dataTable.Rows[0]["phaseID"].ToString();
                tbState.Text = dataTable.Rows[0]["state"].ToString();
                //endring her medfører rapportinnsending //NB!! Må legges til
                tbPriority.Text = dataTable.Rows[0]["priority"].ToString();

                ddlAddUser.DataTextField = "username";
                ddlAddUser.DataValueField = "userID";
                ddlAddUser.DataSource = userTable;
                ddlAddUser.DataBind();

                ddlDependency.DataTextField = "taskName";
                ddlDependency.DataValueField = "taskID";
                ddlDependency.DataSource = taskTable;
                ddlDependency.DataBind();

//                ddlParentTask.DataTextField = "taskName";
//                ddlParentTask.DataValueField = "taskID";
//                ddlParentTask.DataSource = taskTable;
//                ddlParentTask.DataBind();

                string backlogStart = dataTable.Rows[0]["productBacklogID"].ToString();
                tbBacklog.Text = dataTable.Rows[0]["productBacklogID"].ToString();

                ViewState["taskID"] = dataTable.Rows[0]["taskID"].ToString();

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
 
        }

        /**
         * Sender inn oppdaterte verdier **/
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SetProductBacklogID(true, (string)ViewState["taskID"]);

           saveQuery = String.Format("UPDATE Task SET taskName = '{0}', description = '{1}', priority = {2}, state = {3}, hoursAllocated = {4}, phaseID ={5}, productBacklogID = {6} WHERE taskID = {7}",
               tbTaskName.Text, tbDescription.Text, tbPriority.Text, tbState.Text, tbAllocatedTime.Text, tbPhase.Text, (string)ViewState["backlogID"], taskID);
            db.InsertDeleteUpdate(saveQuery);
        }

        private void SetProductBacklogID(Boolean subTask, String _id)
        {
            string id = _id;
            string query = "SELECT productBacklogID FROM TaskCategory WHERE projectID = " + prosjektID + " AND taskCategoryID = 4"; //dummy
            string queryCount = "SELECT COUNT(*) FROM Task WHERE taskCategoryID = 13 AND LENGTH(productBacklogID) = 3";

            backlogTable = db.AdminGetAllUsers(query);
            int count = db.Count(queryCount) + 1;

            backlogID = backlogTable.Rows[0]["productBacklogID"].ToString() + "." + count;

//            if (ddlParentTask.SelectedValue.ToString().Equals(""))
                subTask = false;

            //denne slår inn om den skal være en subtask av en annen task under samme kategori
            if (subTask)
            {
//                string parentID = ddlParentTask.SelectedValue.ToString();
//                backlogQuery = "SELECT productBacklogID FROM Task WHERE taskID = " + parentID;
                backlogTable = db.AdminGetAllUsers(query);

                queryCount = String.Format("SELECT COUNT(*) FROM Task WHERE productBacklogID LIKE '{0}%'", backlogTable.Rows[0]["productBacklogID"].ToString());
                count = db.Count(queryCount);

                backlogID = backlogTable.Rows[0]["productBacklogID"].ToString() + "." + count;

                ViewState["backlogID"] = backlogID;

            }
            tbBacklog.Text = backlogID;
        }

    }
}