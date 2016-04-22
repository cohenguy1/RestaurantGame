<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProceedToGame.aspx.cs" Inherits="RestaurantGame.ProceedToGame" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

</asp:Content>
