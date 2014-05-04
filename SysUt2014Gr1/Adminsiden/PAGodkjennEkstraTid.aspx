<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAGodkjennEkstraTid.aspx.cs" Inherits="Adminsiden.PAGodkjennEkstraTid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="label1" runat="server" Text="Forespørsler om ekstra tid på tasks"></asp:Label>
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
                        <asp:BoundField DataField="BacklogID" HeaderText="BacklogID"/>
                        <asp:BoundField DataField="Tasknavn" HeaderText="Tasknavn"/>
                        <asp:BoundField DataField="Prioritet" HeaderText="Prioritet"/>
                        <asp:BoundField DataField="Beskrivelse" HeaderText="Beskrivelse"/>
                        <asp:BoundField DataField="Brukte timer" HeaderText="Brukte timer"/>
                        <asp:BoundField DataField="Allokerte timer" HeaderText="Allokerte timer"/>
                        <asp:BoundField DataField="Ekstra timer" HeaderText="Ekstra timer"/>
                        <asp:buttonfield buttontype="Button" text="Godkjenn" CommandName="godkjenn" ItemStyle-Width="1%"/>
                        <asp:buttonfield buttontype="Button" text="Ikke godkjenn" CommandName="ikkegodkjenn" ItemStyle-Width="1%"/>
                    </Columns>
                </asp:GridView>   
    </div>
    </form>
</body>
</html>
