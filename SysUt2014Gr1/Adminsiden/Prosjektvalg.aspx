<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prosjektvalg.aspx.cs" Inherits="Adminsiden.Prosjektvalg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 54px;
            margin-top: 0px;
        }
    </style>
</head>
<body style="height: 166px">
    <form id="form1" runat="server">
    <div>
    
    </div>
        <br />
        <asp:DropDownList ID="Dropdown_prosjekt" runat="server" Height="30px" OnSelectedIndexChanged="Dropdown_prosjekt_SelectedIndexChanged" Width="140px">
        </asp:DropDownList>
        <p>
            <asp:TextBox ID="Tb_description" runat="server" Height="57px" Width="137px"></asp:TextBox>
        </p>
    </form>
</body>
</html>
