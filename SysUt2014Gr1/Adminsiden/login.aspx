<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Adminsiden.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <title>Administrator</title>
    <link rel="Stylesheet" type="text/css" href="css/Login.css" />

</head>
<body>
      <div class="container">
          <form class="form-signin" role="form" runat="server">
            <div id="login">
                <h2 class="form-signin-heading">Please sign in</h2>
                <asp:TextBox ID="tbUsername" type="text" class="form-control" placeholder="Brukernavn" runat="server" />
                <asp:TextBox ID="tbPassword" type="password" class="form-control" placeholder="Password" runat="server" />
                <asp:Button  ID="submit" type="submit" OnClick="submit_Click" Text="Logg inn" class="btn btn-lg btn-primary btn-block" runat="server" />
                <asp:Label ID="LabelWarning" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
            </div>
          </form>
       </div> <!-- /container -->
</body>
</html>