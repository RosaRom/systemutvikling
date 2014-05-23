using Adminsiden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    /// <summary>
    /// 
    /// OpprettTeam.apsx.cs av Tommy Langhelle
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Her kan Team opprettes, redigeres og slettes. registrerte brukere kan flyttes mellom teams
    /// og teamleder-status kan flyttes mellom brukere.
    /// 
    /// </summary>
    public partial class OpprettTeam : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private int teamID;
        private int userID;

        /// <summary>
        /// Riktig masterpage blir bestemt ut i fra hvilken status innlogget bruker har.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    getUsers();
                    getTeams();
                    fillGridView();
                }
                else
                {
                    if (ViewState["teamID"] != null)
                        teamID = (int)ViewState["teamID"];
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
           
        }
        private void getTeams()
        {
            string query = "SELECT * FROM Team";
            ddl_selectTeam.DataSource = db.getAll(query);
            ddl_selectTeam.DataTextField = "teamName";
            ddl_selectTeam.DataValueField = "teamID";
            ddl_selectTeam.Items.Insert(0, new ListItem("<Velg team>", "0"));
            ddl_selectTeam.DataBind();
        }
        private void getUsers()
        {
            string query = "SELECT userID, CONCAT (firstname, ' ',  surname) AS FullName FROM User WHERE teamID NOT LIKE " + teamID + " AND aktiv = 1";
            ddl_users.DataSource = db.getAll(query);
            ddl_users.DataTextField = "FullName";
            ddl_users.DataValueField = "userID";
            ddl_users.Items.Insert(0, new ListItem("<Velg bruker>", "0"));
            ddl_users.DataBind(); 
        }
        /// <summary>
        /// Gridview blir fykt med Team valgt i getTeams()
        /// </summary>
        private void fillGridView()
        {
            string query = "SELECT firstname, surname, groupName FROM User, UserGroup WHERE UserGroup.groupID = User.groupID AND teamID =" + teamID;
            GridView1.DataSource = db.getAll(query);
            GridView1.DataBind();
        }

        /// <summary>
        /// Ligger bruker med status "bruker" til i valgt team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_addUser_Click(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(ddl_users.SelectedValue);
            string query = "UPDATE User SET groupID = 1, teamID=" + teamID + " WHERE userID=" + userID;
            db.InsertDeleteUpdate(query);
            fillGridView();

            ddl_users.Items.Clear();
            getUsers();
        }

        /// <summary>
        /// Ligger bruker med status "teamleder" til i valgt team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_addTeamleader_Click(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(ddl_users.SelectedValue);
            string query1 = "UPDATE User SET groupID = 1 WHERE groupID = 2 AND teamID =" + teamID;
            db.InsertDeleteUpdate(query1);
            string query2 = "UPDATE User SET groupID = 2, teamID=" + teamID + " WHERE userID=" + userID;
            db.InsertDeleteUpdate(query2);
            fillGridView();

            ddl_users.Items.Clear();
            getUsers();
        }

        protected void ddl_selectTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            teamID = Convert.ToInt32(ddl_selectTeam.SelectedValue);
            fillGridView();
            ViewState["teamID"] = teamID;

            ddl_users.Items.Clear();
            getUsers();
        }
        /// <summary>
        /// Gjør textboxer nødvendig for å opprette team synlig.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_opprett_Click(object sender, EventArgs e)
        {
            btn_abort.Visible = true;
            tb_newTeam.Visible = true;
            btn_createTeam.Visible = true;
            btn_opprett.Visible = false;
            btn_deleteTeam.Visible = false;
        }

        /// <summary>
        /// Input felter blir sjekket og innhold så videresendt til databasen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_createTeam_Click(object sender, EventArgs e)
        {
            string newTeam = tb_newTeam.Text;
            if (String.IsNullOrEmpty(newTeam))
            {
                Label_warning.Text = "Du må oppgi et navn";
            }
            else
            {
                string query = "INSERT INTO Team (teamName) VALUES ('" + newTeam + "')";
                db.InsertDeleteUpdate(query);

                ddl_selectTeam.Items.Clear();
                getTeams();
                Label_warning.Text = "";

                tb_newTeam.Text = "";
                btn_abort.Visible = false;
                tb_newTeam.Visible = false;
                btn_createTeam.Visible = false;
                btn_opprett.Visible = true;
                btn_deleteTeam.Visible = true;
            }
        }

        /// <summary>
        /// Avbryter oppretting av nytt team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_abort_Click(object sender, EventArgs e)
        {
            tb_newTeam.Text = "";
            btn_abort.Visible = false;
            tb_newTeam.Visible = false;
            btn_createTeam.Visible = false;
            btn_opprett.Visible = true;
            btn_deleteTeam.Visible = true;

            Label_warning.Text = "";
        }

        protected void btn_deleteTeam_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Team WHERE teamID =" + teamID;
            db.InsertDeleteUpdate(query);

            ddl_selectTeam.Items.Clear();
            getTeams();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            
            if (e.CommandName == "slett")
            {
                string selectedUserFirstName = GridView1.Rows[index].Cells[0].ToString();
                string selectedUserSurName = GridView1.Rows[index].Cells[1].ToString();
                string query = String.Format("UPDATE User SET teamID = NULL WHERE firstname = '{0}' AND surname = '{1}'", selectedUserFirstName, selectedUserSurName);
                db.InsertDeleteUpdate(query);

                fillGridView();
            }
        }
    }
}