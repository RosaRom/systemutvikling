﻿using Adminsiden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

///
/// ProsjektAnsvarligNyFase.aspx.cs av Henning Fredriksen
/// SysUt14Gr1 - Systemutvikling - Vår 2014
///
/// Lar prosjektansvarlig manuelt opprette en fase til valgt prosjekt
/// 

namespace Adminsiden
{
    public partial class ProsjektAnsvarligNyFase : System.Web.UI.Page
    {
        private DBConnect db = new DBConnect();

        private DateTime phaseDateFrom = new DateTime();
        private DateTime phaseDateTo = new DateTime();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            String userLoggedIn = (String)Session["userLoggedIn"];

            if (userLoggedIn == "teamMember")
                this.MasterPageFile = "~/Masterpages/Bruker.Master";

            else if (userLoggedIn == "teamLeader")
                this.MasterPageFile = "~/Masterpages/Teamleder.Master";

            else if (userLoggedIn == "admin")
                this.MasterPageFile = "~/Masterpages/Admin.Master";

            else
                this.MasterPageFile = "~/Masterpages/Prosjektansvarlig.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// hjelpemetode som skriver data generert i btnSubmit_Click til database
        /// </summary>
        /// <param name="_pName"></param>
        /// <param name="_pDateFrom"></param>
        /// <param name="_pDateTo"></param>
        /// <param name="_pDesc"></param>
        public void WriteData(String _pName, DateTime _pDateFrom, DateTime _pDateTo, String _pDesc)
        {
            String query = "INSERT INTO Fase (phaseName, phaseFromDate, phaseToDate, phaseDescription) VALUES('" + _pName + "', '" + _pDateFrom.ToString("yyyy-MM-dd") + "', '" + _pDateTo.ToString("yyyy-MM-dd") + "', '" + _pDesc + "')";
            db.InsertDeleteUpdate(query);
        }

        /// <summary>
        /// legger inn properties til fasen som skal opprettes inn i database via WriteData()
        /// Sjekker at alle feltene har verdier og at du ikke har valgt sluttdato tidligere enn startdato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string session = (string)Session["userLoggedIn"];

            if (session == "projectManager")
            {
                lbError.Text = ""; // resetter error msg
                String dateFrom = tbDateFrom.Text;
                String dateTo = tbDateTo.Text;

                try
                {
                    // sjekker om noen inputfelt (med unntak av tbDescription) er tomme       
                    if (tbPhasename.Text != "" && tbDateFrom.Text != "" && tbDateTo.Text != "")
                    {
                        phaseDateFrom = Convert.ToDateTime(dateFrom);
                        phaseDateTo = Convert.ToDateTime(dateTo);

                        // sjekk for om phaseDateTo < phaseDateFrom (så du ikke kan sette sluttdato tidligere enn startdato)
                        if (phaseDateFrom < phaseDateTo)
                        {
                            // sjekker om tbDescription er tom, og varierer input til WriteData() ettersom phaseDescription skal kunne være NULL
                            if (tbDescription.Text == "")
                                WriteData(tbPhasename.Text, phaseDateFrom, phaseDateTo, "");

                            else
                                WriteData(tbPhasename.Text, phaseDateFrom, phaseDateTo, tbDescription.Text);
                        }
                        else
                            throw new Exception("Til-dato kan ikke være før fra-dato.");
                    }
                    else
                        throw new Exception("En fase må ha Fasenavn, fra- og til-dato.");
                }
                catch (Exception ex)
                {
                    lbError.Text = "" + ex.Message;
                }
             
            }
            else
            {
                Server.Transfer("Login.aspx", true);
            } 
            
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