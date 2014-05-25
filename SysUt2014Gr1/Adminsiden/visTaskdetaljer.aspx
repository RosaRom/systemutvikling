<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visTaskdetaljer.aspx.cs" Inherits="Adminsiden.visTaskdetaljer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Taskdetaljer</title>
    <link rel="Stylesheet" type="text/css" href="css/VisTaskdetaljer.css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="MainForm" runat="server">

      
        <div="width: 100%;">

            <div id="left" class="panel panel-primary" style="float: left; width:267px;">
                  <div class="panel-heading"><h4>Task informasjon</h4></div>
                  <div class="panel-body">
                      <b>Navn: </b><asp:Label ID="Label_navn" runat="server"></asp:Label><br /><br />
                      <b>Beskrivelse: </b><br />
                      <asp:TextBox id="tb_desc" rows="3" TextMode="multiline" runat="server" ReadOnly="True" Width="235px" /><br /><br />
                      <b>Prioritet: </b><asp:Label ID="Label_prioritet" runat="server"></asp:Label><br /><br />
                      <b>Status: </b><asp:Label ID="Label_status" runat="server"></asp:Label><br /><br />
                      <b>Product backlog ID: </b><asp:Label ID="Label_backlogID" runat="server"></asp:Label><br /><br />
                      <b>Undertask av: </b>
                             <asp:Label ID="Label_undertask" runat="server"></asp:Label>
                             <asp:linkbutton id="Link_task" onclientclick="NavigateTask()" runat="Server" /><br /><br />
                      <b>fremgang i task: <br />
                      </b><progress id="progressBar1" value="271" max="275"></progress><br />
                      <asp:Label ID="Label_progress" runat="server"></asp:Label><br />
                      <asp:Label ID="Label_tidsavvik" runat="server"></asp:Label>
                  </div>
            </div>
            <div id="center" class="panel panel-primary" style="float: left; width:267px; margin-left: 10px;">
                <div class="panel-heading"><h4>Tilhørende faseinformasjon</h4></div>
                <div class="panel-body">
                    <b>Navn: </b><asp:Label ID="Label_faseNavn" runat="server"></asp:Label><br /><br />
                      <b>Beskrivelse: </b><br />
                      <asp:TextBox id="tb_faseDesc" rows="3" TextMode="multiline" runat="server" ReadOnly="True" Width="235px" />
                    <br />
                    <br />
                    <asp:Label ID="Label_faseTid" runat="server"></asp:Label><br />
                    <br />
                </div>
            </div>    
        </div>
    </form>

      <script type="text/javascript">
          function NavigateProject() {
              javascript: window.open("VisProsjektdetaljer.aspx");
          }
          function NavigateTask() {
              javascript: window.open("visTaskdetaljer.aspx");
          }
        </script>
</asp:Content>
