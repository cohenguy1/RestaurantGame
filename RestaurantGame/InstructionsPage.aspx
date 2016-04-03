<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructionsPage.aspx.cs" Inherits="RestaurantGame.InstructionsPage" %>

<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiviewInstructions" runat="server" ActiveViewIndex="0">
                <asp:View ID="view8" runat="server">

                    <asp:Image ID="InstructionImage" ImageUrl="~/Images/Instructions0.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view9" runat="server">
                    <asp:Image ID="Image1" ImageUrl="~/Images/Instructions1.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view10" runat="server">
                    <asp:Image ID="Image2" ImageUrl="~/Images/Instructions2.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view11" runat="server">
                    <asp:Image ID="Image3" ImageUrl="~/Images/Instructions3.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view12" runat="server">
                    <asp:Image ID="Image4" ImageUrl="~/Images/Instructions4.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view13" runat="server">
                    <asp:Image ID="Image5" ImageUrl="~/Images/Instructions5.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view14" runat="server">
                    <asp:Image ID="Image6" ImageUrl="~/Images/Instructions6.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view15" runat="server">
                    <asp:Image ID="Image7" ImageUrl="~/Images/Instructions7.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view16" runat="server">
                    <asp:Image ID="Image8" ImageUrl="~/Images/Instructions8.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view17" runat="server">
                    <asp:Image ID="Image9" ImageUrl="~/Images/Instructions9.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view18" runat="server">
                    <asp:Image ID="Image10" ImageUrl="~/Images/Instructions10.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view19" runat="server">
                    <asp:Image ID="Image11" ImageUrl="~/Images/Instructions11.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view20" runat="server">
                    <asp:Image ID="Image12" ImageUrl="~/Images/Instructions12.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view21" runat="server">
                    <asp:Image ID="Image13" ImageUrl="~/Images/Instructions13.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view22" runat="server">
                    <asp:Image ID="Image14" ImageUrl="~/Images/Instructions14.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view23" runat="server">
                    <asp:Image ID="Image15" ImageUrl="~/Images/Instructions15.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view24" runat="server">
                    <asp:Image ID="Image16" ImageUrl="~/Images/Instructions16.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view25" runat="server">
                    <asp:Image ID="Image17" ImageUrl="~/Images/Instructions17.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view26" runat="server">
                    <asp:Image ID="Image18" ImageUrl="~/Images/Instructions18.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view27" runat="server">
                    <asp:Image ID="Image19" ImageUrl="~/Images/Instructions19.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
                <asp:View ID="view28" runat="server">
                    <asp:Image ID="Image20" ImageUrl="~/Images/Instructions20.png" Width="700px" Height="400px" runat="server" />
                </asp:View>
            </asp:MultiView>
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
