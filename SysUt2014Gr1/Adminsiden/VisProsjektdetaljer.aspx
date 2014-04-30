<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="VisProsjektdetaljer.aspx.cs" Inherits="Adminsiden.VisTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Prosjektdetaljer</title>
    <link rel="Stylesheet" type="text/css" href="css/VisProsjektdetaljer.css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="MainForm" runat="server">
        
        <div="width: 100%;">

           <div id="left" class="panel panel-primary" style="float: left; width:267px;">
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

            <div id="middle" class="panel panel-primary" style="float: left; width:267px; margin-left: 10px;">
              <div class="panel-heading"><h4>Team informasjon</h4></div>
                 <div class="panel-body">
                    <b>Navn: </b><asp:Label ID="Label_team" runat="server"></asp:Label><br /><br />
                      <asp:ListView ID="ListView_team" runat="server">
                         <itemtemplate> <li><%# Eval("FullName") %> </li> </itemtemplate>
                      </asp:ListView><br />
                 </div>
            </div>

            <div id="right" class="panel panel-primary" style="float: left; width:267px; margin-left: 10px;">
                <div class="panel-heading"><h4>Task informasjon</h4></div>
                <div class="panel-body">
                     <asp:ListView ID="Listview_task" runat="server">
                           <LayoutTemplate> <table border="1"> <thead> <tr> <th>Navn</th> <th>timer brukt</th> </tr> </thead> 
                                <tbody> <asp:PlaceHolder runat="server" ID="itemPlaceholder" /> </tbody> </table>
                           </LayoutTemplate> 
                         
                         <ItemTemplate><tr><td>
                                <asp:HyperLink runat="server" ID="hl" NavigateUrl='<%# "visTaskdetaljer.aspx?taskID="+ Eval("taskID") %>' Text='<%# Eval("taskName") %>'></asp:HyperLink></td><td><%# Eval("hoursUsed") %> / <%# Eval("hoursAllocated") %> timer</td></tr>
                         </ItemTemplate>
                     </asp:ListView>
                     <br />               
                 </div>
            </div>
        </div>

        <div id="chart" class="panel panel-primary" style="width:835px; margin-left: 100px;">
            <div class="panel-heading"><h4>grafisk fremstilling</h4></div>
            <div class="panel-body">
            <asp:Chart ID="projectChart" runat="server" Height="400px" Width="800px" BackColor="LightGray">
                    <Series>
                        <asp:Series ChartType="Line" Name="Brukte timer">
                        </asp:Series>
                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="Allokerte timer">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY Title="Timer">
                            </AxisY>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                </div>
            </div>
    </form>

</asp:Content>
