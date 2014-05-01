<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAGodkjennEkstraTid.aspx.cs" Inherits="Adminsiden.PAGodkjennEkstraTid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="label1" runat="server" Text="Forespørsler om ekstra tid på tasks"></asp:Label>
        <br />
    
        <asp:GridView ID="gvTaskList" runat="server">
        </asp:GridView>      
    
    </div>
    </form>
</body>
</html>
