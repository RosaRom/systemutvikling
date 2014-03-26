<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Bootstrap.Master" CodeBehind="ProsjektAnsvarligNyFase.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligNyFase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ny fase</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarligNyFase.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="NyFaseForm" runat="server">
        <br />
          Fasenavn <asp:TextBox ID="tbPhasename" runat="server" MaxLength="40" Width="298px"></asp:TextBox>
        <br />
          Dato <asp:TextBox ID="tbDateFrom" runat="server" TextMode="Date"></asp:TextBox>&nbsp;til&nbsp;
            <asp:TextBox ID="tbDateTo" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        Beskrivelse <asp:TextBox ID="tbDescription" runat="server" Height="107px" TextMode="MultiLine" Width="300px"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Gjennomfør endringer" Width="212px" OnClick="btnSubmit_Click" />
        <br />
        <asp:Label ID="lbError" runat="server" ForeColor="Red" style="text-align: center"></asp:Label>
   </form>
</asp:Content>
