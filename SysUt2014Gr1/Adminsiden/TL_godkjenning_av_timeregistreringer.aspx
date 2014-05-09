<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TL_godkjenning_av_timeregistreringer.aspx.cs" Inherits="Adminsiden.TL_godkjenning_av_timeregistreringer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <p>
            <h1 style="margin-left: 520px">GODKJENNING AV TIMEREGISTRERINGER</h1></p>
    
    </div>
        <p style="margin-left : 520px">
            &nbsp;</p>
        <p style="margin-left : 520px">
            &nbsp;</p>
        <p style="margin-left : 520px">
            &nbsp;</p>

        <asp:GridView ID="GridView1" runat="server" Height="229px" Width="1565px" AutoGenerateColumns="False">
            <Columns>
                 <asp:BoundField DataField ="start" HeaderText="Start" />
                <asp:BoundField DataField="stop" HeaderText="Slutt" />
                <asp:BoundField DataField="username" HeaderText="Brukernavn" />
                <asp:BoundField DataField="taskName" HeaderText="Task" />
                <asp:BoundField DataField="workplace" HeaderText="Sted" />
                <asp:BoundField DataField="description" HeaderText="Beskrivelse" />
                <asp:BoundField DataField="priority" HeaderText="Prioritet" />
                <asp:ButtonField ButtonType="Button" CommandName="godkjent" HeaderText="Godkjenn" Text="OK" ItemStyle-Width="1%" />
                <asp:ButtonField ButtonType="Button" CommandName="ikkeGodkjent" HeaderText="Ikke godkjenn" Text="Avslå" ItemStyle-Width="1%" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
         <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Tilbake" />
    </form>
</body>
</html>
