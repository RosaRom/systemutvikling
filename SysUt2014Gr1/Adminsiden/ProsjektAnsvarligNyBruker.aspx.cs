using Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace Adminsiden
{
    public partial class ProsjektAnsvarligNyBruker : System.Web.UI.Page
    {
        private DBConnect db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();

            if (!Page.IsPostBack)
            {
                GridViewInsertEmpty();
            }
        }

        protected DataTable DropDownBoxTeam()
        {
            string query = "SELECT * FROM Team";
            DataTable table = new DataTable();
            table = db.AdminGetAllUsers(query);
            table.Rows.InsertAt(table.NewRow(), 0);

            return table;
        }

        private void GridViewInsertEmpty()
        {
            string query = "SELECT userID, surname, firstname, username, password, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE userID = 0 AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID";
            DataTable dt = db.AdminGetAllUsers(query);
            dt.Columns["groupName"].DefaultValue = "Bruker";
            dt.Rows.Add(dt.NewRow());
            GridViewProsjektAnsvarligInsert.DataSource = dt;
            GridViewProsjektAnsvarligInsert.DataBind();
        }

        protected void GridViewProsjektAnsvarligInsert_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewProsjektAnsvarligInsert.Rows[e.RowIndex];

            try
            {
                string surname = e.NewValues["surname"].ToString();
                string firstname = e.NewValues["firstname"].ToString();
                string username = e.NewValues["username"].ToString();
                string password = e.NewValues["password"].ToString();
                string phone = e.NewValues["phone"].ToString();
                string mail = e.NewValues["mail"].ToString();
                DropDownList team = (DropDownList)row.FindControl("dropDownTeam");

                int teamID = Convert.ToInt32(team.SelectedValue);

                string query = String.Format("INSERT INTO User (surname, firstname, password, username, phone, mail, teamID, groupID, aktiv) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8})",
                surname, firstname, password, username, phone, mail, teamID, "1", "1");

                db.InsertDeleteUpdate(query);
                GridViewProsjektAnsvarligInsert.EditIndex = -1;
                
                GridViewInsertEmpty();

                beskjed.Text = "Ny brukeren er lagt til";
            }
            catch (Exception exception)
            {
                string error = exception.Message;
                beskjed.Text = "Det oppsto en feil: " + error;
            }
        }

        protected void GridViewProsjektAnsvarligInsert_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewProsjektAnsvarligInsert.EditIndex = e.NewEditIndex;
            GridViewInsertEmpty();
        }

        protected void GridViewProsjektAnsvarligInsert_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewProsjektAnsvarligInsert.EditIndex = -1;
            GridViewInsertEmpty();
        }
    }
}