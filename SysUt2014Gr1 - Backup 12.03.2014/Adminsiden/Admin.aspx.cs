﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace Admin
{
    public partial class Admin : System.Web.UI.Page
    {
        private DBConnect db;
        private Boolean active = true;
        private DataTable table = new DataTable();

        // Brukes i forhold til sorting og for å lagre view states når det er flere spørringer opp mot websiden
        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "DESC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new DBConnect();
            if (!Page.IsPostBack)
            {
                ViewState["active"] = active;
                GetAllUsersReset();
                GridViewInsertEmpty();
            }
            else
            {
                active = (Boolean)ViewState["active"];                  //sørger for å ta vare på booleanverdien til active mellom postback
                table = (DataTable)ViewState["table"];
            }
        }

        //her hentes alle aktive brukere ut og vises i gridview
        private void GetAllUsersReset()
        {
            string queryActive = "SELECT userID, surname, firstname, username, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE aktiv = '1' AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID";
            table = db.AdminGetAllUsers(queryActive);
            ViewState["table"] = table;

            GridViewAdmin.DataSource = table;
            GridViewAdmin.DataBind();
        }

        private void GetAllUsers()
        {
            GridViewAdmin.DataSource = (DataTable)ViewState["table"];
            GridViewAdmin.DataBind();
        }

        //her hentes alle inaktive brukere ut og vises når admin vil se de
        private void GetInactiveUsersReset()
        {
            string queryInactive = "SELECT userID, surname, firstname, username, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE aktiv = '0' AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID";
            table = db.AdminGetAllUsers(queryInactive);
            ViewState["table"] = table;

            GridViewAdmin.DataSource = table;
            GridViewAdmin.DataBind();
        }

        private void GetInactiveUsers()
        {
            GridViewAdmin.DataSource = (DataTable)ViewState["table"];
            GridViewAdmin.DataBind();
        }

        #region Events
        //metode som kjøres når admin trykker på editknappen
        protected void GridViewAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAdmin.EditIndex = e.NewEditIndex;
            GetUsers();
        }
        //metode som kjøres når admin avbryter en edit
        protected void GridViewAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAdmin.EditIndex = -1;
            GetUsers();
        }
        //metoden kjøres når admin oppdaterer en eksisterende bruker
        protected void GridViewAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewAdmin.Rows[e.RowIndex];

            try
            {
                string id = GridViewAdmin.DataKeys[e.RowIndex]["userID"].ToString();
                string surname = e.NewValues["surname"].ToString();
                string firstname = e.NewValues["firstname"].ToString();
                string username = e.NewValues["username"].ToString();
                string phone = e.NewValues["phone"].ToString();
                string mail = e.NewValues["mail"].ToString();

                DropDownList team = (DropDownList)row.FindControl("dropDownTeamUsers");
                DropDownList group = (DropDownList)row.FindControl("dropDownGroupUsers");

                int teamID = Convert.ToInt32(team.SelectedValue);
                int groupID = Convert.ToInt32(group.SelectedValue);

                string query = String.Format("UPDATE User SET surname = '{0}', firstname = '{1}', username = '{2}', phone = '{3}', mail = '{4}', teamID = '{5}', groupID = '{6}' WHERE userID = {7}",
                surname, firstname, username, phone, mail, teamID, groupID, id);
                db.InsertDeleteUpdate(query);
                GridViewAdmin.EditIndex = -1;

                if (active)
                    GetAllUsersReset();
                else
                    GetInactiveUsersReset();
            }
            catch (Exception exception)
            {
                string error = exception.Message;
            }
        }

        //metode som kjøres når admin aktiverer/deaktiverer en bruker
        protected void GridViewAdmin_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridViewAdmin.DataKeys[e.RowIndex]["userID"].ToString();
            string query;

            if (active)
                query = String.Format("UPDATE User SET aktiv = 0 WHERE userID = {0}", id);

            else
                query = String.Format("UPDATE User SET aktiv = 1 WHERE userID = {0}", id);

            db.InsertDeleteUpdate(query);

            GridViewAdmin.EditIndex = -1;

            if (active)
                GetAllUsersReset();
            else
                GetInactiveUsersReset();
        }

        //RowUpdating kjøres når det legges til en ny bruker
        protected void GridViewInsert_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewInsert.Rows[e.RowIndex];

            try
            {
                string surname = e.NewValues["surname"].ToString();
                string firstname = e.NewValues["firstname"].ToString();
                string username = e.NewValues["username"].ToString();
                string phone = e.NewValues["phone"].ToString();
                string mail = e.NewValues["mail"].ToString();
                DropDownList team = (DropDownList)row.FindControl("dropDownTeam");
                DropDownList group = (DropDownList)row.FindControl("dropDownGroup");

                int teamID = Convert.ToInt32(team.SelectedValue);
                int groupID = Convert.ToInt32(group.SelectedValue);

                string query = String.Format("INSERT INTO User (surname, firstname, password, username, phone, mail, teamID, groupID, aktiv) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8})",
                surname, firstname, "123", username, phone, mail, teamID, groupID, "1");

                db.InsertDeleteUpdate(query);
                GridViewInsert.EditIndex = -1;

                if (active)
                    GetAllUsersReset();
                else
                    GetInactiveUsersReset();

                GridViewInsertEmpty();
            }
            catch (Exception exception)
            {
                string error = exception.Message;
            }
        }

        //kjøres når man trykker edit for å legge til en ny bruker
        protected void GridViewInsert_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewInsert.EditIndex = e.NewEditIndex;
            GridViewInsertEmpty();
        }

        //kjøres når trykker cancel etter å ha trykket edit i legg til ny bruker gridviewen
        protected void GridViewInsert_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewInsert.EditIndex = -1;
            GridViewInsertEmpty();
        }

        //viser alle deaktiverte brukere
        protected void btnDeaktiverte_Click(object sender, EventArgs e)
        {
            active = false;
            ViewState["active"] = active;
            GetInactiveUsersReset();
        }

        //viser aktive brukere
        protected void btnAktiv_Click(object sender, EventArgs e)
        {
            active = true;
            ViewState["active"] = active;
            GetAllUsersReset();
        }

        //søkeknappen for å finne spesifikke brukere
        protected void btnFilter_Click(object sender, EventArgs e) //Når brukeren vil filtrere listen med brukere
        {
            if (FilterSearchTerms.Text.Equals(String.Empty))
            {
                FilterSearchTerms.Text = "Mangler søkevilkår!";
            }
            else
            {
                FilterGridView();
            }
        }


        protected void btnFjernFilter_Click(object sender, EventArgs e)//Når brukeren velger å fjerne filtering
        {
            FilterSearchTerms.Text = "";    //fjerner tekst fra søkevilkårboksen
            if (active)
                GetAllUsersReset();             // Oppdaterer lista 
            else
                GetInactiveUsersReset();
        }

        //metode for sortering av brukere
        protected void GridViewAdmin_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = table;
            GridViewAdmin.DataSource = dataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                ViewState["table"] = dataView.ToTable();

                GridViewAdmin.DataSource = dataView;
                GridViewAdmin.DataBind();
            }
        }
        #endregion

        //henter ut hvilken vei kolonnene skal sorteres
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;
                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }
            return GridViewSortDirection;
        }

        private void GetUsers()
        {
            if (active == true)
                GetAllUsers();
            else
                GetInactiveUsers();
        }

        //setter inn en tom rad for å kunne legge til en bruker
        private void GridViewInsertEmpty()
        {
            string query = "SELECT userID, surname, firstname, username, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE userID = 1 AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID";
            DataTable dt = db.AdminGetAllUsers(query);
            dt.Rows.Add(dt.NewRow());
            GridViewInsert.DataSource = dt;
            GridViewInsert.DataBind();
        }



        //Metode for å filtrere GridViewAdmin
        public void FilterGridView()
        {
            DataTable filterTable = new DataTable(); //Lager en data table for å lagre data fra spørringen

            //Henter alle fra User-tabellen med korrekt kolonnenavn / vilkår fra databasen
            string filterStatement = String.Format("SELECT userID, surname, firstname, username, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE {0} LIKE '%{1}%' AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID", FilterSearchDropdown.Text, FilterSearchTerms.Text);

            filterTable = db.AdminGetAllUsers(filterStatement);

            if (filterTable.Rows.Count > 0) //Hvis søkevilkåret gir resultater
            {
                //Om søket ga resultat
                ViewState["table"] = filterTable;
                GridViewAdmin.DataSource = filterTable; //Setter data source til den filtrerte data table
                GridViewAdmin.DataBind();               //Oppdaterer data i GridView
            }
            else
            {
                //Søket ga ingen resultat, trenger ikke refreshe GridViewAdmin
                FilterSearchTerms.Text = "Søket ga ingen resultat!"; //Gir bruker beskjed
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

        protected DataTable DropDownBoxGroup()
        {
            string query = "SELECT * FROM UserGroup";

            return db.AdminGetAllUsers(query);
        }

        protected DataTable DropDownBoxTeamExistingUsers()
        {
            string query = "SELECT * FROM Team";

            return db.AdminGetAllUsers(query);
        }

        // FUNKER IKKE ENDA
        /*
        protected void GridViewAdmin_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Image sortImage = new Image();

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (GridViewSortDirection.Equals("ASC"))
                {
                    sortImage.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Adminsiden.Properties.Resources.DOWNARROW.gif");
                }
                else if (GridViewSortDirection.Equals("DESC"))
                {
                    sortImage.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Adminsiden.Properties.Resources.UPARROW.gif");
            }

                switch (GridViewSortDirection)
        {

                    case "ASC":
                        e.Row.Cells[Convert.ToInt32(e.Row.DataItemIndex.ToString())].Controls.Add(sortImage);
                        break;
                    case "DESC":
                        e.Row.Cells[Convert.ToInt32(e.Row.DataItemIndex.ToString())].Controls.Add(sortImage);
                        break;
                    default:
                        sortImage.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Adminsiden.Properties.Resources.DOWNARROW.gif");
                        e.Row.Cells[Convert.ToInt32(e.Row.DataItemIndex.ToString())].Controls.Add(sortImage);
                        break; 
            }
            }
        }*/
    }
}