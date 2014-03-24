<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prosjektvalg.aspx.cs" Inherits="Adminsiden.Prosjektvalg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Velg prosjekt</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektvalgStyle.css" />
</head>
<body>

    <div id="Logo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
                <h3>Velg prosjekt</h3>
    </div>

    <form id="prosjektvalgForm" runat="server">
        <asp:GridView ID="GridViewProject" 
            AllowPaging="True" 
            AutoGenerateColumns="False"
            CssClass="gridView" 
            AlternatingRowStyle-CssClass="alt" 
            HeaderStyle-CssClass="gridViewHeader" 
            HorizontalAlign="Left"
            OnRowCommand="GridViewProject_RowCommand"
            runat="server" >
            <Columns>
                <asp:buttonfield buttontype="Button" text="Velg prosjekt"/>
                <asp:BoundField DataField="projectID" HeaderText="ID" />
                <asp:BoundField DataField="projectName" HeaderText="Prosjekt navn" />
                <asp:BoundField DataField="projectDescription" HeaderText="Prosjekt beskrivelse" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
