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
        private DBConnect db = new DBConnect();

        private DateTime phaseDateFrom = new DateTime();
        private DateTime phaseDateTo = new DateTime();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void WriteData(String _pName, DateTime _pDateFrom, DateTime _pDateTo, String _pDesc)
        {
            String query = "INSERT INTO Fase (phaseName, phaseFromDate, phaseToDate, phaseDescription) VALUES('" + _pName + "', '" + _pDateFrom.ToString("yyyy-MM-dd") + "', '" + _pDateTo.ToString("yyyy-MM-dd") + "', '" + _pDesc + "')";
            db.InsertDeleteUpdate(query);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String dateFrom = tbDateFrom.Text;
            phaseDateFrom = Convert.ToDateTime(dateFrom);

            String dateTo = tbDateTo.Text;
            phaseDateTo = Convert.ToDateTime(dateTo);

            // trenger sjekk for om phaseDateTo < phaseDateFrom (så du ikke kan sette sluttdato tidligere enn startdato)

            // sjekker om tbDescription er tom, og varierer input til WriteData() ettersom phaseDescription skal kunne være NULL
            if (tbDescription.Text == "")
                WriteData(tbPhasename.Text, phaseDateFrom, phaseDateTo, "");

            else
                WriteData(tbPhasename.Text, phaseDateFrom, phaseDateTo, tbDescription.Text);
        }
        /*
                protected void Disabled_DayRender(object sender, DayRenderEventArgs e)
                {
                    if (phaseDateTo < phaseDateFrom) //specify your condition on yours days
                    {
                        e.Day.IsSelectable = false;
                        e.Cell.ForeColor = System.Drawing.Color.Gray;
                    }
                }
         */
    }
}