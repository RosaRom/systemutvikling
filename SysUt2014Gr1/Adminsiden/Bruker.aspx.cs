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
        List<String> liste = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetTasks();
                getProjects();
                getWorkplace();
            }         
        }
        //Fyller dropdown med tasks
        private void GetTasks ()
        {
            string query = "SELECT * FROM STask";
            taskName.DataSource = db.getAll(query);
            taskName.DataValueField = "taskName";
            taskName.Items.Insert(0, new ListItem("<Velg task>", "0"));
            taskName.DataBind();
        }
        //Fyller dropdown med prosjekter
        //Oppretter liste med prosjektnavn/beskrivelse for å fylle ut textbox i "projectName_SelectedIndexChanged"
        private void getProjects ()
        {
            string query = "SELECT * FROM SProject";
            DataTable dt = new DataTable();
            dt = db.getAll(query);
            projectName.DataSource = dt;
            projectName.DataValueField = "projectName";
            projectName.Items.Insert(0, new ListItem("<Velg prosjekt>", "0"));
            projectName.DataBind();

            foreach (DataRow row in dt.Rows)
            {
                string strValue = Convert.ToString(row["projectName"]) + ", " +
                         Convert.ToString(row["Description"]);
                liste.Add(strValue);
            }
        }
        //Fyller dropdown med plasser å jobbe fra
        private void getWorkplace()
        {
            string query = "SELECT * FROM SWorkplace";
            workPlace.DataSource = db.getAll(query);
            workPlace.DataValueField = "workplace";
            workPlace.Items.Insert(0, new ListItem("<Velg arbeidsplass>", "0"));
            workPlace.DataBind();
        }
        //Fyller textbox med prosjektbeskrivelse til valgt prosjekt.
        protected void projectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ikke ferdig enda
            string selectedValue = projectName.SelectedValue;
            projectDescription.Text = liste.Find(p => p.Contains(selectedValue));
  
        }
    }
}