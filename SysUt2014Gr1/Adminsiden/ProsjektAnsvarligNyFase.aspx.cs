using Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace Adminsiden
{
    public partial class ProsjektAnsvarligNyFase : System.Web.UI.Page
    {
        private DBConnect db;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void WriteData(String _pName, DateTime _pDateFrom, DateTime _pDateTo, String _pDesc)
        {
            String query = "INSERT INTO Fase (phaseName, phaseDateFrom, phaseDateFrom, phaseDescription) VALUES('" + _pName + "', '" + _pDateFrom + "', '" + _pDateTo + "', '" + _pDesc + "')";

        }
    }
}