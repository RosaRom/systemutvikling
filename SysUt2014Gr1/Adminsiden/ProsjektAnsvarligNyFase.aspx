<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Bootstrap.Master" CodeBehind="ProsjektAnsvarligNyFase.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligNyFase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ny fase</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarligNyFase.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="NyFaseForm" runat="server">

         <div id="regbruker" class="panel panel-primary" style="width:45%;">
           <div class="panel-heading"><h4>Ny fase</h4></div>
            <div class="panel-body">
               <div>
                  <asp:Label ID="lbFasenavn" runat="server" Text="Fasenavn"></asp:Label>
        &nbsp;&nbsp; <asp:TextBox ID="tbPhasename" runat="server" MaxLength="40" Width="300px"></asp:TextBox>
               </div>


                <div>
                  <asp:Label ID="lbDate" runat="server" Text="Dato fra"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;<asp:TextBox ID="tbDateFrom" runat="server" TextMode="Date"></asp:TextBox>&nbsp;til&nbsp;
                    <asp:TextBox ID="tbDateTo" runat="server" TextMode="Date"></asp:TextBox>
                </div>    


                <div id="divDescription">
                <asp:Label ID="lbDescription" runat="server" Text="Beskrivelse"></asp:Label>
                <asp:TextBox ID="tbDescription" runat="server" Height="107px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </div>
        

                <div style="margin-left: 80px">
                <asp:Button ID="btnSubmit" runat="server" Text="Gjennomfør endringer" Width="212px" OnClick="btnSubmit_Click" />
                <asp:Label ID="lbError" runat="server" ForeColor="Red" style="text-align: center"></asp:Label>
                </div>
            </div>
        </div>
   </form>
    
</asp:Content>