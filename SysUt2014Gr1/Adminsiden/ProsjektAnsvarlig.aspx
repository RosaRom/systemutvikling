<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProsjektAnsvarlig.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarlig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Prosjektansvarlig</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarlig.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="form1" runat="server">
         <div id="regbruker" class="panel panel-primary" style="width:30%;">
           <div class="panel-heading"><h4>Administrere prosjekter</h4></div>
            <div class="panel-body">
                <div id="ProjectForm">
                <p>
                <asp:DropDownList ID="projectList" runat="server" Width="197px" OnSelectedIndexChanged="projectList_SelectedIndexChanged">
                </asp:DropDownList>
                </p>
                <p>
                    <asp:Button ID="btnEditProject" runat="server" Text="Endre" Width="99px" OnClick="btnEditProject_Click" />
                    <asp:Button ID="btnShowArchive" runat="server" Text="Vis arkiv" Width="101px" OnClick="btnShowArchive_Click" />
                </p>
                <p>
                    <asp:Button ID="btnNewProject" runat="server" Height="35px" Text="Opprett prosjekt" Width="201px" OnClick="btnNewProject_Click" />
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                </p>
            </div>
           </div>
        </div>
    </form>
</asp:Content>

