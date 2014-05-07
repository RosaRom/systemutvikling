<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProjectArchive.aspx.cs" Inherits="Adminsiden.ViewProjectArchive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Arkiverte prosjekter</title>
    <style type="text/css">
       Body{
            background-color:lightgray;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
    <div id="prosjektvalg" class="panel panel-primary" style="width:100%;">
           <div class="panel-heading"><h4>Arkiverte prosjekter</h4></div>
            <div class="panel-body">
                <asp:Label ID="label1" runat="server" Text="Arkivet"></asp:Label>
                <br />
                <asp:GridView ID="gvTaskList" 
                    AllowPaging="True" 
                    AutoGenerateColumns="False"
                    CssClass="gridView" 
                    AlternatingRowStyle-CssClass="alt" 
                    HeaderStyle-CssClass="gridViewHeader" 
                    HorizontalAlign="Left"
                    OnRowCommand="gvTaskList_RowCommand"
                    runat="server" >
                    <Columns>
                        <asp:BoundField DataField="projectID" HeaderText="prosjektID"/>
                        <asp:BoundField DataField="Prosjektnavn" HeaderText="Prosjektnavn"/>
                        <asp:BoundField DataField="Beskrivelse" HeaderText="Beskrivelse"/>
                        <asp:BoundField DataField="State" HeaderText="State"/>
                        <asp:BoundField DataField="Foreldre-ID" HeaderText="Foreldre-ID"/>
                        <asp:BoundField DataField="teamID" HeaderText="teamID"/>
                        <asp:buttonfield buttontype="Button" text="Gjør aktiv" CommandName="aktiver" ItemStyle-Width="1%"/>
                    </Columns>
                </asp:GridView>   
            </div>
        </div>
    </form>
</asp:Content>
