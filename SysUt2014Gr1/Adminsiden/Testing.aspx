﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="Adminsiden.Testing" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/navbar-fixed-top.css" rel="stylesheet"/>
</head>

<body>
    <div class="navbar navbar-default navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">Morild Data BA</a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li><a href="Profilside.aspx">Profil</a></li>
                    <li><a href="PAAdministrerBrukere.aspx">Administrer brukere</a></li>
                    <li><a href="PAMottaRapporter.aspx">Rapporter</a></li>
                    <li class="dropdown">
                      <a href="Testing.aspx" class="dropdown-toggle" data-toggle="dropdown">Opprett... <b class="caret"></b></a>
                      <ul class="dropdown-menu">
                        <li><a href="OpprettProsjekt.aspx">Prosjekt</a></li>
                        <li><a href="OpprettTeam.aspx">Team</a></li>
                        <li><a href="PALeggTilTasks.aspx">Task</a></li>
                        <li><a href="PANyHovedtask.aspx">Hovedtask</a></li>
                        <li><a href="ProsjektAnsvarligNyBruker.aspx">Bruker</a></li>
                      </ul>
                    </li>
                    <li class="dropdown">
                      <a href="#" class="dropdown-toggle" data-toggle="dropdown">Rediger... <b class="caret"></b></a>
                      <ul class="dropdown-menu">
                        <li><a href="ProsjektAnsvarlig.aspx">Prosjekt</a></li>
                        <li><a href="PickTask.aspx">Task</a></li>
                        <li><a href="PAEditHovedtask.aspx">Hovedtask</a></li>
                      </ul>
                    </li>
                    <li class="dropdown">
                      <a href="#" class="dropdown-toggle" data-toggle="dropdown">Vis... <b class="caret"></b></a>
                      <ul class="dropdown-menu">
                        <li><a href="VisProsjektdetaljer.aspx">Prosjektdetaljer</a></li>
                        <li><a href="VisFase.aspx">Fasedetaljer</a></li>
                        <li><a href="PAVisHovedtask.aspx">Hovedtask</a></li>
                        <li><a href="ProsjektAnsvarligVisTeam.aspx">Team</a></li>
                      </ul>
                    </li>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="LogOut.aspx">Logg ut</a></</li>
                    </ul>
                    <br />
                    <li><a href="Prosjektvalg.aspx">Prosjekt: <span class="label label-default"><asp:Label ID="Label_prosjekt" runat="server"></asp:Label></span></a></li>
                    <li><a href="#">Fase: <span class="label label-default"><asp:Label ID="Label_fase" runat="server" Text="Temp Fase"></asp:Label></span></a></li>
                </ul>
            </div>
            <!--/.nav-collapse -->
        </div>
    </div>
    
    <script src="../Scripts/jQuery 2.1.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
</body>
</html>
