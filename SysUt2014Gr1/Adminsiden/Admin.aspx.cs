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
        DBConnect db = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetAllUsers();
            }
            
        }

        private void GetAllUsers()
        {
            GridViewAdmin.DataSource = db.AdminGetAllUsers();
            GridViewAdmin.DataBind();
        }

        protected void GridViewAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAdmin.EditIndex = e.NewEditIndex;
            GetAllUsers();
        }

        protected void GridViewAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAdmin.EditIndex = -1;
            GetAllUsers();
        }

        protected void GridViewAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = GridViewAdmin.DataKeys[e.RowIndex]["id"].ToString();
            string s1 = e.NewValues["fornavn"].ToString();
            string s2 = e.NewValues["etternavn"].ToString();
            string s3 = e.NewValues["stilling"].ToString();

            string query = String.Format("UPDATE test_brukere SET fornavn = '{0}', etternavn = '{1}', stilling = '{2}' WHERE id = {3}", s1, s2, s3, id);
            db.InsertDeleteUpdate(query);
            GridViewAdmin.EditIndex = -1;
            GetAllUsers();
        }

        protected void GridViewAdmin_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridViewAdmin.DataKeys[e.RowIndex]["id"].ToString();
            string query = String.Format("DELETE FROM test_brukere WHERE id = {0}", id);

            db.InsertDeleteUpdate(query);
            GridViewAdmin.EditIndex = -1;
            GetAllUsers();
        }
    }
}