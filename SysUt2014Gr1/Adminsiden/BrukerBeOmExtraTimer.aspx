<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrukerBeOmExtraTimer.aspx.cs" Inherits="Adminsiden.BrukerBeOmExtraTimer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Be om
            <asp:TextBox ID="tbEkstraTimer" runat="server" Width="24px"></asp:TextBox>
&nbsp;ekstra timer på
            <asp:DropDownList ID="ddlTaskValg" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlTaskValg_SelectedIndexChanged"></asp:DropDownList>
        &nbsp;<asp:Button ID="btnCommit" runat="server" Text="Send" OnClick="btnCommit_Click" />
            <asp:Label ID="lbCommitStatus" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="lbValgtTaskInfo" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
