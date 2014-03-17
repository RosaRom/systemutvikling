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
            <asp:TextBox ID="tb_dateFrom" runat="server" Width="75px"></asp:TextBox>
            <asp:CalendarExtender
                ID="calendarDateFrom"
                TargetControlID="tb_dateFrom"
                Format="dd/MM/yyyy"
                runat="server">
            </asp:CalendarExtender>

            <asp:Label ID="lbl_dateTo" runat="server" Text="Til: "></asp:Label>
            <asp:TextBox ID="tb_dateTo" runat="server" Width="75px"></asp:TextBox>
            <asp:CalendarExtender
                ID="calendarDateTo"
                TargetControlID="tb_dateTo"
                Format="dd/MM/yyyy"
                runat="server">
            </asp:CalendarExtender>

            <br />
            <br />

            <asp:Label ID="lblProjectDesc" runat="server" Text="Prosjektbeskrivelse"></asp:Label>
            <br />
            <textarea id="TextArea_ProjectDescription" style="resize: none" maxlength="300" cols="40" rows="3" onkeydown="return taLimit(this)" onkeyup="return taCount(this, 'counter')"></textarea><br />
            Du har <b><span id="counter">300</span></b> tegn igjen til din beskrivelse...
            <br />
            <br />
            <asp:Label ID="lblUnderprosjekt" runat="server" Text="Gjør til underprosjekt av:"></asp:Label>
            <asp:DropDownList ID="ddlUnderprosjekt" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:DropDownList ID="ddlTeam" runat="server" Width="175px"></asp:DropDownList>
            <asp:Button ID="btnShowTeam" runat="server" Text="Vis" Visible="true" />
            <asp:Button ID="btnCreateTeam" runat="server" Text="Opprett team" Visible="true" />

            <!-- PopupExtender fra Ajax som viser en popup panel, denne ModalPopupExtender
                er linket opp mot knappen btnShowTeam: TargetControlID="btnShowTeam".
                Når brukeren trykker denne knappen kommer det opp et lite popupvindu som 
                viser alle personen som er i det teamet som er valgt i DropDownList for teams. -->
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderTeam" runat="server"
                OkControlID="btnOkay" CancelControlID="btnCancel"
                TargetControlID="btnShowTeam" PopupControlID="testPanel"
                PopupDragHandleControlID="PopupHeader" Drag="true"
                BackgroundCssClass="ModalPopupBG">
            </ajaxToolkit:ModalPopupExtender>

            <!-- Panelet som er koblet opp mot ModalPopupExtenderTeam-->
            <asp:Panel ID="testPanel" runat="server" Style="border: solid 2px #000; display: none; width: 30%; height: auto;">
                <div class="HelloWorldPopup">
                    <div class="PopupHeader" id="PopupHeader" style="cursor: move">Header</div>
                    <div class="PopupBody" id="PopupBody">
                        <p>This is a simple modal dialog</p>
                    </div>
                    <div class="Controls">
                        <input id="btnOkay" type="button" value="Done" />
                        <input id="btnCancel" type="button" value="Cancel" />
                    </div>
                </div>
            </asp:Panel>

            <br />
            <br />
            <asp:Label ID="LabelTasks" runat="server" Text="Hovedtasks"></asp:Label>
            &nbsp;
            <asp:Button ID="btnAddMainTask" runat="server" Text="Legg til" />
            <asp:Button ID="btnCreateMainTask" runat="server" Text="Opprett hovedtask" />
            <br />
            <asp:ListBox ID="lb_tasks" runat="server" Width="200px"></asp:ListBox>
        </div>
    </form>
</body>
</html>
