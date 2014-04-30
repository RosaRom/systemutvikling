<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAEditHovedtask.aspx.cs" Inherits="Adminsiden.PAEditHovedtask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 100px;
            width: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lbTaskCategoryName" runat="server" Text="Tasknavn"></asp:Label>
        <asp:TextBox ID="tbTaskCategoryName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lbDescription" runat="server" Text="Beskrivelse"></asp:Label>
        <asp:TextBox id="taTaskCategoryDesc" TextMode="multiline" Columns="50" Rows="5" runat="server" />
        <br />
        <asp:Button ID="btnCommit" runat="server" OnClick="Button1_Click" Text="Lagre endringer" />
        <br />
        <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
