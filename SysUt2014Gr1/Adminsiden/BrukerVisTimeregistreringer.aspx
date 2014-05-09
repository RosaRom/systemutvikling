<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrukerVisTimeregistreringer.aspx.cs" Inherits="Adminsiden.BrukerVisTimeregistreringer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div class="panel-body">
                <asp:Label ID="lbWhatIsShowing" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Button ID="btShowActiveRegistrations" runat="server" OnClick="btShowActiveRegistrations_Click" Text="Vis aktive timerregisteringer" />
                <asp:Button ID="btShowInactiveRegistrations" runat="server" OnClick="btShowInactiveRegistrations_Click" Text="Vis inaktive timeregistreringer" />
                
                <br />
                <asp:GridView ID="gvTaskList" 
                    AllowPaging="True" 
                    AutoGenerateColumns="False"
                    CssClass="gridView" 
                    AlternatingRowStyle-CssClass="alt" 
                    HeaderStyle-CssClass="gridViewHeader" 
                    HorizontalAlign="Left"
                    OnRowCommand="gvTaskList_RowCommand"
                    runat="server" >
                    <Columns>
                        <asp:BoundField DataField="taskID" HeaderText="Backlog ID"/>
                        <asp:BoundField DataField="taskID" HeaderText="Taskname"/>
                        <asp:BoundField DataField="start" HeaderText="Fra-dato"/>
                        <asp:BoundField DataField="stop" HeaderText="Til-dato"/>
                        <asp:BoundField DataField="description" HeaderText="Beskrivelse"/>
                        <asp:BoundField DataField="state" HeaderText="Status"/>                        
                        <asp:buttonfield buttontype="Button" text="Deaktiver" CommandName="deaktiver" ItemStyle-Width="1%"/>                        
                    </Columns>
                </asp:GridView>   
            </div>
    </form>
</body>
</html>
