using Admin;
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
    public partial class login : System.Web.UI.Page
    {
        static readonly string PasswordHash = "P@@Sw0rdH@$h1ng";
        static readonly string SaltKey = "$@LT&K3Y";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        DBConnect db = new DBConnect();
        DataTable dt = new DataTable();

        protected void submit_Click(object sender, EventArgs e)
        {
      
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            string passwordIn = Encrypt(password);


            string query = String.Format("SELECT userID, password, groupID FROM User WHERE username = '{0}'", username);
            dt = db.getAll(query);

            if(dt != null && dt.Rows.Count > 0)
            {
                int userID = Convert.ToInt16(dt.Rows[0]["userID"]);
                string userPW = Convert.ToString(dt.Rows[0]["password"]);
                int groupID = Convert.ToInt16(dt.Rows[0]["groupID"]);
                Session["userID"] = userID;

                if (passwordIn == userPW)
                {

                switch (groupID)
                {
                    case 1:
                        Server.Transfer("Prosjektvalg.aspx", true);
                        break;
                    case 2:
                        //Her skal det sendes videre til teamleder siden
                        //Server.Transfer("Teamleder.aspx", true);
                          Server.Transfer("Admin.aspx", true);
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
            else
            {
                LabelWarning.Visible = true;
                LabelWarning.Text = "Feil brukernavn og/eller passord.";
            }
            
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

    }
}