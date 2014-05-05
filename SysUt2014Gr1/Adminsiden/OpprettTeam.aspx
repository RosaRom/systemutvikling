<%@ Page Language="C#"   AutoEventWireup="true" CodeBehind="OpprettTeam.aspx.cs" Inherits="Adminsiden.OpprettTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Opprett team</title>
    <link rel="Stylesheet" type="text/css" href="css/OpprettTeamStyle.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="TeamForm" runat="server">
        <div id="regbruker" class="panel panel-primary">
           <div class="panel-heading"><h4>Opprett team</h4></div>
            <div class="panel-body">
                <asp:Label ID="Label_warning" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="btn_opprett" runat="server" OnClick="btn_opprett_Click" Text="Opprett team" />
                <asp:Button ID="btn_deleteTeam" runat="server" Text="Slett team" Width="75px" OnClick="btn_deleteTeam_Click" />
                <asp:TextBox ID="tb_newTeam" runat="server" Visible="False" Width="144px"></asp:TextBox>
                <asp:Button ID="btn_createTeam" runat="server" OnClick="btn_createTeam_Click" Text="OK" Visible="False" Width="39px" />
                <asp:Button ID="btn_abort" runat="server" OnClick="btn_abort_Click" Text="Avbryt" Visible="False" />
                <br />   
                <asp:DropDownList ID="ddl_selectTeam" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddl_selectTeam_SelectedIndexChanged" Width="175px">
                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddl_users" runat="server" AppendDataBoundItems="true" Width="175px"></asp:DropDownList>
                <br />
                <asp:Button ID="btn_addUser" runat="server" Text="Legg til bruker" OnClick="btn_addUser_Click" />
                <asp:Button ID="btn_addTeamleader" runat="server" Text="Legg til teamleder" OnClick="btn_addTeamleader_Click" />
                <br />
                <asp:GridView ID="GridView1" 
                    runat="server"
                    AllowPaging="True" 
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="firstname" HeaderText="Fornavn" ItemStyle-Width="40%" />
                        <asp:BoundField DataField="surname" HeaderText="Etternavn" ItemStyle-Width="20%"/>
                        <asp:BoundField DataField="groupName" HeaderText="Rolle i teamet" ItemStyle-Width="20%" />
                        <asp:buttonfield buttontype="Button" text="Slett" ItemStyle-Width="1%"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
