﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Admin.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="Adminform" runat="server">
    <div>
    
        <asp:GridView ID="GridViewAdmin" AllowSorting="True" AllowPaging="True" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" DataKeyNames="userID" 
            OnRowCancelingEdit="GridViewAdmin_RowCancelingEdit" OnRowEditing="GridViewAdmin_RowEditing" OnRowUpdating="GridViewAdmin_RowUpdating" OnRowDeleting="GridViewAdmin_RowDeleting">
            
            <Columns>
                <asp:BoundField DataField="userID" HeaderText="Id" SortExpression="userID" ReadOnly="True" />
                <asp:BoundField DataField="surname" HeaderText="Etternavn" SortExpression="surname" />
                <asp:BoundField DataField="firstname" HeaderText="Fornavn" SortExpression="firstname" />
                <asp:BoundField DataField="username" HeaderText="Brukernavn" SortExpression="username" />

                <asp:BoundField DataField="phone" HeaderText="Telefon" SortExpression="phone" />
                <asp:BoundField DataField="mail" HeaderText="Mail" SortExpression="mail" />
                <asp:BoundField DataField="teamID" HeaderText="TeamId" SortExpression="teamID" />
                <asp:BoundField DataField="groupID" HeaderText="GruppeId" SortExpression="groupID" />
                <asp:BoundField DataField="aktiv" HeaderText="Aktiv" SortExpression="aktiv" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Aktiver/Deaktiver" />
            </Columns>

        </asp:GridView>

    </div>
    </form>
</body>
</html>
