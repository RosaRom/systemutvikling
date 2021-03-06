﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adminsiden
{
    /// <summary>
    /// LogOut.aspx.cs av Tord-Marius Fredriksen
    /// SysUt14Gr1 - Systemutvikling - Vår 2014
    /// 
    /// Enkel og grei klasse som fjerner loginsession og overfører brukeren til innlogging.
    /// </summary>
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userLoggedIn"] = "";
            Server.Transfer("Login.aspx", true);
        }
    }
}