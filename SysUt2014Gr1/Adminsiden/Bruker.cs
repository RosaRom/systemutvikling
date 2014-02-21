using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* Brukerklassen til prosjektet
 * 
 * Første utkast
 * Under konstruksjon
 * 
 * Angående passord: Skal prosjektleder/admin selv velge passord for brukere forså skifte det til
 * det de vil ha selv? Eller skal skal det bare opprettes bruker, og ved første innlogging så må bruker 
 * velge passord? SVAR: Admin setter nok opp et passord, og så må vi ha funksjonalitet som lar brukere skifte passord
 * f.eks. en profilside.
 * 
 */
namespace Adminsiden
{
    public class Brukerklasse
    {
        private int brukerId, telefon, teamID, groupID;
        private string fornavn, etternavn, brukernavn, email;

        //GroupID erstatter konstantene

        public Brukerklasse(int brukerId, string etternavn, string fornavn, string brukernavn, int telefon,
            string email, int teamID, int groupID)
        {
            this.brukerId = brukerId;
            this.etternavn = etternavn;
            this.fornavn = fornavn;
            this.brukernavn = brukernavn;
            this.telefon = telefon;
            this.email = email;
            this.teamID = teamID;
            this.groupID = groupID;
        }
        
        #region Getters/Setters
        
        public int BrukerId
        {
            get { return brukerId; }
            set { brukerId = value; }
        }
        
        public string Fornavn
        {
            get { return fornavn; }
            set { fornavn = value; }
        }

        public string Etternavn
        {
            get { return etternavn; }
            set { etternavn = value; }
        }
        
        public string Brukernavn
        {
            get { return brukernavn; }
            set { brukernavn = value; }
        } 
        
        public int Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        
        public int TeamID
        {
            get { return teamID; }
            set { teamID = value; }
        }
        
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        #endregion

        //Eksempel på ToString - litt usikker i hvordan sammenheng dette skal brukes.
        //Er ikke sikkert vi har bruk for ToString(), men greit å ha just in case.
        //Hardkodet inn verdiene for groupID, må sjekkes opp mot databasen senere.
        public override string ToString()
        {
            if (groupID == 0)
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Administrator", brukerId, fornavn, etternavn);
            }
            else if (groupID == 1)
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Prosjektansvarlig", brukerId, fornavn, etternavn);
            }
            else if (groupID == 2)
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Teamleder", brukerId, fornavn, etternavn);
            }
            else if (groupID == 3)
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Vanlig bruker", brukerId, fornavn, etternavn);
            }
            else return String.Format("Feil under innhenting av brukerinformasjon.\nKan hende brukeren ikke tilhører en brukergruppe.\nKontakt administrator.");
        }
    }
}