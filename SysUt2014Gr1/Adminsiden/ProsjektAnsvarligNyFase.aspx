<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProsjektAnsvarligNyFase.aspx.cs" Inherits="Adminsiden.ProsjektAnsvarligNyFase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #Text2 {
            margin-bottom: 1px;
        }
        #TextArea1 {
            height: 99px;
            width: 264px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <p style="height: 75px; margin-left: 40px">
            Fasenavn&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tbPhasename" runat="server" MaxLength="40" Width="298px"></asp:TextBox>
            <br />
            <br />
            Dato&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbDateFrom" runat="server" TextMode="Date"></asp:TextBox>
&nbsp;til
            <asp:TextBox ID="tbDateTo" runat="server" TextMode="Date"></asp:TextBox>
<%-- 
            <asp:RangeValidator ID="RangeValidator1"
                ControlToValidate="tbDateTo"
                MinimumValue="2005-01-01"
                MaximumValue="2005-12-31"
                Type="Date"
                EnableClientScript="false"
                Text="The date must be between 2005-01-01 and 2005-12-31!"
                runat="server" />
--%>
        </p>
        <p style="margin-left: 40px">
            Beskrivelse&nbsp;&nbsp;
            <asp:TextBox ID="tbDescription" runat="server" Height="107px" TextMode="MultiLine" Width="300px"></asp:TextBox>
        </p>
    
    </div>
        <p style="margin-left: 40px">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Gjennomfør endringer" Width="212px" OnClick="btnSubmit_Click" />
        </p>
    </form>
</body>
</html>
