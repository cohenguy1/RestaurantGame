<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstructionsPage.aspx.cs" Inherits="RestaurantGame.InstructionsPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <img id="InstructionImage" src="Images/Instructions0.png" width="700" height="400" />

    <br />
    <br />
    <div id="myProgress">
        <div id="myBar">
            <div id="label"></div>
        </div>
    </div>

    <br />

    <button id="prevBtn" onclick="prev()" type='button' disabled="disabled">Prev</button>
    <button id="nextBtn" onclick="next()" type='button'>Next</button>
    <asp:Button ID="continueToQuiz" Text="Continue to Quiz" style="display:none" runat="server" OnClick="btnNextInstruction_Click"/>
    <script type="text/javascript">
        var currentInstruction = 0;
        var totalInstructions = 19;

        function updateInstruction() {
            document.getElementById("InstructionImage").src = "Images/Instructions" + currentInstruction.toString() + ".png";
        }

        function getCurrentProgress() {
            return Math.round(currentInstruction * 100 / totalInstructions);
        }

        function updateProgressBar() {
            var progress = getCurrentProgress();
            var elem = document.getElementById("myBar");
            elem.style.width = progress + '%';
            document.getElementById("label").innerHTML = progress * 1 + '%';
        }

        function next() {
            if (currentInstruction <= totalInstructions) {
                currentInstruction++;

                updateInstruction();

                var prevBtn = document.getElementById("prevBtn");
                prevBtn.disabled = false;
                
                if (currentInstruction == totalInstructions) {
                    var nextBtn = document.getElementById("nextBtn");
                    nextBtn.disabled = true;
                    nextBtn.hidden = true;

                    var quizBtn = document.getElementById('<%= continueToQuiz.ClientID %>');
                    if (quizBtn) {
                        quizBtn.style.display = 'inherit';
                    }
                }

                updateProgressBar();
                return false;
            }
        }

        function prev() {
            if (currentInstruction > 0) {
                currentInstruction--;

                updateInstruction();

                var nextBtn = document.getElementById("nextBtn");
                nextBtn.disabled = false;
                nextBtn.hidden = false;

                var quizBtn = document.getElementById('<%= continueToQuiz.ClientID %>');
                if (quizBtn) {
                    quizBtn.style.display = 'none';
                }

                if (currentInstruction == 0) {
                    var prevBtn = document.getElementById("prevBtn");
                    prevBtn.disabled = true;
                }

                updateProgressBar();
                return false;
            }
        }
    </script>
</asp:Content>
