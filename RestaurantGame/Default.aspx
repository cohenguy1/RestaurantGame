<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantGame.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #StickManImage {
            Height: 79px;
            Width: 30px;
            visibility: hidden;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            
            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="1500"></asp:Timer>
            
            <div style="text-align: center">
            <h2>Position: Manager</h2>
            </div>

            <div class="Interviews">
                <table class="tblTop">
                    <tr>
                        <td>
                            <asp:Image ID="StickMan1" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan2" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan3" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan4" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan5" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan6" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan7" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan8" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan9" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan10" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan11" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan12" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan13" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan14" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan15" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan16" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan17" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan18" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan19" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                        <td>
                            <asp:Image ID="StickMan20" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>

                    </tr>

                </table>

            </div>

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Image ID="ImageManForward" runat="server" Height="220px" ImageUrl="~/Images/ManForward.gif" Width="276px" />
            <asp:Image ID="ImageInterview" runat="server" Height="220px" ImageUrl="~/Images/JobInterview.png" Width="276px" Visible="False" />
            <asp:Image ID="ImageManBack" runat="server" Height="220px" ImageUrl="~/Images/ManBack.gif" Width="276px" Visible="False" />

            <!--<div class="Taken">
                Manager
            </div>-->
        </asp:View>
    </asp:MultiView>
</asp:Content>
