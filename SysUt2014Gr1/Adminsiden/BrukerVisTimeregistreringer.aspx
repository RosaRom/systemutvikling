<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrukerVisTimeregistreringer.aspx.cs" Inherits="Adminsiden.BrukerVisTimeregistreringer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vis registrerte timer</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
         <div id="regbruker" class="panel panel-primary" style="width:100%;">
           <div class="panel-heading"><h4><asp:Label ID="lbWhatIsShowing" runat="server" Text="Label"></asp:Label></h4></div>
            <div class="panel-body">
                
                <br />
                <asp:Button ID="btShowActiveRegistrations" runat="server" OnClick="btShowActiveRegistrations_Click" Text="Vis aktive timerregisteringer" />
                <asp:Button ID="btShowInactiveRegistrations" runat="server" OnClick="btShowInactiveRegistrations_Click" Text="Vis inaktive timeregistreringer" />
                
                <br />
                <asp:GridView ID="gvTaskList" 
                    AllowPaging="false"             
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
                        <asp:BoundField DataField="state" HeaderText="Status" ItemStyle-Width="1%"/>                        
                        <asp:buttonfield buttontype="Button" text="Deaktiver" CommandName="deaktiver" ItemStyle-Width="1%"/>                        
                    </Columns>
                </asp:GridView>   
             </div>
          </div>
    </form>
</asp:Content>
