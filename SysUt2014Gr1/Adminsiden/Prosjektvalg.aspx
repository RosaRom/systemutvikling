<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Prosjektvalg.aspx.cs" Inherits="Adminsiden.Prosjektvalg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" type="text/css" href="css/ProsjektvalgStyle.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="prosjektvalgForm" runat="server">
        <div id="header"><h3>Velg prosjekt</h3></div>
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
                <asp:buttonfield buttontype="Button" text="Velg prosjekt" ItemStyle-Width="1%"/>
                <asp:BoundField DataField="projectID" HeaderText="ID" ItemStyle-Width="5%"/>
                <asp:BoundField DataField="projectName" HeaderText="Prosjekt navn" ItemStyle-Width="20%" />
                <asp:BoundField DataField="projectDescription" HeaderText="Prosjekt beskrivelse" ItemStyle-Width="30%" />
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>