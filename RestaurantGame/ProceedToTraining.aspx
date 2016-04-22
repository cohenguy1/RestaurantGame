<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProceedToTraining.aspx.cs" Inherits="RestaurantGame.ProceedToTraining" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <img src="/Images/InstructionsTraining.png" Width="500px" Height="300px" />
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

</asp:Content>

