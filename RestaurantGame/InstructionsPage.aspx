<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructionsPage.aspx.cs" Inherits="RestaurantGame.InstructionsPage" %>

<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Image ID="InstructionImage1" ImageUrl="~/Images/Instructions0.png" Width="700px" Height="400px" runat="server" />

            <br />
            <br />

            <asp:Panel ID="PanelProgress" runat="server" Style="margin-left: 200px">
                <eo:ProgressBar ID="ProgressBar1" runat="server" BorderColor="Black" BorderWidth="1px"
                    Height="20px" IndicatorColor="0x0066ff" ControlSkinID="None" BorderStyle="Solid"
                    Width="300px" ShowPercentage="true" Font-Bold="true" Font-Size="Small">
                </eo:ProgressBar>
            </asp:Panel>
            <br />
            <asp:Button ID="btnPrevInstruction" runat="server" Text="Prev" OnClick="btnPrevInstruction_Click" Enabled="false" />
            <asp:Button ID="btnNextInstruction" runat="server" Text="Next" OnClick="btnNextInstruction_Click" />
</asp:Content>
