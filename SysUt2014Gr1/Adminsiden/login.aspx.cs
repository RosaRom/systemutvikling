using Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql;
using System.Data;

namespace Adminsiden
{
    public partial class login : System.Web.UI.Page
    {
        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            string query = "SELECT userID, groupID FROM User WHERE username = '" + username + "' AND password = '" + password + "'";
            dt = db.getAll(query);

            if(dt != null && dt.Rows.Count > 0)
            {
                int userID = Convert.ToInt16(dt.Rows[0]["userID"]);
                int groupID = Convert.ToInt16(dt.Rows[0]["groupID"]);
                Session["userID"] = userID;

                switch (groupID)
                {
                    case 1:
                        Server.Transfer("Prosjektvalg.aspx", true);
                        break;
                    case 2:
                        //Her skal det sendes videre til teamleder siden
                        //Server.Transfer("Teamleder.aspx", true);
                        break;
                    case 3:
                        Server.Transfer("ProsjektAnsvarlig.aspx", true);
                        break;
                    case 4:
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
    }
}