<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantGame.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input type="hidden" value="" name="clientScreenHeight" id="clientScreenHeight" />
    <input type="hidden" value="" name="clientScreenWidth" id="clientScreenWidth" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h2>You will recieve 30 cents for this HIT and it will take about 10 minutes of your time. </h2>
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
                <td>For Each position you have 15 candidates, ranked from 1 to 15. Your (and the HR executive's) interest is to pick highly 
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
                    <b><span style="font-size: 20px;"> You will receive a bonus after the game. </span></b>
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
</asp:Content>
