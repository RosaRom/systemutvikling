<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="PANyHovedtask.aspx.cs" Inherits="Adminsiden.PANyHovedtask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Ny Hovedtask</title>
    <link rel="Stylesheet" type="text/css" href="css/PANyHovedtask.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="form1" runat="server">

        <div id="regbruker" class="panel panel-primary" style="width:32%;">
           <div class="panel-heading"><h4>Ny hovedtask</h4></div>
            <div class="panel-body">

                <div id="divNavn" class="nyHovedtask">
                    <label>Navn på kategori</label>
                    <asp:TextBox CssClass="hovedtaskNavn" ID="hovedtaskNavn" runat="server"></asp:TextBox>
                </div>

                <div class="nyHovedtask">
                    <label>Project Backlog id</label>
                    <asp:TextBox CssClass="id" ID="id" runat="server"></asp:TextBox>
                </div>

                <div id="divBeskrivelse" class="nyHovedtask">
                    <label id="lbBeskrivelse">Beskrivelse</label>
                    <asp:TextBox CssClass="beskrivelse" ID="txtBeskrivelse" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>

                <div class="nyHovedtask">
                    <br /><br />
                    <label>Velg fase</label>
                    <asp:DropDownList ID="DropDownFase" runat="server" DataValueField="phaseID" DataTextField="phaseName"></asp:DropDownList>
                    <asp:Button CssClass="nyTask" ID="btnNyTask" runat="server" Text="Ny Task" OnClick="BtnNyTask_Click"/>
                </div>

                <div class="divLagre">
                    <asp:Button ID="btnLagreHovedtask" runat="server" Text="Lagre hovedtask" OnClick="btnLagreHovedtask_Click" /><br />
                    <asp:Label ID="lbBeskjed" runat="server"></asp:Label>
                </div>
            </div>
         </div>
        
    </form>
</asp:Content>