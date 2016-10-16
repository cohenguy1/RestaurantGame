<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="RestaurantGame.Quiz" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                <td>Choose the <font color="red"><b>Incorrect</b></font> sentence:
                        <asp:RadioButtonList ID="rbl1" runat="server">
                            <asp:ListItem>The candidates arrive in a random order</asp:ListItem>
                            <asp:ListItem>You need to interview all the candidates and then must decide which one to hire</asp:ListItem>
                            <asp:ListItem>After rejecting a candidate he cannot be recalled</asp:ListItem>
                            <asp:ListItem>After each interview you must decide whether to hire or reject the candidate</asp:ListItem>
                        </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfv1" Style="color: Red;" ControlToValidate="rbl1" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Let's suppose you hired a worker with the rank #2.
                            <br />
                    How many prize points you will receive?
                        <asp:RadioButtonList ID="rbl2" runat="server">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>90</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfv2" Style="color: Red;" ControlToValidate="rbl2" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Assuming you have managed to accumulate 1000 prize points. What will be your bonus?
                        <asp:RadioButtonList ID="rbl3" runat="server">
                            <asp:ListItem>10 cents</asp:ListItem>
                            <asp:ListItem>40 cents</asp:ListItem>
                            <asp:ListItem>25 cents</asp:ListItem>
                            <asp:ListItem>20 cents</asp:ListItem>
                        </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="rfv3" Style="color: Red;" ControlToValidate="rbl3" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnNextToProceedToGame" runat="server" Text="Next" OnClick="btnNextToProceedToGame_Click" />

</asp:Content>
