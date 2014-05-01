<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="ProsjektAnsvarligNyBruker.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligNyBruker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Legg til nye brukere</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarligNyBruker.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="ProsjektAnsvarligNyBruker" runat="server">
    
    <div>

         <div id="regbruker" class="panel panel-primary" style="width:100%;">
           <div class="panel-heading"><h4>Legg til ny bruker</h4></div>
            <div class="panel-body">

                <asp:GridView ID="GridViewProsjektAnsvarligInsert" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AutoGenerateEditButton="True"
                        OnRowCancelingEdit="GridViewProsjektAnsvarligInsert_RowCancelingEdit" OnRowEditing="GridViewProsjektAnsvarligInsert_RowEditing" OnRowUpdating="GridViewProsjektAnsvarligInsert_RowUpdating"
                        CssClass="gridView" AlternatingRowStyle-CssClass="alt">

                        <AlternatingRowStyle CssClass="alt" />

                        <Columns>
                            <asp:BoundField DataField="surname" HeaderText="Etternavn"/>
                            <asp:BoundField DataField="firstname" HeaderText="Fornavn"/>
                            <asp:BoundField DataField="username" HeaderText="Brukernavn"/>
                            <asp:BoundField DataField="password" HeaderText="Passord"/>
                            <asp:BoundField DataField="phone" HeaderText="Telefon"/>
                            <asp:BoundField DataField="mail" HeaderText="Mail"/>

                            <asp:TemplateField HeaderText="Team">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="dropDownTeam" DataSource="<%# DropDownBoxTeam() %>" 
                                        DataTextField="teamName" DataValueField="teamID" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Brukertype">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="dropDownBruker" runat="server">
                                        <asp:ListItem Text="Bruker" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Teamleder" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                 </asp:GridView>

            </div>
        </div>

        <div id="beskjedTilBruker">
                <asp:Label ID="beskjed" runat="server"></asp:Label>
        </div>
    </div>
    </form>
</asp:Content>

