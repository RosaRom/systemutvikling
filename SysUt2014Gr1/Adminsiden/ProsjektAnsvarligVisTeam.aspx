<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="ProsjektAnsvarligVisTeam.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligVisTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Vis Team</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarligVisTeam.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="ProsjektAnsvarligVisTeam" runat="server">
        <div id="regbruker" class="panel panel-primary" style="width:100%;">
           <div class="panel-heading"><h4>Vis team</h4></div>
            <div class="panel-body">
                <div>
                    <h1 id="overskriftTeamNavn">
                         <asp:Label ID="teamNavn" runat="server"></asp:Label>
                    </h1>
                </div>
                    <asp:GridView ID="GridViewTeam" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridViewTeam_RowDeleting" CssClass="gridView" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="userID" HeaderText="Id" ItemStyle-Width="1%"/>
                            <asp:BoundField DataField="firstname" HeaderText="Fornavn"/>
                            <asp:BoundField DataField="surname" HeaderText="Etternavn"/>
                            <asp:BoundField DataField="groupName" HeaderText="Brukertype"/>
                            <asp:BoundField DataField="teamName" HeaderText="Team"/>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="Fjern fra team" ItemStyle-Width="10%"/>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="GridViewProject" runat="server" AutoGenerateColumns="False" CssClass="gridView" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="projectID" HeaderText="ID" ItemStyle-Width="1%"/>
                            <asp:BoundField DataField="projectName" HeaderText="Prosjekt"/>
                            <asp:BoundField DataField="projectDescription" HeaderText="Beskrivelse"/>
                        </Columns>
                    </asp:GridView>

                    <div id="nyTeamleder">
                        <asp:Label ID="info" runat="server">Endre valgt bruker til Teamleder</asp:Label>
                        <br/>
                        <asp:DropDownList ID="DropDownTeam" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" />
                    </div>
              </div>
         </div>
    </form>
</asp:Content>
