<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Bruker.aspx.cs" Inherits="Bruker.Bruker" %>

<script type="text/javascript" src="Styles/bootstrap-datepicker.js"></script>
<script type="text/javascript" src="Scripts/jquery.timepicker.js"></script>
<script type="text/javascript" src="Scripts/jquery.datepair.js"></script>

<script type="text/javascript" src="Scripts/jquery.timepicker.min.js"></script>
<link rel="stylesheet" type="text/css" href="Scripts/jquery.timepicker.css" />
<script type="text/javascript" src="lib/base.js"></script>
<link rel="stylesheet" type="text/css" href="lib/base.css" />

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
   <script runat="server" >
                
   </script>

<head runat="server">
    <title> Brukerside </title>
    <style type="text/css">
        .auto-style2 {
            width: 225px;
            margin-left: 360px;
            height: 387px;
        }
        #TextArea1 {
            z-index: 1;
            left: 16px;
            top: 525px;
            position: absolute;
            height: 37px;
            width: 216px;
        }
        #form1 {
            width: 250px;
        }
        #TextArea2 {
            width: 214px;
        }
        #projectDescription {
            width: 216px;
        }
        #TA_Comment {
            width: 211px;
            height: 41px;
        }
    </style>
</head>
<body>
   <form id="Brukerform" runat="server">
      <h3Bruker timeregistrering</h3>
       <table cellpadding="5">
         <tr>
            <td class="auto-style2">
                   
                   <br />
                <br />
                   <asp:Calendar ID="Calendar1" runat="server" Width="220px"></asp:Calendar>
                   <br />
                   <asp:Label ID="Label1" runat="server" Text="Arbeid påbegynt:"></asp:Label>
&nbsp;&nbsp;
                   <asp:Label ID="Label2" runat="server" Text="Arbeid avsluttet:"></asp:Label>
                <br />
                   <asp:DropDownList ID="ddl_hour_from" runat="server" Height="20px" Width="50px">
                   </asp:DropDownList>
                   <asp:DropDownList ID="ddl_min_from" runat="server" Height="20px" style="margin-bottom: 0px" Width="50px">
                   </asp:DropDownList>
                   &nbsp;
                   <asp:DropDownList ID="ddl_hour_to" runat="server" Height="20px" Width="50px">
                   </asp:DropDownList>
                   <asp:DropDownList ID="ddl_min_to" runat="server" Height="20px" Width="50px">
                   </asp:DropDownList>
                   <br />
                <br />
                <asp:DropDownList ID="workPlace" runat="server" AppendDataBoundItems="true" Height="20px" Width="220px" OnSelectedIndexChanged="workPlace_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="taskName" runat="server" AppendDataBoundItems="true" Height="20px" Width="220px" OnSelectedIndexChanged="taskName_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                <asp:TextBox id="TxtArea_userComment" TextMode="multiline" Columns="25" Rows="5" runat="server" />
                <br />
                <br />
                   <asp:Button ID="btn_ok" runat="server" OnClick="btn_ok_Click" Text="OK" Width="223px" />
                <br />
                <br />
                   <asp:Label ID="label_result" runat="server" Text="Label" Visible="False"></asp:Label>
                   <br />
            </td>
         </tr>
      </table>
   </form>
</body>
</html>