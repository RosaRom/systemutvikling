<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Admin.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridViewAdmin" AllowSorting="True" AllowPaging="True" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" DataKeyNames="id" 
            OnRowCancelingEdit="GridViewAdmin_RowCancelingEdit" OnRowEditing="GridViewAdmin_RowEditing" OnRowUpdating="GridViewAdmin_RowUpdating">
            
            <Columns>
                <asp:BoundField DataField="id" HeaderText="Id" SortExpression="id" ReadOnly="True" />
                <asp:BoundField DataField="fornavn" HeaderText="Fornavn" SortExpression="fornavn" />
                <asp:BoundField DataField="etternavn" HeaderText="Etternavn" SortExpression="etternavn" />
                <asp:BoundField DataField="stilling" HeaderText="Stilling" SortExpression="stilling" />
            </Columns>

        </asp:GridView>

    </div>
    </form>
</body>
</html>
