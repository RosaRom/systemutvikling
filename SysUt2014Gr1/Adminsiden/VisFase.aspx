<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Bootstrap.Master" CodeBehind="VisFase.aspx.cs" Inherits="Adminsiden.VisFase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vis Fase</title>
    <link rel="Stylesheet" type="text/css" href="css/VisFase.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="VisFaseForm" runat="server">
         <div id="regbruker" class="panel panel-primary" style="width:73%;">
           <div class="panel-heading"><h4>Fase informasjon</h4></div>
            <div class="panel-body">
                <div>
                    <asp:Label ID="lbPhaseName" runat="server" Text="Fasenavn"></asp:Label>        
                    <br />
                </div>
                <br />
                <div>
                    <asp:Label ID="lbDateFrom" runat="server" Text="Dato Fra"></asp:Label> - 
                    <asp:Label ID="lbDateTo" runat="server" Text="tilDato"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lbDescription" runat="server" Text="Beskrivelse"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lbHoursUsed" runat="server" Text="Totale timer brukt"></asp:Label> /         
                    <asp:Label ID="lbHoursAllocated" runat="server" Text="totale timer allokert"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lbFinishedTasks" runat="server" Text="Ferdige tasks: "></asp:Label>
                    <asp:Label ID="lbFinishedTaskNum" runat="server" Text="tall"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lbUnfinishedTasks" runat="server" Text="Uferdige tasks: "></asp:Label>
                    <asp:Label ID="lbUnfinishedTaskNum" runat="server" Text="tall"></asp:Label>
                </div>     
                <div>   
                    <asp:GridView ID="gvTaskList" runat="server"></asp:GridView>     
                </div>
                <br />
                <div>
                    <asp:Chart ID="phaseChart" runat="server" Width="800px" BackColor="LightGray" Height="350px">
                        <Series>
                            <asp:Series ChartType="Line" Name="Brukte timer" YValuesPerPoint="2">
                            </asp:Series>
                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="Allokerte timer" YValuesPerPoint="2">
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
        </div>
            

    </form>    
</asp:Content>