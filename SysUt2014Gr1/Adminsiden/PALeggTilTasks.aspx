<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PALeggTilTasks.aspx.cs" Inherits="Adminsiden.PALeggTilTasks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Legg til ny task</title>
    <link rel="Stylesheet" type="text/css" href="css/PALeggTilTasks.css" />
</head>
<body>

    <div id="adminLogo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>

    <h1 id="overskrift">Lag nye tasks her</h1>

    <form id="PALeggTilTasks" runat="server">
    
        <div class="leggTilTask">
            <label>Velg Hovedtask</label>
            <asp:DropDownList ID="DropDownMainTask" runat="server" DataTextField="taskCategoryName" DataValueField="taskCategoryID" 
                OnSelectedIndexChanged="DropDownMainTask_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>

        <div class="leggTilTask">
            <label>Navn på task</label>
            <asp:TextBox ID="taskNavn" runat="server"></asp:TextBox>
        </div>

        <div class="leggTilTask">
            <label>Timer allokert</label>
            <asp:TextBox ID="timerAllokert" runat="server"></asp:TextBox>
        </div>
        
        <div class="leggTilTask">
            <label id="lbBeskrivelse">Beskrivelse</label>
            <asp:TextBox ID="beskrivelse" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div class="leggTilTask">
            <label>Prioritet</label>
            <asp:DropDownList ID="DropDownPrioritering" runat="server">
                <asp:ListItem Text="Høy" Value="1"></asp:ListItem>
                <asp:ListItem Text="Middels" Value="2"></asp:ListItem>
                <asp:ListItem Text="Lav" Value="3"></asp:ListItem>
            </asp:DropDownList>
            
        </div>

        <div class="leggTilTask">
            <label>Gjør til subtask av</label>
            <asp:DropDownList ID="DropDownSubTask" runat="server" DataTextField="taskName" DataValueField="taskID"></asp:DropDownList>
        </div>

        <div id="divLagre">
            <asp:Button ID="BtnLagreTask" runat="server" Text="Lagre Task" OnClick="BtnLagreTask_Click" /><br />
            <asp:Label ID="beskjed" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
