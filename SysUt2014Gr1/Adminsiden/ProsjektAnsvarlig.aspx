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
                    <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" Text="&lt;&lt; Velg" />
                </p>
                <p>
                    <asp:Button ID="btnEditProject" runat="server" Text="Endre" Width="66px" OnClick="btnEditProject_Click" />
                    <asp:Button ID="btnArchiveProject" runat="server" Text="Arkiver" Width="81px" OnClick="btnArchiveProject_Click" />
                    <asp:Button ID="btnShowArchive" runat="server" Text="Vis arkiv" Width="90px" />
                    <asp:Button ID="btnShowProject" runat="server" Text="Vis" />
                </p>
                <p>
                    <asp:Button ID="btnNewProject" runat="server" Height="35px" Text="Opprett prosjekt" Width="236px" OnClick="btnNewProject_Click" />
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Button ID="btnEditProfile" runat="server" Text="Endre profilinstillinger" Width="236px" />
                </p>
            </div>
           </div>
        </div>
    </form>
</asp:Content>

