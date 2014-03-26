<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PANyHovedtask.aspx.cs" Inherits="Adminsiden.PANyHovedtask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ny Hovedtask</title>
    <link rel="Stylesheet" type="text/css" href="css/PANyHovedtask.css" />
</head>

<body>
    <form id="form1" runat="server">

        <div id="adminLogo">
            <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
        </div>

        <div id="divNavn">
            <label>Navn på hovedtask</label>
            <asp:TextBox ID="hovedtaskNavn" runat="server"></asp:TextBox>
        </div>

        <div id="divBeskrivelse">
            <label id="lbBeskrivelse">Beskrivelse</label>
            <asp:TextBox ID="beskrivelse" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div>
            <label>Fra fase</label>
            <asp:DropDownList ID="DropDownFraFase" runat="server"></asp:DropDownList>
            <label>Til fase</label>
            <asp:DropDownList ID="DropDownTilFase" runat="server"></asp:DropDownList>
        </div>

        <asp:Button ID="btnLagreHovedtask" runat="server" Text="Lagre hovedtask" />
    </form>
</body>
</html>
