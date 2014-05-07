<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PickTask.aspx.cs" Inherits="Adminsiden.PickTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Velg task for endring</title>
    <style type="text/css">
       Body{
            background-color:lightgray;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
    <div id="prosjektvalg" class="panel panel-primary" style="width:100%;">
           <div class="panel-heading"><h4>Velg task for endring</h4></div>
            <div class="panel-body">
                <asp:Label ID="label1" runat="server" Text="Tasks"></asp:Label>
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
                        <asp:BoundField DataField="taskID" HeaderText="taskID"/>
                        <asp:BoundField DataField="taskCategoryID" HeaderText="taskCategoryID"/>
                        <asp:BoundField DataField="taskName" HeaderText="taskName"/>
                        <asp:BoundField DataField="description" HeaderText="description"/>
                        <asp:buttonfield buttontype="Button" text="Endre" CommandName="endre" ItemStyle-Width="1%"/>
                    </Columns>
                </asp:GridView>   
            </div>
        </div>
    </form>
</asp:Content>
