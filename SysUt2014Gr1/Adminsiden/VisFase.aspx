<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisFase.aspx.cs" Inherits="Adminsiden.VisFase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lbPhaseName" runat="server" Text="FASENAVN"></asp:Label>
        <br />
    
    </div>
        <asp:Label ID="lbDateFrom" runat="server" Text="fraDato"></asp:Label>
&nbsp;-
        <asp:Label ID="lbDateTo" runat="server" Text="tilDato"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbDescription" runat="server" Text="Beskrivelse"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbHoursUsed" runat="server" Text="Totale timer brukt"></asp:Label>
&nbsp;/
        <asp:Label ID="lbHoursAllocated" runat="server" Text="totale timer allokert"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbFinishedTasks" runat="server" Text="Ferdige tasks:"></asp:Label>
&nbsp;<asp:Label ID="lbFinishedTaskNum" runat="server" Text="x"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbUnfinishedTasks" runat="server" Text="Uferdige tasks:"></asp:Label>
&nbsp;<asp:Label ID="lbUnfinishedTaskNum" runat="server" Text="y"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="gvTaskList" runat="server">
        </asp:GridView>
    </form>
</body>
</html>
