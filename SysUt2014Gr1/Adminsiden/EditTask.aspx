<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="EditTask.aspx.cs" Inherits="Adminsiden.EditTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <title></title>

    <link rel="Stylesheet" type="text/css" href="css/OpprettProsjektStyle.css" />

    <style type="text/css">
        #form1 {
            height: 1032px;
            width: 1543px;
        }
    </style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label3" runat="server" Text="Tasknavn"></asp:Label>
        <br />
        <asp:TextBox ID="tbTaskName" runat="server"></asp:TextBox>
    
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Legg til bruker"></asp:Label>
    
    </div>
        <asp:DropDownList ID="ddlAddUser" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Allokert tid"></asp:Label>
        <p>
            <asp:TextBox ID="tbAllocatedTime" runat="server"></asp:TextBox>
        </p>
        <asp:Label ID="Label2" runat="server" Text="Prioritet"></asp:Label>
        <p>
        <asp:TextBox ID="tbPriority" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label6" runat="server" Text="Beskrivelse"></asp:Label>
        </p>
        <p>
            <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
        </p>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Fase"></asp:Label>
        <br />
        <asp:TextBox ID="tbPhase" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label8" runat="server" Text="State"></asp:Label>
        <br />
        <asp:TextBox ID="tbState" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label9" runat="server" Text="BacklogID"></asp:Label>
        <br />
        <asp:TextBox ID="tbBacklog" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Gjør avhengig av"></asp:Label>
        <asp:DropDownList ID="ddlDependency" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnDependency" runat="server" Text="OK" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Lagre endringer" />
        <br />
    </form>

    
</asp:Content>
