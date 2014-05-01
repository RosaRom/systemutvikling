<%@ Page Language="C#"  MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Adminsiden.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Administrator</title>
    <link rel="Stylesheet" type="text/css" href="css/AdminStyle.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="Adminform" runat="server">
        <div>
          <div id="regbruker" class="panel panel-primary" style="width:100%;">
           <div class="panel-heading"><h4>Legg til bruker</h4></div>
            <div class="panel-body">
                <asp:GridView ID="GridViewInsert"
                    runat="server"
                    AutoGenerateColumns="False"
                    ShowHeaderWhenEmpty="True"
                    AutoGenerateEditButton="True"
                    OnRowCancelingEdit="GridViewInsert_RowCancelingEdit"
                    OnRowEditing="GridViewInsert_RowEditing"
                    OnRowUpdating="GridViewInsert_RowUpdating"
                    CssClass="gridView"
                    AlternatingRowStyle-CssClass="alt" OnSelectedIndexChanged="GridViewInsert_SelectedIndexChanged">

                    <AlternatingRowStyle CssClass="alt" />

                    <Columns>
                        <asp:BoundField DataField="surname" HeaderText="Etternavn" />
                        <asp:BoundField DataField="firstname" HeaderText="Fornavn" />
                        <asp:BoundField DataField="username" HeaderText="Brukernavn" /> 
                            <asp:TemplateField HeaderText="password">
                                <EditItemTemplate>
                                    <asp:TextBox ID="password" runat="server" TextMode="Password" Text='<%# Bind("password") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="phone" HeaderText="Telefon" />
                        <asp:BoundField DataField="mail" HeaderText="Mail" />

                        <asp:TemplateField HeaderText="Team">
                            <EditItemTemplate>
                                <asp:DropDownList ID="dropDownTeam" DataSource="<%# DropDownBoxTeam() %>"
                                    DataTextField="teamName" DataValueField="teamID" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Brukertype">
                            <EditItemTemplate>
                                <asp:DropDownList ID="dropDownGroup" DataSource="<%# DropDownBoxGroup() %>"
                                    DataTextField="groupName" DataValueField="groupID" runat="server">
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
            
            <div id="brukergrid" class="panel panel-primary" style="width:100%;">
                <div class="panel-heading"><h4>Oversikt over alle brukere</h4></div>
                <div class="panel-body">

                    <div id="søkefelt">
                        <asp:TextBox ID="FilterSearchTerms" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="FilterSearchDropdown" runat="server">
                            <asp:ListItem Value="userID">Bruker ID</asp:ListItem>
                            <asp:ListItem Value="firstname">Fornavn</asp:ListItem>
                            <asp:ListItem Value="surname">Etternavn</asp:ListItem>
                            <asp:ListItem Value="username">Brukernavn</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnFilter" runat="server" Text="Søk" OnClick="btnFilter_Click" />
                        <asp:Button ID="btnFjernFilter" runat="server" Text="Fjern filter" OnClick="btnFjernFilter_Click" />
                    </div>

                    <div id="knapperAktivInaktiv">
                        <asp:Button ID="btnDeaktiverte" runat="server" OnClick="btnDeaktiverte_Click" Text="Deaktiverte brukere"  />
                        <asp:Button ID="btnAktiv" runat="server" OnClick="btnAktiv_Click" Text="Aktive brukere" />
                    </div>


                            <asp:GridView ID="GridViewAdmin" AllowSorting="True" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" EnableViewState="true" DataKeyNames="userID"
                                OnRowCancelingEdit="GridViewAdmin_RowCancelingEdit" OnRowEditing="GridViewAdmin_RowEditing" OnRowUpdating="GridViewAdmin_RowUpdating" OnRowDeleting="GridViewAdmin_RowDeleting" OnSorting="GridViewAdmin_Sorting"
                                CssClass="gridView" AlternatingRowStyle-CssClass="alt" HeaderStyle-CssClass="gridViewHeader">

                                <Columns>
                                    <asp:BoundField DataField="userID" HeaderText="Id" SortExpression="userID" ReadOnly="True" ItemStyle-Width="1%" />
                                    <asp:BoundField DataField="surname" HeaderText="Etternavn" SortExpression="surname" />
                                    <asp:BoundField DataField="firstname" HeaderText="Fornavn" SortExpression="firstname" />
                                    <asp:BoundField DataField="username" HeaderText="Brukernavn" SortExpression="username" />

                                    <asp:BoundField DataField="phone" HeaderText="Telefon" SortExpression="phone" />
                                    <asp:BoundField DataField="mail" HeaderText="Mail" SortExpression="mail" />

                                    <asp:TemplateField HeaderText="Team">

                                        <ItemTemplate>
                                            <%# Eval("teamName")%>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList ID="dropDownTeamUsers" DataSource="<%# DropDownBoxTeamExistingUsers() %>"
                                                DataTextField="teamName" DataValueField="teamID" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Brukertype">

                                        <ItemTemplate>
                                            <%# Eval("groupName")%>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList ID="dropDownGroupUsers" DataSource="<%# DropDownBoxGroup() %>"
                                                DataTextField="groupName" DataValueField="groupID" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>

                                    </asp:TemplateField>

                                    <asp:CommandField ShowDeleteButton="True" DeleteText="Aktiver/Deaktiver" />
                                </Columns>

                            </asp:GridView>
                            </div>
                        </div>
                </div>

    </form>
</asp:Content>
