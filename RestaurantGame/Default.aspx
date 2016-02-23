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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Timer ID="TimerGame" OnTick="TimerGame_Tick" runat="server" Enabled="false"></asp:Timer>
    <asp:Timer ID="TimerBlinkRemainingCandidates" OnTick="TimerBlinkRemainingCandidates_Tick" runat="server" Enabled="false"></asp:Timer>
    <asp:Timer ID="TimerRearrangeCandidatesMap" OnTick="TimerRearrangeCandidatesMap_Tick" runat="server" Enabled="false"></asp:Timer>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="view0" runat="server">

            <h2 class="style3">You will recieve &lt;X&gt; cents for this Game and it will take &lt;X&gt; minutes of your time. </h2>

            <p class="style3">
                &nbsp;
            </p>
            <h2>Game Background</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: center; max-width: 640px;">
                    <tr>
                        <td>You have decided to open up a restaurant.</td>
                    </tr>
                    <tr>
                        <td>&lt;Dudi will fill here&gt; In this game you and an Human Resource (HR) team  will interview people for 10 positions for the restaurant.</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&lt;Dudi will fill here&gt; The HR team will interview the candidates and decide about who fills each position,
                        and you will choose the uniform for each position</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>For Each position you have 20 candidates, ranked from 1 to 20. Your (and the HR's) interest is to pick the best candidate for each position.<br />
                            <br />
                            <asp:Label ID="backgroundText" runat="server"
                                Text="When a candidate is interviewed, we must decide whether to hire him or not."></asp:Label>
                            <br />
                            If the candidate is rejected, he cannot be recalled.
                        <br />
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="label44" runat="server" Font-Bold="true"> You will receive a bonus after the game. </asp:Label>
                            <br />
                            The amount of the bonus will be in accordance with the people you hire.
                    <br />
                            The better the ranking of the people you hire, the higher the bonus you get.
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button ID="btnNextToInfo" runat="server" Text="Next" OnClick="btnNextToInfo_Click" Enabled="false" /></td>
                    </tr>
                </table>
            </div>
        </asp:View>

        <asp:View ID="viewInfo" runat="server">
            <h2 style="color: #0066FF;"><span class="style13"><strong>Some information before we start..</strong></span></h2>
            <asp:Panel ID="panel" runat="server"
                Style="text-align: left; margin-top: 0px;">
                <asp:Label ID="label3" runat="server" Text="Your Gender:" Style="color: #000000; text-align: center;"
                    Font-Size="Large" Font-Bold="True" Font-Names="Comic Sans MS"
                    Width="400px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="170px"
                    Style="text-align: center">
                    <asp:ListItem>-- select one --</asp:ListItem>
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredGenderValidator" Style="color: Red; font-family: 'Comic Sans MS'; font-size: small;"
                    runat="server" ErrorMessage="  Please select gender" ControlToValidate="DropDownList1"
                    ValidationGroup="selectDropDownList" InitialValue="-- select one --">
                </asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="label4" runat="server" Font-Bold="True" Font-Names="Comic Sans MS"
                    Font-Size="Large" Style="color: #000000; text-align: center;"
                    Text="Your Age:" Width="400px"></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server" Height="22px" Width="170px">
                    <asp:ListItem>-- select one --</asp:ListItem>
                    <asp:ListItem>0-10</asp:ListItem>
                    <asp:ListItem>11-20</asp:ListItem>
                    <asp:ListItem>21-25</asp:ListItem>
                    <asp:ListItem>26-30</asp:ListItem>
                    <asp:ListItem>31-35</asp:ListItem>
                    <asp:ListItem>36-40</asp:ListItem>
                    <asp:ListItem>41-45</asp:ListItem>
                    <asp:ListItem>46-50</asp:ListItem>
                    <asp:ListItem>51-55</asp:ListItem>
                    <asp:ListItem>56-60</asp:ListItem>
                    <asp:ListItem>61-65</asp:ListItem>
                    <asp:ListItem>66-70</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredAgeValidator" Style="color: Red; font-family: 'Comic Sans MS'; font-size: small;"
                    runat="server" ErrorMessage="  Please select age" ControlToValidate="DropDownList2"
                    ValidationGroup="selectDropDownList" InitialValue="-- select one --">
                </asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="label5" runat="server" Font-Bold="True" Font-Names="Comic Sans MS"
                    Font-Size="Large" Style="color: #000000; text-align: center;" Text="Your Education:"
                    Width="400px"></asp:Label>
                <asp:DropDownList ID="DropDownList3" runat="server" Height="22px" Width="170px">
                    <asp:ListItem>-- select one --</asp:ListItem>
                    <asp:ListItem>Primary education</asp:ListItem>
                    <asp:ListItem>Secondary education</asp:ListItem>
                    <asp:ListItem>Bachelor</asp:ListItem>
                    <asp:ListItem>Master</asp:ListItem>
                    <asp:ListItem>Doctoral</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredEducationValidator" Style="color: Red; font-family: 'Comic Sans MS'; font-size: small;"
                    runat="server" ErrorMessage="Please select education" ControlToValidate="DropDownList3"
                    ValidationGroup="selectDropDownList" InitialValue="-- select one --">
                </asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="label6" runat="server" Font-Bold="True" Font-Names="Comic Sans MS"
                    Font-Size="Large" Style="color: #000000; text-align: center;" Text="Your Nationaity:"
                    Width="400px"></asp:Label>
                <asp:DropDownList ID="DropDownList4" runat="server" Height="22px" Width="170px">
                    <asp:ListItem>-- select one --</asp:ListItem>
                    <asp:ListItem>United States</asp:ListItem>
                    <asp:ListItem>India</asp:ListItem>
                    <asp:ListItem>Russian Federation</asp:ListItem>
                    <asp:ListItem>Afghanistan</asp:ListItem>
                    <asp:ListItem>Albania</asp:ListItem>
                    <asp:ListItem>Algeria</asp:ListItem>
                    <asp:ListItem>American Samoa</asp:ListItem>
                    <asp:ListItem>Andorra</asp:ListItem>
                    <asp:ListItem>Angola</asp:ListItem>
                    <asp:ListItem>Anguilla</asp:ListItem>
                    <asp:ListItem>Antarctica</asp:ListItem>
                    <asp:ListItem>Antigua And Barbuda</asp:ListItem>
                    <asp:ListItem>Argentina</asp:ListItem>
                    <asp:ListItem>Armenia</asp:ListItem>
                    <asp:ListItem>Aruba</asp:ListItem>
                    <asp:ListItem>Australia</asp:ListItem>
                    <asp:ListItem>Austria</asp:ListItem>
                    <asp:ListItem>Azerbaijan</asp:ListItem>
                    <asp:ListItem>Bahamas</asp:ListItem>
                    <asp:ListItem>Bahrain</asp:ListItem>
                    <asp:ListItem>Bangladesh</asp:ListItem>
                    <asp:ListItem>Barbados</asp:ListItem>
                    <asp:ListItem>Belarus</asp:ListItem>
                    <asp:ListItem>Belgium</asp:ListItem>
                    <asp:ListItem>Belize</asp:ListItem>
                    <asp:ListItem>Benin</asp:ListItem>
                    <asp:ListItem>Bermuda</asp:ListItem>
                    <asp:ListItem>Bhutan</asp:ListItem>
                    <asp:ListItem>Bolivia</asp:ListItem>
                    <asp:ListItem>Bosnia And Herzegowina</asp:ListItem>
                    <asp:ListItem>Botswana</asp:ListItem>
                    <asp:ListItem>Bouvet Island</asp:ListItem>
                    <asp:ListItem>Brazil</asp:ListItem>
                    <asp:ListItem>British Indian Ocean Territory</asp:ListItem>
                    <asp:ListItem>Brunei Darussalam</asp:ListItem>
                    <asp:ListItem>Bulgaria</asp:ListItem>
                    <asp:ListItem>Burkina Faso</asp:ListItem>
                    <asp:ListItem>Burundi</asp:ListItem>
                    <asp:ListItem>Cambodia</asp:ListItem>
                    <asp:ListItem>Cameroon</asp:ListItem>
                    <asp:ListItem>Canada</asp:ListItem>
                    <asp:ListItem>Cape Verde</asp:ListItem>
                    <asp:ListItem>Cayman Islands</asp:ListItem>
                    <asp:ListItem>Central African Republic</asp:ListItem>
                    <asp:ListItem>Chad</asp:ListItem>
                    <asp:ListItem>Chile</asp:ListItem>
                    <asp:ListItem>China</asp:ListItem>
                    <asp:ListItem>Colombia</asp:ListItem>
                    <asp:ListItem>Comoros</asp:ListItem>
                    <asp:ListItem>Congo</asp:ListItem>
                    <asp:ListItem>Cook Islands</asp:ListItem>
                    <asp:ListItem>Costa Rica</asp:ListItem>
                    <asp:ListItem>Cote D'Ivoire</asp:ListItem>
                    <asp:ListItem>Croatia</asp:ListItem>
                    <asp:ListItem>Cuba</asp:ListItem>
                    <asp:ListItem>Cyprus</asp:ListItem>
                    <asp:ListItem>Czech Republic</asp:ListItem>
                    <asp:ListItem>Denmark</asp:ListItem>
                    <asp:ListItem>Djibouti</asp:ListItem>
                    <asp:ListItem>Dominica</asp:ListItem>
                    <asp:ListItem>Dominican Republic</asp:ListItem>
                    <asp:ListItem>East Timor</asp:ListItem>
                    <asp:ListItem>Ecuador</asp:ListItem>
                    <asp:ListItem>Egypt</asp:ListItem>
                    <asp:ListItem>El Salvador</asp:ListItem>
                    <asp:ListItem>Equatorial Guinea</asp:ListItem>
                    <asp:ListItem>Eritrea</asp:ListItem>
                    <asp:ListItem>Estonia</asp:ListItem>
                    <asp:ListItem>Ethiopia</asp:ListItem>
                    <asp:ListItem>Falkland Islands (Malvinas)</asp:ListItem>
                    <asp:ListItem>Faroe Islands</asp:ListItem>
                    <asp:ListItem>Fiji</asp:ListItem>
                    <asp:ListItem>Finland</asp:ListItem>
                    <asp:ListItem>France</asp:ListItem>
                    <asp:ListItem>French Guiana</asp:ListItem>
                    <asp:ListItem>French Polynesia</asp:ListItem>
                    <asp:ListItem>French Southern Territories</asp:ListItem>
                    <asp:ListItem>Gabon</asp:ListItem>
                    <asp:ListItem>Gambia</asp:ListItem>
                    <asp:ListItem>Georgia</asp:ListItem>
                    <asp:ListItem>Germany</asp:ListItem>
                    <asp:ListItem>Ghana</asp:ListItem>
                    <asp:ListItem>Gibraltar</asp:ListItem>
                    <asp:ListItem>Greece</asp:ListItem>
                    <asp:ListItem>Greenland</asp:ListItem>
                    <asp:ListItem>Grenada</asp:ListItem>
                    <asp:ListItem>Guadeloupe</asp:ListItem>
                    <asp:ListItem>Guam</asp:ListItem>
                    <asp:ListItem>Guatemala</asp:ListItem>
                    <asp:ListItem>Guinea</asp:ListItem>
                    <asp:ListItem>Guinea-Bissau</asp:ListItem>
                    <asp:ListItem>Guyana</asp:ListItem>
                    <asp:ListItem>Haiti</asp:ListItem>
                    <asp:ListItem>Honduras</asp:ListItem>
                    <asp:ListItem>Hong Kong</asp:ListItem>
                    <asp:ListItem>Hungary</asp:ListItem>
                    <asp:ListItem>Icel And</asp:ListItem>
                    <asp:ListItem>Indonesia</asp:ListItem>
                    <asp:ListItem>Iran</asp:ListItem>
                    <asp:ListItem>Iraq</asp:ListItem>
                    <asp:ListItem>Ireland</asp:ListItem>
                    <asp:ListItem>Israel</asp:ListItem>
                    <asp:ListItem>Italy</asp:ListItem>
                    <asp:ListItem>Jamaica</asp:ListItem>
                    <asp:ListItem>Japan</asp:ListItem>
                    <asp:ListItem>Jordan</asp:ListItem>
                    <asp:ListItem>Kazakhstan</asp:ListItem>
                    <asp:ListItem>Kenya</asp:ListItem>
                    <asp:ListItem>Kiribati</asp:ListItem>
                    <asp:ListItem>Korea</asp:ListItem>
                    <asp:ListItem>Kuwait</asp:ListItem>
                    <asp:ListItem>Kyrgyzstan</asp:ListItem>
                    <asp:ListItem>Latvia</asp:ListItem>
                    <asp:ListItem>Lebanon</asp:ListItem>
                    <asp:ListItem>Lesotho</asp:ListItem>
                    <asp:ListItem>Liberia</asp:ListItem>
                    <asp:ListItem>Libyan</asp:ListItem>
                    <asp:ListItem>Liechtenstein</asp:ListItem>
                    <asp:ListItem>Lithuania</asp:ListItem>
                    <asp:ListItem>Luxembourg</asp:ListItem>
                    <asp:ListItem>Macau</asp:ListItem>
                    <asp:ListItem>Macedonia</asp:ListItem>
                    <asp:ListItem>Madagascar</asp:ListItem>
                    <asp:ListItem>Malawi</asp:ListItem>
                    <asp:ListItem>Malaysia</asp:ListItem>
                    <asp:ListItem>Maldives</asp:ListItem>
                    <asp:ListItem>Mali</asp:ListItem>
                    <asp:ListItem>Malta</asp:ListItem>
                    <asp:ListItem>Marshall Islands</asp:ListItem>
                    <asp:ListItem>Martinique</asp:ListItem>
                    <asp:ListItem>Mauritania</asp:ListItem>
                    <asp:ListItem>Mauritius</asp:ListItem>
                    <asp:ListItem>Mayotte</asp:ListItem>
                    <asp:ListItem>Mexico</asp:ListItem>
                    <asp:ListItem>Micronesia, Federated States</asp:ListItem>
                    <asp:ListItem>Moldova, Republic Of</asp:ListItem>
                    <asp:ListItem>Monaco</asp:ListItem>
                    <asp:ListItem>Mongolia</asp:ListItem>
                    <asp:ListItem>Montserrat</asp:ListItem>
                    <asp:ListItem>Morocco</asp:ListItem>
                    <asp:ListItem>Mozambique</asp:ListItem>
                    <asp:ListItem>Myanmar</asp:ListItem>
                    <asp:ListItem>Namibia</asp:ListItem>
                    <asp:ListItem>Nauru</asp:ListItem>
                    <asp:ListItem>Nepal</asp:ListItem>
                    <asp:ListItem>Netherlands</asp:ListItem>
                    <asp:ListItem>Netherlands Ant Illes</asp:ListItem>
                    <asp:ListItem>New Caledonia</asp:ListItem>
                    <asp:ListItem>New Zealand</asp:ListItem>
                    <asp:ListItem>Nicaragua</asp:ListItem>
                    <asp:ListItem>Niger</asp:ListItem>
                    <asp:ListItem>Nigeria</asp:ListItem>
                    <asp:ListItem>Niue</asp:ListItem>
                    <asp:ListItem>Norfolk Island</asp:ListItem>
                    <asp:ListItem>Northern Mariana Islands</asp:ListItem>
                    <asp:ListItem>Norway</asp:ListItem>
                    <asp:ListItem>Oman</asp:ListItem>
                    <asp:ListItem>Pakistan</asp:ListItem>
                    <asp:ListItem>Palau</asp:ListItem>
                    <asp:ListItem>Panama</asp:ListItem>
                    <asp:ListItem>Papua New Guinea</asp:ListItem>
                    <asp:ListItem>Paraguay</asp:ListItem>
                    <asp:ListItem>Peru</asp:ListItem>
                    <asp:ListItem>Philippines</asp:ListItem>
                    <asp:ListItem>Pitcairn</asp:ListItem>
                    <asp:ListItem>Poland</asp:ListItem>
                    <asp:ListItem>Portugal</asp:ListItem>
                    <asp:ListItem>Puerto Rico</asp:ListItem>
                    <asp:ListItem>Qatar</asp:ListItem>
                    <asp:ListItem>Reunion</asp:ListItem>
                    <asp:ListItem>Romania</asp:ListItem>
                    <asp:ListItem>Rwanda</asp:ListItem>
                    <asp:ListItem>Saint K Itts And Nevis</asp:ListItem>
                    <asp:ListItem>Saint Lucia</asp:ListItem>
                    <asp:ListItem>Saint Vincent, The Grenadines</asp:ListItem>
                    <asp:ListItem>Samoa</asp:ListItem>
                    <asp:ListItem>San Marino</asp:ListItem>
                    <asp:ListItem>Sao Tome And Principe</asp:ListItem>
                    <asp:ListItem>Saudi Arabia</asp:ListItem>
                    <asp:ListItem>Senegal</asp:ListItem>
                    <asp:ListItem>Seychelles</asp:ListItem>
                    <asp:ListItem>Sierra Leone</asp:ListItem>
                    <asp:ListItem>Singapore</asp:ListItem>
                    <asp:ListItem>Slovakia (Slovak Republic)</asp:ListItem>
                    <asp:ListItem>Slovenia</asp:ListItem>
                    <asp:ListItem>Solomon Islands</asp:ListItem>
                    <asp:ListItem>Somalia</asp:ListItem>
                    <asp:ListItem>South Africa</asp:ListItem>
                    <asp:ListItem>South Georgia</asp:ListItem>
                    <asp:ListItem>Spain</asp:ListItem>
                    <asp:ListItem>Sri Lanka</asp:ListItem>
                    <asp:ListItem>St. Helena</asp:ListItem>
                    <asp:ListItem>St. Pierre And Miquelon</asp:ListItem>
                    <asp:ListItem>Sudan</asp:ListItem>
                    <asp:ListItem>Suriname</asp:ListItem>
                    <asp:ListItem>Svalbard, Jan Mayen Islands</asp:ListItem>
                    <asp:ListItem>Sw Aziland</asp:ListItem>
                    <asp:ListItem>Sweden</asp:ListItem>
                    <asp:ListItem>Switzerland</asp:ListItem>
                    <asp:ListItem>Syrian Arab Republic</asp:ListItem>
                    <asp:ListItem>Taiwan</asp:ListItem>
                    <asp:ListItem>Tajikistan</asp:ListItem>
                    <asp:ListItem>Tanzania, United Republic Of</asp:ListItem>
                    <asp:ListItem>Thailand</asp:ListItem>
                    <asp:ListItem>Togo</asp:ListItem>
                    <asp:ListItem>Tokelau</asp:ListItem>
                    <asp:ListItem>Tonga</asp:ListItem>
                    <asp:ListItem>Trinidad And Tobago</asp:ListItem>
                    <asp:ListItem>Tunisia</asp:ListItem>
                    <asp:ListItem>Turkey</asp:ListItem>
                    <asp:ListItem>Turkmenistan</asp:ListItem>
                    <asp:ListItem>Turks And Caicos Islands</asp:ListItem>
                    <asp:ListItem>Tuvalu</asp:ListItem>
                    <asp:ListItem>Uganda</asp:ListItem>
                    <asp:ListItem>Ukraine</asp:ListItem>
                    <asp:ListItem>United Arab Emirates</asp:ListItem>
                    <asp:ListItem>United Kingdom</asp:ListItem>
                    <asp:ListItem>Uruguay</asp:ListItem>
                    <asp:ListItem>Uzbekistan</asp:ListItem>
                    <asp:ListItem>Vanuatu</asp:ListItem>
                    <asp:ListItem>Venezuela</asp:ListItem>
                    <asp:ListItem>Viet Nam</asp:ListItem>
                    <asp:ListItem>Virgin Islands (British)</asp:ListItem>
                    <asp:ListItem>Virgin Islands (U.S.)</asp:ListItem>
                    <asp:ListItem>Wallis And Futuna Islands</asp:ListItem>
                    <asp:ListItem>Western Sahara</asp:ListItem>
                    <asp:ListItem>Yemen</asp:ListItem>
                    <asp:ListItem>Yugoslavia</asp:ListItem>
                    <asp:ListItem>Zaire</asp:ListItem>
                    <asp:ListItem>Zambia</asp:ListItem>
                    <asp:ListItem>Zimbabwe</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredNationalityValidator" Style="color: Red; font-family: 'Comic Sans MS'; font-size: small;"
                    runat="server" ErrorMessage="  Please select nationality"
                    ControlToValidate="DropDownList4" ValidationGroup="selectDropDownList" InitialValue="-- select one --">
                </asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="label7" runat="server" Font-Bold="True" Font-Names="Comic Sans MS"
                    Font-Size="Large" Style="color: #000000; text-align: center;" Text="Reason for participating:"
                    Width="400px"></asp:Label>
                <asp:DropDownList ID="DropDownList5" runat="server" Height="22px" Width="170px">
                    <asp:ListItem>-- select one --</asp:ListItem>
                    <asp:ListItem>I am at work and I have spare time</asp:ListItem>
                    <asp:ListItem>My primary work is Amazon Turk</asp:ListItem>
                    <asp:ListItem>I need to "burn" free time</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredReasonValidator" Style="color: Red; font-family: 'Comic Sans MS'; font-size: small;"
                    runat="server" ErrorMessage="Please select your reason" ControlToValidate="DropDownList5"
                    ValidationGroup="selectDropDownList" InitialValue="-- select one --">
                </asp:RequiredFieldValidator>

            </asp:Panel>
            <br />
            <asp:Button ID="btnNextToInstructions" runat="server" OnClick="btnNextToInstructions_Click" Text="OK"
                ValidationGroup="selectDropDownList" />

            <asp:HiddenField ID="windowWidth0" runat="server" Value="" />
            <asp:HiddenField ID="windowHeight0" runat="server" Value="" />

        </asp:View>

        <asp:View ID="viewInstructions" runat="server">
            <asp:MultiView ID="MultiviewInstructions" runat="server" ActiveViewIndex="0">
                <asp:View ID="view8" runat="server">

                    <asp:Image ID="InstructionImage" ImageUrl="~/Images/Instructions0.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view9" runat="server">
                    <asp:Image ID="Image1" ImageUrl="~/Images/Instructions1.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view10" runat="server">
                    <asp:Image ID="Image2" ImageUrl="~/Images/Instructions2.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view11" runat="server">
                    <asp:Image ID="Image3" ImageUrl="~/Images/Instructions3.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view12" runat="server">
                    <asp:Image ID="Image4" ImageUrl="~/Images/Instructions4.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view13" runat="server">
                    <asp:Image ID="Image5" ImageUrl="~/Images/Instructions5.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view14" runat="server">
                    <asp:Image ID="Image6" ImageUrl="~/Images/Instructions6.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view15" runat="server">
                    <asp:Image ID="Image7" ImageUrl="~/Images/Instructions7.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view16" runat="server">
                    <asp:Image ID="Image8" ImageUrl="~/Images/Instructions8.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view17" runat="server">
                    <asp:Image ID="Image9" ImageUrl="~/Images/Instructions9.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view18" runat="server">
                    <asp:Image ID="Image10" ImageUrl="~/Images/Instructions10.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view19" runat="server">
                    <asp:Image ID="Image11" ImageUrl="~/Images/Instructions11.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view20" runat="server">
                    <asp:Image ID="Image12" ImageUrl="~/Images/Instructions12.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view21" runat="server">
                    <asp:Image ID="Image13" ImageUrl="~/Images/Instructions13.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view22" runat="server">
                    <asp:Image ID="Image14" ImageUrl="~/Images/Instructions14.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view23" runat="server">
                    <asp:Image ID="Image15" ImageUrl="~/Images/Instructions15.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view24" runat="server">
                    <asp:Image ID="Image16" ImageUrl="~/Images/Instructions16.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view25" runat="server">
                    <asp:Image ID="Image17" ImageUrl="~/Images/Instructions17.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view26" runat="server">
                    <asp:Image ID="Image18" ImageUrl="~/Images/Instructions18.png" Width="700px" runat="server" />
                </asp:View>
                <asp:View ID="view27" runat="server">
                    <asp:Image ID="Image19" ImageUrl="~/Images/Instructions19.png" Width="700px" runat="server" />
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
            <h2>Game Instructions</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <br />
                            We will now proceed to the training mode.
                            <br />
                            <br />
                            In the training mode, we will play as the Human resource team, and you will choose whether to accept or reject each candidate.
                            <br />
                            <br />
                            After a candidate is accepted, you will also choose the uniform for the position.
                            <br />
                            <br />
                            You need to play the first 3 positions before you can proceed to the real game.
                            <br />
                            <br />
                            The training will not affect your bonus.
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnNextToTraining" runat="server" Text="Next" OnClick="btnNextToTraining_Click" />

        </asp:View>

        <asp:View ID="viewQuiz" runat="server">
            <h2>Quiz</h2>
            <div style="text-align: center; width: 640px; margin: 0 auto;">
                <table style="text-align: left; width: 640px;" border="1">
                    <tr>
                        <td>
                            <asp:Label ID="lblQuiz1" Style="color: Red;" runat="server" Text="Please answer the questions"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>What is the relative rank of the candidate that arrives first?
                        <asp:RadioButtonList ID="rbl1" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>Can be every rank in range 1-20</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv1" Style="color: Red;" ControlToValidate="rbl1" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>What is the absolute rank of the candidate that arrives first?
                        <asp:RadioButtonList ID="rbl2" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>Can be every rank in range 1-20</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv2" Style="color: Red;" ControlToValidate="rbl2" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>What is the absolute rank of the candidate that arrives last (19 candidates already interviewed)?
                        <asp:RadioButtonList ID="rbl3" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>Can be every rank in range 1-20</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv3" Style="color: Red;" ControlToValidate="rbl3" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!--<tr>
                        <td>Lets suppose we rejected 10 candidates, and a new candidate arrives with relative rank of 11 (worst so far).
                            <br />
                            What can be the absolute rank of the candidate?
                        <asp:RadioButtonList ID="rbl4" runat="server">
                            <asp:ListItem>Must be 11</asp:ListItem>
                            <asp:ListItem>Can be every rank in range 1-10</asp:ListItem>
                            <asp:ListItem>Can be every rank in range 11-20</asp:ListItem>
                            <asp:ListItem>Can be every rank in range 1-20</asp:ListItem>
                        </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfv4" Style="color: Red;" ControlToValidate="rbl4" runat="server" ErrorMessage="You have to answer"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    -->
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
                            We will now proceed to the play the game.
                            <br />
                            <br />
                            In the game, the Human Resource team will choose whether to accept or reject each candidate.
                            <br />
                            <br />
                            After a candidate is accepted, you will choose the uniform for the position.
                            <br />
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
                            <asp:Image ID="ImageManForward" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/ManForward.gif" />
                            <asp:Image ID="ImageInterview" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/JobInterview.png" Visible="False" />
                            <asp:Image ID="ImageHired" runat="server" Height="147px" Width="184px" ImageUrl="~/Images/Hired.jpg" Visible="False" />
                            <asp:Label ID="MovingToNextPositionLabel" runat="server" Font-Size="Larger" Visible="false"></asp:Label>
                            <asp:Label ID="MovingJobTitleLabel" runat="server" Style="margin-top: 20px;" Font-Bold="true" Font-Size="X-Large" Visible="false" ForeColor="Green"></asp:Label>
                            <br />
                            <asp:Image ID="ChooseBaloon" runat="server" ImageUrl="~/Images/Choose.png" Height="55px" Width="94px" Visible="false" Style="margin-left: -94px" />
                            <asp:ImageButton ID="btnThumbsDown" runat="server" ImageUrl="~/Images/thumbsDownButton.jpg" Height="48px" Width="48px" OnClick="btnThumbsDown_Click" ToolTip="Reject" Enabled="false" />
                            <asp:ImageButton ID="btnThumbsUp" runat="server" ImageUrl="~/Images/thumbsUpButton.jpg" Height="48px" Width="48px" OnClick="btnThumbsUp_Click" ToolTip="Accept" Enabled="false" />
                            <br />
                            <asp:ImageButton ID="btnFastBackwards" runat="server" ImageUrl="~/Images/fbButton.png" OnClick="btnFastBackwards_Click" Enabled="true" />
                            <asp:ImageButton ID="btnPausePlay" runat="server" ImageUrl="~/Images/pauseButton.png" OnClick="btnPausePlay_Click" Enabled="true" />
                            <asp:ImageButton ID="btnFastForward" runat="server" ImageUrl="~/Images/ffButton.png" OnClick="btnFastForward_Click" Enabled="true" Style="margin-bottom: auto" />
                            <br />
                            <asp:Label ID="LabelSpeed" runat="server" Font-Size="Medium" Text="&nbsp;Speed: x1.0"></asp:Label>
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
                                        &nbsp;We stopped for a moment so you can rate your advisor.
                                        <br />
                                        &nbsp;Your rating should be based on how good you think your advisor is.
                                        <br />
                                        <br />

                                        <asp:Label runat="server" Font-Italic="true" Font-Bold="true">
                                        &nbsp;The rating you give will not affect the game or the bonus you get.
                                        </asp:Label>
                                        <br />
                                        <br />
                                        &nbsp;Rate the advisor from 1 to 10, 10 being the best:
                                        <br />
                                        <br />
                                        <asp:RadioButtonList ID="RatingRbL" runat="server">
                                            <asp:ListItem>1 - The worst advisor ever!</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10 - I'm loving it!</asp:ListItem>
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
                    <asp:TableRow Height="35px">
                        <asp:TableCell ID="AvgRankCell" Font-Bold="true" ForeColor="Purple" HorizontalAlign="Left" Text="&nbsp;Average Rank:"></asp:TableCell>
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
                            Thanks for playing, You did great!
                            <br />
                            <br />
                            The average rank of the people you hired was:
                            <br />
                            <br />
                            <asp:Label ID="AverageRank" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Medium"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="Bonus" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="Medium"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Next" OnClick="btnNextToTraining_Click" />

        </asp:View>
    </asp:MultiView>
</asp:Content>
