<%@ Page Language="C#" AutoEventWireup="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
   <script runat="server" >

   </script>

<head runat="server">
    <title> DropDownList Example </title>
    <style type="text/css">
        .auto-style2 {
            width: 225px;
            margin-left: 320px;
            height: 366px;
        }
        #TextArea1 {
            z-index: 1;
            left: 16px;
            top: 437px;
            position: absolute;
            height: 58px;
            width: 212px;
        }
        #form1 {
            width: 250px;
        }
    </style>
</head>
<body>

   <form id="form1" runat="server">
      <h3> Bruker timeregistrering</h3>
     
      <br /><br /> 
       <table cellpadding="5">
         <tr>
            <td class="auto-style2">
               <asp:DropDownList id="taskName"
                    AutoPostBack="True"
                    runat="server" Height="20px" Width="220px">

                  <asp:ListItem Selected="True" Value="task1"> task navn </asp:ListItem>
                  <asp:ListItem Value="task2"> task2 </asp:ListItem>
                  <asp:ListItem Value="task3"> task3 </asp:ListItem>
                  <asp:ListItem Value="task4"> task4 </asp:ListItem>

               </asp:DropDownList>
                <br />
                <br />
               <asp:DropDownList ID="projectName"
                   AutoPostBack="true"
                   runat="server" Height="20px" Width="220px">

                   <asp:ListItem Selected="True" Value="test1"> Prosjekt navn </asp:ListItem>
                   <asp:ListItem Value="test2"> test2 </asp:ListItem>
                   <asp:ListItem Value="test3"> test3 </asp:ListItem>
               </asp:DropDownList>
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

                <asp:TextBox ID="TextBox1" runat="server" style="z-index: 1; left: 73px; top: 369px; position: absolute; width: 50px"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server" style="z-index: 1; top: 369px; position: absolute; width: 50px; right: 904px; left: 181px;"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 34px; top: 370px; position: absolute" Text="Fra: "></asp:Label>
                <asp:Label ID="Label2" runat="server" style="z-index: 1; left: 150px; top: 370px; position: absolute" Text="Til: "></asp:Label>
                <br />
                <br />

                <asp:DropDownList ID="Location"
                   AutoPostBack="true"
                   runat="server" Height="20px" Width="220px">

                   <asp:ListItem Selected="True" Value="test1"> Sted </asp:ListItem>
                   <asp:ListItem Value="test2"> Hjemmekontor </asp:ListItem>
                   <asp:ListItem Value="test3"> Ute </asp:ListItem>
                   <asp:ListItem Value="test3"> Kontor </asp:ListItem>

               </asp:DropDownList>

                <br />

                <br />
                <br />
                <textarea id="TextArea1" name="S1"></textarea><br />
                <br />
            </td>
         </tr>
      </table>
   </form>
</body>
</html>