using Adminsiden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Adminsiden
{
    /// <summary>
    /// LogIn.aspx.cs av Tord-Marius Fredriksen
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Dette er første side en bruker ser. Her logger brukeren inn med sin konto. Passord krypteres og
    /// sjekkes mot database sammen med brukernavn.
    /// </summary>
    public partial class login : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        protected void submit_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string passwordIn = Encryption.Encrypt(password);

            string query = String.Format("SELECT userID, password, groupID, aktiv FROM User WHERE username = '{0}'", username);
            dt = db.getAll(query);

            if(dt != null && dt.Rows.Count > 0)
            {
                int userID = Convert.ToInt16(dt.Rows[0]["userID"]);
                string userPW = Convert.ToString(dt.Rows[0]["password"]);
                int groupID = Convert.ToInt16(dt.Rows[0]["groupID"]);
                int aktiv = Convert.ToInt16(dt.Rows[0]["aktiv"]); //for å sjekke om brukeren er aktiv

                Session["userID"] = userID;

                if (passwordIn == userPW && aktiv != 0)
                {
                    switch (groupID) //sjekker brukertype, for så å sende videre til rett form
                    {
                        case 1:
                            Session["userLoggedIn"] = "teamMember";
                            Server.Transfer("Prosjektvalg.aspx", true);
                            break;
                        case 2:
                            Session["userLoggedIn"] = "teamLeader";
                            Server.Transfer("Prosjektvalg.aspx", true);
                            break;
                        case 3:
                            Session["userLoggedIn"] = "projectManager";
                            Server.Transfer("Prosjektvalg.aspx", true);
                            break;
                        case 4:
                            Session["userLoggedIn"] = "admin";
                            Server.Transfer("Admin.aspx", true);
                            break;
                    }
                }
                else
                {
                    LabelWarning.Visible = true;
                    LabelWarning.Text = "Feil brukernavn og/eller passord.";
                }                
            }
            else
            {
                LabelWarning.Visible = true;
                LabelWarning.Text = "Feil brukernavn og/eller passord.";
            }          
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }       
    }
}