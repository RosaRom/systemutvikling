<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAVisHovedtask.aspx.cs" Inherits="Adminsiden.PAVisHovedtask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lbTaskCategoryName" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lbTaskCategoryDesc" runat="server" Text="Beskrivelse"></asp:Label><asp:TextBox id="taTaskCategoryDesc" TextMode="multiline" Columns="50" Rows="5" runat="server" />
    
    </div>
    </form>
</body>
</html>
