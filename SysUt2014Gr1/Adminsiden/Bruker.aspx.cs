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
        private int userID = 2; //Denne må sittes under login, noe som ikke er skrevet enda. Hardkodet inntil videre.
        private int projectID = 1; //Denne må sittes under login, noe som ikke er skrevet enda. Hardkodet inntil videre.
        private int active = 0; //Denne må sittes under login, noe som ikke er skrevet enda. Hardkodet inntil videre.
        private int TaskID;
        private int WorkplaceID;

        List<String> projectList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetTasks();
                getWorkplace();
            }         
        }
        //Fyller dropdown med tasks
        private void GetTasks ()
        {
            string query = "SELECT * FROM Task Where projectID=" + projectID;
            taskName.DataSource = db.getAll(query);
            taskName.DataValueField = "taskID";
            taskName.DataTextField = "taskName";
            taskName.Items.Insert(0, new ListItem("<Velg task>", "0"));
            taskName.DataBind();
        }
        //Fyller dropdown med prosjekter
        /*private void getProjects ()
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
        }*/
        //Fyller dropdown med plasser å jobbe fra
        private void getWorkplace()
        {
            string query = "SELECT * FROM Workplace";
            workPlace.DataSource = db.getAll(query);
            workPlace.DataValueField = "workplaceID";
            workPlace.DataTextField = "workplace";
            workPlace.Items.Insert(0, new ListItem("<Velg arbeidsplass>", "0"));
            workPlace.DataBind();
        }
        //Herfra og ned er ikke ferdig enda->

        protected void taskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskID = Convert.ToInt32(taskName.SelectedValue);
        }
        protected void workPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            WorkplaceID = Convert.ToInt32(workPlace.SelectedValue);
        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string userDescription = TxtArea_userComment.Text;

            DateTime timeFrom = DateTime.Parse(Tb_fra.Text);
            DateTime timeTo = DateTime.Parse(Tb_til.Text);
            db.InsertTimeSheet(timeFrom, timeTo, userID, TaskID, userDescription, WorkplaceID, active, projectID);
        }
    }
}