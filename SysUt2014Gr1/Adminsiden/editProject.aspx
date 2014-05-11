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
                    <label>Prosjektnavn </label>
                    <asp:TextBox ID="tbProjectName" runat="server" Width="202px"></asp:TextBox><br />
                    <label>Prosjektbeskrivelse:</label>
                    <asp:TextBox ID="tbProjectDescription" runat="server" TextMode="multiline" Height="149px" Width="202px"></asp:TextBox><br />
                    
                    <asp:Label ID="lbMakeSubproject" runat="server" Text="Underprosjekt av "></asp:Label>
                    <asp:DropDownList ID="ddlSubProject" runat="server" Height="20px" Width="200px" DataTextField="projectName" DataValueField="projectID">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="lbAddTeam" runat="server" Text="Forandre team til prosjektet"></asp:Label>
                    <asp:DropDownList ID="ddlTeam" runat="server" DataTextField="teamName" DataValueField="teamID" Width="200px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:label>Status på prosjektet </asp:label>
                    <asp:DropDownList ID="dropDownState" runat="server">
                        <asp:ListItem Text="Inaktiv" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Aktiv" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Arkiver" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="btnUpdateQuery" runat="server" OnClick="btnUpdateQuery_Click" Text="Oppdater" />
                    <br />
                    <asp:Label ID="lblMessageOK" runat="server" Text=""></asp:Label>
                </div>
               </div>
            </div>
    </form>
</asp:Content>

