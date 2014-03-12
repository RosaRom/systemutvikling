<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProsjektAnsvarligVisTeam.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligVisTeam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vis Team</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarligVisTeam.css" />
</head>
<body>

    <div id="adminLogo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>

    <form id="ProsjektAnsvarligVisTeam" runat="server">
       
    <div>
        
        <h1 id="overskriftTeamNavn">
             <asp:Label ID="teamNavn" runat="server">Team 1</asp:Label>
        </h1>

    </div>
        <asp:GridView ID="GridViewTeam" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridViewTeam_RowDeleting">
            <Columns>
                <asp:BoundField DataField="userID" HeaderText="Id"/>
                <asp:BoundField DataField="firstname" HeaderText="Fornavn"/>
                <asp:BoundField DataField="surname" HeaderText="Etternavn"/>
                <asp:BoundField DataField="groupName" HeaderText="Brukertype"/>
                <asp:CommandField ShowDeleteButton="True" DeleteText="Fjern fra team"/>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
