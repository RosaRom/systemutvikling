<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Admin.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrator</title>
    <link rel="Stylesheet" type="text/css" href="AdminStyle.css" />
</head>
<body>

    <div id="adminLogo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>

    <form id="Adminform" runat="server">

        <div>
            <div id="overskriftInsert">
                <asp:Label runat="server">Legg til nye brukere</asp:Label>
            </div>

            <asp:GridView ID="GridViewInsert" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AutoGenerateEditButton="True"
                OnRowCancelingEdit="GridViewInsert_RowCancelingEdit" OnRowEditing="GridViewInsert_RowEditing" OnRowUpdating="GridViewInsert_RowUpdating"
                CssClass="gridView" AlternatingRowStyle-CssClass="alt" OnSelectedIndexChanged="GridViewInsert_SelectedIndexChanged">

                <AlternatingRowStyle CssClass="alt" />

                <Columns>
                    <asp:BoundField DataField="surname" HeaderText="Etternavn" SortExpression="surname" />
                    <asp:BoundField DataField="firstname" HeaderText="Fornavn" SortExpression="firstname" />
                    <asp:BoundField DataField="username" HeaderText="Brukernavn" SortExpression="username" />

                    <asp:BoundField DataField="phone" HeaderText="Telefon" SortExpression="phone" />
                    <asp:BoundField DataField="mail" HeaderText="Mail" SortExpression="mail" />
                    <asp:BoundField DataField="teamID" HeaderText="TeamId" SortExpression="teamID" />
                    <asp:BoundField DataField="groupID" HeaderText="GruppeId" SortExpression="groupID" />
                </Columns>
            </asp:GridView>

            <div id="søkefelt">
                <asp:TextBox ID="FilterSearchTerms" runat="server"></asp:TextBox>
                <asp:DropDownList ID="FilterSearchDropdown" runat="server" OnSelectedIndexChanged="FilterSearchDropdown_SelectedIndexChanged">
                    <asp:ListItem Value="userID">Bruker ID</asp:ListItem>
                    <asp:ListItem Value="firstname">Fornavn</asp:ListItem>
                    <asp:ListItem Value="surname">Etternavn</asp:ListItem>
                    <asp:ListItem Value="username">Brukernavn</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnFilter" runat="server" Text="Søk" OnClick="btnFilter_Click" />
                <asp:Button ID="btnFjernFilter" runat="server" Text="Fjern filter" OnClick="btnFjernFilter_Click" />
            </div>

            <div id="knapperAktivInaktiv">
                <asp:Button ID="btnDeaktiverte" runat="server" OnClick="btnDeaktiverte_Click" Text="Deaktiverte brukere" />
                <asp:Button ID="btnAktiv" runat="server" OnClick="btnAktiv_Click" Text="Aktive brukere" />
                <asp:Label runat="server">Oversikt over alle brukere</asp:Label>
            </div>

            <asp:GridView ID="GridViewAdmin" AllowSorting="True" AllowPaging="True" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" EnableViewState="true" DataKeyNames="userID"
                OnRowCancelingEdit="GridViewAdmin_RowCancelingEdit" OnRowEditing="GridViewAdmin_RowEditing" OnRowUpdating="GridViewAdmin_RowUpdating" OnRowDeleting="GridViewAdmin_RowDeleting" OnSorting="GridViewAdmin_Sorting"
                CssClass="gridView" AlternatingRowStyle-CssClass="alt" HeaderStyle-CssClass="gridViewHeader">

                <Columns>
                    <asp:BoundField DataField="userID" HeaderText="Id" SortExpression="userID" ReadOnly="True" />
                    <asp:BoundField DataField="surname" HeaderText="Etternavn" SortExpression="surname" />
                    <asp:BoundField DataField="firstname" HeaderText="Fornavn" SortExpression="firstname" />
                    <asp:BoundField DataField="username" HeaderText="Brukernavn" SortExpression="username" />

                    <asp:BoundField DataField="phone" HeaderText="Telefon" SortExpression="phone" />
                    <asp:BoundField DataField="mail" HeaderText="Mail" SortExpression="mail" />
                    <asp:BoundField DataField="teamID" HeaderText="TeamId" SortExpression="teamID" />
                    <asp:BoundField DataField="groupID" HeaderText="GruppeId" SortExpression="groupID" />
                    <asp:BoundField DataField="aktiv" HeaderText="Aktiv" SortExpression="aktiv" ReadOnly="True" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Aktiver/Deaktiver" />
                </Columns>

            </asp:GridView>
        </div>

    </form>
</body>
</html>
