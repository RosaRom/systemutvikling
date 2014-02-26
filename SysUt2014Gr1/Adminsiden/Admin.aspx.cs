using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Admin
{
    public partial class Admin : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();
        private Boolean active = true;

        // Brukes i forhold til sorting i ConvertSortDirectionToSql-metoden
        private string GridViewSortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "DESC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewInsertEmpty();
            if (!Page.IsPostBack)
            {
                ViewState["active"] = active;
                GetUsers();
                
            }
            else
            {
                active = (Boolean)ViewState["active"];
            }
            
        }

        private void GridViewInsertEmpty()
        {
            string query = "SELECT * FROM SUser WHERE userID = 1";
            DataTable dt = db.AdminGetAllUsers(query);
            
            dt.Rows.Add(dt.NewRow());
            GridViewInsert.DataSource = dt;
            GridViewInsert.DataBind();

            //i tilfelle userID returnerer en bruker og ikke en tom rad
            //GridViewInsert.Rows[0].Visible = false;
            //GridViewInsert.Rows[0].Controls.Clear();
        }

        private void GetAllUsers()
        {
            string queryActive = "SELECT * FROM SUser WHERE aktiv = '1'";

            GridViewAdmin.DataSource = db.AdminGetAllUsers(queryActive);
            GridViewAdmin.DataBind();
        }

        private void GetInactiveUsers()
        {
            string queryInactive = "SELECT * FROM SUser WHERE aktiv = '0'";

            GridViewAdmin.DataSource = db.AdminGetAllUsers(queryInactive);
            GridViewAdmin.DataBind();
        }

        protected void GridViewAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAdmin.EditIndex = e.NewEditIndex;
            GetUsers();
        }
        
        
        protected void GridViewAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAdmin.EditIndex = -1;
            GetUsers();
        }
        
        //Kjøres fra listen med brukere nå en bruker oppdateres
        protected void GridViewAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = GridViewAdmin.DataKeys[e.RowIndex]["userID"].ToString();
            string surname = e.NewValues["surname"].ToString();
            string firstname = e.NewValues["firstname"].ToString();
            string username = e.NewValues["username"].ToString();
            string phone = e.NewValues["phone"].ToString();
            string mail = e.NewValues["mail"].ToString();
            string teamID = e.NewValues["teamID"].ToString();
            string groupID = e.NewValues["groupID"].ToString();

            string query = String.Format("UPDATE SUser SET surname = '{0}', firstname = '{1}', username = '{2}', phone = '{3}', mail = '{4}', teamID = '{5}', groupID = '{6}' WHERE userID = {7}",
                surname, firstname, username, phone, mail, teamID, groupID, id);
            db.InsertDeleteUpdate(query);
            GridViewAdmin.EditIndex = -1;
            GetUsers();
        }

        //RowEditing for å legge til nye brukere
        protected void GridViewInsert_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewInsert.EditIndex = e.NewEditIndex;
            GridViewInsertEmpty();
        }

        //kjøres om man trykke cancel ved edit av ny bruker
        protected void GridViewInsert_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewInsert.EditIndex = -1;
            GridViewInsertEmpty();
        }

        //RowUpdating kjøres når det legges til en ny bruker
        protected void GridViewInsert_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string surname = GridViewInsert.DataKeys[e.RowIndex]["surname"].ToString();
            string firstname = e.NewValues["firstname"].ToString();
            string username = e.NewValues["username"].ToString();
            string phone = e.NewValues["phone"].ToString();
            string mail = e.NewValues["mail"].ToString();
            string teamID = e.NewValues["teamID"].ToString();
            string groupID = e.NewValues["groupID"].ToString();

            string query = String.Format("INSERT INTO SUser VALUES('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7})",
                surname, firstname, "123", username, phone, mail, teamID, groupID);

            db.InsertDeleteUpdate(query);
            GridViewInsert.EditIndex = -1;

            GetUsers();
            GridViewInsertEmpty();
        }

        private void GridViewUpdate(GridView gridView, GridViewUpdateEventArgs e)
        {
            
             
        }

        protected void GridViewAdmin_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridViewAdmin.DataKeys[e.RowIndex]["userID"].ToString();
            string aktiv = e.Values["aktiv"].ToString();
            string query;

            if (aktiv.Equals("1"))
                query = String.Format("UPDATE SUser SET aktiv = 0 WHERE userID = {0}", id);

            else
                query = String.Format("UPDATE SUser SET aktiv = 1 WHERE userID = {0}", id);

            db.InsertDeleteUpdate(query);

            GridViewAdmin.EditIndex = -1;

            GetUsers();
        }

        private void GetUsers()
        {
            if (active == true)
                GetAllUsers();
            else
                GetInactiveUsers();
        }

        protected void btnDeaktiverte_Click(object sender, EventArgs e)
        {
            active = false;
            ViewState["active"] = active;
            GetInactiveUsers();
        }

        protected void btnAktiv_Click(object sender, EventArgs e)
        {
            active = true;
            ViewState["active"] = active;
            GetAllUsers();
        }
        
        // Sorting metode
        protected void GridViewAdmin_Sorting(object sender, GridViewSortEventArgs e)
        {
            string query;
            if (active)
            {
                query = "SELECT * FROM SUser WHERE aktiv = '1'";
            }
            else
            {
                query = "SELECT * FROM SUser WHERE aktiv = '0'";
            }

            DataTable dataTable = db.AdminGetAllUsers(query);
            GridViewAdmin.DataSource = dataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridViewAdmin.DataSource = dataView;
                GridViewAdmin.DataBind();
            }      
        }

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

 

        


        // FUNKER IKKE ENDA
        /*
        protected void GridViewAdmin_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (String.Empty != GridViewSortDirection)
                {
                    AddSortImage(e.Row);
                }
            }
        }

        void AddSortImage(GridViewRow header)
        {
            Int32 columnIndex = Convert.ToInt32(header.DataItemIndex.ToString());
            if (-1 == columnIndex)
            {
                return;
            }
            //Lager bildet utifra sort direction
            Image sortImage = new Image(); 
            string downURL = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Adminsiden.Properties.Resources.DOWNARROW.gif");
            string upURL = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Adminsiden.Properties.Resources.UPARROW.gif");

            if (GridViewSortDirection.Equals("ASC"))
            {
                sortImage.ImageUrl = downURL;
                sortImage.AlternateText = "Stigende rekkefølge.";
            }
            else
            {
                sortImage.ImageUrl = upURL;
                sortImage.AlternateText = "Synkende rekkefølge.";
            }

            //Legger til bildet i riktig header
            header.Cells[columnIndex].Controls.Add(sortImage);
        }*/
    }
}