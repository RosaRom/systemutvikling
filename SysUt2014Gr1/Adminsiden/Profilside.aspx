<%@ Page Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Profilside.aspx.cs" Inherits="Adminsiden.Profilside" %>

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
        <ul class="list-group">
          <li class="list-group-item">Logget inn som: <asp:Label ID="Label_name" runat="server"></asp:Label></li>

          <li class="list-group-item">Brukernavn: <asp:Label ID="Label_username" runat="server"></asp:Label></li>

          <li class="list-group-item">
              <asp:Label ID="Label_titlePW" runat="server" Text="Passord: ******"></asp:Label>
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
              <asp:Label ID="Label_titleTlf" runat="server" Text="Telefon: "></asp:Label>
              <asp:Label ID="Label_telefon" runat="server"></asp:Label>
              <asp:Button ID="btn_endretlf" runat="server" Height="30px" Text="Endre" OnClick="btn_endretlf_Click" />
              <asp:Label ID="Label_NyttTlf" runat="server" Text="Nytt nummer: " Visible="False"></asp:Label>
              <asp:TextBox ID="tb_nyttNr" runat="server" Height="22px" Visible="False" MaxLength ="8" onkeypress="return onlyNumbers(this);"></asp:TextBox>
              <asp:Button ID="btn_confirmChangeTlf" runat="server" OnClick="btn_confirmChangeTlf_Click" Text="Bekreft" Visible="False" />
              <asp:Button ID="btn_abortTlf" runat="server" OnClick="btn_abortTlf_Click" Text="Avbryt" Visible="False" /></li>

          <li class="list-group-item">
              <asp:Label ID="Label_titleEmail" runat="server" Text="E-Mail: "></asp:Label>
              <asp:Label ID="Label_email" runat="server"></asp:Label>
              <asp:Button ID="btn_endremail" runat="server" Height="30px" Text="Endre" OnClick="btn_endremail_Click" />
              <asp:Label ID="Label_nyMail" runat="server" Text="Ny E-mail: " Visible="False"></asp:Label>
              <asp:TextBox ID="tb_nyMail" runat="server" Height="23px" Visible="False"></asp:TextBox>
              <asp:Button ID="btn_confirmChangeMail" runat="server" OnClick="btn_confirmChangeMail_Click" Text="Bekreft" Visible="False" />
              <asp:Button ID="btn_abortEmail" runat="server" OnClick="btn_abortEmail_Click" Text="Avbryt" Visible="False" /></li>

          <li class="list-group-item">Brukerstatus: <asp:Label ID="Label_status" runat="server"></asp:Label></li>
        </ul>
    </form>
</asp:Content>
