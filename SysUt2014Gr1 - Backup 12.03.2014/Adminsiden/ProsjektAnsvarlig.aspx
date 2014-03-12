<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProsjektAnsvarlig.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarlig" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <p>
        <asp:DropDownList ID="projectList" runat="server" Width="197px">
        </asp:DropDownList>
            <asp:Button ID="btnShowProject" runat="server" Text="Vis" />
        </p>
        <p>
            <asp:Button ID="btnEditProject" runat="server" Text="Endre" Width="66px" OnClick="btnEditProject_Click" />
            <asp:Button ID="btnArchiveProject" runat="server" Text="Arkiver" Width="81px" />
            <asp:Button ID="btnShowArchive" runat="server" Text="Vis arkiv" Width="90px" />
        </p>
        <p>
            <asp:Button ID="btnNewProject" runat="server" Height="35px" Text="Opprett prosjekt" Width="236px" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="btnEditProfile" runat="server" Text="Endre profilinstillinger" Width="236px" />
        </p>
    </form>
</body>
</html>
