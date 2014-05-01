<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Adminsiden.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Administrator</title>
    <link rel="Stylesheet" type="text/css" href="css/Login.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


      <div class="container">

          <form class="form-signin" role="form" runat="server">
            <h2 class="form-signin-heading">Please sign in</h2>
            <asp:TextBox ID="tbUsername" type="text" class="form-control" placeholder="Brukernavn" runat="server" />
            <asp:TextBox ID="tbPassword" type="password" class="form-control" placeholder="Password" runat="server" />
            <asp:Button  ID="submit" type="submit" OnClick="submit_Click" Text="Logg inn" class="btn btn-lg btn-primary btn-block" runat="server" />
            <asp:Label ID="LabelWarning" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>

          </form>
    </div> <!-- /container -->
</asp:Content>

