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
            Prosjekt</p>
        <p>
            <asp:Button ID="newProject" runat="server" Height="35px" Text="Nytt prosjekt" Width="196px" />
        </p>
        <asp:DropDownList ID="projectList" runat="server" Width="197px">
        </asp:DropDownList>
        <p>
            <asp:Button ID="viewProjectFromList" runat="server" Text="Endre prosjekt" Width="196px" />
        </p>
    </form>
</body>
</html>
