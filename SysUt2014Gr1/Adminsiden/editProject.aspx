<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editProject.aspx.cs" Inherits="Adminsiden.editProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:TextBox ID="tbProjectName" runat="server" Width="202px"></asp:TextBox>
        <p>
            <asp:TextBox ID="tbProjectDescription" runat="server" Height="149px" Width="202px"></asp:TextBox>
        </p>
        <asp:Button ID="btnUpdateQuery" runat="server" OnClick="btnUpdateQuery_Click" Text="Send" />
        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" />
    </form>
</body>
</html>
