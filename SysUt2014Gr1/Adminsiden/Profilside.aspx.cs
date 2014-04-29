using Adminsiden;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    public partial class Profilside : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();
        int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userLoggedIn"] == "teamMember" || Session["userLoggedIn"] == "teamLeader" || Session["userLoggedIn"] == "projectManager")
            { 
                getUserInfo();
            }
            else
            {
                Server.Transfer("Login.aspx", true);

            }
        }
        public void getUserInfo()
        {
            
            string query = String.Format("SELECT *, CONCAT (firstname, ' ',  surname) AS FullName FROM User, UserGroup WHERE userID = {0} AND User.groupID = UserGroup.groupID", Session["userID"]);// + Session["userID"];
            dt = db.getAll(query);
            userID = Convert.ToInt16(dt.Rows[0]["userID"]);
            string fullname = Convert.ToString(dt.Rows[0]["FullName"]);
            string username = Convert.ToString(dt.Rows[0]["username"]);
            string phone = Convert.ToString(dt.Rows[0]["phone"]);
            string email = Convert.ToString(dt.Rows[0]["mail"]);
            string usertype = Convert.ToString(dt.Rows[0]["groupName"]);

            Label_name.Text = fullname;
            Label_username.Text = username;
            Label_telefon.Text = phone;
            Label_email.Text = email;
            Label_status.Text = usertype;
        }

        // behandler endring av passord felt og labels

        protected void btn_endrePW_Click(object sender, EventArgs e)
        {
            Label_titlePW.Visible = false;
            btn_endrePW.Visible = false;
            changeVisiblePWFields(true);
        }

        protected void btn_confirmChangePW_Click(object sender, EventArgs e)
        {
            string enteredOldPW = tb_gp.Text;
            string enteredNewPW1 = tb_np.Text;
            string enteredNewPW2 = tb_np1.Text;

            if(enteredOldPW == Convert.ToString(dt.Rows[0]["password"]))
            {
                if (enteredNewPW1 == enteredNewPW2)
                {
                    string query = "UPDATE User SET password = '" + enteredNewPW1 + "' WHERE userID = " + userID;
                    db.InsertDeleteUpdate(query);
                    changeVisiblePWFields(false);
                    btn_endrePW.Visible = true;
                    Label_warningPW.Visible = false;
                    Label_titlePW.Visible = true;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Ditt passord har blitt endret');", true);
                }
                else
                {
                    Label_warningPW.Visible = true;
                    Label_warningPW.Text = "Ditt ønskede nye passord er ikke likt";
                }
            }
            else
            {
                Label_warningPW.Visible = true;
                Label_warningPW.Text = "Det gamle passordet er ikke riktig";
            }
        }

        protected void btn_abortPW_Click(object sender, EventArgs e)
        {
            Label_warningPW.Visible = false;
            btn_endrePW.Visible = true;
            changeVisiblePWFields(false);
            Label_titlePW.Visible = true;
            tb_gp.Text = "";
            tb_np.Text = "";
            tb_np1.Text = "";
        }

        // behandler endring av telefon felt og labels

        protected void btn_endretlf_Click(object sender, EventArgs e)
        {
            btn_endretlf.Visible = false;
            Label_titleTlf.Visible = false;
            Label_telefon.Visible = false;
            changeVisibleTlfFields(true);
            
        }

        protected void btn_confirmChangeTlf_Click(object sender, EventArgs e)
        {
            string newTlf = tb_nyttNr.Text;
            string query = "UPDATE User SET phone = '" + newTlf + "' WHERE userID = " + userID;
            db.InsertDeleteUpdate(query);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Ditt registrerte telefonummer er endret');", true);

            btn_endretlf.Visible = true;
            Label_titleTlf.Visible = true;
            Label_telefon.Visible = true;
            changeVisibleTlfFields(false);

            getUserInfo();
        }

        protected void btn_abortTlf_Click(object sender, EventArgs e)
        {
            btn_endretlf.Visible = true;
            Label_titleTlf.Visible = true;
            Label_telefon.Visible = true;
            changeVisibleTlfFields(false);
        }

        //behandler endring av email felt og labels

        protected void btn_endremail_Click(object sender, EventArgs e)
        {
            btn_endremail.Visible = false;
            Label_titleEmail.Visible = false;
            Label_email.Visible = false;
            changeVisibleEmailFields(true);
        }

        protected void btn_confirmChangeMail_Click(object sender, EventArgs e)
        {
            string newEmail = tb_nyMail.Text;
            string query = "UPDATE User SET mail = '" + newEmail + "' WHERE userID = " + userID;
            db.InsertDeleteUpdate(query);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Din epost er endret');", true);

            btn_endremail.Visible = true;
            Label_titleEmail.Visible = true;
            Label_email.Visible = true;
            changeVisibleEmailFields(false);

            getUserInfo();
        }

        protected void btn_abortEmail_Click(object sender, EventArgs e)
        {
            btn_endremail.Visible = true;
            Label_titleEmail.Visible = true;
            Label_email.Visible = true;
            changeVisibleEmailFields(false);
        }

        //Metoder for å begrense kode-duplikat

        void changeVisiblePWFields(Boolean input)
        {
            Label_gp.Visible = input;
            Label_np.Visible = input;
            Label_np1.Visible = input;
            tb_gp.Visible = input;
            tb_np.Visible = input;
            tb_np1.Visible = input;
            btn_confirmChangePW.Visible = input;
            btn_abortPW.Visible = input;
        }

        void changeVisibleTlfFields(Boolean input)
        {
            Label_NyttTlf.Visible = input;
            tb_nyttNr.Visible = input;
            btn_confirmChangeTlf.Visible = input;
            btn_abortTlf.Visible = input;
        }

        void changeVisibleEmailFields(Boolean input)
        {
            Label_nyMail.Visible = input;
            tb_nyMail.Visible = input;
            btn_confirmChangeMail.Visible = input;
            btn_abortEmail.Visible = input;
        }

        

       

      

    



    }
    
}