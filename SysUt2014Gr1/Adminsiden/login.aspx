<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Adminsiden.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Administrator</title>
    <link rel="Stylesheet" type="text/css" href="css/Login.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="loginForm" runat="server">
        <h3>
        <asp:Label ID="UsernameLabel" Text="Username"  runat="server" />
        </h3>
        <h3>
        <asp:TextBox ID="tbUsername" runat="server" />
        </h3>
        <h3>
        <asp:Label ID="PasswordLabel" Text="Password" runat="server" />
        </h3>
        <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" />
        <br /><br />
        <asp:Button ID="submit" Text="Submit" runat="server" OnClick="submit_Click" />
        <br />
        <asp:Label ID="LabelWarning" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
    </form>
</asp:Content>

