<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpprettTeam.aspx.cs" Inherits="Adminsiden.OpprettTeam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opprett team</title>
    <link rel="Stylesheet" type="text/css" href="css/OpprettTeamStyle.css" />
</head>
<body>
    <div id="Logo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>
    <form id="TeamForm" runat="server">
        <div id="Team">   
            <br />
            <asp:Button ID="btn_opprett" runat="server" OnClick="btn_opprett_Click" Text="Opprett team" />
            <asp:TextBox ID="tb_newTeam" runat="server" Visible="False" Width="144px"></asp:TextBox>
            <asp:Button ID="btn_createTeam" runat="server" OnClick="btn_createTeam_Click" Text="OK" Visible="False" Width="39px" />
            <asp:Button ID="btn_abort" runat="server" OnClick="btn_abort_Click" Text="Avbryt" Visible="False" />
            <br />   
            <asp:DropDownList ID="ddl_selectTeam" runat="server" AppendDataBoundItems="true">
            </asp:DropDownList>
            <asp:Button ID="btn_selectTeam" runat="server" OnClick="btn_selectTeam_Click" Text="Velg" />
            <br />
            <asp:DropDownList ID="ddl_users" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
            <br />
            <asp:Button ID="btn_addUser" runat="server" Text="Legg til bruker" OnClick="btn_addUser_Click" />
            <asp:Button ID="btn_addTeamleader" runat="server" Text="Legg til teamleder" OnClick="btn_addTeamleader_Click" />
            <br />
            <asp:GridView ID="GridView1" 
                runat="server"
                AllowPaging="True" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="firstname" HeaderText="Fornavn" />
                    <asp:BoundField DataField="surname" HeaderText="Etternavn" />
                    <asp:BoundField DataField="groupName" HeaderText="Rolle i teamet" />
                    <asp:buttonfield buttontype="Button" text="Slett"/>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
