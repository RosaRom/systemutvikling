<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProsjektAnsvarligNyBruker.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligNyBruker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Legg til nye brukere</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarligNyBruker.css" />
</head>
<body>

    <div id="adminLogo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>

    <form id="ProsjektAnsvarligNyBruker" runat="server">
    
    <div>

        <div id="overskriftInsert">
                <asp:Label runat="server">Legg til nye brukere</asp:Label>
            </div>

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

                    <asp:BoundField DataField="groupName" HeaderText="Brukertype" ReadOnly="True" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                </Columns>
         </asp:GridView>

        <div id="beskjedTilBruker">
                <asp:Label ID="beskjed" runat="server"></asp:Label>
            </div>
    </div>
    </form>
</body>
</html>
