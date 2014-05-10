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

         <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Tilbake" />

        <asp:GridView ID="GridView1"  
                    Height="229px" 
                    Width="1565px"  
                    AllowPaging="True" 
                    AutoGenerateColumns="False"
                    CssClass="gridView" 
                    AlternatingRowStyle-CssClass="alt" 
                    HeaderStyle-CssClass="gridViewHeader" 
                    HorizontalAlign="Left"
                    OnRowCommand="GridView1_RowCommand"
                    runat="server" >
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
         <p>
             &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
