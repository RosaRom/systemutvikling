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
             <asp:Label ID="teamNavn" runat="server"></asp:Label>
        </h1>

    </div>
        <asp:GridView ID="GridViewTeam" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridViewTeam_RowDeleting" CssClass="gridView" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="userID" HeaderText="Id"/>
                <asp:BoundField DataField="firstname" HeaderText="Fornavn"/>
                <asp:BoundField DataField="surname" HeaderText="Etternavn"/>
                <asp:BoundField DataField="groupName" HeaderText="Brukertype"/>
                <asp:BoundField DataField="teamName" HeaderText="Team"/>
                <asp:CommandField ShowDeleteButton="True" DeleteText="Fjern fra team"/>
            </Columns>
        </asp:GridView>

        <asp:GridView ID="GridViewProject" runat="server" AutoGenerateColumns="False" CssClass="gridView" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField DataField="projectID" HeaderText="ID"/>
                <asp:BoundField DataField="projectName" HeaderText="Prosjekt"/>
                <asp:BoundField DataField="projectDescription" HeaderText="Beskrivelse"/>
            </Columns>
        </asp:GridView>

        <div id="nyTeamleder">
            <asp:Label ID="info" runat="server">Endre valgt bruker til Teamleder</asp:Label>
            <br/>
            <asp:DropDownList ID="DropDownTeam" runat="server">
            </asp:DropDownList>

            <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" />
        </div>
    </form>
</body>
</html>
