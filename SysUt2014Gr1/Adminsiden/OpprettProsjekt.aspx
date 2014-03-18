<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpprettProsjekt.aspx.cs" Inherits="Adminsiden.OpprettProsjekt" %>

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

        /** Denne skriptfunksjonen er linket opp mote textbox med CalendarExtender fra Ajax. 
            Dette skriptet gjør at brukeren kan navigere seg i kalenderen ved hjelp av piltastene.
            Skriptet tar utgangspunkt i valgt dato, by default: dagens dato.
        function DateField_KeyDown(dateField, calendarExtenderName) {

            lastKeyCodeEntered = event.keyCode;

            if ((lastKeyCodeEntered == '37')        //keyCode 37 = left arrow
                || (lastKeyCodeEntered == '38')     //keyCode 38 = up arrow
                || (lastKeyCodeEntered == '39')     //keyCode 39 = right arrow
                || (lastKeyCodeEntered == '40'))    //keyCode 40 = down arrow
            {
                var calendar = $find(calendarExtenderName);
                var enteredDate = calendar.get_selectedDate();
                var advanceDays = 0; //lagt til

                if (enteredDate == null) {
                    enteredDate = new Date();
                }
                else {
                    //advanceDays = 0;
                    switch (lastKeyCodeEntered) {
                        case 37:
                            advanceDays = -1;
                            break;
                        case 38:
                            advanceDays = -7;
                            break;
                        case 39:
                            advanceDays = 1;
                            break;
                        case 40:
                            advanceDays = 7;
                            break;
                    }
                    enteredDate.setDate(enteredDate.getDate() + advanceDays);
                }
                dateField.value = enteredDate.getDate() + "." + (enteredDate.getMonth() + 1) + "." + enteredDate.getFullYear();
                calendar.set_selectedDate(enteredDate.getDate() + "/" + (enteredDate.getMonth() + 1) + "/" + enteredDate.getFullYear());
            }   
            
            i asp koden: onkeydown="DateField_KeyDown(this, 'calendarDateFrom')"
        }*/

    </script>

</head>
<body>
    <div id="Logo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>

    <form id="OpprettProsjekt" runat="server">

        <div id="textbox">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"
                EnableScriptGlobalization="true" EnableScriptLocalization="true">
            </asp:ToolkitScriptManager>

            <asp:Label ID="lbl_projectName" runat="server" Text="Prosjekt navn"></asp:Label>
            <br />
            <asp:TextBox ID="tb_projectName" runat="server" Width="200px"></asp:TextBox>
            <br />
            <br />

            <!--Textbox og CalendarExtender (Ajax). 
                Når brukeren trykker på textboxen kommer det en popupkalender.
                Textbox er satt til ReadOnly for å forenkle sjekk opp mot datoformat
                om brukeren kunne ha skrevet det inn i ren tekst.
                -->
            <asp:Label ID="lbl_dateFrom" runat="server" Text="Fra: "></asp:Label>
            <asp:TextBox ID="tb_dateFrom" runat="server" Width="75px" ReadOnly="true"></asp:TextBox>
            <asp:CalendarExtender
                ID="calendarDateFrom"
                TargetControlID="tb_dateFrom"
                Format="dd.MM.yyyy"
                PopupPosition="BottomRight"
                runat="server"
                CssClass="blue"
                TodaysDateFormat="dd. MMMM, yyyy">
            </asp:CalendarExtender>

            <asp:Label ID="lbl_dateTo" runat="server" Text="Til: "></asp:Label>
            <asp:TextBox ID="tb_dateTo" runat="server" Width="75px" ReadOnly="true"></asp:TextBox>
            <asp:CalendarExtender
                ID="calendarDateTo"
                TargetControlID="tb_dateTo"
                Format="dd.MM.yyyy"
                runat="server"
                CssClass="blue"
                TodaysDateFormat="dd. MMMM, yyyy">
            </asp:CalendarExtender>

            <br />
            <br />

            <asp:Label ID="lbl_ProjectDesc" runat="server" Text="Prosjektbeskrivelse"></asp:Label>
            <br />
            <textarea id="TextArea_ProjectDescription" style="resize: none" maxlength="300" cols="40" rows="3" onkeydown="return taLimit(this)" onkeyup="return taCount(this, 'counter')"></textarea><br />
            Du har <b><span id="counter">300</span></b> tegn igjen til din beskrivelse...
            <br />
            <br />
            <asp:Label ID="lbl_Underprosjekt" runat="server" Text="Gjør til underprosjekt av:"></asp:Label>
            <asp:DropDownList ID="ddl_Hovedprosjekt" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
            <br />
            <br />
            <asp:DropDownList ID="ddl_Team" runat="server" AppendDataBoundItems="true" Width="175px" OnSelectedIndexChanged="ddl_Team_SelectedIndexChanged"></asp:DropDownList>
            <span id="dummy" runat="server"></span>
            <asp:Button ID="btn_ShowTeam" runat="server" Text="Vis" Visible="true" OnClick="ModalPopup_ShowTeam" />
            <asp:Button ID="btn_CreateTeam" runat="server" Text="Opprett team" Visible="true" />

            <!--PopupExtender fra Ajax som viser en popup panel, denne ModalPopupExtender
                er linket opp mot knappen btnShowTeam: TargetControlID="btn_ShowTeam".
                Når brukeren trykker denne knappen kommer det opp et lite popupvindu som 
                viser alle personer som er i det teamet som er valgt i DropDownList for teams. 
                -->
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_Team" runat="server"
                OkControlID="btnOkay"
                TargetControlID="dummy" PopupControlID="gridViewPanel"
                PopupDragHandleControlID="PopupHeader" Drag="true"
                BackgroundCssClass="ModalPopupBackground">
            </ajaxToolkit:ModalPopupExtender>

            <!-- Panelet som er koblet opp mot ModalPopupExtender_Team
                -->
            <asp:Panel ID="gridViewPanel" runat="server" Style="border: solid 2px #000; display: none; width: auto; height: auto;">
                <div class="TeamGridViewPopup">
                    <div class="PopupHeader" id="PopupHeader" style="cursor: move">Header</div>
                    <div class="PopupGridView" id="PopupGridView">
                        <asp:GridView ID="gv_selectedTeam" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="firstname" HeaderText="Fornavn" />
                                <asp:BoundField DataField="surname" HeaderText="Etternavn" />
                                <asp:BoundField DataField="groupName" HeaderText="Rolle" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="Controls">
                        <input id="btnOkay" type="button" value="Done" />
                    </div>
                </div>
            </asp:Panel>

            <br />
            <br />
            <asp:Label ID="lbl_Tasks" runat="server" Text="Hovedtasks"></asp:Label>
            &nbsp;
            <asp:Button ID="btn_AddMainTask" runat="server" Text="Legg til" />
            <asp:Button ID="btn_CreateMainTask" runat="server" Text="Opprett hovedtask" />
            <br />
            <asp:ListBox ID="lb_tasks" runat="server" Width="200px"></asp:ListBox>
        </div>
    </form>
</body>
</html>
