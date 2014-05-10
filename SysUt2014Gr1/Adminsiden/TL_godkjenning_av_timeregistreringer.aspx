﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TL_godkjenning_av_timeregistreringer.aspx.cs" Inherits="Adminsiden.TL_godkjenning_av_timeregistreringer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Godkjenning av timereg</title>
    <style type="text/css">
       Body{
            background-color:lightgray;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form id="form1" runat="server">

        <p style="margin-left : 520px">
            &nbsp;</p>
        <p style="margin-left : 520px">
            &nbsp;</p>
        <p style="margin-left : 520px">
            &nbsp;</p>

        <div id="prosjektvalg" class="panel panel-primary">
           <div class="panel-heading"><h4>Godkjenning av timeregistreringer</h4></div>
            <div class="panel-body">
                <asp:GridView ID="GridView1" runat="server" Height="229px" Width="100%" AutoGenerateColumns="False">
                    <Columns>
                         <asp:BoundField DataField ="start" HeaderText="Start"  />
                        <asp:BoundField DataField="stop" HeaderText="Slutt" />
                        <asp:BoundField DataField="username" HeaderText="Brukernavn" />
                        <asp:BoundField DataField="taskName" HeaderText="Task" />
                        <asp:BoundField DataField="workplace" HeaderText="Sted" />
                        <asp:BoundField DataField="description" HeaderText="Beskrivelse" />
                        <asp:BoundField DataField="priority" HeaderText="Prioritet" />
                        <asp:ButtonField ButtonType="Button" CommandName="godkjent" HeaderText="Godkjenn" Text="OK" ItemStyle-Width="1%" />
                        <asp:ButtonField ButtonType="Button" CommandName="ikkeGodkjent" HeaderText="Ikke godkjenn" Text="Avslå" ItemStyle-Width="1%" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>

