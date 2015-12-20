<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantGame.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        img#StickMan {
            Height: 79px;
            Width: 30px;
            visibility: hidden;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Enabled="false"></asp:Timer>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="view0" runat="server">

            <h2 class="style3">You will recieve 5 cents for this Game and it will take 15 minutes of your time. </h2>
            <h2 class="style3">Please do not expect to recieve additional payment!</h2>
            <p class="style3">
                &nbsp;
            </p>
            <h2>Game Background</h2>
            <table style="text-align: center; width: 640px;" align="center">
                <tr>
                    <td>You have decided to open up a restaurant.</td>
                </tr>
                <tr>
                    <td>In this game you and other player will interview people for 10 positions for the restaurant.</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>One of you will interview the candidates and decide about who fill each position,<br />
                        and the other player will choose the uniform for each position</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>For Each position you have 20 candidates, ranked from 1 to 20. Your interest is to pick the best candidate for each position.<br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td><strong>In the game, you will rank how good is your teammate, and he will rank you.<br />
                        This is not a competition, just choose how well you think your teammate performs.<br />
                        &nbsp;</strong></td>
                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnNext0" runat="server" Text="Next" OnClick="btnNext_Click" Enabled="false" /></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="view1" runat="server">
            <h2>Quiz</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <asp:Label ID="lblQuiz1" Style="color: Red;" runat="server" Text="Please answer the questions"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>How much money will you get by just picking the first candidate?
                        <asp:RadioButtonList ID="rbl1" runat="server">
                            <asp:ListItem>0 cents</asp:ListItem>
                            <asp:ListItem>5 cents</asp:ListItem>
                            <asp:ListItem>10 cents</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv1" Style="color: Red;" ControlToValidate="rbl1" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td>Will you get extra money if you pick the best candidate for each position?
                        <asp:RadioButtonList ID="rbl2" runat="server">
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv2" Style="color: Red;" ControlToValidate="rbl2" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnNext2" runat="server" Text="Next" OnClick="btnNext2_Click" />

        </asp:View>

        <asp:View ID="View2" runat="server">

            <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                <asp:View ID="view3" runat="server">
                    <asp:Label ID="PositionHeader" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;"></asp:Label>
                    <br />
                    <asp:Panel ID="Panel4" runat="server" Style="margin-left: 0px; float: right">
                        <br />

                        <asp:Panel ID="Page1ViewPanel" runat="server" Width="700px" Style="margin-left: 20px;">
                            <br />
                            <br />
                            <div class="Interviews">
                                <table class="tblTop">
                                    <tr>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan1" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan2" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan3" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan4" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan5" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan6" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan7" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan8" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan9" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan10" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan11" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan12" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan13" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan14" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan15" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan16" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan17" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan18" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan19" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        <td>
                                            <asp:Image class="StickMan" ID="StickMan20" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>

                                    </tr>

                                </table>

                            </div>

                        </asp:Panel>

                        <asp:Panel ID="Panel1" runat="server" Style="margin-left: 0px; margin-top: 60px; align-content: center;" Width="600px">
                            <asp:Image ID="ImageManForward" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/ManForward.gif" />
                            <asp:Image ID="ImageInterview" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/JobInterview.png" Visible="False" />
                            <asp:Image ID="ImageHired" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/Hired.jpg" Visible="False" />
                            <br />
                            <br />
                            <asp:Button ID="FB" runat="server" Text="<<" OnClick="btnFB_Click" Enabled="true" />
                            <asp:Button ID="FF" runat="server" Text=">>" OnClick="btnFF_Click" Enabled="true" />

                        </asp:Panel>
                    </asp:Panel>
                </asp:View>
                <asp:View ID="view4" runat="server">

                    <asp:Label ID="Label2" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;">Adviser Rating</asp:Label>
                    <br />
                    <asp:Panel ID="Panel3" runat="server" Style="margin-left: 20px; float: right">
                        <br />
                        <div style="text-align: center; width: 600px; margin: 0 auto;">
                            <table style="text-align: left; width: 600px;" border="1">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" Style="color: Red;" runat="server" Text="Please answer the questions"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>On a scale of 1 to 10, 10 being the best, How would you rate your adviser?
                        <asp:RadioButtonList ID="RatingRbL" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: Red;" ControlToValidate="RatingRbL" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnRate" runat="server" Text="Rate!" OnClick="btnRate_Click" />
                    </asp:Panel>
                </asp:View>
            </asp:MultiView>

            <asp:Panel ID="Panel2" runat="server" Style="margin-left: 0px; float: left">
                <asp:Table ID="Positions"
                    BorderColor="black"
                    BorderWidth="1"
                    GridLines="Both"
                    runat="server">
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="cell1" Width="155px" HorizontalAlign="Left" Text="&nbsp;Restaurant Positions" Style="color: blue;" Font-Bold="true"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="ManagerCell" HorizontalAlign="Left" Text="&nbsp;Manager:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="HeadChefCell" HorizontalAlign="Left" Text="&nbsp;Head Chef:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="CookCell" HorizontalAlign="Left" Text="&nbsp;Cook:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="BakerCell" HorizontalAlign="Left" Text="&nbsp;Baker:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="DishwasherCell" HorizontalAlign="Left" Text="&nbsp;Dishwasher:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="Waiter1Cell" Width="120px" HorizontalAlign="Left" Text="&nbsp;Waiter 1:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="Waiter2Cell" HorizontalAlign="Left" Text="&nbsp;Waiter 2:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="Waiter3Cell" HorizontalAlign="Left" Text="&nbsp;Waiter 3:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="HostCell" HorizontalAlign="Left" Text="&nbsp;Host:"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="BartenderCell" HorizontalAlign="Left" Text="&nbsp;Bartender:"></asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>
</asp:Content>
