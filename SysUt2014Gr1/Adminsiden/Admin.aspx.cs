using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class Admin : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAllUsers();
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
            //GridViewRow row = GridViewAdmin.Rows[e.RowIndex];

            //string s0 = e.NewValues["id"].ToString();
            string s1 = e.NewValues["fornavn"].ToString();
            string s2 = e.NewValues["etternavn"].ToString();
            string s3 = e.NewValues["stilling"].ToString();
            
            //string query = String.Format("INSERT INTO test_brukere (fornavn, etternavn, stilling) VALUES ('{0}', '{1}', '{2}') WHERE id = {3}", s1, s2, s3, s0);
            //db.InsertDeleteUpdate(query);
        }
    }
}