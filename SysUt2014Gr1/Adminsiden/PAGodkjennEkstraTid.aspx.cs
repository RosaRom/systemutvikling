﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// har et problem, siden refresher ikke når man har gjort en forandring

namespace Adminsiden
{
    public partial class PAGodkjennEkstraTid : System.Web.UI.Page
    {
        int projectID = 1;
        
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                Populate();
            }
           else
            {
                Server.Transfer("Login.aspx", true);
            } 
        }

        public void Populate()
        {
            string query = String.Format("SELECT productBacklogID \"BacklogID\", taskName \"Tasknavn\", priority \"Prioritet\", description \"Beskrivelse\"," +
                " hoursUsed \"Brukte timer\", hoursAllocated \"Allokerte timer\", hoursExtra \"Ekstra timer\", taskID, hoursAllocated, hoursExtra" + 
                " FROM Task WHERE hoursExtra != 0 AND phaseID IN (SELECT phaseID FROM Fase WHERE projectID = {0})", projectID);
            
            dt = db.getAll(query);
            ViewState["table"] = dt;

            gvTaskList.DataSource = dt;
            gvTaskList.DataBind();
                                           
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int prioritet = Convert.ToInt32(dt.Rows[i]["Prioritet"]);

                switch (prioritet)
                {
                    case 1:
                        gvTaskList.Rows[i].Cells[2].Text = "Høy";
                        break;
                    case 2:
                        gvTaskList.Rows[i].Cells[2].Text = "Mid";
                        break;
                    case 3:
                        gvTaskList.Rows[i].Cells[2].Text = "Lav";
                        break;
                }
            }
        }

        protected void gvTaskList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            
            if (e.CommandName == "godkjenn")
            {
                int taskID = Convert.ToInt32(dt.Rows[index]["taskID"].ToString());
                int hoursAllocated = Convert.ToInt32(dt.Rows[index]["hoursAllocated"].ToString());                
                int hoursExtra = Convert.ToInt32(dt.Rows[index]["hoursExtra"].ToString());
                int newHoursAllocated = hoursAllocated + hoursExtra;
                string query = String.Format("UPDATE Task SET hoursAllocated = {0}, hoursExtra = 0  WHERE taskID = {1}", newHoursAllocated, taskID);
                db.InsertDeleteUpdate(query);
            }

            if (e.CommandName == "ikkegodkjenn")
            {
                int taskID = Convert.ToInt32(dt.Rows[index]["taskID"].ToString());
                string query = String.Format("UPDATE Task SET hoursExtra = 0 WHERE taskID = {0}", taskID);
                db.InsertDeleteUpdate(query); 
            }            
        }
    }
}