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
        private int brukerId, telefon, teamID, groupID, brukertype;
        private string fornavn, etternavn, brukernavn, email;

        //Brukertyper; `const` ekvivalent med `final` syntax i Java
        public const int admin = 0, vanlig_bruker = 1, team_leder = 2, prosjekt_leder = 3;

        public Brukerklasse(int brukerId, string etternavn, string fornavn, string brukernavn, int telefon,
            string email, int teamID, int groupID, int brukertype)
        {
            this.brukerId = brukerId;
            this.etternavn = etternavn;
            this.fornavn = fornavn;
            this.brukernavn = brukernavn;
            this.telefon = telefon;
            this.email = email;
            this.teamID = teamID;
            this.groupID = groupID;
            this.brukertype = brukertype;
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
        
        public int Brukertype
        {
            get { return brukertype; }
            set { brukertype = value; }
        }
        #endregion

        //Eksempel på ToString - litt usikker i hvordan sammenheng dette skal brukes
        public override string ToString()
        {
            if (brukertype.Equals(admin))
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Administrator", brukerId, fornavn, etternavn);
            }

            else if (brukertype.Equals(prosjekt_leder))
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Bruker", brukerId, fornavn, etternavn);
            }

            else if (brukertype.Equals(team_leder))
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Teamleder", brukerId, fornavn, etternavn);
            }

            else if (brukertype.Equals(vanlig_bruker))
            {
                return String.Format("{0}: {1} {2}\nBrukertype: Bruker", brukerId, fornavn, etternavn);
            }
            else return String.Format("Feil under innhenting av brukerinformasjon.\nKontakt administrator.");
            
        }
    }
}