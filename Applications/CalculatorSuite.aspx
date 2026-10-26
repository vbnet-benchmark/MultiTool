<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CalculatorSuite.aspx.vb" Inherits="MultiTool.CalculatorSuite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container my-4">

        <!-- Calculator Selector Dropdown (searchable) -->
        <div class="mb-4">
            <label class="form-label fw-bold">Select Calculator:</label>
            <asp:DropDownList ID="ddlCalculators" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCalculators_SelectedIndexChanged">
            </asp:DropDownList>
        </div>

        <!-- Title / Description for Print -->
        <div class="mb-3 d-flex flex-column flex-md-row align-items-start justify-content-between gap-2">
            <div class="flex-grow-1">
                <asp:TextBox ID="txtCalcTitle" runat="server" CssClass="form-control mb-2" Placeholder="Enter title for your calculation..." />
                <asp:TextBox ID="txtCalcDesc" runat="server" TextMode="MultiLine" CssClass="form-control" Placeholder="Optional description..." Rows="2" />
            </div>
            <div class="d-flex align-items-start gap-2">
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success mt-md-2" UseSubmitBehavior="false" Text="Print / Save PDF" OnClientClick="return printCalculator();" />
            </div>
        </div>

        <!-- Dynamic Calculator Display -->
<asp:MultiView ID="mvCalculators" runat="server" ActiveViewIndex="0">

    <!-- Advanced Algebra Solver -->
    <asp:View ID="viewAlgebraAdvanced" runat="server">
        <asp:Panel ID="pnlAlgebra" runat="server">

            <div class="card shadow-sm mb-4 p-4">
                <h4>Algebra Solver</h4>
                <p>Evaluate expressions or solve for x (e.g., 2*x + 5 = 15)</p>

                <div class="mb-3">
                    <label>Enter Expression or Equation:</label>
                    <asp:TextBox ID="txtAlgExpression" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3 d-flex gap-2">
                    <asp:Button ID="btnAlgEval" runat="server" Text="Evaluate / Solve" CssClass="btn btn-primary btn-sm" OnClick="btnAlgEval_Click" />
                    <asp:Button ID="btnAlgClear" runat="server" Text="Clear History" CssClass="btn btn-secondary btn-sm" OnClick="btnAlgClear_Click" />
                </div>

                <div class="mb-3 mt-2">
                    <asp:Label ID="lblAlgResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <div class="mb-3">
                    <label>History:</label>
                    <div id="divAlgHistory" runat="server" class="history-box"></div>
                    <%--<asp:TextBox ID="txtAlgHistory" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" ReadOnly="True"></asp:TextBox>--%>
                </div>
            </div>

            </asp:Panel>
    </asp:View>

    <!-- 2. Advanced BMI / Body Fat Calculator (Metric & Imperial) -->
    <asp:View ID="viewBMI" runat="server">
        <asp:Panel ID="pnlBMI" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>BMI / Body Fat Calculator</h4>
                <p>Enter your weight, height, age, and gender to calculate BMI, estimated body fat, and healthy weight range.</p>

                <div class="row mb-3">
                    <div class="col-md-4">
                        <label>Weight:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtBMIWeight" runat="server" CssClass="form-control" />
                            <asp:DropDownList ID="ddlBMIWeightUnit" runat="server" CssClass="form-select">
                                <asp:ListItem Text="kg" Value="kg" />
                                <asp:ListItem Text="lb" Value="lb" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <label>Height:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtBMIHeight" runat="server" CssClass="form-control" />
                            <asp:DropDownList ID="ddlBMIHeightUnit" runat="server" CssClass="form-select">
                                <asp:ListItem Text="cm" Value="cm" />
                                <asp:ListItem Text="in" Value="in" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <label>Age (years):</label>
                        <asp:TextBox ID="txtBMIAge" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <div class="mb-3">
                    <label>Gender:</label>
                    <asp:DropDownList ID="ddlBMIGender" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Male" Value="M" />
                        <asp:ListItem Text="Female" Value="F" />
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <asp:Button ID="btnBMICalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnBMICalc_Click" />
                </div>

                <div class="mb-3 mt-2">
                    <asp:Label ID="lblBMIResult" runat="server" CssClass="fw-bold" />
                </div>

                <div class="mb-3">
                    <label>History:</label>
                    <div id="divBMIHistory" runat="server" class="history-box"></div>

                    <%--<asp:TextBox ID="txtBMIHistory" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" ReadOnly="True" Wrap="False" />--%>
                </div>
            </div>
        </asp:Panel>
    </asp:View>
    
    <!-- 3. BMR / TDEE Calculator -->
    <asp:View ID="viewBMR" runat="server">
        <asp:Panel ID="pnlBMR" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>BMR / TDEE Calculator</h4>

                <!-- Weight, Height, Age row -->
                <div class="row mb-3">
                    <!-- Weight -->
                    <div class="col-md-4">
                        <label>Weight:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtBMRWeight" runat="server" CssClass="form-control" />
                            <asp:DropDownList ID="ddlBMRWeightUnit" runat="server" CssClass="form-select">
                                <asp:ListItem Value="kg" Selected="True">kg</asp:ListItem>
                                <asp:ListItem Value="lb">lb</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Height -->
                    <div class="col-md-4">
                        <label>Height:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtBMRHeight" runat="server" CssClass="form-control" />
                            <asp:DropDownList ID="ddlBMRHeightUnit" runat="server" CssClass="form-select">
                                <asp:ListItem Value="cm" Selected="True">cm</asp:ListItem>
                                <asp:ListItem Value="in">in</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Age -->
                    <div class="col-md-4">
                        <label>Age:</label>
                        <asp:TextBox ID="txtBMRAge" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <!-- Gender and Activity row -->
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label>Gender:</label>
                        <asp:DropDownList ID="ddlBMRGender" runat="server" CssClass="form-select">
                            <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label>Activity Level (for TDEE):</label>
                        <asp:DropDownList ID="ddlBMRActivity" runat="server" CssClass="form-select">
                            <asp:ListItem Value="1.2" Selected="True">Sedentary (little/no exercise)</asp:ListItem>
                            <asp:ListItem Value="1.375">Lightly active (light exercise 1-3 days/wk)</asp:ListItem>
                            <asp:ListItem Value="1.55">Moderately active (moderate exercise 3-5 days/wk)</asp:ListItem>
                            <asp:ListItem Value="1.725">Very active (hard exercise 6-7 days/wk)</asp:ListItem>
                            <asp:ListItem Value="1.9">Extra active (very hard exercise or physical job)</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Button -->
                <div class="mb-3">
                    <asp:Button ID="btnBMRCalc" runat="server" Text="Calculate BMR / TDEE" CssClass="btn btn-primary" OnClick="btnBMRCalc_Click" />
                </div>

                <!-- Result -->
                <div class="mb-3 mt-2">
                    <asp:Label ID="lblBMRResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <!-- History -->
                <div class="mb-3">
                    <label>History:</label>
                    <div id="divBMRHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:View>

    <!-- 4. Break-even Calculator -->
    <asp:View ID="viewBreakEven" runat="server">
        <asp:Panel ID="pnlBreakEven" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>Break-even Calculator</h4>

                <!-- Row 1: Fixed Costs, Price per Unit, Variable Cost per Unit -->
                <div class="row mb-3">
                    <!-- Fixed Costs -->
                    <div class="col-md-4">
                        <label>Fixed Costs ($):</label>
                        <asp:TextBox ID="txtBreakEvenFixed" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Price per Unit -->
                    <div class="col-md-4">
                        <label>Price per Unit ($):</label>
                        <asp:TextBox ID="txtBreakEvenPrice" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Variable Cost per Unit -->
                    <div class="col-md-4">
                        <label>Variable Cost per Unit ($):</label>
                        <asp:TextBox ID="txtBreakEvenVariable" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <!-- Row 2: Target Profit -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label>Optional Target Profit ($):</label>
                        <asp:TextBox ID="txtBreakEvenTargetProfit" runat="server" CssClass="form-control" Placeholder="Optional" />
                    </div>
                </div>

                <!-- Calculate Button -->
                <div class="mb-3">
                    <asp:Button ID="btnBreakEvenCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnBreakEvenCalc_Click" />
                </div>

                <!-- Result -->
                <div class="mb-3 mt-2">
                    <asp:Label ID="lblBreakEvenResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <!-- History -->
                <div class="mb-3">
                    <label>Calculation History:</label>
                    <div id="divBreakEvenHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:View>

    <!-- 5. Caloric Intake Calculator -->
    <asp:View ID="viewCalorieIntake" runat="server">
        <asp:Panel ID="pnlCalorie" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>Calorie Intake Calculator</h4>

                <!-- Row 1: Weight + Height + Age + Gender -->
                <div class="row mb-3">
                    <div class="col-md-3">
                        <label>Weight:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtCalorieWeight" runat="server" CssClass="form-control" />
                            <asp:DropDownList ID="ddlCalorieWeightUnit" runat="server" CssClass="form-select">
                                <asp:ListItem Value="kg" Selected="True">kg</asp:ListItem>
                                <asp:ListItem Value="lb">lb</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label>Height:</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtCalorieHeight" runat="server" CssClass="form-control" />
                            <asp:DropDownList ID="ddlCalorieHeightUnit" runat="server" CssClass="form-select">
                                <asp:ListItem Value="cm" Selected="True">cm</asp:ListItem>
                                <asp:ListItem Value="in">in</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label>Age:</label>
                        <asp:TextBox ID="txtCalorieAge" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-3">
                        <label>Gender:</label>
                        <asp:DropDownList ID="ddlCalorieGender" runat="server" CssClass="form-select">
                            <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Row 2: Activity Level + Fitness Goal -->
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label>Activity Level:</label>
                        <asp:DropDownList ID="ddlCalorieActivity" runat="server" CssClass="form-select">
                            <asp:ListItem Value="1.2" Selected="True">Sedentary (Little or no exercise)</asp:ListItem>
                            <asp:ListItem Value="1.375">Lightly Active (Light exercise/sports 1–3 days/week)</asp:ListItem>
                            <asp:ListItem Value="1.55">Moderately Active (Moderate exercise 3–5 days/week)</asp:ListItem>
                            <asp:ListItem Value="1.725">Very Active (Hard exercise 6–7 days/week)</asp:ListItem>
                            <asp:ListItem Value="1.9">Extremely Active (Very hard daily exercise or physical job)</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-6">
                        <label>Fitness Goal:</label>
                        <asp:DropDownList ID="ddlFitnessGoal" runat="server" CssClass="form-select">
                            <asp:ListItem Value="lose_standard" Selected="True">Lose Weight</asp:ListItem>
                            <asp:ListItem Value="lose_aggressive">Lose Weight (Aggressive)</asp:ListItem>
                            <asp:ListItem Value="maintain">Maintain</asp:ListItem>
                            <asp:ListItem Value="gain_lean">Gain Lean</asp:ListItem>
                            <asp:ListItem Value="gain_bulk">Gain Bulk</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Button -->
                <div class="mb-3">
                    <asp:Button ID="btnCalorieCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnCalorieCalc_Click" />
                </div>

                <!-- Result -->
                <div class="mb-3 mt-2">
                    <asp:Label ID="lblCalorieResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <!-- Macro Bar Chart -->
                <div class="mb-3">
                    <label>Macro Breakdown:</label>
                    <div class="progress" style="height:25px;">
                        <div id="divProteinBar" runat="server" class="progress-bar bg-success" role="progressbar" style="width:0%">Protein</div>
                        <div id="divCarbBar" runat="server" class="progress-bar bg-info" role="progressbar" style="width:0%">Carbs</div>
                        <div id="divFatBar" runat="server" class="progress-bar bg-warning" role="progressbar" style="width:0%">Fat</div>
                    </div>
                </div>

                <!-- History -->
                <div class="mb-3">
                    <label>Calculation History:</label>
                    <div id="divCalorieHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:View>

    <!-- 6. Car Loan Calculator -->
    <asp:View ID="viewCarLoan" runat="server">
        <asp:Panel ID="pnlCarLoan" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>Car Loan Calculator</h4>

                <!-- Row 1: Loan Amount + Interest Rate + Term (Months) -->
                <div class="row mb-3">
                    <div class="col-md-3">
                        <label>Loan Amount:</label>
                        <asp:TextBox ID="txtCarLoanAmount" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-3">
                        <label>Interest Rate (% per year):</label>
                        <asp:TextBox ID="txtCarLoanRate" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-3">
                        <label>Term (Months):</label>
                        <asp:TextBox ID="txtCarLoanMonths" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-3">
                        <label>Extra Monthly Payment (Optional):</label>
                        <asp:TextBox ID="txtCarLoanExtra" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <!-- Calculate Button -->
                <div class="mb-3">
                    <asp:Button ID="btnCarLoanCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnCarLoanCalc_Click" />
                </div>

                <!-- Result -->
                <div class="mb-3 mt-2">
                    <asp:Label ID="lblCarLoanResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <!-- Amortization Table -->
                <div class="mb-3">
                    <label>Amortization Breakdown:</label>
                    <div id="divCarLoanAmortization" runat="server" class="history-box" style="height:auto; max-height:200px; overflow:auto; white-space:pre-wrap;"></div>
                </div>

                <!-- Calculation History -->
                <div class="mb-3">
                    <label>Calculation History:</label>
                    <div id="divCarLoanHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:View>

    <!-- 7. Chemistry Molarity Calculator -->
    <asp:View ID="viewChemistryMolarity" runat="server">
        <asp:Panel ID="pnlMolarity" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>Chemistry Molarity Calculator</h4>

                <!-- Row 1: Mass / Moles + Molar Mass -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label>Amount of solute:</label>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label>Unit:</label>
                        <asp:DropDownList ID="ddlAmountUnit" runat="server" CssClass="form-select">
                            <asp:ListItem Value="moles" Selected="True">Moles</asp:ListItem>
                            <asp:ListItem Value="grams">Grams</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label>Molar Mass (g/mol, required if grams):</label>
                        <asp:TextBox ID="txtMolarMass" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <!-- Row 2: Volume -->
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label>Volume of solution:</label>
                        <asp:TextBox ID="txtVolume" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label>Volume unit:</label>
                        <asp:DropDownList ID="ddlVolumeUnit" runat="server" CssClass="form-select">
                            <asp:ListItem Value="L" Selected="True">Liters (L)</asp:ListItem>
                            <asp:ListItem Value="mL">Milliliters (mL)</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Calculate Button -->
                <div class="mb-3">
                    <asp:Button ID="btnMolarityCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnMolarityCalc_Click" />
                </div>

                <!-- Result -->
                <div class="mb-3 mt-2">
                    <asp:Label ID="lblMolarityResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <!-- Calculation History -->
                <div class="mb-3">
                    <label>Calculation History:</label>
                    <div id="divMolarityHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:View>

    <!-- 8. Compound Interest Calculator -->
    <asp:View ID="viewCompoundInterest" runat="server">
        <asp:Panel ID="pnlCompoundInterest" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>Compound Interest Calculator</h4>

                <!-- Row 1: Principal + Rate + Years -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label>Principal:</label>
                        <asp:TextBox ID="txtCIPrincipal" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label>Annual Interest Rate (%):</label>
                        <asp:TextBox ID="txtCIRate" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label>Years:</label>
                        <asp:TextBox ID="txtCIYears" runat="server" CssClass="form-control" />
                    </div>
                </div>

                <!-- Row 2: Compounds + Extra Contribution + Frequency -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label>Compounds per Year:</label>
                        <asp:TextBox ID="txtCICompounds" runat="server" CssClass="form-control" Text="12" />
                    </div>
                    <div class="col-md-4">
                        <label>Extra Contribution per Period:</label>
                        <asp:TextBox ID="txtCIExtra" runat="server" CssClass="form-control" Text="0" />
                    </div>
                    <div class="col-md-4">
                        <label>Contribution Frequency:</label>
                        <asp:DropDownList ID="ddlCIExtraFreq" runat="server" CssClass="form-select">
                            <asp:ListItem Value="monthly" Selected="True">Monthly</asp:ListItem>
                            <asp:ListItem Value="quarterly">Quarterly</asp:ListItem>
                            <asp:ListItem Value="annually">Annually</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Button -->
                <div class="mb-3">
                    <asp:Button ID="btnCICalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnCICalc_Click" />
                </div>

                <!-- Result -->
                <div class="mb-3 mt-2">
                    <asp:Label ID="lblCIResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <!-- History -->
                <div class="mb-3">
                    <label>Calculation History:</label>
                    <div id="divCIHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:View>

    <!-- 9. Cryptocurrency ROI Calculator -->
    <asp:View ID="viewCryptoROI" runat="server">
        <asp:Panel ID="pnlCrypto" runat="server">
            <div class="card shadow-sm mb-4 p-4">
                <h4>Advanced Cryptocurrency ROI Calculator</h4>

                <!-- Row 1: Investment Inputs -->
                <div class="row mb-3">
                    <div class="col-md-3">
                        <label>Initial Investment ($):</label>
                        <asp:TextBox ID="txtCryptoInvestment" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3">
                        <label>Final Value ($):</label>
                        <asp:TextBox ID="txtCryptoFinal" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3">
                        <label>Investment Period (Years):</label>
                        <asp:TextBox ID="txtCryptoYears" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-3">
                        <label>Extra Contribution per Period ($):</label>
                        <asp:TextBox ID="txtCryptoContribution" runat="server" CssClass="form-control" Text="0" />
                    </div>
                </div>

                <!-- Row 2: Contribution Frequency -->
                <div class="row mb-3">
                    <div class="col-md-3">
                        <label>Contribution Frequency:</label>
                        <asp:DropDownList ID="ddlCryptoFreq" runat="server" CssClass="form-select">
                            <asp:ListItem Value="1" Selected="True">Monthly</asp:ListItem>
                            <asp:ListItem Value="3">Quarterly</asp:ListItem>
                            <asp:ListItem Value="12">Yearly</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Button -->
                <div class="mb-3">
                    <asp:Button ID="btnCryptoCalc" runat="server" Text="Calculate ROI" CssClass="btn btn-primary" OnClick="btnCryptoCalc_Click" />
                </div>

                <!-- Result -->
                <div class="mb-3 mt-2">
                    <asp:Label ID="lblCryptoResult" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <!-- History -->
                <div class="mb-3">
                    <label>Calculation History:</label>
                    <div id="divCryptoHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
                </div>
            </div>
        </asp:Panel>
    </asp:View>

    <!-- 10. Currency Converter -->
    <asp:View ID="viewCurrencyConverter" runat="server">
    <asp:Panel ID="pnlCurrency" runat="server">
        <div class="card shadow-sm mb-4 p-4">
            <h4>Advanced Currency Converter</h4>

            <!-- Row 1: Amount + Rate -->
            <div class="row mb-3">
                <div class="col-md-6">
                    <label>Amount:</label>
                    <asp:TextBox ID="txtCurrencyAmount" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label>Exchange Rate:</label>
                    <asp:TextBox ID="txtCurrencyRate" runat="server" CssClass="form-control" />
                </div>
            </div>

            <!-- Row 2: Optional Fee / Markup -->
            <div class="row mb-3">
                <div class="col-md-12">
                    <label>Fee / Markup (%):</label>
                    <asp:TextBox ID="txtCurrencyFee" runat="server" CssClass="form-control" Text="0" />
                </div>
            </div>

            <!-- Row 3: Optional Extra Target Currencies -->
            <div class="row mb-3">
                <div class="col-md-12">
                    <label>Additional Target Rates (comma separated, optional):</label>
                    <asp:TextBox ID="txtCurrencyExtraRates" runat="server" CssClass="form-control" Placeholder="e.g., 1.1,0.9,0.75" />
                </div>
            </div>

            <!-- Button -->
            <div class="mb-3">
                <asp:Button ID="btnCurrencyCalc" runat="server" Text="Convert" CssClass="btn btn-primary" OnClick="btnCurrencyCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3 mt-2">
                <asp:Label ID="lblCurrencyResult" runat="server" CssClass="fw-bold"></asp:Label>
            </div>

            <!-- History -->
            <div class="mb-3">
                <label>Conversion History:</label>
                <div id="divCurrencyHistory" runat="server" class="history-box" style="height:auto; min-height:100px; overflow:auto; white-space:pre-wrap;"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>

    <!-- 11. Goal Achievement Planner -->
    <asp:View ID="viewGoalPlanner" runat="server">
    <asp:Panel ID="pnlGoalPlanner" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Goal Achievement Planner</h4>

            <!-- Goal Description -->
            <div class="mb-3">
                <label>Goal Description:</label>
                <asp:TextBox ID="txtGoalDesc" runat="server" CssClass="form-control" placeholder="e.g., Save $500" />
            </div>

                <!-- Start Date (Date Picker) -->
            <div class="mb-3">
                <label>Start Date:</label>
                <asp:TextBox 
                    ID="txtStartDate" 
                    runat="server" 
                    CssClass="form-control" 
                    Text='<%# DateTime.Now.ToString("yyyy-MM-dd") %>'
                    TextMode="Date" />
            </div>

            <!-- Target Days -->
            <div class="mb-3">
                <label>Target Date (days from start):</label>
                <asp:TextBox ID="txtGoalDays" runat="server" CssClass="form-control" placeholder="e.g., 30" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnGoalCalc" runat="server" Text="Calculate Plan" CssClass="btn btn-primary" OnClick="btnGoalCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divGoalResult" runat="server" class="history-box"></div>
            </div>

            <!-- Progress Bar -->
            <div class="mb-3">
                <label>Progress:</label>
                <div class="progress">
                    <div id="divProgressBar" runat="server" class="progress-bar" role="progressbar" style="width:0%" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">0%</div>
                </div>
            </div>

            <!-- Tips -->
            <asp:Panel ID="pnlGoalDetails" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Next Steps:</strong>
                    <ul>
                        <li>Break your goal into small daily or weekly actions.</li>
                        <li>Track your progress automatically over time.</li>
                        <li>Celebrate small wins to stay motivated.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>

    <!-- 12. Heart Rate / Target Zone Calculator -->
    <asp:View ID="viewHeartRate" runat="server">
    <asp:Panel ID="pnlHeartRate" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Heart Rate / Target Zone Calculator</h4>

            <!-- Age Input -->
            <div class="mb-3">
                <label>Age:</label>
                <asp:TextBox ID="txtHRAge" runat="server" CssClass="form-control" placeholder="Enter your age" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnHRCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnHRCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divHRResult" runat="server" class="history-box"></div>
            </div>

            <!-- Target Zones Bar -->
            <div class="mb-3">
                <label>Target Heart Rate Zones:</label>
                <div class="progress position-relative" style="height: 40px;">
                    <div id="divHRZoneLight" runat="server" class="progress-bar bg-success" role="progressbar" style="width:0%">Light</div>
                    <div id="divHRZoneModerate" runat="server" class="progress-bar bg-warning" role="progressbar" style="width:0%">Moderate</div>
                    <div id="divHRZoneIntense" runat="server" class="progress-bar bg-danger" role="progressbar" style="width:0%">Intense</div>
                    <div id="divCurrentHR" runat="server" class="position-absolute top-0 start-0" style="width:2px; height:40px; background-color:black;"></div>
                </div>
            </div>


            <!-- Tips Panel -->
            <asp:Panel ID="pnlHRTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Maximum heart rate = 220 - age.</li>
                        <li>Light zone: 50–60% of max HR.</li>
                        <li>Moderate zone: 60–75% of max HR.</li>
                        <li>Intense zone: 75–90% of max HR.</li>
                        <li>Slider shows your current HR and highlights your zone live.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>

    
</asp:View>





    <!-- 13. Inventory Turnover Calculator -->
    <asp:View ID="viewInventoryTurnover" runat="server">
    <asp:Panel ID="pnlInventoryTurnover" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Inventory Turnover Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Cost of Goods Sold (COGS):</label>
                <asp:TextBox ID="txtCOGS" runat="server" CssClass="form-control" placeholder="Enter COGS amount" />
            </div>

            <div class="mb-3">
                <label>Average Inventory:</label>
                <asp:TextBox ID="txtAvgInventory" runat="server" CssClass="form-control" placeholder="Enter average inventory" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnInventoryCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnInventoryCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divInventoryResult" runat="server" class="history-box"></div>
            </div>

            <!-- Efficiency Gauge (Visual) -->
            <div class="mb-3">
                <label>Turnover Efficiency:</label>
                <div class="progress" style="height: 30px;">
                    <div id="divInventoryGauge" runat="server" class="progress-bar bg-success" role="progressbar" style="width:0%">0%</div>
                </div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlInventoryTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Inventory Turnover = Cost of Goods Sold ÷ Average Inventory.</li>
                        <li>Higher turnover indicates efficient inventory management.</li>
                        <li>Low turnover may indicate overstocking or slow sales.</li>
                        <li>Use this metric to optimize purchasing and stock levels.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 14. Internal Rate of Return (IRR) Calculator (basic) -->
    <asp:View ID="viewIRR" runat="server">
    <asp:Panel ID="pnlIRR" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">IRR Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Initial Investment:</label>
                <asp:TextBox ID="txtIRRInvestment" runat="server" CssClass="form-control" placeholder="Enter initial investment" />
            </div>

            <div class="mb-3">
                <label>Final Value:</label>
                <asp:TextBox ID="txtIRRFinal" runat="server" CssClass="form-control" placeholder="Enter final value" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnIRRCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnIRRCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divIRRResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlIRRTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>IRR estimates the annualized return of an investment.</li>
                        <li>Simple IRR = (Final Value / Initial Investment)^(1/n) - 1, assuming n periods = 1 year here.</li>
                        <li>Use IRR to compare profitability of different investments.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 15. Loan Amortization Calculator -->
    <asp:View ID="viewLoanAmort" runat="server">
    <asp:Panel ID="pnlLoanAmort" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Loan Amortization Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Loan Amount:</label>
                <asp:TextBox ID="txtLoanAmt" runat="server" CssClass="form-control" placeholder="Enter loan amount" />
            </div>

            <div class="mb-3">
                <label>Interest Rate (% per year):</label>
                <asp:TextBox ID="txtLoanInterest" runat="server" CssClass="form-control" placeholder="Enter interest rate" />
            </div>

            <div class="mb-3">
                <label>Term (Years):</label>
                <asp:TextBox ID="txtLoanTerm" runat="server" CssClass="form-control" placeholder="Enter term in years" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnLoanAmortCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnLoanAmortCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divLoanAmortResult" runat="server" class="history-box"></div>
            </div>

            <!-- Amortization Table (first few months) -->
            <div class="mb-3">
                <label>Amortization Schedule (First 12 months):</label>
                <div id="divLoanAmortTable" runat="server" class="history-box table-responsive"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlLoanAmortTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Monthly Payment = P * r / (1 - (1 + r)^-n), where r = monthly interest rate, n = total months.</li>
                        <li>Total interest = total payments - principal.</li>
                        <li>This calculator shows the monthly payment and a preview of the first 12 months of principal vs. interest.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 16. Macro Nutrient Calculator -->
    <asp:View ID="viewMacro" runat="server">
    <asp:Panel ID="pnlMacro" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Macro Nutrient Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Calories:</label>
                <asp:TextBox ID="txtMacroCalories" runat="server" CssClass="form-control" placeholder="Enter total calories" />
            </div>

            <div class="mb-3">
                <label>Protein %:</label>
                <asp:TextBox ID="txtMacroProtein" runat="server" CssClass="form-control" placeholder="Protein percentage" />
            </div>

            <div class="mb-3">
                <label>Carbs %:</label>
                <asp:TextBox ID="txtMacroCarbs" runat="server" CssClass="form-control" placeholder="Carbs percentage" />
            </div>

            <div class="mb-3">
                <label>Fat %:</label>
                <asp:TextBox ID="txtMacroFat" runat="server" CssClass="form-control" placeholder="Fat percentage" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnMacroCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnMacroCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divMacroResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlMacroTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>1 gram of protein = 4 calories</li>
                        <li>1 gram of carbohydrate = 4 calories</li>
                        <li>1 gram of fat = 9 calories</li>
                        <li>Percentages should sum to 100%</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 17. Markup / Margin Calculator -->
    <asp:View ID="viewMarkupMargin" runat="server">
    <asp:Panel ID="pnlMarkupMargin" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Markup / Margin Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Cost:</label>
                <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" placeholder="Enter cost amount" />
            </div>

            <div class="mb-3">
                <label>Price:</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter selling price" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnMarkupCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnMarkupCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divMarkupResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlMarkupTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Markup (%) = ((Price - Cost) / Cost) * 100</li>
                        <li>Margin (%) = ((Price - Cost) / Price) * 100</li>
                        <li>Use markup to determine selling price from cost.</li>
                        <li>Use margin to understand profitability relative to price.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 18. Mortgage Calculator -->
    <asp:View ID="viewMortgageCalc" runat="server">
    <asp:Panel ID="pnlMortgage" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Mortgage Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Loan Amount:</label>
                <asp:TextBox ID="txtMortgageAmt" runat="server" CssClass="form-control" placeholder="Enter loan amount" />
            </div>

            <div class="mb-3">
                <label>Interest Rate (% per year):</label>
                <asp:TextBox ID="txtMortgageInterest" runat="server" CssClass="form-control" placeholder="Enter interest rate" />
            </div>

            <div class="mb-3">
                <label>Term (Years):</label>
                <asp:TextBox ID="txtMortgageTerm" runat="server" CssClass="form-control" placeholder="Enter term in years" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnMortgageCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnMortgageCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divMortgageResult" runat="server" class="history-box"></div>
            </div>

            <!-- Amortization Table (first 12 months) -->
            <div class="mb-3">
                <label>Amortization Schedule (First 12 months):</label>
                <div id="divMortgageTable" runat="server" class="history-box table-responsive"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlMortgageTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Monthly Payment = P * r / (1 - (1 + r)^-n), where r = monthly interest rate, n = total months.</li>
                        <li>Total interest = total payments - principal.</li>
                        <li>This calculator shows the monthly payment and a preview of the first 12 months of principal vs. interest.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 19. Net Present Value (NPV) Calculator -->
    <asp:View ID="viewNPV" runat="server">
    <asp:Panel ID="pnlNPV" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Net Present Value (NPV) Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Initial Investment:</label>
                <asp:TextBox ID="txtNPVInvestment" runat="server" CssClass="form-control" placeholder="Enter initial investment" />
            </div>

            <div class="mb-3">
                <label>Future Value:</label>
                <asp:TextBox ID="txtNPVFuture" runat="server" CssClass="form-control" placeholder="Enter future value" />
            </div>

            <div class="mb-3">
                <label>Discount Rate (% per period):</label>
                <asp:TextBox ID="txtNPVRate" runat="server" CssClass="form-control" placeholder="Enter discount rate" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnNPVCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnNPVCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divNPVResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlNPVTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>NPV = Future Value / (1 + Discount Rate) - Initial Investment</li>
                        <li>NPV > 0 means the investment is profitable.</li>
                        <li>NPV < 0 means the investment may not meet desired returns.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 20. Pet Weight / Dosage Calculator -->
    <asp:View ID="viewPetDosage" runat="server">
    <asp:Panel ID="pnlPetDosage" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Pet Weight / Dosage Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Pet Weight (kg):</label>
                <asp:TextBox ID="txtPetWeight" runat="server" CssClass="form-control" placeholder="Enter pet weight in kg" />
            </div>

            <div class="mb-3">
                <label>Medication Dose (mg/kg):</label>
                <asp:TextBox ID="txtPetDose" runat="server" CssClass="form-control" placeholder="Enter dosage per kg" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnPetCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnPetCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divPetResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlPetTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Total Dosage = Pet Weight × Dose per kg</li>
                        <li>Double-check the units of your medication before administering.</li>
                        <li>Consult a veterinarian for any uncertainty.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 21. Physics Calculator (basic formula: F = m * a) -->
<asp:View ID="viewPhysics" runat="server">
    <asp:Panel ID="pnlPhysics" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Physics Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Mass (kg):</label>
                <asp:TextBox ID="txtMass" runat="server" CssClass="form-control" placeholder="Enter mass in kg" />
            </div>

            <div class="mb-3">
                <label>Acceleration (m/s²):</label>
                <asp:TextBox ID="txtAcceleration" runat="server" CssClass="form-control" placeholder="Enter acceleration" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnPhysicsCalc" runat="server" Text="Calculate Force" CssClass="btn btn-primary" OnClick="btnPhysicsCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divPhysicsResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlPhysicsTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Force (N) = Mass (kg) × Acceleration (m/s²)</li>
                        <li>Ensure units are consistent for correct results.</li>
                        <li>Newton (N) is the SI unit of force.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 22. Price / Profit Calculator -->
    <asp:View ID="viewPriceProfit" runat="server">
    <asp:Panel ID="pnlPriceProfit" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Price / Profit Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Cost:</label>
                <asp:TextBox ID="txtPriceCost" runat="server" CssClass="form-control" placeholder="Enter cost" />
            </div>

            <div class="mb-3">
                <label>Sale Price:</label>
                <asp:TextBox ID="txtPriceSale" runat="server" CssClass="form-control" placeholder="Enter sale price" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnPriceProfitCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnPriceProfitCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divPriceProfitResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlPriceProfitTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Profit = Sale Price - Cost</li>
                        <li>Profit Margin (%) = (Profit / Sale Price) × 100</li>
                        <li>Use profit margin to understand percentage of revenue that is profit.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 23. Pregnancy Calorie Calculator -->
    <asp:View ID="viewPregnancyCalorie" runat="server">
    <asp:Panel ID="pnlPregCalorie" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Pregnancy Calorie Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Pre-pregnancy Weight (kg):</label>
                <asp:TextBox ID="txtPregWeight" runat="server" CssClass="form-control" placeholder="Enter pre-pregnancy weight" />
            </div>

            <div class="mb-3">
                <label>Height (cm):</label>
                <asp:TextBox ID="txtPregHeight" runat="server" CssClass="form-control" placeholder="Enter height in cm" />
            </div>

            <div class="mb-3">
                <label>Trimester (1/2/3):</label>
                <asp:TextBox ID="txtPregTrimester" runat="server" CssClass="form-control" placeholder="Enter trimester number" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnPregCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnPregCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Estimated Daily Calories:</label>
                <div id="divPregResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlPregTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Basic BMR formula: 10×weight + 6.25×height - 5×age + 5 (male) or -161 (female). Here we simplify to weight & height only.</li>
                        <li>Add calories based on trimester: +0 kcal for 1st, +340 kcal for 2nd, +450 kcal for 3rd trimester.</li>
                        <li>Consult a healthcare professional for personalized recommendations.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 24. Productivity Calculator -->
    <asp:View ID="viewProductivity" runat="server">
    <asp:Panel ID="pnlProductivity" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Productivity Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Total Tasks:</label>
                <asp:TextBox ID="txtProdTasks" runat="server" CssClass="form-control" placeholder="Enter total tasks" />
            </div>

            <div class="mb-3">
                <label>Completed Tasks:</label>
                <asp:TextBox ID="txtProdCompleted" runat="server" CssClass="form-control" placeholder="Enter completed tasks" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnProdCalc" runat="server" Text="Calculate Productivity (%)" CssClass="btn btn-primary" OnClick="btnProdCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divProdResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlProdTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Productivity (%) = (Completed Tasks / Total Tasks) × 100</li>
                        <li>Ensure Total Tasks is not zero to avoid division errors.</li>
                        <li>Use productivity percentage to track efficiency over time.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 25. Quadratic Equation Solver -->
    <asp:View ID="viewQuadratic" runat="server">
    <asp:Panel ID="pnlQuadratic" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-2">Quadratic Equation Solver</h4>
            <p>Solves ax² + bx + c = 0</p>

            <!-- Inputs -->
            <div class="mb-3">
                <label>a:</label>
                <asp:TextBox ID="txtQuadA" runat="server" CssClass="form-control" placeholder="Enter coefficient a" />
            </div>

            <div class="mb-3">
                <label>b:</label>
                <asp:TextBox ID="txtQuadB" runat="server" CssClass="form-control" placeholder="Enter coefficient b" />
            </div>

            <div class="mb-3">
                <label>c:</label>
                <asp:TextBox ID="txtQuadC" runat="server" CssClass="form-control" placeholder="Enter coefficient c" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnQuadCalc" runat="server" Text="Solve" CssClass="btn btn-primary" OnClick="btnQuadCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divQuadResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlQuadTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Discriminant = b² - 4ac</li>
                        <li>If discriminant > 0 → two real roots</li>
                        <li>If discriminant = 0 → one real root</li>
                        <li>If discriminant < 0 → two complex roots</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 26. Retirement Planner -->
    <asp:View ID="viewRetirement" runat="server">
    <asp:Panel ID="pnlRetirement" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Retirement Planner</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Current Savings:</label>
                <asp:TextBox ID="txtRetSavings" runat="server" CssClass="form-control" placeholder="Enter current savings" />
            </div>

            <div class="mb-3">
                <label>Monthly Contribution:</label>
                <asp:TextBox ID="txtRetContribution" runat="server" CssClass="form-control" placeholder="Enter monthly contribution" />
            </div>

            <div class="mb-3">
                <label>Years to Retirement:</label>
                <asp:TextBox ID="txtRetYears" runat="server" CssClass="form-control" placeholder="Enter years to retirement" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnRetCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnRetCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divRetResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlRetTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Future Value = Current Savings × (1 + monthlyRate)^(months) + Contribution × (((1 + monthlyRate)^(months) - 1)/monthlyRate)</li>
                        <li>Assumes 5% annual return by default.</li>
                        <li>Use consistent units (months/years) for contributions.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 27. ROI Calculator -->
    <asp:View ID="viewROI" runat="server">
    <asp:Panel ID="pnlROI" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">ROI Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Initial Investment:</label>
                <asp:TextBox ID="txtROIInit" runat="server" CssClass="form-control" placeholder="Enter initial investment" />
            </div>

            <div class="mb-3">
                <label>Final Value:</label>
                <asp:TextBox ID="txtROIFinalVal" runat="server" CssClass="form-control" placeholder="Enter final value" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnROICalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnROICalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divROIResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlROITips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>ROI (%) = ((Final Value - Initial Investment) / Initial Investment) × 100</li>
                        <li>Positive ROI indicates profit; negative ROI indicates loss.</li>
                        <li>Use consistent units (dollars, euros, etc.) for both initial and final values.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 28. Savings Goal Calculator -->
    <asp:View ID="viewSavingsGoal" runat="server">
    <asp:Panel ID="pnlSavingsGoal" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Savings Goal Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Goal Amount:</label>
                <asp:TextBox ID="txtGoalAmount" runat="server" CssClass="form-control" placeholder="Enter savings goal" />
            </div>

            <div class="mb-3">
                <label>Monthly Savings:</label>
                <asp:TextBox ID="txtGoalMonthly" runat="server" CssClass="form-control" placeholder="Enter monthly savings" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnGoalAmountCalc" runat="server" Text="Calculate Months Needed" CssClass="btn btn-primary" OnClick="btnGoalAmountCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divGoalAmountResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlGoalTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Months Needed = Goal Amount ÷ Monthly Savings</li>
                        <li>Round up to the next whole month if not exact.</li>
                        <li>Consider interest or investment growth for more accurate long-term planning.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 29. Sleep Needs Calculator -->
    <asp:View ID="viewSleepNeeds" runat="server">
    <asp:Panel ID="pnlSleepNeeds" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Sleep Needs Calculator</h4>

            <!-- Input -->
            <div class="mb-3">
                <label>Age:</label>
                <asp:TextBox ID="txtSleepAge" runat="server" CssClass="form-control" placeholder="Enter your age in years" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnSleepCalc" runat="server" Text="Calculate Hours Needed" CssClass="btn btn-primary" OnClick="btnSleepCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divSleepResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlSleepTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Newborns (0-3 months): 14-17 hours/day</li>
                        <li>Infants (4-11 months): 12-15 hours/day</li>
                        <li>Children (1-13 years): 9-14 hours/day</li>
                        <li>Teenagers (14-17 years): 8-10 hours/day</li>
                        <li>Adults (18-64 years): 7-9 hours/day</li>
                        <li>Older Adults (65+ years): 7-8 hours/day</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 30. Stock Investment Calculator -->
    <asp:View ID="viewStockInvestment" runat="server">
    <asp:Panel ID="pnlStockInvestment" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Stock Investment Calculator</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Initial Investment:</label>
                <asp:TextBox ID="txtStockInit" runat="server" CssClass="form-control" placeholder="Enter initial investment" />
            </div>

            <div class="mb-3">
                <label>Final Value:</label>
                <asp:TextBox ID="txtStockFinal" runat="server" CssClass="form-control" placeholder="Enter final value" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnStockCalc" runat="server" Text="Calculate ROI" CssClass="btn btn-primary" OnClick="btnStockCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divStockResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlStockTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>ROI (%) = ((Final Value - Initial Investment) / Initial Investment) × 100</li>
                        <li>Positive ROI indicates a gain; negative ROI indicates a loss.</li>
                        <li>Consider dividends and fees for a more accurate ROI.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 31. Study / Learning Time Planner -->
    <asp:View ID="viewStudyPlanner" runat="server">
    <asp:Panel ID="pnlStudyPlanner" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Study / Learning Time Planner</h4>

            <!-- Inputs -->
            <div class="mb-3">
                <label>Total Hours Available:</label>
                <asp:TextBox ID="txtStudyTotalHours" runat="server" CssClass="form-control" placeholder="Enter total study hours" />
            </div>

            <div class="mb-3">
                <label>Subjects to Study (comma separated):</label>
                <asp:TextBox ID="txtStudySubjects" runat="server" CssClass="form-control" placeholder="e.g., Math, Science, English" />
            </div>

            <!-- Calculate Button -->
            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnStudyCalc" runat="server" Text="Calculate Time per Subject" CssClass="btn btn-primary" OnClick="btnStudyCalc_Click" />
            </div>

            <!-- Result -->
            <div class="mb-3">
                <label>Result:</label>
                <div id="divStudyResult" runat="server" class="history-box"></div>
            </div>

            <!-- Tips Panel -->
            <asp:Panel ID="pnlStudyTips" runat="server" Visible="False" CssClass="mt-3">
                <div class="alert alert-info shadow-sm">
                    <strong>Tips:</strong>
                    <ul>
                        <li>Divide total available hours equally among subjects for balanced study.</li>
                        <li>Adjust allocation based on difficulty or priority of subjects.</li>
                        <li>Take breaks to improve retention and focus.</li>
                    </ul>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 32. TDEE Calculator -->
    <asp:View ID="viewTDEE" runat="server">
    <asp:Panel ID="pnlTDEE" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">TDEE Calculator</h4>

            <div class="mb-3">
                <label>Weight (kg):</label>
                <asp:TextBox ID="txtTDEEWeight" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Height (cm):</label>
                <asp:TextBox ID="txtTDEEHeight" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Age:</label>
                <asp:TextBox ID="txtTDEEAge" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Gender (M/F):</label>
                <asp:TextBox ID="txtTDEEGender" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Activity Level (1-5):</label>
                <asp:TextBox ID="txtTDEEActivity" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnTDEECalc" runat="server" Text="Calculate TDEE" CssClass="btn btn-primary" OnClick="btnTDEECalc_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divTDEResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 33. Travel / Fuel Cost Calculator -->
    <asp:View ID="viewTravelFuel" runat="server">
    <asp:Panel ID="pnlTravelFuel" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Travel / Fuel Cost Calculator</h4>

            <div class="mb-3">
                <label>Distance (km):</label>
                <asp:TextBox ID="txtTravelDistance" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Fuel Efficiency (km/l):</label>
                <asp:TextBox ID="txtTravelEfficiency" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Fuel Price per Liter:</label>
                <asp:TextBox ID="txtTravelFuelPrice" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnTravelCalc" runat="server" Text="Calculate Cost" CssClass="btn btn-primary" OnClick="btnTravelCalc_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divTravelResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 34. Unit Conversion Calculator -->
    <asp:View ID="viewUnitConversion" runat="server">
    <asp:Panel ID="pnlUnitConversion" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Unit Conversion Calculator</h4>

            <div class="mb-3">
                <label>Value:</label>
                <asp:TextBox ID="txtUnitValue" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>From Unit:</label>
                <asp:TextBox ID="txtUnitFrom" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>To Unit:</label>
                <asp:TextBox ID="txtUnitTo" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnUnitConvert" runat="server" Text="Convert" CssClass="btn btn-primary" OnClick="btnUnitConvert_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divUnitResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 35. VAT / Tax Calculator -->
    <asp:View ID="viewVAT" runat="server">
    <asp:Panel ID="pnlVAT" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">VAT / Tax Calculator</h4>

            <div class="mb-3">
                <label>Price:</label>
                <asp:TextBox ID="txtVATPrice" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Tax Rate (%):</label>
                <asp:TextBox ID="txtVATRate" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnVATCalc" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="btnVATCalc_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divVATResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 37. Pomodoro / Focus Timer -->
    <asp:View ID="viewPomodoro" runat="server">
    <asp:Panel ID="pnlPomodoro" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Pomodoro / Focus Timer</h4>

            <div class="mb-3">
                <label>Work Minutes:</label>
                <asp:TextBox ID="txtPomWork" runat="server" CssClass="form-control" Text="25" />
            </div>
            <div class="mb-3">
                <label>Break Minutes:</label>
                <asp:TextBox ID="txtPomBreak" runat="server" CssClass="form-control" Text="5" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnPomodoroStart" runat="server" Text="Start Timer" CssClass="btn btn-primary" OnClick="btnPomodoroStart_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divPomResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 37. Water Intake Calculator -->
    <asp:View ID="viewWaterIntake" runat="server">
    <asp:Panel ID="pnlWaterIntake" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Water Intake Calculator</h4>

            <div class="mb-3">
                <label>Weight (kg):</label>
                <asp:TextBox ID="txtWaterWeight" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnWaterCalc" runat="server" Text="Calculate Liters Needed" CssClass="btn btn-primary" OnClick="btnWaterCalc_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divWaterResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 38. Chemistry Molarity Calculator -->
    <asp:View ID="viewChemMolarity" runat="server">
    <asp:Panel ID="pnlChemMolarity" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Chemistry Molarity Calculator</h4>

            <div class="mb-3">
                <label>Moles:</label>
                <asp:TextBox ID="txtChemMoles" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Volume (Liters):</label>
                <asp:TextBox ID="txtChemVolume" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnChemCalc" runat="server" Text="Calculate Molarity" CssClass="btn btn-primary" OnClick="btnChemCalc_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divChemResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>


    <!-- 39. Body Surface Area Calculator -->
    <asp:View ID="viewBSA" runat="server">
    <asp:Panel ID="pnlBSA" runat="server">
        <div class="card shadow-sm mb-4 p-4 border-0 rounded-3 bg-light">
            <h4 class="text-primary fw-bold mb-4">Body Surface Area Calculator</h4>

            <div class="mb-3">
                <label>Weight (kg):</label>
                <asp:TextBox ID="txtBSAWeight" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Height (cm):</label>
                <asp:TextBox ID="txtBSAHeight" runat="server" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 mb-3">
                <asp:Button ID="btnBSACalc" runat="server" Text="Calculate BSA" CssClass="btn btn-primary" OnClick="btnBSACalc_Click" />
            </div>

            <div class="mb-3">
                <label>Result:</label>
                <div id="divBSAResult" runat="server" class="history-box"></div>
            </div>
        </div>
    </asp:Panel>
</asp:View>



</asp:MultiView>


    </div>
  
     <!-- Scripts for Select2 (searchable dropdown) and print -->
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script>
        $(document).ready(function () {
            $('#<%= ddlCalculators.ClientID %>').select2({
                placeholder: "Select a calculator...",
                allowClear: true,
                width: '100%'
            });
        });

        function printCalculator() {
            const title = document.getElementById('<%= txtCalcTitle.ClientID %>').value;
            const desc = document.getElementById('<%= txtCalcDesc.ClientID %>').value;

            // Grab the card content (active view)
            let content = document.querySelector('.card').outerHTML;

            // Open a new window
            let printWindow = window.open('', '_blank', 'height=700,width=900');

            // Write the full HTML
            printWindow.document.open();
            printWindow.document.write(`
                <!DOCTYPE html>
                <html>
                <head>
                    <title>${title || 'Calculator Printout'}</title>
                    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
                    <style>
                        body { font-family: Arial, sans-serif; margin: 20px; }
                        h2 { margin-bottom: 10px; }
                        p { margin-bottom: 20px; }
                    </style>
                </head>
                <body>
                    ${title ? `<h2>${title}</h2>` : ''}
                    ${desc ? `<p>${desc}</p>` : ''}
                    ${content}
                </body>
                </html>
            `);
            printWindow.document.close(); // Ensure DOM is ready
            printWindow.focus();          // Focus for printing
            setTimeout(() => {
                printWindow.print();
                printWindow.close();
            }, 200); // Small delay to ensure rendering
            return false;
        }

    </script>
    <style>
        .history-box {
          white-space: pre-wrap;        /* preserve line breaks */
          overflow: visible;           /* no scrollbars */
          min-height: 6em;             /* starting height */
          padding: .5rem;
          border: 1px solid #ced4da;
          border-radius: .25rem;
          background: #f8f9fa;
          font-family: "Courier New", monospace; /* monospaced makes history easy to read */
          font-size: 0.9rem;
        }

    </style>
</asp:Content>

