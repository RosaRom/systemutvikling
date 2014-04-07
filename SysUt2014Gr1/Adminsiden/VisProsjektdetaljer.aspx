<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="VisProsjektdetaljer.aspx.cs" Inherits="Adminsiden.VisTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Prosjektdetaljer</title>
    <link rel="Stylesheet" type="text/css" href="css/VisProsjektdetaljer.css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="MainForm" runat="server">
        
        <div="width: 100%;">

           <div id="left" class="panel panel-primary" style="float: left; width: 33%;">
              <div class="panel-heading"><h4>Prosjekt informasjon</h4></div>
              <div class="panel-body">
                <b>Navn: </b><asp:Label ID="Label_navn" runat="server"></asp:Label><br /><br />
                <b>Beskrivelse: </b><br />
                  <asp:TextBox id="tb_desc" rows="3" TextMode="multiline" runat="server" ReadOnly="True" Width="235px" /><br /><br />
                <b>Tidsperiode: </b> Fra - til dato</><br /><br />
                <b>Prosjektleder: </b><asp:Label ID="Label_Prosjektleder" runat="server"></asp:Label>
                  <br />
                  <br />
                  <asp:Label ID="Label_warning" runat="server" ForeColor="Red"></asp:Label>
               </div>
            </div>

            <div id="middle" class="panel panel-primary" style="float: left; width: 33%;">
              <div class="panel-heading"><h4>Team informasjon</h4></div>
                 <div class="panel-body">
                    <b>Navn: </b><asp:Label ID="Label_team" runat="server"></asp:Label><br /><br />
                      <asp:ListView ID="ListView1" runat="server">
                         <itemtemplate> <li><%# Eval("FullName") %> </li> </itemtemplate>
                      </asp:ListView><br />
                 </div>
            </div>

            <div id="right" class="panel panel-primary" style="float: left; width: 33%;">
                <div class="panel-heading"><h4>Task informasjon</h4></div>
                 <div class="panel-body">
                    <b>Info skal komme her </b><br /><br />
                     <a>Task1</a> - x/y timer<br />
                     <a>Task2</a> - x/y timer<br />
                     <a>Task3</a> - x/y timer<br />
                     <a>Task4</a> - x/y timer<br />
                </div>
            </div>
        </div>
    </form>

</asp:Content>
