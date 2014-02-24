﻿using System;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetUsers();
            }
            
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
            
            //GridViewAdmin.DataSource = null;
            //GridViewAdmin.DataBind();

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

        protected void GridViewAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            /*
            string id = GridViewAdmin.DataKeys[e.RowIndex]["id"].ToString();
            string s1 = e.NewValues["fornavn"].ToString();
            string s2 = e.NewValues["etternavn"].ToString();
            string s3 = e.NewValues["stilling"].ToString();

            string query = String.Format("UPDATE test_brukere SET fornavn = '{0}', etternavn = '{1}', stilling = '{2}' WHERE id = {3}", s1, s2, s3, id);
            db.InsertDeleteUpdate(query);
            GridViewAdmin.EditIndex = -1;
            GetAllUsers();
             * */
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
            GetInactiveUsers();
        }

        protected void btnAktiv_Click(object sender, EventArgs e)
        {
            active = true;
            GetAllUsers();
        }
    }
}