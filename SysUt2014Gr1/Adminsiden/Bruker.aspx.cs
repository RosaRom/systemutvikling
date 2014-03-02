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
        int userID = 1; //Denne må sittes under login, noe som ikke er skrevet enda. Hardkodet inntil videre.
        List<String> projectList = new List<string>();

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
            DataTable dt = new DataTable();
            string query = "SELECT * FROM SProject";
            dt = db.getAll(query);
            projectName.DataSource = dt;
            projectName.DataValueField = "projectName";
            projectName.Items.Insert(0, new ListItem("<Velg prosjekt>", "0"));
            projectName.DataBind();

            foreach (DataRow row in dt.Rows)
            {
                string strValue =   Convert.ToInt32(row["projectID"]) + ", " + 
                                    Convert.ToString(row["projectName"]) + ", " +
                                    Convert.ToString(row["Description"]);
                projectList.Add(strValue);
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
        //Herfra og ned er ikke ferdig enda->

        //Fyller textbox med prosjektbeskrivelse til valgt prosjekt.
        protected void projectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            selectedProject = projectName.SelectedValue;
            TxtArea_projectComment.Text = projectList.Find(p => p.Contains(selectedProject));
        }
        protected void taskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedTaskID = taskName.SelectedValue;
        }
        protected void workPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedWorkplaceID = workPlace.SelectedValue;
        }

        private string selectedProject;
        private int selectedTaskID;
        private int selectedProjectID;
        private int selectedWorkplaceID;
        int active = 0;

        protected void btn_ok_Click(object sender, EventArgs e)
        {
           /* var projectID = projectList.Find(item == "test").value;
            string userDescription = TxtArea_userComment.Text;

            string timeFrom = Tb_fra.Text;
            string timeTo = Tb_til.Text;

            db.InsertTimeSheet(timeFrom, timeTo, userID, selectedTaskID, userDescription, selectedWorkplaceID, active, selectedProjectID);*/
        }



       
    }
}