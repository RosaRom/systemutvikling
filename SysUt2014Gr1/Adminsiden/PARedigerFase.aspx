<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARedigerFase.aspx.cs" Inherits="Adminsiden.PARedigerFase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ny fase</title>
    <link rel="Stylesheet" type="text/css" href="css/ProsjektAnsvarligNyFase.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="RedigerFaseForm" runat="server">

         <div id="regbruker" class="panel panel-primary" style="width:45%;">
           <div class="panel-heading"><h4>Ny fase</h4></div>
            <div class="panel-body">
               <div>
                   <label>Velg en fase her: </label>
                   <asp:DropDownList ID="velgFase" runat="server" DataTextField="phaseName" DataValueField="phaseID"
                       OnSelectedIndexChanged="VelgFase_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                  <asp:Label ID="lbPAFasenavn" runat="server" Text="Fasenavn"></asp:Label>
        &nbsp;&nbsp; <asp:TextBox ID="tbPAPhasename" runat="server" MaxLength="40" Width="300px"></asp:TextBox>
               </div>


                <div>
                  <asp:Label ID="lbPADate" runat="server" Text="Dato fra"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;<asp:TextBox ID="tbPADateFrom" runat="server" TextMode="Date"></asp:TextBox>&nbsp;til&nbsp;
                    <asp:TextBox ID="tbPADateTo" runat="server" TextMode="Date"></asp:TextBox>
                </div>    


                <div id="divDescription">
                <asp:Label ID="lbPADescription" runat="server" Text="Beskrivelse"></asp:Label>
                <asp:TextBox ID="tbPADescription" runat="server" Height="107px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                </div>
        

                <div style="margin-left: 80px">
                <asp:Button ID="btnPASubmit" runat="server" Text="Gjennomfør endringer" Width="212px" OnClick="btnSubmit_Click" /><br />
                <asp:Label ID="lbPAError" runat="server" ForeColor="Red" style="text-align: center"></asp:Label>
                </div>
            </div>
        </div>
   </form>
    
</asp:Content>
