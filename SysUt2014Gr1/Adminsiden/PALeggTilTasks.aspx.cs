using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin;
using System.Data;

namespace Adminsiden
{
    public partial class PALeggTilTasks : System.Web.UI.Page
    {
        private int prosjektID = 2;                                                     //hentes fra forrige side
        private DBConnect db = new DBConnect();
        private DataTable table = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillMainTasks();
            }
        }

        //lagrer en ny task i databasen
        protected void BtnLagreTask_Click(object sender, EventArgs e)
        {
            try
            {
                string taskCategoryID = DropDownMainTask.SelectedValue.ToString();
                
                string taskName;
                if (taskNavn.Text.Equals(""))
                    throw new Exception("Task må ha et navn");                          //lager exception med en message om ikke navn er satt
                else
                    taskName = taskNavn.Text;

                string description;
                if (beskrivelse.Text.Equals(""))
                    description = "NULL";
                else
                    description = "'" + beskrivelse.Text + "'";

                string priority = DropDownPrioritering.SelectedValue.ToString();
                string state = "0";

                string hoursAllocated;
                if (timerAllokert.Text.Equals(""))
                    hoursAllocated = "NULL";
                else
                {
                    int temp;
                    
                    if(int.TryParse(timerAllokert.Text, out temp))
                        hoursAllocated = "'" + timerAllokert.Text + "'";
                    else
                        throw new Exception("Antall timer må være et tall eller blankt");
                }
                    

                string parentTaskID;
                if (DropDownSubTask.SelectedValue.ToString().Equals(""))
                    parentTaskID = "NULL";
                else
                    parentTaskID = "'" + DropDownSubTask.SelectedValue.ToString() + "'";


                string query = String.Format("INSERT INTO Task VALUES(null, '{0}', '{1}', {2}, '{3}', '{4}', {5}, {6})",
                    taskCategoryID, taskName, description, priority, state, hoursAllocated, parentTaskID);
                db.InsertDeleteUpdate(query);

                beskjed.Text = "Ny task er lagt til";

                FillTasks();
                ResetForm();
            } catch (Exception ex)
            {
                beskjed.Text = "Noe gikk galt: " + ex.Message;                              //skriver ut beskjed om noe gikk galt under lagring av task
            }
            
        }

        //henter ut alle tasks som tilhører en valgt hovedtask, blir oppdatert hver gang hovedtask endres
        private void FillTasks()
        {
            string queryTask = "SELECT taskID, taskName FROM Task WHERE TaskCategoryID = " + DropDownMainTask.SelectedValue.ToString();

            table = db.AdminGetAllUsers(queryTask);
            table.Rows.InsertAt(table.NewRow(), 0);

            DropDownSubTask.DataSource = table;
            DropDownSubTask.DataBind();
        }

        //kjøres ved oppstart av siden, henter ut alle hovedtasks til en gitt projectID
        private void FillMainTasks()
        {
            string queryMainTask = "SELECT taskCategoryID, taskCategoryName FROM TaskCategory WHERE projectID = " + prosjektID;
            table = db.AdminGetAllUsers(queryMainTask);

            DropDownMainTask.DataSource = table;
            DropDownMainTask.DataBind();

            FillTasks();
        }

        //kjøres ved hver forandring av hovedtask, fyller opp tilhørende tasks på nytt
        protected void DropDownMainTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTasks();
            beskjed.Text = "";
            ResetForm();
        }

        private void ResetForm()
        {
            taskNavn.Text = "";
            beskrivelse.Text = "";
            DropDownPrioritering.SelectedIndex = 0;
            timerAllokert.Text = "";
            DropDownSubTask.SelectedIndex = 0;
        }
    }
}