<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProsjektAnsvarligNyFase.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligNyFase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Text2 {
            margin-bottom: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <p style="height: 75px; margin-left: 40px">
            Fasenavn
            <asp:TextBox ID="tbPhasename" runat="server"></asp:TextBox>
            <br />
            <br />
            Dato
            <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
&nbsp;til
            <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
        </p>
        <p style="margin-left: 40px">
            Beskrivelse
            <textarea id="taDescription" cols="20" name="S1" rows="2"></textarea></p>
    
    </div>
        <p style="margin-left: 40px">
            <asp:Button ID="btnSubmit" runat="server" Text="Gjennomfør endringer" Width="212px" />
        </p>
    </form>
</body>
</html>
