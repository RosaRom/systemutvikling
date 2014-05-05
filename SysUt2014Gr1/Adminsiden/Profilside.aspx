<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profilside.aspx.cs" Inherits="Adminsiden.Profilside" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <title>Din profil</title>
    <link rel="Stylesheet" type="text/css" href="css/Profile.css" />

    <script type="text/javascript">
        function onlyNumbers(evt)
        {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
   
    <form id="profile" runat="server">
         <div id="regbruker" class="panel panel-primary" style="width:100%;">
           <div class="panel-heading"><h4>Din profil</h4></div>
            <div class="panel-body">
                <ul class="list-group">
                  <li class="list-group-item"><b>Logget inn som: </b><asp:Label ID="Label_name" runat="server"></asp:Label></li>

                  <li class="list-group-item"><b>Brukernavn: </b><asp:Label ID="Label_username" runat="server"></asp:Label></li>

                  <li class="list-group-item">
                      <asp:Label ID="Label_titlePW" runat="server" Text="<b>Passord: ******</b>"></asp:Label>
                      <asp:Button ID="btn_endrePW" runat="server" Height="30px" Text="Endre" OnClick="btn_endrePW_Click" />
                      <asp:Label ID="Label_gp" runat="server" Text="Gammelt passord: " Visible="False"></asp:Label>
                      <asp:TextBox ID="tb_gp" runat="server" Height="22px" Visible="False" MaxLength="15" TextMode="Password"></asp:TextBox>
                      <asp:Label ID="Label_np" runat="server" Text="Nytt passord: " Visible="False"></asp:Label>
                      <asp:TextBox ID="tb_np" runat="server" Height="22px" Visible="False" MaxLength="15" TextMode="Password"></asp:TextBox>
                      <asp:Label ID="Label_np1" runat="server" Text="Nytt passord: " Visible="False"></asp:Label>
                      <asp:TextBox ID="tb_np1" runat="server" Height="22px" Visible="False" MaxLength="15" TextMode="Password"></asp:TextBox>
                      <asp:Button ID="btn_confirmChangePW" runat="server" OnClick="btn_confirmChangePW_Click" style="height: 26px" Text="Bekreft" Visible="False" />
                      <asp:Button ID="btn_abortPW" runat="server" OnClick="btn_abortPW_Click" Text="Avbryt" Visible="False" />
                      <asp:Label ID="Label_warningPW" runat="server" ForeColor="Red"></asp:Label></li>

                  <li class="list-group-item">
                      <asp:Label ID="Label_titleTlf" runat="server" Text="<b>Telefon: </b>"></asp:Label>
                      <asp:Label ID="Label_telefon" runat="server"></asp:Label>
                      <asp:Button ID="btn_endretlf" runat="server" Height="30px" Text="Endre" OnClick="btn_endretlf_Click" />
                      <asp:Label ID="Label_NyttTlf" runat="server" Text="Nytt nummer: " Visible="False"></asp:Label>
                      <asp:TextBox ID="tb_nyttNr" runat="server" Height="22px" Visible="False" MaxLength ="8" onkeypress="return onlyNumbers(this);"></asp:TextBox>
                      <asp:Button ID="btn_confirmChangeTlf" runat="server" OnClick="btn_confirmChangeTlf_Click" Text="Bekreft" Visible="False" />
                      <asp:Button ID="btn_abortTlf" runat="server" OnClick="btn_abortTlf_Click" Text="Avbryt" Visible="False" /></li>

                  <li class="list-group-item">
                      <asp:Label ID="Label_titleEmail" runat="server" Text="<b>E-Mail: </b>"></asp:Label>
                      <asp:Label ID="Label_email" runat="server"></asp:Label>
                      <asp:Button ID="btn_endremail" runat="server" Height="30px" Text="Endre" OnClick="btn_endremail_Click" />
                      <asp:Label ID="Label_nyMail" runat="server" Text="Ny E-mail: " Visible="False"></asp:Label>
                      <asp:TextBox ID="tb_nyMail" runat="server" Height="23px" Visible="False"></asp:TextBox>
                      <asp:Button ID="btn_confirmChangeMail" runat="server" OnClick="btn_confirmChangeMail_Click" Text="Bekreft" Visible="False" />
                      <asp:Button ID="btn_abortEmail" runat="server" OnClick="btn_abortEmail_Click" Text="Avbryt" Visible="False" /></li>

                  <li class="list-group-item"><b>Brukerstatus:</b> <asp:Label ID="Label_status" runat="server"></asp:Label></li>
                </ul>
            </div>
        </div>
    </form>
</asp:Content>
