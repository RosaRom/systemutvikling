<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NyttProsjekt.aspx.cs" Inherits="Adminsiden.NyttProsjekt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lbProjectName" runat="server" Text="Prosjektnavn"></asp:Label>
&nbsp;<asp:TextBox ID="tbProjectName" runat="server" Width="284px"></asp:TextBox>
        <br />
        <asp:Label ID="lbSelectStartDate" runat="server" Text="Startdato"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbStartDate" runat="server" TextMode="Date" AutoPostBack="True" OnTextChanged="tbStartDate_TextChanged"></asp:TextBox>
        &nbsp;<asp:Label ID="lbEndDate" runat="server" Text="Tildato"></asp:Label>
&nbsp;<asp:TextBox ID="tbEndDate" runat="server" ReadOnly="True" TextMode="Date"></asp:TextBox>
        <br />
        <asp:Label ID="lbSelectNumberOfPhases" runat="server" Text="Antall faser" ></asp:Label>
&nbsp;<asp:TextBox ID="tbSelectNumberOfPhases" runat="server" TextMode="Number" Width="50px" AutoPostBack="True" OnTextChanged="tbSelectNumberOfPhases_TextChanged" onkeypress="return functionx(event)"></asp:TextBox>
&nbsp;<asp:Label ID="lbSelectLengthOfPhase" runat="server" Text="Fasene skal være"></asp:Label>
&nbsp;<asp:TextBox ID="tbSelectNumberOfDaysPerPhase" runat="server" TextMode="Number" Width="50px" AutoPostBack="True" OnTextChanged="tbSelectNumberOfDaysPerPhase_TextChanged" onkeypress="return functionx(event)"></asp:TextBox>
&nbsp;<asp:Label ID="lbDays" runat="server" Text="dager lange."></asp:Label>
        <br />
        <asp:Label ID="lbDescription" runat="server" Text="Beskrivelse"></asp:Label>
&nbsp;<asp:TextBox ID="tbDescription" runat="server" Rows="5" TextMode="MultiLine" Width="300px"></asp:TextBox>
        <br />
        <asp:Label ID="lbMakeSubproject" runat="server" Text="Underprosjekt av "></asp:Label>
&nbsp;<asp:DropDownList ID="ddlSubProject" runat="server" AppendDataBoundItems="True" Height="20px" Width="200px">
        </asp:DropDownList>
        <br />
        <asp:Label ID="lbAddTeam" runat="server" Text="Legg team til prosjektet"></asp:Label>
&nbsp;<asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="True" Width="200px">
        </asp:DropDownList>
        <br />
        <asp:Label ID="lbAddTaskCategory" runat="server" Text="Legg til hovedtask"></asp:Label>
&nbsp;<asp:DropDownList ID="ddlTaskCategory" runat="server" AppendDataBoundItems="True" Width="200px">
        </asp:DropDownList>
&nbsp;<asp:Button ID="btnAddTaskCategory" runat="server" OnClick="btnAddTaskCategory_Click" style="margin-bottom: 0px" Text="Legg til hovedtask" />
        <asp:Label ID="lbTaskCategoryError" runat="server"></asp:Label>
        <br />
&nbsp;<asp:ListBox ID="taskCategoryList" runat="server" AppendDataBoundItems="True" Rows="5" Width="200px"></asp:ListBox>
        <br />
        <br />
        <asp:Button ID="btnCreateProject" runat="server" OnClick="btnCreateProject_Click" Text="Legg til prosjekt" />
        <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
        <br />
    
    </div>
    </form>
     <script type = "text/javascript">
         function functionx(evt) {
             if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                 alert("Allow Only Numbers");                 
                 return false;
             }
         }
     </script>
</body>
</html>
