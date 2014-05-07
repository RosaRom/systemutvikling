<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="EditProject.aspx.cs" Inherits="Adminsiden.editProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Rediger prosjekt</title>
    <link rel="Stylesheet" type="text/css" href="css/EditProject.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
        <div id="regbruker" class="panel panel-primary" style="width:25%;">
           <div class="panel-heading"><h4>Rediger prosjekt</h4></div>
            <div class="panel-body">
                <div id="EditForm">
                    <asp:TextBox ID="tbProjectName" runat="server" Width="202px"></asp:TextBox>
                    <p>
                        <asp:TextBox ID="tbProjectDescription" runat="server" TextMode="multiline" Height="149px" Width="202px"></asp:TextBox>
                    </p>
                    <br>
                    <asp:Label runat="server" Text="Project State (0/1)"></asp:Label>
                    <asp:TextBox ID="tbState" runat="server" Width="202px"></asp:TextBox>
                    <asp:Button ID="btnUpdateQuery" runat="server" OnClick="btnUpdateQuery_Click" Text="Send" />
                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" /> <br>
                    <asp:Label ID="lblMessageOK" runat="server" Text=""></asp:Label>
                </div>
               </div>
            </div>
    </form>
</asp:Content>

