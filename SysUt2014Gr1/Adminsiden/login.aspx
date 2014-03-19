<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Adminsiden.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="Stylesheet" type="text/css" href="css/Login.css" />
</head>
<body>
     <div id="Logo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>
    <form id="loginForm" runat="server">
        <h3>
        <asp:Label ID="UsernameLabel" Text="Username"  runat="server" />
        </h3>
        <h3>
        <asp:TextBox ID="UsernameTextBox" runat="server" />
        </h3>
        <h3>
        <asp:Label ID="PasswordLabel" Text="Password" runat="server" />
        </h3>
        <asp:TextBox ID="PasswordTextBox" TextMode="Password" runat="server" />
        <br /><br />
        <asp:Button ID="submit" Text="Submit" runat="server" OnClick="submit_Click" />
        <br />
        <asp:Label ID="LabelWarning" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
    </form>
</body>
</html>
