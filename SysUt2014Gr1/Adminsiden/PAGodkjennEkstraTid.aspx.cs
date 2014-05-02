﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                " hoursUsed \"Brukte timer\", hoursAllocated \"Allokerte timer\", hoursExtra \"Ekstra timer\"" + 
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
        }
    }
}