<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantGame.Default" %>

<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function hideRemainingCandidatesImages() {
            for (var i = 1; i <= 20; i++) {
                var imageControl = document.getElementById("remainImage" + i);
                imageControl.style.visibility = 'hidden';
            }
        }

        function showRemainingCandidatesImages() {
            for (var i = 1; i <= 20; i++) {
                var imageControl = document.getElementById("remainImage" + i);
                imageControl.style.visibility = 'visible';
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        img#StickMan {
            Height: 79px;
            Width: 30px;
            visibility: hidden;
        }
    </style>

    <input type="hidden" value="" name="clientScreenHeight" id="clientScreenHeight" />
    <input type="hidden" value="" name="clientScreenWidth" id="clientScreenWidth" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Timer ID="TimerGame" OnTick="TimerGame_Tick" runat="server" Enabled="false"></asp:Timer>
    <asp:Timer ID="TimerBlinkRemainingCandidates" OnTick="TimerBlinkRemainingCandidates_Tick" runat="server" Enabled="false"></asp:Timer>
    <asp:Timer ID="TimerRearrangeCandidatesMap" OnTick="TimerRearrangeCandidatesMap_Tick" runat="server" Enabled="false"></asp:Timer>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="view0" runat="server">
            <h2>You will recieve 50 cents for this HIT and it will take about 15 minutes of your time. </h2>
            <h2>Game Background</h2>
            <div style="text-align: center; width: 700px; margin: 0 auto;">
                <table style="text-align: center; max-width: 700px; font-size: large">
                    <tr>
                        <td>You have decided to open up a restaurant.</td>
                    </tr>
                    <tr>
                        <td>In this game you and an Human Resource (HR) executive will interview people for 10 positions in the restaurant.</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>The HR executive will interview the candidates and decide about who fills each position,
                        and you will choose the uniform for each position</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>For Each position you have 20 candidates, ranked from 1 to 20. Your (and the HR executive's) interest is to pick highly 
                            qualified (as possible) workers for the different positions.<br />
                            <br />
                            <asp:Label ID="backgroundText" runat="server"
                                Text="When a candidate is interviewed, we must decide whether to hire him or not."></asp:Label>
                            <br />
                            <asp:Label ID="backgroundText2" runat="server"
                                Text="If the candidate is rejected, he leaves forever and cannot be recalled."></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="label44" runat="server" Font-Size="Large" Font-Bold="true"> You will receive a bonus after the game. </asp:Label>
                            <br />
                            The bonus will be in accordance with the average ranking of the people you hired – you will be given now an additional 20 cents as a bonus and we will deduct from that amount the average of the rankings of the people you've hired (e.g., if you end up hiring the third-best person for the job (on average) your bonus will be 17 cents).
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Press 'Next' to continue. </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnNextToInfo" runat="server" Text="Next" OnClick="btnNextToInfo_Click" Enabled="true" /></td>
                    </tr>
                </table>
            </div>
        </asp:View>

        <asp:View ID="viewInstructions" runat="server">
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
        </asp:View>

        <asp:View ID="viewProceedToTraining" runat="server">
            <h2>Training Mode</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            &nbsp;We will now proceed to the training mode.
                            <br />
                            <br />
                            &nbsp;In the training mode, we will play as the Human Resource executive, and you will choose whether to accept or reject each candidate.
                            <br />
                            <br />
                            &nbsp;After a candidate is hired, you will also choose the uniform for the position.
                            <br />
                            <br />
                            &nbsp;You need to fill in 3 positions in the training mode before you can proceed to the real game.
                            <br />
                            <br />
                            &nbsp;The training will not affect your bonus.
                            <br />
                            <br />
                            <center>
                                <asp:Image ID="Image21" ImageUrl="~/Images/InstructionsTraining.png" Width="500px" Height="300px" runat="server" />
                            </center>
                            <br />
                            &nbsp;Press 'Next' to continue.
                            <br />
                            <br />

                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnNextToTraining" runat="server" Text="Next" OnClick="btnNextToTraining_Click" Enabled="true" />

        </asp:View>

        <asp:View ID="viewQuiz" runat="server">
            <h2>Quiz</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <h3>Think carefully before you answer, you have only 3 chances to pass the quiz.</h3>

                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <asp:Label ID="lblQuiz1" Style="color: Red;" runat="server" Text="Please answer the questions"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>What is the <b>relative ranking</b> of the candidate that arrives first?
                        <asp:RadioButtonList ID="rbl1" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>Can be every ranking in range 1-20</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv1" Style="color: Red;" ControlToValidate="rbl1" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>What is the <b>absolute ranking</b> of the candidate that arrives first?
                        <asp:RadioButtonList ID="rbl2" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>Can be every ranking in range 1-20</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv2" Style="color: Red;" ControlToValidate="rbl2" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>What is the <b>absolute ranking</b> of the candidate that arrives last (19 candidates already interviewed)?
                        <asp:RadioButtonList ID="rbl3" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>Can be every ranking in range 1-20</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv3" Style="color: Red;" ControlToValidate="rbl3" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Let's suppose you ended up with an average ranking of 4.
                            <br />
                            What will be your bonus?
                        <asp:RadioButtonList ID="rbl4" runat="server">
                            <asp:ListItem>0 cents</asp:ListItem>
                            <asp:ListItem>16 cents</asp:ListItem>
                            <asp:ListItem>17 cents</asp:ListItem>
                            <asp:ListItem>20 cents</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv4" Style="color: Red;" ControlToValidate="rbl4" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnNextToProceedToGame" runat="server" Text="Next" OnClick="btnNextToProceedToGame_Click" />

        </asp:View>

        <asp:View ID="viewProceedToGame" runat="server">
            <h2>Game Instructions</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            &nbsp;We will now proceed to the real game.
                            <br />
                            <br />
                            &nbsp;In the game, the Human Resource executive will choose whether to accept or reject each candidate.
                            <br />
                            <br />
                            &nbsp;After a candidate is hired, you will choose the uniform for the position.
                            <br />
                            <br />
                            &nbsp;This is a fun part where the HR executive will be doing most of the work for you!
                            <br />
                            <br />
                            <br />
                            &nbsp;Press 'Next' to continue.
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnNextToGame" runat="server" Text="Next" OnClick="btnNextToGame_Click" />

        </asp:View>

        <asp:View ID="View2" runat="server">

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
                                        <tr>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow1" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow2" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow3" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow4" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow5" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow6" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow7" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow8" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow9" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow10" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow11" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow12" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow13" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow14" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow15" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow16" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow17" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow18" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow19" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                            <td>
                                                <asp:Image class="StickMan" ID="StickManSecondRow20" runat="server" Height="79px" Width="30px" Visible="false"></asp:Image></td>
                                        </tr>
                                    </table>

                                </div>

                            </asp:Panel>


                            <asp:Panel ID="PanelBasket" runat="server" Width="65px" Style="margin-left: 20px; float: right">
                                <br />
                                <br />
                                <asp:Label ID="CandidatesRemainingLbl" runat="server" Font-Size="Medium" Text="Candidates Remaining:"></asp:Label>
                                <br />
                                <br />
                                <div class="Basket">
                                    <asp:Table ID="table1" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage1" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage2" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage3" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage4" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage5" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage6" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage7" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage8" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage9" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage10" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage11" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage12" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage13" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage14" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage15" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage16" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage17" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage18" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage19" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Image ID="remainImage20" runat="server" ImageUrl="~/Images/SmallStickMan.png" Height="55px" Width="23px" Visible="true"></asp:Image>
                                            </asp:TableCell>

                                        </asp:TableRow>

                                    </asp:Table>

                                </div>

                            </asp:Panel>

                        </asp:Panel>

                        <br />
                        <asp:Panel ID="PanelSpeed" runat="server" Style="margin-left: 0px; margin-top: 60px; align-content: center;" Width="600px">
                            <asp:Image ID="ChooseBaloon" runat="server" ImageUrl="~/Images/Choose.png" Height="130px" Width="150px" Visible="false" Style="margin-left: -150px" />
                            <asp:Image ID="ImageManForward" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/ManForward.gif" />
                            <asp:Image ID="ImageInterview" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/JobInterview.png" Visible="False" />
                            <asp:Image ID="ImageHired" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/Hired.jpg" Visible="False" />
                            <asp:Label ID="MovingToNextPositionLabel" runat="server" Font-Size="Larger" Visible="false"></asp:Label>
                            <asp:Label ID="MovingJobTitleLabel" runat="server" Style="margin-top: 20px;" Font-Bold="true" Font-Size="X-Large" Visible="false" ForeColor="Green"></asp:Label>
                            <br />
                            <asp:ImageButton ID="btnThumbsUp" runat="server" ImageUrl="~/Images/thumbsUpButton.jpg" Height="48px" Width="48px" OnClick="btnThumbsUp_Click" ToolTip="Accept" Enabled="false" />
                            <asp:ImageButton ID="btnThumbsDown" runat="server" ImageUrl="~/Images/thumbsDownButton.jpg" Height="48px" Width="48px" OnClick="btnThumbsDown_Click" ToolTip="Reject" Enabled="false" />
                            <br />
                            <asp:ImageButton ID="btnFastBackwards" runat="server" ImageUrl="~/Images/fbButton.png" OnClick="btnFastBackwards_Click" Enabled="true" />
                            <asp:ImageButton ID="btnPausePlay" runat="server" ImageUrl="~/Images/pauseButton.png" OnClick="btnPausePlay_Click" Enabled="true" />
                            <asp:ImageButton ID="btnFastForward" runat="server" ImageUrl="~/Images/ffButton.png" OnClick="btnFastForward_Click" Enabled="true" Style="margin-bottom: auto" />
                            <asp:Label ID="PositionSummaryLbl1" runat="server" Font-Size="Large" Visible="false" Text="The worker you hired has an absolute rank of&nbsp;"></asp:Label>
                            <asp:Label ID="PositionSummaryLbl2" runat="server" Font-Size="X-Large" Visible="false" ForeColor="Green" Font-Bold="true"></asp:Label>
                            <asp:Label ID="PositionSummaryLbl3" runat="server" Font-Size="Large" Visible="false" Text="."></asp:Label>
                            <asp:Label ID="SummaryNextLbl" runat="server" Visible="false"></asp:Label>
                            <br />
                            <asp:Label ID="LabelSpeed" runat="server" Font-Size="Medium" Text="&nbsp;Speed: x1.0"></asp:Label>
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
                                        <asp:RadioButtonList ID="RatingRbL" runat="server" value="null">
                                            <asp:ListItem>1 - The worst HR executive ever!</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3 - Making poor decisions.</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8 - Making good decisions.</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10 - I&#39;m loving him!</asp:ListItem>
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

                <asp:View ID="ViewTrainingAsk" runat="server">

                    <asp:Label ID="Label9" runat="server" Font-Size="X-Large" Font-Bold="true" Style="margin-left: 20px; align-content: center;">Advisor Rating</asp:Label>
                    <br />
                    <asp:Panel ID="Panel2" runat="server" Style="margin-left: 20px; float: right">
                        <br />
                        <div style="text-align: center; width: 600px; margin: 0 auto;">
                            <table style="text-align: left; width: 600px;" border="1">
                                <tr>
                                    <td>&nbsp;Would you like to continue the training or end it and continue to the game?
                                        <br />
                                        <br />
                                        <asp:RadioButtonList ID="trainingRBL" runat="server">
                                            <asp:ListItem>Continue Training</asp:ListItem>
                                            <asp:ListItem>Start Game</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: Red;" ControlToValidate="trainingRBL" runat="server" ErrorMessage="Please choose one of the options above"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnNextToQuiz" runat="server" Text="Send" OnClick="btnNextToQuiz_Click" />
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
                        <asp:TableCell ID="cell1" Width="157px" HorizontalAlign="Left" Text="&nbsp;Restaurant Positions" Style="color: blue;" Font-Bold="true"></asp:TableCell>
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
                        <asp:TableCell ID="AvgRankCell" Font-Bold="true" ForeColor="Purple" HorizontalAlign="Left" Text="&nbsp;Average Ranking:"></asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </asp:Panel>
        </asp:View>

        <asp:View ID="Thanks" runat="server">
            <h2>Thank you for playing!</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            &nbsp;Thanks for playing, You did great!
                            <br />
                            <br />
                            &nbsp;Please provide feedback about the game (how hard it was, was it fun, game graphics):
                            <br />
                            <br />
                            <asp:TextBox ID="feedbackTxtBox" onkeypress="return this.value.length<=120" runat="server" Rows="4" Columns="40" TextMode="multiline" Style="margin-left: 5px"></asp:TextBox>
                            <br />
                            <br />
                            &nbsp;The average ranking of the people you hired is:
                            <br />
                            <br />
                            <center>
                            <asp:Label ID="AverageRank" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Larger"></asp:Label>
                            </center>
                            <br />
                            &nbsp;Your bonus is:
                            <br />
                            <br />
                            <center>
                            <asp:Label ID="Bonus" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="Larger"></asp:Label>
                            </center>
                            <br />
                            <br />
                            &nbsp;Please click on 'Collect your reward' to submit the HIT and send your feedback.
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <asp:Button ID="rewardBtn" runat="server" OnClick="rewardBtn_Click" Text="Collect your reward" />
        </asp:View>

        <asp:View ID="QuizWrong" runat="server">
            <h2>Quiz</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="QuizWrongLbl" runat="server"></asp:Label>
                            <br />
                            <br />
                            &nbsp;The game is over, thank you for your time.
                            <br />
                            <br />
                            &nbsp;Please return the HIT.
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>

        <asp:View ID="ResolutionProblem" runat="server">
            <h2></h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="Label10" runat="server"></asp:Label>
                            <br />
                            &nbsp;Sorry, you cannot enter the game from a mobile device, or with a small browser size.
                            <br />
                            <br />
                            &nbsp;The game is compatible with a browser resolution of at least 800x500.
                            <br />
                            <br />
                            &nbsp;Please return the HIT.
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>

        <asp:View ID="NoMoreSlots" runat="server">
            <h2></h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="Label11" runat="server"></asp:Label>
                            <br />
                            &nbsp;Sorry, no available slots to play the game.
                            <br />
                            <br />
                            &nbsp;Please return the HIT and come back later.
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>

        <asp:View ID="iExplorerProblem" runat="server">
            <h2></h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="Label12" runat="server"></asp:Label>
                            <br />
                            &nbsp;Sorry, you cannot play this game from Internet explorer.
                            <br />
                            <br />
                            &nbsp;Please return the HIT.
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
