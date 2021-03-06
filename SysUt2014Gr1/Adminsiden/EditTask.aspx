﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTask.aspx.cs" Inherits="Adminsiden.EditTask" %>

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
           <div class="panel-heading"><h4>Rediger task</h4></div>
            <div class="panel-body">
                 <div class="leggTilTask">
                    <label>Velg Kategori</label>
                    <asp:DropDownList ID="DropDownMainTask" runat="server" DataTextField="taskCategoryName" DataValueField="taskCategoryID" 
                        OnSelectedIndexChanged="DropDownMainTask_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <br />
                <div class="leggTilTask">
                    <label>Navn på task</label>
                    <asp:TextBox CssClass="textbox" ID="taskNavn" runat="server"></asp:TextBox>
                </div>
                <br />
                <div class="leggTilTask">
                    <label>Timer allokert</label>
                    <asp:TextBox CssClass="textbox" ID="timerAllokert" runat="server"></asp:TextBox>
                </div>
                <br />
                <div class="leggTilTask">
                    <label id="lbBeskrivelse">Beskrivelse</label>
                    <asp:TextBox CssClass="textbox" ID="beskrivelse" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <br />
                <div class="leggTilTask">
                    <label>ID i Product Backlog</label>
                    <asp:TextBox CssClass="textbox" ID="pbID" runat="server"></asp:TextBox>
                </div>
                <br />
                <div class="leggTilTask">
                    <br />
                    <label>Fase</label>
                    <asp:DropDownList ID="DropDownFase" runat="server" DataValueField="phaseID" DataTextField="phaseName"></asp:DropDownList>
                </div>
                <br />
                <div class="leggTilTask">
                    <label>Prioritet</label>
                    <asp:DropDownList ID="DropDownPrioritering" runat="server">
                        <asp:ListItem Text="Høy" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Middels" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Lav" Value="3"></asp:ListItem>
                    </asp:DropDownList>
            
                </div>
                <br />
                <div class="leggTilTask">
                    <label>Gjør til subtask av</label>
                    <asp:DropDownList ID="DropDownSubTask" runat="server" DataTextField="taskName" DataValueField="taskID"
                        OnSelectedIndexChanged="DropDownSubTask_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <br />
                <div id="divLagre">
                    <asp:Button ID="BtnLagreTask" runat="server" Text="Lagre Task" OnClick="BtnLagreTask_Click" /><br />
                    <asp:Label ID="beskjed" runat="server"></asp:Label>
                </div>
                        
            </div>
           </div>
    </form>

    
</asp:Content>
