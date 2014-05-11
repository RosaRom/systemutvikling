<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAEditHovedtask.aspx.cs" Inherits="Adminsiden.PAEditHovedtask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Rediger task</title>
    <style type="text/css">
        #TextArea1 {
            height: 100px;
            width: 300px;
        }
       Body{
            background-color:lightgray;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
        <div id="regbruker" class="panel panel-primary" style="width:31%;">
           <div class="panel-heading"><h4>Rediger hovedtask</h4></div>
            <div class="panel-body">                
                <asp:DropDownList ID="ddlTaskCategory" runat="server" AppendDataBoundItems="True" Height="20px" Width="200px" OnSelectedIndexChanged="ddlTaskCategory_SelectedIndexChanged"></asp:DropDownList>
                
                <asp:Label ID="lbTaskCategoryName" runat="server" Text="Tasknavn"></asp:Label>
                <asp:TextBox ID="tbTaskCategoryName" runat="server"></asp:TextBox>
                <br /><br />
                <asp:Label ID="lbDescription" runat="server" Text="Beskrivelse"></asp:Label><br />
                <asp:TextBox id="taTaskCategoryDesc" TextMode="multiline" Columns="48" Rows="5" runat="server" />
                <br />
                <asp:Button ID="btnCommit" runat="server" OnClick="Button1_Click" Text="Lagre endringer" />
                <br />
                <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</asp:Content>
