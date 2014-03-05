<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prosjektvalg.aspx.cs" Inherits="Adminsiden.Prosjektvalg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 54px;
            margin-top: 0px;
        }
    </style>
</head>
<body style="height: 166px">
    <form id="form1" runat="server">
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="gridView" AlternatingRowStyle-CssClass="alt" HeaderStyle-CssClass="gridViewHeader">
            <Columns>
               <asp:BoundField DataField="projectName" HeaderText="Prosjekt navn" />
               <asp:BoundField DataField="projectDescription" HeaderText="Prosjekt beskrivelse" />

            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
