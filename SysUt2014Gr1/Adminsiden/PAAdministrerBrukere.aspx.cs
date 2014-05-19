using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Adminsiden
{
    /// <summary>
    /// PAAdministreBrukere.aspx.cs av Kristian Alm
    /// SysUt14Gr1 - SystemUtvikling - Vår 2014
    /// Siden er nesten helt lik Admin.aspx.cs med unntak av at
    /// bare bruker og teamleder vises og kan opprettes
    /// </summary>
    public partial class PAAdministrerBrukere : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private Boolean active = true;
        private DataTable table = new DataTable();
        private DataTable tableNull = new DataTable();

        // Brukes i forhold til sorting og for å lagre view states når det er flere spørringer opp mot websiden
        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "DESC"; }
            set { ViewState["SortDirection"] = value; }
        }

        /// <summary>
        /// Sjekker på cookie hvilken type bruker det er som er logget inn.
        /// Er en standard metode vi har i alle klasser, da admin siden kun er tilgjengelig for administrator.
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

        /// <summary>
        /// Laster inn siden, sjekker session at brukeren er innlogget som
        /// administrator før siden vises.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                if (!Page.IsPostBack)
                {
                    aktiveEllerDeaktiv.Text = "Aktive brukere";
                    ViewState["active"] = active;
                    GetAllUsersReset();
                    GridViewInsertEmpty();
                }
                else
                {
                    active = (Boolean)ViewState["active"];                  //sørger for å ta vare på booleanverdien til active mellom postback
                    table = (DataTable)ViewState["table"];
                    beskjed.Text = "|";
                }
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
            
        }

        /// <summary>
        /// Her hentes alle brukerene ut ved kjøring første gang.
        /// Hentes ut på nytt når en ny bruker blir lagt til.
        /// Begrenses til brukere og teamledere.
        /// </summary>
        private void GetAllUsersReset()
        {
            string queryActive = "SELECT userID, surname, firstname, username, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE aktiv = '1' AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID AND (User.groupID = 1 OR User.groupID = 2)";
            table = db.AdminGetAllUsers(queryActive);

            string queryNull = "SELECT userID, surname, firstname, username, phone, mail, teamID \"teamName\", groupName FROM User, UserGroup WHERE aktiv =  '1' AND User.teamID IS NULL  AND User.groupID = UserGroup.groupID AND (User.groupID = 1 OR User.groupID = 2)";
            tableNull = db.AdminGetAllUsers(queryNull);

            table = db.AdminGetAllUsers(queryActive);
            table.Merge(tableNull, true, MissingSchemaAction.Ignore);

            ViewState["table"] = table;

            GridViewAdmin.DataSource = table;
            GridViewAdmin.DataBind();
        }

        /// <summary>
        /// Metode som binder DataTable til gridview.
        /// Henter table som er lagret i ViewState for å huske forandringer og sorteringer.
        /// </summary>
        private void GetAllUsers()
        {
            GridViewAdmin.DataSource = (DataTable)ViewState["table"];
            GridViewAdmin.DataBind();
        }

        /// <summary>
        /// Ved å trykke på knappen for deaktiverte brukere vil de her hentes ut fra databasen
        /// og bindes til gridview. Vil også lagres i Viewstate så siden "husker" hva som er valgt.
        /// Begrenses til brukere og teamledere.
        /// </summary>
        private void GetInactiveUsersReset()
        {
            string queryInactive = "SELECT userID, surname, firstname, username, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE aktiv = '0' AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID AND (User.groupID = 1 OR User.groupID = 2)";
            table = db.AdminGetAllUsers(queryInactive);

            string queryNull = "SELECT userID, surname, firstname, username, phone, mail, teamID \"teamName\", groupName FROM User, UserGroup WHERE aktiv =  '0' AND User.teamID IS NULL  AND User.groupID = UserGroup.groupID AND (User.groupID = 1 OR User.groupID = 2)";
            tableNull = db.AdminGetAllUsers(queryNull);

            table.Merge(tableNull, true, MissingSchemaAction.Ignore);
            ViewState["table"] = table;

            GridViewAdmin.DataSource = table;
            GridViewAdmin.DataBind();
        }

        /// <summary>
        /// Her hentes de deaktiverte ut fra ViewState.
        /// Dette gjør ved postback. Ved større foandringer vil GetInactiveUsersReset()
        /// hente ut alle på nytt.
        /// </summary>
        private void GetInactiveUsers()
        {
            GridViewAdmin.DataSource = (DataTable)ViewState["table"];
            GridViewAdmin.DataBind();
        }

        #region Events
        /// <summary>
        /// metode som kjøres når admin trykker på editknappen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAdmin.EditIndex = e.NewEditIndex;
            GetUsers();
        }
        /// <summary>
        /// metode som kjøres når admin avbryter en edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAdmin.EditIndex = -1;
            GetUsers();
        }
        /// <summary>
        /// Metoden kjøres når admin oppdaterer en eksisterende bruker.
        /// Ved oppdatering vil det skrives inn til databasen og en ny
        /// liste vil bli hentet ut igjen etterpå for å huske forandringene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewAdmin.Rows[e.RowIndex];

            try
            {
                string id = GridViewAdmin.DataKeys[e.RowIndex]["userID"].ToString();
                string surname = e.NewValues["surname"].ToString();
                string firstname = e.NewValues["firstname"].ToString();
                string username = e.NewValues["username"].ToString();
                string passwordIn = e.NewValues["password"].ToString();
                string password = Encryption.Encrypt(passwordIn);
                string phone = e.NewValues["phone"].ToString();
                string mail = e.NewValues["mail"].ToString();

                DropDownList team = (DropDownList)row.FindControl("dropDownTeamUsers");
                DropDownList group = (DropDownList)row.FindControl("dropDownGroupUsers");

                int teamID = Convert.ToInt32(team.SelectedValue);
                int groupID = Convert.ToInt32(group.SelectedValue);

                string query = String.Format("UPDATE User SET surname = '{0}', firstname = '{1}', username = '{2}', password = '{8}', phone = '{3}', mail = '{4}', teamID = '{5}', groupID = '{6}' WHERE userID = {7}",
                surname, firstname, username, phone, mail, teamID, groupID, id, password);
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

        /// <summary>
        /// metode som kjøres når admin aktiverer/deaktiverer en bruker.
        /// Listen vil så bli hentet ut på nytt så gridview oppdateres.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// RowUpdating kjøres når det legges til en ny bruker.
        /// Henter ut informasjon fra hver celle og sender det til databasen.
        /// Deretter blir gridview oppdatert for å vise den nye brukeren.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewInsert_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewInsert.Rows[e.RowIndex];
            string query;

            try
            {
                string surname = e.NewValues["surname"].ToString();
                string firstname = e.NewValues["firstname"].ToString();
                string username = e.NewValues["username"].ToString();
                string passwordIn = e.NewValues["password"].ToString();
                string password = Encryption.Encrypt(passwordIn);
                string phone = e.NewValues["phone"].ToString();
                string mail = e.NewValues["mail"].ToString();
                DropDownList team = (DropDownList)row.FindControl("dropDownTeam");
                DropDownList group = (DropDownList)row.FindControl("dropDownGroup");

                string teamID = Convert.ToString(team.SelectedValue);
                int groupID = Convert.ToInt32(group.SelectedValue);

                if (teamID.Equals(""))
                {
                    query = String.Format("INSERT INTO User (surname, firstname, password, username, phone, mail, groupID, aktiv) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7})",
                        surname, firstname, password, username, phone, mail, groupID, "1");
                }

                else
                {
                    query = String.Format("INSERT INTO User (surname, firstname, password, username, phone, mail, teamID, groupID, aktiv) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8})",
                        surname, firstname, password, username, phone, mail, teamID, groupID, "1");
                }

                db.InsertDeleteUpdate(query);
                GridViewInsert.EditIndex = -1;

                if (active)
                    GetAllUsersReset();
                else
                    GetInactiveUsersReset();

                GridViewInsertEmpty();
                beskjed.Text = "Ny bruker lagt til";
            }
            catch (Exception exception)
            {
                string error = exception.Message;
                beskjed.Text = "En feil oppsto: " + error;
            }
        }

        /// <summary>
        /// kjøres når man trykker edit for å legge til en ny bruker
        /// Kjøres automatisk ved oppstart, så den står alltid i editmode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewInsert_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewInsert.EditIndex = e.NewEditIndex;
            GridViewInsertEmpty();
        }

        /// <summary>
        /// kjøres når trykker cancel etter å ha trykket edit i legg til ny bruker gridviewen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewInsert_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewInsert.EditIndex = -1;
            beskjed.Text = "|";
            GridViewInsertEmpty();
        }

        /// <summary>
        /// viser alle deaktiverte brukere
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeaktiverte_Click(object sender, EventArgs e)
        {
            aktiveEllerDeaktiv.Text = "Deaktiverte brukere";
            active = false;
            ViewState["active"] = active;
            GetInactiveUsersReset();
        }

        /// <summary>
        /// viser aktive brukere
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAktiv_Click(object sender, EventArgs e)
        {
            aktiveEllerDeaktiv.Text = "Aktive brukere";
            active = true;
            ViewState["active"] = active;
            GetAllUsersReset();
        }

        /// <summary>
        /// søkeknappen for å finne spesifikke brukere
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Fjerner søk, og henter ut igjen alle brukere.
        /// Sjekker om aktive eller deaktiverte er valgt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFjernFilter_Click(object sender, EventArgs e)//Når brukeren velger å fjerne filtering
        {
            FilterSearchTerms.Text = "";    //fjerner tekst fra søkevilkårboksen
            if (active)
                GetAllUsersReset();             // Oppdaterer lista 
            else
                GetInactiveUsersReset();
        }

        /// <summary>
        /// Metode for sortering av brukere, kjøres når en kolonne blir trykket på.
        /// Sorterer radene etter informasjonen i kolonnen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// henter ut hvilken vei kolonnene skal sorteres.
        /// </summary>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Kjøres for å binde viewstate tabell til gridview.
        /// Sjekker om aktive eller deaktiverte brukere er valgt.
        /// </summary>
        private void GetUsers()
        {
            if (active == true)
                GetAllUsers();
            else
                GetInactiveUsers();
        }

        /// <summary>
        /// setter inn en tom rad for å kunne legge til en bruker i gridViewInsert
        /// </summary>
        private void GridViewInsertEmpty()
        {
            string query = "SELECT userID, surname, firstname, username, password, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE userID = 0 AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID";
            DataTable dt = db.AdminGetAllUsers(query);
            dt.Rows.Add(dt.NewRow());

            GridViewInsert.EditIndex = 0;
            GridViewInsert.DataSource = dt;
            GridViewInsert.DataBind();
        }



        /// <summary>
        /// Metode for å søke på brukere.
        /// Lager en query basert på valg og søkeord og binder den nye informasjonen til en datatable.
        /// Om resultatet er tomt vil det ikke skje noen forandring.
        /// Kan ikke finne brukere som ikke er bruker eller teamleder
        /// </summary>
        public void FilterGridView()
        {
            DataTable filterTable = new DataTable(); //Lager en data table for å lagre data fra spørringen

            //Henter alle fra User-tabellen med korrekt kolonnenavn / vilkår fra databasen
            string filterStatement = String.Format("SELECT userID, surname, firstname, username, password, phone, mail, teamName, groupName FROM User, Team, UserGroup WHERE {0} LIKE '%{1}%' AND User.teamID = Team.teamID AND User.groupID = UserGroup.groupID  AND (User.groupID = 1 OR User.groupID = 2)", FilterSearchDropdown.Text, FilterSearchTerms.Text);

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

        /// <summary>
        /// Binder data til en dropdownlist som viser alle teams
        /// </summary>
        /// <returns></returns>
        protected DataTable DropDownBoxTeam()
        {
            string query = "SELECT * FROM Team";
            DataTable table = new DataTable();
            table = db.AdminGetAllUsers(query);
            table.Rows.InsertAt(table.NewRow(), 0);

            return table;
        }

        /// <summary>
        /// må være med selv om metoden er tom, brukes av gridview hvor det legges til ny bruker. men brukes ikke til noe av gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewInsert_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}