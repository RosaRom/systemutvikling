<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAVisHovedtask.aspx.cs" Inherits="Adminsiden.PAVisHovedtask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Vis hovedtask</title>
    <style type="text/css">
       Body{
            background-color:lightgray;
        }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
         <div id="regbruker" class="panel panel-primary" style="width:31%;">
           <div class="panel-heading"><h4>Vis hovedtask</h4></div>
            <div class="panel-body">
                <b>Tasknavn:</b>
                <asp:Label ID="lbTaskCategoryName" runat="server" Text="Label"></asp:Label>
                <br /><br />
                <b>Beskrivelse</b><br />
                <asp:TextBox id="taTaskCategoryDesc" TextMode="multiline" Columns="50" Rows="5" runat="server" />
            </div>
        </div>
    </form>
</asp:Content>
