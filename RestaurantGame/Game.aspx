<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="RestaurantGame.Game" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Timer ID="TimerGame" OnTick="TimerGame_Tick" runat="server" Enabled="false"></asp:Timer>


    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewGame" runat="server">
            <asp:Label ID="PositionHeader" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;"></asp:Label>

            <br />
            <asp:Panel ID="PanelInterviewSpeedBasket" runat="server" Width="600px" Style="margin-left: 0px; float: right">
                <asp:Panel ID="PanelInterviewBasket" runat="server" Style="margin-left: 0px">
                    <br />
                    <asp:Panel ID="PanelInterview" runat="server" Width="600px" Style="margin-left: 20px; float: left">
                        <br />
                        <asp:Panel runat="server" Style="margin-left: 0px; text-align: left">
                            <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                        </asp:Panel>
                        <br />
                    </asp:Panel>

                </asp:Panel>

                <br />
                <asp:Panel ID="PanelSpeed" runat="server" Style="margin-left: 0px; margin-top: 60px; align-content: center;" Width="600px">
                    <asp:Label ID="LabelInterviewing" runat="server" Font-Size="Medium" Text="Interviewing Candidates...<br /><br /><br />"></asp:Label>
                    <asp:Image ID="ImageInterview" runat="server" Height="187px" Width="235px" ImageUrl="~/Images/InterviewingCandidates.gif" Visible="False" />
                    <asp:Label ID="MovingToNextPositionLabel" runat="server" Font-Size="Larger" Visible="false"></asp:Label>
                    <asp:Label ID="MovingJobTitleLabel" runat="server" Style="margin-top: 20px;" Font-Bold="true" Font-Size="X-Large" Visible="false" ForeColor="Green"></asp:Label>
                    <br />
                    <asp:Image ID="ImageHired" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/Hired.jpg" Visible="False" />
                    <asp:Label ID="PositionSummaryLbl1" runat="server" Font-Size="Large" Visible="false" Text="<br />The worker you hired has an absolute rank of&nbsp;"></asp:Label>
                    <asp:Label ID="PositionSummaryLbl2" runat="server" Font-Size="X-Large" Visible="false" ForeColor="Green" Font-Bold="true"></asp:Label>
                    <asp:Label ID="PositionSummaryLbl3" runat="server" Font-Size="Large" Visible="false" Text="."></asp:Label>
                    <asp:Label ID="BonusLbl1" runat="server" Font-Size="Large" Visible="false" Text="<br /><br />Bonus added:&nbsp;"></asp:Label>
                    <asp:Label ID="BonusLbl2" runat="server" Font-Size="X-Large" Visible="false" ForeColor="Green" Font-Bold="true"></asp:Label>
                    <asp:Label ID="BonusLbl3" runat="server" Font-Size="Large" Visible="false" Text="."></asp:Label>
                    <asp:Label ID="SummaryNextLbl" runat="server" Visible="false"></asp:Label>
                    <br />
                    <asp:Button ID="btnNextToUniform" runat="server" Visible="false" Text="Next" OnClick="btnNextToUniform_Click" />
                </asp:Panel>


            </asp:Panel>
        </asp:View>
        <asp:View ID="ViewRating" runat="server">

            <asp:Label ID="Label2" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;">Advisor Rating</asp:Label>
            <br />
            <asp:Panel ID="Panel3" runat="server" Style="margin-left: 20px; float: right">
                <br />

                <div style="text-align: center; width: 600px; margin: 0 auto;">
                    <table style="text-align: left; width: 600px;" border="1">
                        <tr>
                            <td>&nbsp;Hi!
                                        <br />
                                <br />
                                &nbsp;We stopped for a moment so you can rate the HR executive.
                                        <br />
                                &nbsp;Your rating should be based on how good you think the HR executive is.
                                        <br />
                                <br />

                                <asp:Label runat="server" Font-Italic="true" Font-Bold="true">
                                        &nbsp;The rating you give will not affect the game or the bonus you get.
                                </asp:Label>
                                <br />
                                <br />
                                &nbsp;Rate the HR executive from 1 to 10, 10 being the best:
                                <br />
                                <br />

                                <div>
                                    <div class="rating-star-block" id='rating-star-block'>
                                        <div class="star outline" href="#" rating="1" title="rate 1">rate 1</div>
                                        <div class="star outline" href="#" rating="2" title="rate 2">rate 2</div>
                                        <div class="star outline" href="#" rating="3" title="rate 3">rate 3</div>
                                        <div class="star outline" href="#" rating="4" title="rate 4">rate 4</div>
                                        <div class="star outline" href="#" rating="5" title="rate 5">rate 5</div>
                                        <div class="star outline" href="#" rating="6" title="rate 6">rate 6</div>
                                        <div class="star outline" href="#" rating="7" title="rate 7">rate 7</div>
                                        <div class="star outline" href="#" rating="8" title="rate 8">rate 8</div>
                                        <div class="star outline" href="#" rating="9" title="rate 9">rate 9</div>
                                        <div class="star outline" href="#" rating="10" title="rate 10">rate 10</div>
                                        <div class="ratingLabel" id="ratingLbl"></div>
                                    </div>
                                </div>

                                <asp:HiddenField ID="ratingHdnValue" Value="0" runat="server" />

                                &nbsp;Please explain why you rated the HR executive as you did:
                            <br />
                                <br />
                                <center>
                            <asp:TextBox ID="reasonTxtBox" onkeypress="return this.value.length<=300" runat="server" Rows="5" Columns="40" TextMode="multiline" Style="margin-left: 5px"></asp:TextBox>
                            </center>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <asp:Button ID="btnRate" runat="server" Text="Rate!" OnClientClick="returnString();" OnClick="btnRate_Click" />
            </asp:Panel>
        </asp:View>

        <asp:View ID="ViewUniformPicker" runat="server">

            <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;">Pick Uniform</asp:Label>
            <br />
            <asp:Panel ID="Panel1" runat="server" Style="margin-left: 20px; float: right">
                <br />

                <br />
                <br />
                <div style="text-align: center; width: 600px; margin: 0 auto;">
                    <table style="text-align: left; width: 600px;" border="1">
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Font-Size="Medium" Text="&nbsp;It's your turn now." Style="margin-left: 20px; align-content: center;"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="UniformPickForPosition" runat="server" Font-Size="Medium" Style="margin-left: 20px; align-content: center;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:ImageButton ID="Uniform1" runat="server" Visible="true" Width="140px" Style="margin-left: 20px" OnClick="btnPickUniform_Click" />
                                <asp:ImageButton ID="Uniform2" runat="server" Visible="true" Width="140px" Style="margin-left: 60px" OnClick="btnPickUniform_Click" />
                                <asp:ImageButton ID="Uniform3" runat="server" Visible="true" Width="140px" Style="margin-left: 60px" OnClick="btnPickUniform_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>

    <asp:Panel ID="PanelPositions" runat="server" Style="margin-left: 0px; float: left">
        <asp:Table ID="PositionsTable"
            BorderColor="black"
            BorderWidth="1"
            GridLines="Both"
            runat="server">
            <asp:TableRow Height="35px">
                <asp:TableCell ID="cell1" Width="165px" HorizontalAlign="Left" Text="&nbsp;Restaurant Positions" Style="color: blue;" Font-Bold="true"></asp:TableCell>
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
            <asp:TableRow Height="35px">
                <asp:TableCell ID="TotalBonusCell" Font-Size="Larger" Font-Bold="true" ForeColor="Purple" HorizontalAlign="Left" Text="&nbsp;Total Bonus:"></asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </asp:Panel>


    <script type="text/javascript">
        function returnString() {
            debugger;
            var ratingStarBlock = document.getElementById('rating-star-block');
            var stars = ratingStarBlock.getElementsByTagName('div');

            var savedRank = 0;
            for (var i = 0; i < stars.length; i++) {
                var child = stars[i];
                if (child.classList.contains("selected")) {
                    savedRank++;
                }
            }

            if (savedRank == 0) {
                alert("Please rate the HR executive!");
                return false;
            }

            document.getElementById('<%=ratingHdnValue.ClientID %>').value = savedRank.toString();
        }
    </script>
</asp:Content>
