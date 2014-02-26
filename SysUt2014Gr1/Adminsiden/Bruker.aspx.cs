using Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace Bruker
{
    public partial class Bruker : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetTasks();
                getProjects();
                getWorkplace();
            }         
        }

        private void GetTasks ()
        {
            string query = "SELECT * FROM STask";
            taskName.DataSource = db.getAll(query);
            taskName.DataValueField = "taskName";
            taskName.Items.Insert(0, new ListItem("<Velg task>", "0"));
            taskName.DataBind();
        }
        private void getProjects ()
        {
            string query = "SELECT * FROM SProject";
            projectName.DataSource = db.getAll(query);
            projectName.DataValueField = "projectName";
            projectName.Items.Insert(0, new ListItem("<Velg prosjekt>", "0"));
            projectName.DataBind();
        }
        private void getWorkplace()
        {
            string query = "SELECT * FROM SWorkplace";
            workPlace.DataSource = db.getAll(query);
            workPlace.DataValueField = "workplace";
            workPlace.Items.Insert(0, new ListItem("<Velg arbeidsplass>", "0"));
            workPlace.DataBind();
        }

        protected void projectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ikke ferdig enda
            string selectedValue = projectName.SelectedValue;
            string query ="SELECT Description From SProsjekt WHERE prosjektName =" + selectedValue;
            //projectDescription.Text = db.getAll(query);

            
        }
    }
}