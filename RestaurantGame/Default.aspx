<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantGame.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="View1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="1500"></asp:Timer>
        
        <h2>Position: Manager</h2>
        <br /><br /><br />
        <asp:Image ID="ImageManForward" runat="server" Height="220px" ImageUrl="~/Images/ManForward.gif" Width="276px" />
        <asp:Image ID="ImageInterview" runat="server" Height="220px" ImageUrl="~/Images/JobInterview.png" Width="276px" Visible="False" />
        <asp:Image ID="ImageManBack" runat="server" Height="220px" ImageUrl="~/Images/ManBack.gif" Width="276px" Visible="False" />

    </asp:View>
</asp:MultiView>
</asp:Content>
