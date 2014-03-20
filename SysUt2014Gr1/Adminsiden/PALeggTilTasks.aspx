<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PALeggTilTasks.aspx.cs" Inherits="Adminsiden.PALeggTilTasks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Legg til ny task</title>
    <link rel="Stylesheet" type="text/css" href="css/PALeggTilTask.css" />
</head>
<body>
    <form id="PALeggTilTasks" runat="server">
    
        <div>
            <label>Velg Hovedtask</label>
            <asp:DropDownList ID="DropDownMainTask" runat="server" DataTextField="taskCategoryName" DataValueField="taskCategoryID"></asp:DropDownList>
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
            <label>Beskrivelse</label>
            <asp:TextBox ID="beskrivelse" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div>
            <label>Gjør til subtask av</label>
            <asp:DropDownList ID="DropDownSubTask" runat="server" DataTextField="taskName" DataValueField="taskID"></asp:DropDownList>
        </div>
        <asp:Button ID="BtnLagreTask" runat="server" Text="Lagre Task" OnClick="BtnLagreTask_Click" />
    </form>
</body>
</html>
