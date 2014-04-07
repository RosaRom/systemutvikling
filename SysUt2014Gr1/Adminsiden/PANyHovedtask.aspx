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

        <h2 class="nyHovedtask">Lag ny kategori</h2>

        <div id="divNavn" class="nyHovedtask">
            <label>Navn på kategori</label>
            <asp:TextBox ID="hovedtaskNavn" runat="server"></asp:TextBox>
        </div>

        <div class="nyHovedtask">
            <label>ID</label>
            <asp:TextBox ID="id" runat="server"></asp:TextBox>
        </div>

        <div id="divBeskrivelse" class="nyHovedtask">
            <label id="lbBeskrivelse">Beskrivelse</label>
            <asp:TextBox ID="beskrivelse" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div class="nyHovedtask">
            <label>Fra fase</label>
            <asp:DropDownList ID="DropDownFraFase" runat="server" DataValueField="phaseID" DataTextField="phaseName"></asp:DropDownList>
            <label>Til fase</label>
            <asp:DropDownList ID="DropDownTilFase" runat="server" DataValueField="phaseID" DataTextField="phaseName"></asp:DropDownList>
        </div>

        <div class="divLagre">
            <asp:Button ID="btnLagreHovedtask" runat="server" Text="Lagre hovedtask" OnClick="btnLagreHovedtask_Click" /><br />
            <asp:Label ID="lbBeskjed" runat="server"></asp:Label>
        </div>
        
    </form>
</body>
</html>
