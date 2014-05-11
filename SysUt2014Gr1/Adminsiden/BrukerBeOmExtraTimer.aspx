<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrukerBeOmExtraTimer.aspx.cs" Inherits="Adminsiden.BrukerBeOmExtraTimer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ekstra timer</title>
    <style type="text/css">
       Body{
            background-color:lightgray;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
        <div>
            <div id="prosjektvalg" class="panel panel-primary" style="width: 45%;">
           <div class="panel-heading"><h4>Be om ekstra timer</h4></div>
            <div class="panel-body">
                Be om
                <asp:TextBox ID="tbEkstraTimer" runat="server" Width="24px"></asp:TextBox>
                 &nbsp;ekstra timer på
                <asp:DropDownList ID="ddlTaskValg" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlTaskValg_SelectedIndexChanged"></asp:DropDownList>
                 &nbsp;<asp:Button ID="btnCommit" runat="server" Text="Send" OnClick="btnCommit_Click" />
                <asp:Label ID="lbCommitStatus" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lbValgtTaskInfo" runat="server"></asp:Label>
            </div>
           </div>
        </div>
    </form>
</asp:Content>

