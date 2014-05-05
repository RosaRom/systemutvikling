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
    public partial class editProject : System.Web.UI.Page
    {
        private DBConnect db;
        private string query;
        private DataTable dataTable = new DataTable();
        private int projectID;
        private string name = "test";

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
            db = new DBConnect();

            string session = (string)Session["userLoggedIn"];

            if (session == "teamLeader" || session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    projectID = Convert.ToInt32(Request.QueryString["id"]);
                    Refresh();
                }
                else
                {
                    if (ViewState["name"] != null)
                    {
                        name = (string)ViewState["name"];
                    }

                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
            
         

        }
        private void EditProject()
        {
            /*
            query = String.Format("UPDATE Project SET projectName = '{0}', projectDescription = '{1}', projectState = '{2}', parentProjectID = '{3}' WHERE projectID = '{4}')",
                tbProjectName.Text, tbProjectDescription.Text, "1", "0", "2");
             * */
            

             query = String.Format("UPDATE Project SET projectName = '{0}' WHERE projectID = '{1}'",
                name, projectID);
            db.InsertDeleteUpdate(query);

        }
        private void Refresh()
        {
            query = String.Format("SELECT * FROM Project WHERE projectID = '{0}'", projectID);// +listRef;
            dataTable = db.getAll(query);

            try
            {
                tbProjectName.Text = dataTable.Rows[0]["projectName"].ToString();
                tbProjectDescription.Text = dataTable.Rows[0]["projectDescription"].ToString();
                ViewState["name"] = "teamTest";
                ViewState["description"] = tbProjectDescription.Text;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
 
        }

        protected void btnUpdateQuery_Click(object sender, EventArgs e)
        {
            EditProject();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}