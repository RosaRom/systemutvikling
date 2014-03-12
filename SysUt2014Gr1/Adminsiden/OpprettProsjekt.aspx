<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="OpprettProsjekt.aspx.cs" Inherits="Adminsiden.OpprettProsjekt" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!-- Kommentar til registrering av AjaxControlToolkit:
     Ajax er nå kun lagt til i denne webformen, må legges til i Web.Config for å fungere 
     i hele prosjektet. Ikke funnet ut hvordan dette gjøres - Ari -->

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opprett prosjekt</title>
    <link rel="Stylesheet" type="text/css" href="css/OpprettProsjektStyle.css" />

    <script type="text/javascript">
        /** Tegntellerskript for å telle og forhindre bruker i å skrive for mange tegn i en textbox, textview etc... 
            Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
         */

        maxL = 300;                         //Antall tilatte tegn i forhold til ProjectDescription field i databasen.
        var bName = navigator.appName;      //Navnet på nettleseren
        var keyevent = event.keyCode;       //Key event variabel som holder referanse til trykt knapp

        /** Funksjonen taLimit() brukes i onkeydown-event i f.eks. Text Area; id="TextArea_ProjectDescription". 
            Når en tast blir trykket sjekker metoden det totale antallet tegn som er tastet opp mot
            maxvalue på text area (maxL). Hvis grensen er nådd returnerer metoden false, ingen ytterligere 
            tastetrykkhendelse vil skje. Dette resulterte i en liten bug slik at bruker ikke kunne bruke backspacetasten
            når brukeren har brukt opp antall tegn, måtte legge til  "if (key == 8) return true", for å få dette til
            å fungere. Dette resulterte i en ny bug slik at maxvalue er nødt til å hardkodes inn i asp-syntaxen: maxlength="300".
            Skal endringer gjøres må maxL-variabelen endres i tillegg til maxvalue property i asp-komponentet.
          */
        function taLimit(taObj) {
            // 8 er keyCode for backspaceknappen
            if (key == 8) return true;
            if (taObj.value.length == maxL) return false;

            return true;
        }

        /** Funksjonen taCount() brukes i onkeyup-event. Bruker denne metoden for å endre verdien på telleren som vises
            og tatt høyde for copy-paste i feltet. Bruker innertext for å endre verdien på telleren, se i SPAN-elementet.
            */
        function taCount(taObj, Cnt) {
            objCnt = createObject(Cnt);
            objVal = taObj.value;
            if (objVal.length > maxL) objVal = objVal.substring(0, maxL);
            if (objCnt) {
                if (bName == "Netscape") {
                    objCnt.textContent = maxL - objVal.length;
                }
                else { objCnt.innerText = maxL - objVal.length; }
            }
            return true;
        }
        /** Metode for å lage document objekt */
        function createObject(objId) {
            if (document.getElementById) return document.getElementById(objId);
            else if (document.layers) return eval("document." + objId);
            else if (document.all) return eval("document.all." + objId);
            else return eval("document." + objId);
        }
    </script>

</head>
<body>
    <div id="Logo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>
    <form id="OpprettProsjekt" runat="server">
        <div id="textbox">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

            <asp:Label ID="lbl_projectName" runat="server" Text="Prosjekt navn"></asp:Label>
            <br />
            <asp:TextBox ID="tb_projectName" runat="server" Width="200px"></asp:TextBox>
            <br />

            <br />
            <asp:Label ID="lbl_dateFrom" runat="server" Text="Fra: "></asp:Label>
            <asp:TextBox ID="tb_dateFrom" runat="server"></asp:TextBox>
            <asp:CalendarExtender
                ID="calendarDateFrom"
                TargetControlID="tb_dateFrom"
                Format="dd/MM/yyyy"
                runat="server">
            </asp:CalendarExtender>

            <asp:Label ID="lbl_dateTo" runat="server" Text="Til: "></asp:Label>
            <asp:TextBox ID="tb_dateTo" runat="server"></asp:TextBox>
            <asp:CalendarExtender
                ID="calendarDateTo"
                TargetControlID="tb_dateTo"
                Format="dd/MM/yyyy"
                runat="server">
            </asp:CalendarExtender>

            <br />
            <br />

            <asp:Label ID="lblProjectDesc" runat="server" Text="Prosjektbeskrivelse"></asp:Label>

            <textarea id="TextArea_ProjectDescription" style="resize: none" maxlength="300" cols="40" rows="3" onkeydown="return taLimit(this)" onkeyup="return taCount(this, 'counter')"></textarea><br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            Du har <b><span id="counter">300</span></b> tegn igjen til din beskrivelse...
            <br />
            <br />
            <asp:Label ID="LabelTasks" runat="server" Text="Legg til tasks"></asp:Label>
            <br />
            <asp:TextBox ID="tb_tasks" runat="server" Width="200px"></asp:TextBox>
            <br />
            <asp:ListBox ID="lb_tasks" runat="server" Width="200px"></asp:ListBox>
            <br />
            <br />
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Subprosjekt" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Teams"></asp:Label>
            <br />
            <asp:ListBox ID="lb_parentProject" runat="server" Width="200px"></asp:ListBox>
            <asp:ListBox ID="lb_Team" runat="server" Width="200px"></asp:ListBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Team members"></asp:Label>
            <br />
            <asp:ListBox ID="lb_teamMembers" runat="server" Width="200px"></asp:ListBox>
            <br />
        </div>
    </form>
</body>
</html>
