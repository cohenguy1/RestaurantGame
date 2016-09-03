<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RestaurantGame.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input type="hidden" value="" name="clientScreenHeight" id="clientScreenHeight" />
    <input type="hidden" value="" name="clientScreenWidth" id="clientScreenWidth" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h2>You will recieve 20 cents for this HIT and it will take about 5 minutes of your time. </h2>
    <h2>Game Background</h2>

    <div style="text-align: center; width: 800px; margin: 0 auto;">
        <table style="text-align: center; max-width: 800px; font-size: large">
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
                        and you will choose the uniform for each position.</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>For each position you have 10 candidates, ranked from 1 to 10. Your (and the HR executive's) interest is to pick highly 
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
                    <b><span style="font-size: 20px;">You will receive a bonus after the game. </span></b>
                    <br />
                    For each worker you hire, you will receive 0.2 cents per rank. For example, if you hire the worst candidate, with an absolute ranking of 10, you will receive exactly 0.2 cents.
                    <br />
                    On the other hand, if you hire the best candidate, with an absolute ranking of 1, you will receive 2 cents.
                    <br />
                    The bonus you get will be the money you accumulated. The bonus can be up to 20 cents.
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