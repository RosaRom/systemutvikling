<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Bootstrap.Master" CodeBehind="VisFase.aspx.cs" Inherits="Adminsiden.VisFase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vis Fase</title>
    <link rel="Stylesheet" type="text/css" href="css/VisFase.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="VisFaseForm" runat="server">
        <div>
            <asp:Label ID="lbPhaseName" runat="server" Text="Fasenavn"></asp:Label>        
            <br />
        </div>
        <br />
        <div>
            <asp:Label ID="lbDateFrom" runat="server" Text="Dato Fra"></asp:Label> - 
            <asp:Label ID="lbDateTo" runat="server" Text="tilDato"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbDescription" runat="server" Text="Beskrivelse"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbHoursUsed" runat="server" Text="Totale timer brukt"></asp:Label> /         
            <asp:Label ID="lbHoursAllocated" runat="server" Text="totale timer allokert"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbFinishedTasks" runat="server" Text="Ferdige tasks: "></asp:Label>
            <asp:Label ID="lbFinishedTaskNum" runat="server" Text="tall"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbUnfinishedTasks" runat="server" Text="Uferdige tasks: "></asp:Label>
            <asp:Label ID="lbUnfinishedTaskNum" runat="server" Text="tall"></asp:Label>
        </div>     
        <div>   
            <asp:GridView ID="gvTaskList" runat="server"></asp:GridView>        
        </div>

    </form>    
</asp:Content>