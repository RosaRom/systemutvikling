<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="EditTask.aspx.cs" Inherits="Adminsiden.EditTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <title></title>

    <link rel="Stylesheet" type="text/css" href="css/EditTask.css" />


    <style type="text/css">
        #form1 {
            height: 1032px;
            width: 1543px;
        }
    </style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


    <form id="form1" runat="server">
        <div id="regbruker" class="panel panel-primary" style="width:25%;">
           <div class="panel-heading"><h4>Legg til bruker</h4></div>
            <div class="panel-body">
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Tasknavn"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="tbTaskName" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Legg til bruker"></asp:Label>
    
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlAddUser" runat="server">
                    </asp:DropDownList>
    
  
                    <br />
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="Allokert tid"></asp:Label>
                    &nbsp;<asp:TextBox ID="tbAllocatedTime" runat="server"></asp:TextBox>
                    <p>
                        &nbsp;</p>
                    <asp:Label ID="Label2" runat="server" Text="Prioritet"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="tbPriority" runat="server"></asp:TextBox>
                    <p>
                        &nbsp;</p>
                    <p>
                        <asp:Label ID="Label6" runat="server" Text="Beskrivelse"></asp:Label>
                    &nbsp;<asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
                    </p>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Fase"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="tbPhase" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server" Text="State"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="tbState" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="Sett foreldretask"></asp:Label>
            &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlParentTask" runat="server" Height="16px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label9" runat="server" Text="BacklogID"></asp:Label>
                    <asp:TextBox ID="tbBacklog" class="form-textbox" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <br /> 
                    <asp:Label ID="Label1" runat="server" Text="Gjør avhengig av"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDependency" runat="server">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <asp:Button class="btn btn-primary" runat="server" OnClick="btnSave_Click" Text="Lagre endringer" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                </div>
            </div>
    </form>

    
</asp:Content>
