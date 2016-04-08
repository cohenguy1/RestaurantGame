<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IExplorerProblem.aspx.cs" Inherits="RestaurantGame.IExplorerProblem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
</asp:Content>
