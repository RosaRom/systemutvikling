<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Bruker.aspx.cs" Inherits="Bruker.Bruker" %>

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
            margin-left: 320px;
            height: 366px;
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
      <h3> Bruker timeregistrering</h3>
      <br />
      <br /> 
       <table cellpadding="5">
         <tr>
            <td class="auto-style2">
                   <asp:DropDownList ID="taskName" runat="server" AppendDataBoundItems="true" Height="20px" Width="220px" OnSelectedIndexChanged="taskName_SelectedIndexChanged">
                   </asp:DropDownList>
                <br />
                <br />
                   <asp:DropDownList ID="projectName" runat="server" AppendDataBoundItems="true" Height="20px" Width="220px" OnSelectedIndexChanged="projectName_SelectedIndexChanged">
                   </asp:DropDownList>
                <br />
                <br />
                 <asp:TextBox id="TxtArea_projectComment" TextMode="multiline" Columns="25" Rows="5" runat="server" ReadOnly="True" />
                <br />
                <br />
                <asp:Calendar id="Calendar1"
                       runat="server" Height="180px" Width="220px" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" style="margin-left: 0px">
                      <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                      <NextPrevStyle VerticalAlign="Bottom" />
                      <OtherMonthDayStyle ForeColor="#808080" />
                      <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                      <SelectorStyle BackColor="#CCCCCC" />
                      <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                      <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                      <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
                <br />
                <asp:Label ID="Lb_fra" runat="server" Text="Fra: "></asp:Label>
                <asp:TextBox ID="Tb_fra" runat="server" style="z-index: 1; left: 73px; top: 369px; width: 50px"></asp:TextBox>
                <asp:Label ID="Lb_til" runat="server" Text="Til: "></asp:Label>
                <asp:TextBox ID="Tb_til" runat="server" style="z-index: 1; left: 181px; top: 369px; width: 50px; right: 904px; "></asp:TextBox>
                <br />
                <br />
                <asp:DropDownList ID="workPlace" runat="server" AppendDataBoundItems="true" Height="20px" Width="220px" OnSelectedIndexChanged="workPlace_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                <asp:TextBox id="TxtArea_userComment" TextMode="multiline" Columns="25" Rows="5" runat="server" />
                <br />
                <br />
                   <asp:Button ID="btn_ok" runat="server" OnClick="btn_ok_Click" Text="OK" Width="223px" />
                <br />
            </td>
         </tr>
      </table>
   </form>
</body>
</html>