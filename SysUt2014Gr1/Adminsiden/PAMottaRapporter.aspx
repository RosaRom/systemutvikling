<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAMottaRapporter.aspx.cs" Inherits="Adminsiden.PAMottaRapporter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Avviksrapporter og Klager</title>
    <link rel="Stylesheet" type="text/css" href="css/PAMottaRapporter.css" />
</head>
<body>
    <form id="PAMottaRapporter" runat="server">
    <div>
        <div id="rapporter">
            <div class="border">
                <asp:Label ID="lbAvvik" runat="server" Text="Avviksrapporter: "></asp:Label>
                <asp:Label ID="lbAntallNyeRapporter" runat="server" Text="0 nye"></asp:Label>
            
                <div class="buttons">
                    <asp:Button ID="btnNyeRapporter" runat="server" Text="Vis Nye" />
                    <asp:Button ID="btnAlleRapporter" runat="server" Text="Vis Alle" />
                </div>
            </div>
            
            <br />
            <asp:GridView ID="gvRapporter"
                runat="server"
                AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" 
                OnRowDeleting="gvRapporter_RowDeleting" 
                DataKeyNames="deviationID"
                CssClass="gridView"
                AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:CommandField ShowDeleteButton="true" DeleteText="Les" />
                    <asp:BoundField DataField="deviationid" HeaderText="ID" />
                    <asp:BoundField DataField="deviationTitle" HeaderText="Overskrift" />
                </Columns>
            </asp:GridView>
        </div>
        
        <div id="klager">
            <div class="border">
                <asp:Label ID="lbKlager" runat="server" Text="Klager: "></asp:Label>
                <asp:Label ID="lbAntallNyeKlager" runat="server" Text="0 nye"></asp:Label>
            
                <div class="buttons">
                    <asp:Button ID="btnNyeKlager" runat="server" Text="Vis Nye" />
                    <asp:Button ID="btnAlleKlager" runat="server" Text="Vis Alle" />
                </div>
            </div>
            <br />
            <asp:GridView ID="gvKlager" 
                runat="server"
                AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" 
                OnRowDeleting="gvKlager_RowDeleting" 
                DataKeyNames="deviationID"
                CssClass="gridView"
                AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:CommandField ShowDeleteButton="true" DeleteText="Les" />
                    <asp:BoundField DataField="deviationid" HeaderText="ID" />
                    <asp:BoundField DataField="deviationTitle" HeaderText="Overskrift" />
                </Columns>
            </asp:GridView>
        </div>
            
        <div id="tekstboks">
            <label id="tekstbokslabel" class="border">Beskrivelse av rapporten</label><br />    
            <asp:TextBox ID="informasjon" runat="server" TextMode="MultiLine" Rows="5" Columns="30"></asp:TextBox>
        </div>
        
    </div>
    </form>
</body>
</html>
