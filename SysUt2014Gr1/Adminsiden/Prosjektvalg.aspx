<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prosjektvalg.aspx.cs" Inherits="Adminsiden.Prosjektvalg" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <title>Prosjektvalg</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektvalgStyle.css" />
</head>
<body>
    <form id="prosjektvalgForm" runat="server">

        <div id="prosjektvalg" class="panel panel-primary">
           <div class="panel-heading"><h4>Velg prosjekt</h4></div>
            <div class="panel-body">
                <asp:Label ID="Label_ingenProsjekt" runat="server" visible="false"></asp:Label>
                <asp:GridView ID="GridViewProject" 
                    AutoGenerateColumns="False"
                    CssClass="gridView" 
                    AlternatingRowStyle-CssClass="alt" 
                    HeaderStyle-CssClass="gridViewHeader" 
                    HorizontalAlign="Left"
                    OnRowCommand="GridViewProject_RowCommand"
                    runat="server" OnSelectedIndexChanged="GridViewProject_SelectedIndexChanged" >
                    <Columns>
                        <asp:buttonfield buttontype="Button" text="Velg prosjekt" ItemStyle-Width="1%"/>
                        <asp:BoundField DataField="projectID" HeaderText="ID" ItemStyle-Width="5%"/>
                        <asp:BoundField DataField="projectName" HeaderText="Prosjekt navn" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="projectDescription" HeaderText="Prosjekt beskrivelse" ItemStyle-Width="30%" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>