<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="visTaskdetaljer.aspx.cs" Inherits="Adminsiden.visTaskdetaljer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Taskdetaljer</title>
    <link rel="Stylesheet" type="text/css" href="css/VisTaskdetaljer.css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="MainForm" runat="server">
    
        <div="width: 100%;">

            <div id="left" class="panel panel-primary" style="float: left; width: 33%;">
                  <div class="panel-heading"><h4>Task informasjon</h4></div>
                  <div class="panel-body">
                      <b>Navn: </b><asp:Label ID="Label_navn" runat="server"></asp:Label><br /><br />
                      <b>Beskrivelse: </b><br />
                      <asp:TextBox id="tb_desc" rows="3" TextMode="multiline" runat="server" ReadOnly="True" Width="235px" /><br /><br />
                      <b>Prioritet: </b><asp:Label ID="Label_prioritet" runat="server"></asp:Label><br /><br />
                  </div>
            </div>
            <div id="middle" class="panel panel-primary" style="float: left; width: 33%;">
                <div class="panel-heading"><h4>Tidsoversikt for task </h4>
                </div>
                <div class="panel-body">
                    <b>fremgang i task: </b><progress value="20" max="100" id="progressbar1"></progress>
                </div>
            </div>
            <div id="right" class="panel panel-primary" style="float: left; width: 33%;">
                <div class="panel-heading"><h4>Tilhørende faseinformasjon</h4></div>
                <div class="panel-body">
                </div>
            </div>
    
    </div>
    </form>
</asp:Content>
