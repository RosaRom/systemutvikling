<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="True" CodeBehind="Bruker.aspx.cs" Inherits="Bruker.Bruker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> Brukerside </title>
     <link rel="Stylesheet" type="text/css" href="css/BrukerStyle.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
   <form id="Brukerform" runat="server">
    
    <h3>Bruker timeregistrering</h3>
    <br />
    <br />
    <asp:Calendar ID="Calendar1"  runat="server" Width="220px"></asp:Calendar>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Arbeid påbegynt:"></asp:Label>
    &nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="Arbeid avsluttet:"></asp:Label>
    <br />
    <asp:DropDownList ID="ddl_hour_from" runat="server" Height="20px" Width="50px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddl_min_from" runat="server" Height="20px" style="margin-bottom: 0px" Width="50px">
    </asp:DropDownList>
    &nbsp;
    <asp:DropDownList ID="ddl_hour_to" runat="server" Height="20px" Width="50px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddl_min_to" runat="server" Height="20px" Width="50px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:DropDownList ID="workPlace" runat="server" AppendDataBoundItems="true" Height="20px" Width="220px" OnSelectedIndexChanged="workPlace_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:DropDownList ID="taskName" runat="server" AppendDataBoundItems="true" Height="20px" Width="220px" OnSelectedIndexChanged="taskName_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:TextBox id="TxtArea_userComment" TextMode="multiline" Columns="25" Rows="5" runat="server" Height="80px" Width="220px" />
    <br />
    <br />
    <asp:Button ID="btn_ok" runat="server" OnClick="btn_ok_Click" Text="OK" Width="223px" />
    <br />
    <br />
    <asp:Label ID="label_result" runat="server" Visible="False"></asp:Label>
    <br />
   </form>
</asp:Content>