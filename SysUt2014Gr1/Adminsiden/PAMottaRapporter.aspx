<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="PAMottaRapporter.aspx.cs" Inherits="Adminsiden.PAMottaRapporter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Avviksrapporter og Klager</title>
    <link rel="Stylesheet" type="text/css" href="css/PAMottaRapporter.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="PAMottaRapporter" runat="server">
        <div id="regbruker" class="panel panel-primary" style="width:50%;">
           <div class="panel-heading"><h4>Rapporter</h4></div>
            <div class="panel-body">
                <div id="tekstboks">
                    <label id="tekstbokslabel" class="border">Beskrivelse av rapporten</label><br />    
                    <asp:TextBox ID="informasjon" runat="server" TextMode="MultiLine" Rows="5" Columns="30"></asp:TextBox>
                </div>
        
                <div id="rapporter">
                    <div class="border">
                        <asp:Label ID="lbAvvik" runat="server" Text="Avviksrapporter: "></asp:Label>
                        <asp:Label ID="lbAntallNyeRapporter" runat="server" Text="0 nye"></asp:Label>
            
                        <div class="buttons">
                            <br />
                            <asp:Button ID="btnNyeRapporter" runat="server" Text="Vis Nye" OnClick="btnNyeRapporter_Click" />
                            <asp:Button ID="btnAlleRapporter" runat="server" Text="Vis Alle" OnClick="btnAlleRapporter_Click" />
                        </div>
                    </div>
            
                    <br />
                    <asp:GridView ID="gvRapporter"
                        runat="server"
                        AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="True" 
                        OnRowDeleting="gvRapporter_RowDeleting" 
                        DataKeyNames="deviationID"
                        CssClass="gridView"
                        AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="true" DeleteText="Les" />
                            <asp:BoundField DataField="deviationid" HeaderText="ID" />
                            <asp:BoundField DataField="deviationTitle" HeaderText="Overskrift" />
                            <asp:BoundField DataField="timeAndDate" HeaderText="Dato og Tid" />
                        </Columns>
                    </asp:GridView>
                </div>
        
                <div id="klager">
                    <div class="border">
                        <asp:Label ID="lbKlager" runat="server" Text="Klager: "></asp:Label>
                        <asp:Label ID="lbAntallNyeKlager" runat="server" Text="0 nye"></asp:Label>
            
                        <div class="buttons">
                            <br />
                            <asp:Button ID="btnNyeKlager" runat="server" Text="Vis Nye" OnClick="btnNyeKlager_Click" />
                            <asp:Button ID="btnAlleKlager" runat="server" Text="Vis Alle" OnClick="btnAlleKlager_Click" />
                        </div>
                    </div>
                    <br />
                    <asp:GridView ID="gvKlager" 
                        runat="server"
                        AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="True" 
                        OnRowDeleting="gvKlager_RowDeleting" 
                        DataKeyNames="deviationID"
                        CssClass="gridView"
                        AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="true" DeleteText="Les" />
                            <asp:BoundField DataField="deviationid" HeaderText="ID" />
                            <asp:BoundField DataField="deviationTitle" HeaderText="Overskrift" />
                            <asp:BoundField DataField="timeAndDate" HeaderText="Dato og Tid" />
                        </Columns>
                    </asp:GridView>
                </div> 
            </div>
        </div>
    </form>
</asp:Content>

