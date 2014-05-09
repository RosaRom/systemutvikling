<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TeamLeder.aspx.cs" Inherits="Adminsiden.Teamleder" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> Teamlederside </title>
     <link rel="Stylesheet" type="text/css" href="css/Teamleder.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
   <form id="Brukerform" runat="server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"
            EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>

    <div id="regbruker" class="panel panel-primary" style="width:50%;">
        <div class="panel-heading"><h4>Timeregistrering</h4></div>
        <div class="panel-body">
                Velg Dato:<br />
                <asp:TextBox ID="TB_Date" runat="server"></asp:TextBox>
                <asp:CalendarExtender 
                    ID="TB_Date_CalendarExtender" 
                    runat="server" 
                    TargetControlID="TB_Date"
                    Format="yyyy-MM-dd"
                    PopupPosition="BottomRight"
                    CssClass="blue"
                    TodaysDateFormat="dd. MMMM, yyyy">
                </asp:CalendarExtender>
            <br />
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
                Beskrivelse:<br />
            <asp:TextBox id="TxtArea_userComment" TextMode="multiline" Columns="25" Rows="5" runat="server" Height="80px" Width="220px" />
            <br />
            <br />
            <asp:Button ID="btn_ok" runat="server" OnClick="btn_ok_Click" Text="OK" Width="223px" />
            <br />
            <br />
            <asp:Label ID="label_result" runat="server" Visible="False"></asp:Label>
                <br />
                <br />
             <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Godkjenning av timeregistreringer" Width="270px" />
                <br />
                <br />
                <asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged">
                </asp:DropDownList>
        &nbsp;<asp:DropDownList ID="ddlBruker" runat="server" AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Overfør teamlederstatus" />
                <br />
                <asp:Label ID="lbTeamlederTransferred" runat="server"></asp:Label>
            <br />
        </div>
    </div>
   </form>
</asp:Content>