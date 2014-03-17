<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PALeggTilTasks.aspx.cs" Inherits="Adminsiden.PALeggTilTasks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Legg til ny task</title>
    <link rel="Stylesheet" type="text/css" href="css/" />
</head>
<body>
    <form id="PALeggTilTasks" runat="server">
    
        <div>
            <label>Type task</label>
            <asp:DropDownList ID="DropDownTaskType" runat="server">
                <asp:ListItem Selected="True" Text="Hovedtask" Value="0"></asp:ListItem>
                <asp:ListItem Text="Task" Value="1"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div>
            <label>Navn på task</label>
            <asp:TextBox ID="taskName" runat="server"></asp:TextBox>
        </div>

        <div>
            <label>Timer allokert</label>
            <asp:TextBox ID="timerAllokert" runat="server"></asp:TextBox>
        </div>

        <div>
            <label>Fase</label>
            <label>Fra</label>
            <asp:DropDownList ID="DropDownFaseFra" runat="server"></asp:DropDownList>
            <label>Til</label>
            <asp:DropDownList ID="DropDownFaseTil" runat="server"></asp:DropDownList>
            <asp:Button ID="BtnOpprettFase" runat="server" Text="Opprett Fase" />
        </div>

        <div>
            <label>Beskrivelse</label>
            <
        </div>

        <div>
            <label>Gjør til subtask av</label>
            <asp:DropDownList ID="DropDownSubtask" runat="server"></asp:DropDownList>
        </div>
    </form>
</body>
</html>
