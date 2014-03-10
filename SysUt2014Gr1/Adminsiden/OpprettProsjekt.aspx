<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpprettProsjekt.aspx.cs" Inherits="Adminsiden.OpprettProsjekt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opprett prosjekt</title>
    <link rel="Stylesheet" type="text/css" href="css/OpprettProsjektStyle.css" />
    <style type="text/css">
        #TextArea1 {
            height: 45px;
        }
    </style>

    <script type="text/javascript">
        /**
         * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
         */

        maxL = 255;
        var bName = navigator.appName;
        function taLimit(taObj) {
            if (taObj.value.length == maxL) return false;
            return true;
        }

        function taCount(taObj, Cnt) {
            objCnt = createObject(Cnt);
            objVal = taObj.value;
            if (objVal.length > maxL) objVal = objVal.substring(0, maxL);
            if (objCnt) {
                if (bName == "Netscape") {
                    objCnt.textContent = maxL - objVal.length;
                }
                else { objCnt.innerText = maxL - objVal.length; }
            }
            return true;
        }
        function createObject(objId) {
            if (document.getElementById) return document.getElementById(objId);
            else if (document.layers) return eval("document." + objId);
            else if (document.all) return eval("document.all." + objId);
            else return eval("document." + objId);
        }
    </script>

</head>
<body>
    <div id="Logo">
        <asp:Image runat="server" ImageUrl="Resources/MorildData.png" AlternateText="Morild Data BA" />
    </div>
    <form id="OpprettProsjekt" runat="server">
        <div id="textbox">
            <asp:Label ID="Label_projectName" runat="server" Text="Prosjekt navn"></asp:Label>
            <br />
            <asp:TextBox ID="tb_projectName" runat="server" Width="200px"></asp:TextBox>
            <br />
            <br />
            <br />

            <asp:Label ID="LabelprojectDesc" runat="server" Text="Prosjekt beskrivelse"></asp:Label>

            <textarea id="TextArea_ProjectDescription" cols="40" rows="3" onkeydown="return taLimit(this)" onkeyup="return taCount(this, 'counter')"></textarea><br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            Du har <b><span id="counter">255</span></b> tegn igjen til din beskrivelse...
            <br />
            <br />
            <asp:Label ID="LabelTasks" runat="server" Text="Ligg til tasks"></asp:Label>
            <br />
            <asp:TextBox ID="tb_tasks" runat="server" Width="200px"></asp:TextBox>
            <br />
            <asp:ListBox ID="lb_tasks" runat="server" Width="200px"></asp:ListBox>
            <br />
            <br />
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Subprosjekt" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Teams"></asp:Label>
            <br />
            <asp:ListBox ID="lb_parentProject" runat="server" Width="200px"></asp:ListBox>
            <asp:ListBox ID="lb_Team" runat="server" Width="200px"></asp:ListBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Team members"></asp:Label>
            <br />
            <asp:ListBox ID="lb_teamMembers" runat="server" Width="200px"></asp:ListBox>
            <br />
        </div>
        <div id="calendar">
            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddl_hour" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddl_min" runat="server">
            </asp:DropDownList>
            <br />
        </div>
    </form>
</body>
</html>
