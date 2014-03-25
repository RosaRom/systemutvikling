<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTask.aspx.cs" Inherits="Adminsiden.EditTask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:TextBox ID="tbTaskName" runat="server"></asp:TextBox>
    
    </div>
        <asp:DropDownList ID="ddlAddUser" runat="server">
        </asp:DropDownList>
        <p>
            <asp:TextBox ID="tbAllocatedTime" runat="server"></asp:TextBox>
        </p>
        <asp:TextBox ID="tbPriority" runat="server"></asp:TextBox>
        <p>
            <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
        </p>
        <asp:Label ID="Label1" runat="server" Text="Gjør avhengig av"></asp:Label>
        <asp:DropDownList ID="ddlDependency" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnDependency" runat="server" Text="OK" />
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Lagre endringer" />
        <br />
    </form>
</body>
</html>
