Imports System.Data
Imports System.Text.RegularExpressions
Partial Class CalculatorSuite
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Full list of calculators
            Dim calculators As New List(Of String) From {"Algebra Solver",
                "BMI / Body Fat Calculator",
                "BMR Calculator",
                "Break-even Calculator",
                "Caloric Intake Calculator",
                "Car Loan Calculator",
                "Chemistry Molarity Calculator",
                "Compound Interest Calculator",
                "Cryptocurrency ROI Calculator",
                "Currency Converter",
                "Goal Achievement Planner",
                "Heart Rate / Target Zone Calculator",
                "Inventory Turnover Calculator",
                "Internal Rate of Return (IRR) Calculator",
                "Loan Amortization Calculator",
                "Macro Nutrient Calculator",
                "Markup / Margin Calculator",
                "Mortgage Calculator",
                "Net Present Value (NPV) Calculator",
                "Pet Weight / Dosage Calculator",
                "Physics Calculator",
                "Price / Profit Calculator",
                "Pregnancy Calorie Calculator",
                "Productivity Calculator",
                "Quadratic Equation Solver",
                "Retirement Planner",
                "ROI Calculator",
                "Savings Goal Calculator",
                "Sleep Needs Calculator",
                "Stock Investment Calculator",
                "Study / Learning Time Planner",
                "TDEE Calculator",
                "Travel / Fuel Cost Calculator",
                "Unit Conversion Calculator",
                "VAT / Tax Calculator",
                "Water Intake Calculator",
                "Pomodoro / Focus Timer",
                "Body Surface Area Calculator"
            }

            ' Alphabetically sort the list
            calculators = calculators.OrderBy(Function(c) c).ToList()

            ' Bind to dropdown
            ddlCalculators.DataSource = calculators
            ddlCalculators.DataBind()
            'ddlCalculators.Items.Insert(0, New ListItem("-- Select Calculator --", ""))
        End If
    End Sub

    Protected Sub ddlCalculators_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Switch the MultiView to the selected calculator
        Select Case ddlCalculators.SelectedValue
            Case "Algebra Solver"
                mvCalculators.SetActiveView(viewAlgebraAdvanced)
                pnlAlgebra.DefaultButton = btnAlgEval.UniqueID
            Case "BMI / Body Fat Calculator"
                mvCalculators.SetActiveView(viewBMI)
                pnlBMI.DefaultButton = btnBMICalc.UniqueID
            Case "BMR Calculator"
                mvCalculators.SetActiveView(viewBMR)
                pnlBMR.DefaultButton = btnBMRCalc.UniqueID
            Case "Break-even Calculator"
                mvCalculators.SetActiveView(viewBreakEven)
                pnlBreakEven.DefaultButton = btnBreakEvenCalc.UniqueID
            Case "Caloric Intake Calculator"
                mvCalculators.SetActiveView(viewCalorieIntake)
                pnlCalorie.DefaultButton = btnCalorieCalc.UniqueID
            Case "Car Loan Calculator"
                mvCalculators.SetActiveView(viewCarLoan)
                pnlCarLoan.DefaultButton = btnCarLoanCalc.UniqueID
            Case "Chemistry Molarity Calculator"
                mvCalculators.SetActiveView(viewChemistryMolarity)
                pnlMolarity.DefaultButton = btnMolarityCalc.UniqueID
            Case "Compound Interest Calculator"
                mvCalculators.SetActiveView(viewCompoundInterest)
                pnlCompoundInterest.DefaultButton = btnCICalc.UniqueID
            Case "Cryptocurrency ROI Calculator"
                mvCalculators.SetActiveView(viewCryptoROI)
                pnlCrypto.DefaultButton = btnCryptoCalc.UniqueID
            Case "Currency Converter"
                mvCalculators.SetActiveView(viewCurrencyConverter)
                pnlCurrency.DefaultButton = btnCurrencyCalc.UniqueID
            Case "Goal Achievement Planner"
                mvCalculators.SetActiveView(viewGoalPlanner)
                pnlGoalPlanner.DefaultButton = btnGoalCalc.UniqueID
            Case "Heart Rate / Target Zone Calculator"
                mvCalculators.SetActiveView(viewHeartRate)
                pnlHeartRate.DefaultButton = btnHRCalc.UniqueID
            Case "Inventory Turnover Calculator"
                mvCalculators.SetActiveView(viewInventoryTurnover)
                pnlInventoryTurnover.DefaultButton = btnInventoryCalc.UniqueID
            Case "Internal Rate of Return (IRR) Calculator"
                mvCalculators.SetActiveView(viewIRR)
                pnlIRR.DefaultButton = btnIRRCalc.UniqueID
            Case "Loan Amortization Calculator"
                mvCalculators.SetActiveView(viewLoanAmort)
                pnlLoanAmort.DefaultButton = btnLoanAmortCalc.UniqueID
            Case "Macro Nutrient Calculator"
                mvCalculators.SetActiveView(viewMacro)
                pnlMacro.DefaultButton = btnMacroCalc.UniqueID
            Case "Markup / Margin Calculator"
                mvCalculators.SetActiveView(viewMarkupMargin)
                pnlMarkupMargin.DefaultButton = btnMarkupCalc.UniqueID
            Case "Mortgage Calculator"
                mvCalculators.SetActiveView(viewMortgageCalc)
                pnlMortgage.DefaultButton = btnMortgageCalc.UniqueID
            Case "Net Present Value (NPV) Calculator"
                mvCalculators.SetActiveView(viewNPV)
                pnlNPV.DefaultButton = btnNPVCalc.UniqueID
            Case "Pet Weight / Dosage Calculator"
                mvCalculators.SetActiveView(viewPetDosage)
                pnlPetDosage.DefaultButton = btnPetCalc.UniqueID
            Case "Physics Calculator"
                mvCalculators.SetActiveView(viewPhysics)
                pnlPhysics.DefaultButton = btnPhysicsCalc.UniqueID
            Case "Price / Profit Calculator"
                mvCalculators.SetActiveView(viewPriceProfit)
                pnlPriceProfit.DefaultButton = btnPriceProfitCalc.UniqueID
            Case "Pregnancy Calorie Calculator"
                mvCalculators.SetActiveView(viewPregnancyCalorie)
                pnlPregCalorie.DefaultButton = btnPregCalc.UniqueID
            Case "Productivity Calculator"
                mvCalculators.SetActiveView(viewProductivity)
                pnlProductivity.DefaultButton = btnProdCalc.UniqueID
            Case "Quadratic Equation Solver"
                mvCalculators.SetActiveView(viewQuadratic)
                pnlQuadratic.DefaultButton = btnQuadCalc.UniqueID
            Case "Retirement Planner"
                mvCalculators.SetActiveView(viewRetirement)
                pnlRetirement.DefaultButton = btnRetCalc.UniqueID
            Case "ROI Calculator"
                mvCalculators.SetActiveView(viewROI)
                pnlROI.DefaultButton = btnROICalc.UniqueID
            Case "Savings Goal Calculator"
                mvCalculators.SetActiveView(viewSavingsGoal)
                pnlSavingsGoal.DefaultButton = btnGoalAmountCalc.UniqueID
            Case "Sleep Needs Calculator"
                mvCalculators.SetActiveView(viewSleepNeeds)
                pnlSleepNeeds.DefaultButton = btnSleepCalc.UniqueID
            Case "Stock Investment Calculator"
                mvCalculators.SetActiveView(viewStockInvestment)
                pnlStockInvestment.DefaultButton = btnStockCalc.UniqueID
            Case "Study / Learning Time Planner"
                mvCalculators.SetActiveView(viewStudyPlanner)
                pnlStudyPlanner.DefaultButton = btnStudyCalc.UniqueID
            Case "TDEE Calculator"
                mvCalculators.SetActiveView(viewTDEE)
                pnlTDEE.DefaultButton = btnTDEECalc.UniqueID
            Case "Travel / Fuel Cost Calculator"
                mvCalculators.SetActiveView(viewTravelFuel)
                pnlTravelFuel.DefaultButton = btnTDEECalc.UniqueID
            Case "Unit Conversion Calculator"
                mvCalculators.SetActiveView(viewUnitConversion)
                pnlUnitConversion.DefaultButton = btnUnitConvert.UniqueID
            Case "VAT / Tax Calculator"
                mvCalculators.SetActiveView(viewVAT)
                pnlVAT.DefaultButton = btnVATCalc.UniqueID
            Case "Water Intake Calculator"
                mvCalculators.SetActiveView(viewWaterIntake)
                pnlWaterIntake.DefaultButton = btnWaterCalc.UniqueID
            Case "Pomodoro / Focus Timer"
                mvCalculators.SetActiveView(viewPomodoro)
                pnlPomodoro.DefaultButton = btnPomodoroStart.UniqueID
            Case "Chemistry Molarity Calculator"
                mvCalculators.SetActiveView(viewChemistryMolarity)
                pnlChemMolarity.DefaultButton = btnChemCalc.UniqueID
            Case "Body Surface Area Calculator"
                mvCalculators.SetActiveView(viewBSA)
                pnlBSA.DefaultButton = btnBSACalc.UniqueID
        End Select
    End Sub

    Protected Sub btnAlgEval_Click(sender As Object, e As EventArgs)
        Dim input As String = txtAlgExpression.Text.Trim()
        Dim output As String = ""

        ' Solve or evaluate
        If input.Contains("=") Then
            output = SolveLinearEquation(input)
        Else
            output = EvaluateExpression(input)
        End If

        ' Format input and output as numbers if possible
        Dim formattedInput As String = input
        Dim formattedOutput As String = output

        Dim dblInput As Double
        If Double.TryParse(input, dblInput) Then
            formattedInput = dblInput.ToString("#,##0.##")
        End If

        Dim dblOutput As Double
        If Double.TryParse(output, dblOutput) Then
            formattedOutput = dblOutput.ToString("#,##0.##")
        End If

        ' Show latest result in label
        lblAlgResult.Text = "Output: " & formattedOutput

        ' Prepend new history entry at the top
        Dim newHistory As String = DateTime.Now.ToString("HH:mm:ss") & " | Input: " & formattedInput.PadRight(12) & " | Output: " & formattedOutput.PadRight(12)
        'txtAlgHistory.Text = newHistory & vbCrLf & txtAlgHistory.Text
        ' Build formatted entry (you already have formattedInput/formattedOutput)
        Dim newEntry As String = DateTime.Now.ToString("HH:mm:ss") & " | Input: " & formattedInput.PadRight(12) & " | Output: " & formattedOutput.PadRight(12)

        ' Prepend encoded entry to existing InnerHtml. Replace CRLF with <br/>
        divAlgHistory.InnerHtml = Server.HtmlEncode(newEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divAlgHistory.InnerHtml


        ' Clear input
        txtAlgExpression.Text = ""
        txtAlgExpression.Focus()
    End Sub


    Protected Sub btnAlgClear_Click(sender As Object, e As EventArgs)
        'txtAlgHistory.Text = ""
        divAlgHistory.InnerHtml = ""
        lblAlgResult.Text = ""
    End Sub

    ' Evaluate numeric expression
    Private Function EvaluateExpression(expr As String) As String
        Try
            Dim dt As New DataTable()
            dt.CaseSensitive = False
            Dim result As Object = dt.Compute(expr, "")
            Return result.ToString()
        Catch ex As Exception
            Return "Error: Invalid numeric expression."
        End Try
    End Function

    ' Solve simple linear equation: ax + b = c
    Private Function SolveLinearEquation(equation As String) As String
        Try
            Dim parts() As String = equation.Split("="c)
            If parts.Length <> 2 Then Return "Error: Invalid equation format."

            Dim left As String = parts(0).Trim()
            Dim right As String = parts(1).Trim()

            Dim match As Match = Regex.Match(left, "([+-]?\d*\.?\d*)\*?([a-zA-Z])")
            If Not match.Success Then Return "Error: Could not find variable."

            Dim coeffStr As String = match.Groups(1).Value
            Dim variable As String = match.Groups(2).Value

            Dim coeff As Double
            If String.IsNullOrEmpty(coeffStr) Or coeffStr = "+" Then
                coeff = 1
            ElseIf coeffStr = "-" Then
                coeff = -1
            Else
                coeff = Double.Parse(coeffStr)
            End If

            Dim leftConstStr As String = Regex.Replace(left, "([+-]?\d*\.?\d*)\*?" & variable, "")
            Dim leftConst As Double = 0
            If Not String.IsNullOrWhiteSpace(leftConstStr) Then
                leftConst = Double.Parse(EvaluateExpression(leftConstStr))
            End If

            Dim rightVal As Double = Double.Parse(EvaluateExpression(right))
            Dim x As Double = (rightVal - leftConst) / coeff

            Return variable & " = " & x.ToString()
        Catch ex As Exception
            Return "Error: Could not solve equation."
        End Try
    End Function

    Protected Sub btnBMICalc_Click(sender As Object, e As EventArgs) Handles btnBMICalc.Click
        Dim weight As Double
        Dim heightCm As Double
        Dim age As Integer
        Dim gender As String = ddlBMIGender.SelectedValue
        Dim result As String = ""

        ' Validate weight
        If Not Double.TryParse(txtBMIWeight.Text, weight) OrElse weight <= 0 Then
            lblBMIResult.Text = "Please enter a valid weight."
            Return
        End If

        ' Convert weight to kg if input is in lb
        Dim displayWeightUnit As String = ddlBMIWeightUnit.SelectedValue
        If displayWeightUnit = "lb" Then
            weight = weight * 0.453592
        End If

        ' Validate height
        If Not Double.TryParse(txtBMIHeight.Text, heightCm) OrElse heightCm <= 0 Then
            lblBMIResult.Text = "Please enter a valid height."
            Return
        End If

        ' Convert height to cm if input is in inches
        Dim displayHeightUnit As String = ddlBMIHeightUnit.SelectedValue
        If displayHeightUnit = "in" Then
            heightCm = heightCm * 2.54
        End If

        ' Validate age
        If Not Integer.TryParse(txtBMIAge.Text, age) OrElse age <= 0 Then
            lblBMIResult.Text = "Please enter a valid age."
            Return
        End If

        ' Convert height to meters
        Dim heightM As Double = heightCm / 100

        ' Calculate BMI
        Dim bmi As Double = weight / (heightM * heightM)

        ' Determine BMI category
        Dim bmiCategory As String = ""
        Select Case bmi
            Case < 18.5
                bmiCategory = "Underweight"
            Case 18.5 To 24.9
                bmiCategory = "Normal weight"
            Case 25 To 29.9
                bmiCategory = "Overweight"
            Case >= 30
                bmiCategory = "Obese"
        End Select

        ' Estimate Body Fat Percentage (US Navy method approximation)
        Dim bodyFat As Double
        If gender = "M" Then
            bodyFat = 1.2 * bmi + 0.23 * age - 16.2
        Else
            bodyFat = 1.2 * bmi + 0.23 * age - 5.4
        End If

        ' Healthy weight range for BMI 18.5-24.9
        Dim minHealthyWeight As Double = 18.5 * (heightM * heightM)
        Dim maxHealthyWeight As Double = 24.9 * (heightM * heightM)

        ' Convert outputs back to user's selected units
        If displayWeightUnit = "lb" Then
            minHealthyWeight *= 2.20462
            maxHealthyWeight *= 2.20462
        End If

        ' Prepare result string
        result &= $"BMI: {bmi:F2} ({bmiCategory}) <br/>"
        result &= $"Estimated Body Fat: {bodyFat:F1}% <br/>"
        result &= $"Healthy Weight Range: {minHealthyWeight:F1}-{maxHealthyWeight:F1} {displayWeightUnit}"

        ' Display results
        lblBMIResult.Text = result

        ' Add to history with timestamp (newest at top)
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | BMI: {bmi:F2} | Body Fat: {bodyFat:F1}% | Healthy Weight: {minHealthyWeight:F1}-{maxHealthyWeight:F1} {displayWeightUnit}"

        ' Prepend to div history
        divBMIHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divBMIHistory.InnerHtml
    End Sub


    Protected Sub btnBMRCalc_Click(sender As Object, e As EventArgs) Handles btnBMRCalc.Click
        Dim weight As Double
        Dim heightCm As Double
        Dim age As Integer
        Dim gender As String = ddlBMRGender.SelectedValue
        Dim activityFactor As Double = Convert.ToDouble(ddlBMRActivity.SelectedValue)
        Dim result As String = ""

        ' Validate weight
        If Not Double.TryParse(txtBMRWeight.Text, weight) OrElse weight <= 0 Then
            lblBMRResult.Text = "Please enter a valid weight."
            Return
        End If

        ' Convert weight to kg if needed
        If ddlBMRWeightUnit.SelectedValue = "lb" Then
            weight = weight * 0.453592
        End If

        ' Validate height
        If Not Double.TryParse(txtBMRHeight.Text, heightCm) OrElse heightCm <= 0 Then
            lblBMRResult.Text = "Please enter a valid height."
            Return
        End If

        ' Convert height to cm if needed
        If ddlBMRHeightUnit.SelectedValue = "in" Then
            heightCm = heightCm * 2.54
        End If

        ' Validate age
        If Not Integer.TryParse(txtBMRAge.Text, age) OrElse age <= 0 Then
            lblBMRResult.Text = "Please enter a valid age."
            Return
        End If

        ' Calculate BMR using Mifflin-St Jeor Equation
        Dim bmr As Double
        If gender = "M" Then
            bmr = 10 * weight + 6.25 * heightCm - 5 * age + 5
        Else
            bmr = 10 * weight + 6.25 * heightCm - 5 * age - 161
        End If

        ' Calculate TDEE
        Dim tdee As Double = bmr * activityFactor

        ' Format results
        result &= $"BMR: {bmr:F0} kcal/day <br/>"
        result &= $"TDEE (with activity factor): {tdee:F0} kcal/day"
        lblBMRResult.Text = result

        ' Add to history (newest at top)
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | BMR: {bmr:F0} kcal | TDEE: {tdee:F0} kcal"

        ' Prepend entry to div and maintain line breaks
        divBMRHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divBMRHistory.InnerHtml
    End Sub


    Protected Sub btnBreakEvenCalc_Click(sender As Object, e As EventArgs) Handles btnBreakEvenCalc.Click
        Dim fixedCosts As Double
        Dim pricePerUnit As Double
        Dim variableCostPerUnit As Double
        Dim targetProfit As Double = 0
        Dim result As String = ""

        ' Validate inputs
        If Not Double.TryParse(txtBreakEvenFixed.Text, fixedCosts) OrElse fixedCosts < 0 Then
            lblBreakEvenResult.Text = "Please enter a valid Fixed Costs value."
            Return
        End If

        If Not Double.TryParse(txtBreakEvenPrice.Text, pricePerUnit) OrElse pricePerUnit <= 0 Then
            lblBreakEvenResult.Text = "Please enter a valid Price per Unit."
            Return
        End If

        If Not Double.TryParse(txtBreakEvenVariable.Text, variableCostPerUnit) OrElse variableCostPerUnit < 0 Then
            lblBreakEvenResult.Text = "Please enter a valid Variable Cost per Unit."
            Return
        End If

        ' Optional target profit
        If txtBreakEvenTargetProfit.Text <> "" Then
            Double.TryParse(txtBreakEvenTargetProfit.Text, targetProfit)
        End If

        ' Check if price > variable cost
        If pricePerUnit <= variableCostPerUnit Then
            lblBreakEvenResult.Text = "Price per Unit must be greater than Variable Cost per Unit."
            Return
        End If

        ' Calculate break-even units and revenue
        Dim breakEvenUnits As Double = fixedCosts / (pricePerUnit - variableCostPerUnit)
        Dim breakEvenRevenue As Double = breakEvenUnits * pricePerUnit

        ' Calculate units/revenue for target profit
        Dim targetUnits As Double = (fixedCosts + targetProfit) / (pricePerUnit - variableCostPerUnit)
        Dim targetRevenue As Double = targetUnits * pricePerUnit

        ' Format result label
        result &= $"Break-even Point: {breakEvenUnits:N0} units <br/>"
        result &= $"Break-even Revenue: {breakEvenRevenue:C2} <br/>"

        If targetProfit > 0 Then
            result &= $"Units for Target Profit (${targetProfit:N0}): {targetUnits:N0} <br/>"
            result &= $"Revenue for Target Profit: {targetRevenue:C2}"
        End If

        lblBreakEvenResult.Text = result

        ' Add to history (newest at top)
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | BE Units: {breakEvenUnits:N0} | BE Revenue: {breakEvenRevenue:C2}"
        If targetProfit > 0 Then
            historyEntry &= $" | Target Units: {targetUnits:N0} | Target Revenue: {targetRevenue:C2}"
        End If

        ' Prepend to div (HTML encode and use <br/>)
        divBreakEvenHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divBreakEvenHistory.InnerHtml
    End Sub
    Protected Sub btnCalorieCalc_Click(sender As Object, e As EventArgs) Handles btnCalorieCalc.Click
        Dim weight As Double
        Dim heightCm As Double
        Dim age As Integer
        Dim gender As String = ddlCalorieGender.SelectedValue
        Dim result As String = ""

        ' --- Validate weight ---
        If Not Double.TryParse(txtCalorieWeight.Text, weight) OrElse weight <= 0 Then
            lblCalorieResult.Text = "Please enter a valid weight."
            Return
        End If
        If ddlCalorieWeightUnit.SelectedValue = "lb" Then weight *= 0.453592

        ' --- Validate height ---
        If Not Double.TryParse(txtCalorieHeight.Text, heightCm) OrElse heightCm <= 0 Then
            lblCalorieResult.Text = "Please enter a valid height."
            Return
        End If
        If ddlCalorieHeightUnit.SelectedValue = "in" Then heightCm *= 2.54

        ' --- Validate age ---
        If Not Integer.TryParse(txtCalorieAge.Text, age) OrElse age <= 0 Then
            lblCalorieResult.Text = "Please enter a valid age."
            Return
        End If

        ' --- Calculate BMR ---
        Dim bmr As Double
        If gender = "M" Then
            bmr = 10 * weight + 6.25 * heightCm - 5 * age + 5
        Else
            bmr = 10 * weight + 6.25 * heightCm - 5 * age - 161
        End If

        ' --- Calculate TDEE ---
        Dim activityFactor As Double = Double.Parse(ddlCalorieActivity.SelectedValue)
        Dim tdee As Double = bmr * activityFactor

        ' --- Fitness goal and macros ---
        Dim fitnessGoal As String = ddlFitnessGoal.SelectedValue
        Dim recommendedCalories As Double = tdee
        Dim proteinPerKg As Double = 2.0
        Dim fatPerc As Double = 0.25
        Dim carbPerc As Double = 0.4 ' realistic cap for carbs
        Select Case fitnessGoal
            Case "lose_standard"
                recommendedCalories = tdee - 500
                proteinPerKg = 2.0
                fatPerc = 0.3
                carbPerc = 0.35
            Case "lose_aggressive"
                recommendedCalories = tdee - 750
                proteinPerKg = 2.2
                fatPerc = 0.3
                carbPerc = 0.35
            Case "maintain"
                recommendedCalories = tdee
                proteinPerKg = 1.8
                fatPerc = 0.25
                carbPerc = 0.4
            Case "gain_lean"
                recommendedCalories = tdee + 300
                proteinPerKg = 2.0
                fatPerc = 0.25
                carbPerc = 0.4
            Case "gain_bulk"
                recommendedCalories = tdee + 500
                proteinPerKg = 2.0
                fatPerc = 0.25
                carbPerc = 0.45
        End Select

        ' --- Calculate macros ---
        Dim proteinGrams As Double = weight * proteinPerKg
        Dim proteinCalories As Double = proteinGrams * 4

        Dim fatCalories As Double = recommendedCalories * fatPerc
        Dim fatGrams As Double = fatCalories / 9

        Dim carbCalories As Double = recommendedCalories * carbPerc
        Dim carbGrams As Double = carbCalories / 4

        ' --- Format result ---
        result = $"BMR: {bmr:F0} kcal/day  <br/> TDEE: {tdee:F0} kcal/day  <br/> Recommended Intake ({fitnessGoal}): {recommendedCalories:F0} kcal/day  <br/> Protein: {proteinGrams:F0} g  <br/> Carbs: {carbGrams:F0} g  <br/> Fat: {fatGrams:F0} g"

        ' --- Display result ---
        lblCalorieResult.Text = result

        ' --- Update macro bars ---
        Dim totalCalories As Double = proteinCalories + fatCalories + carbCalories
        divProteinBar.Style("width") = ((proteinCalories / totalCalories) * 100).ToString("F0") & "%"
        divProteinBar.InnerText = $"Protein {proteinGrams:F0}g"

        divCarbBar.Style("width") = ((carbCalories / totalCalories) * 100).ToString("F0") & "%"
        divCarbBar.InnerText = $"Carbs {carbGrams:F0}g"

        divFatBar.Style("width") = ((fatCalories / totalCalories) * 100).ToString("F0") & "%"
        divFatBar.InnerText = $"Fat {fatGrams:F0}g"

        ' --- Add to history ---
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | {result}"
        divCalorieHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divCalorieHistory.InnerHtml
    End Sub


    Protected Sub btnCarLoanCalc_Click(sender As Object, e As EventArgs) Handles btnCarLoanCalc.Click
        Dim loanAmount As Double
        Dim annualRate As Double
        Dim termMonths As Integer
        Dim extraPayment As Double = 0

        ' --- Validate inputs ---
        If Not Double.TryParse(txtCarLoanAmount.Text, loanAmount) OrElse loanAmount <= 0 Then
            lblCarLoanResult.Text = "Please enter a valid loan amount."
            Return
        End If

        If Not Double.TryParse(txtCarLoanRate.Text, annualRate) OrElse annualRate < 0 Then
            lblCarLoanResult.Text = "Please enter a valid interest rate."
            Return
        End If

        If Not Integer.TryParse(txtCarLoanMonths.Text, termMonths) OrElse termMonths <= 0 Then
            lblCarLoanResult.Text = "Please enter a valid term in months."
            Return
        End If

        Double.TryParse(txtCarLoanExtra.Text, extraPayment)

        ' --- Monthly interest rate ---
        Dim monthlyRate As Double = annualRate / 100 / 12

        ' --- Standard monthly payment ---
        Dim monthlyPayment As Double
        If monthlyRate = 0 Then
            monthlyPayment = loanAmount / termMonths
        Else
            monthlyPayment = loanAmount * monthlyRate / (1 - Math.Pow(1 + monthlyRate, -termMonths))
        End If

        Dim totalPaid As Double = 0
        Dim totalInterest As Double = 0
        Dim balance As Double = loanAmount
        Dim monthCounter As Integer = 0

        ' --- Amortization table ---
        Dim amortization As New System.Text.StringBuilder()
        amortization.AppendLine("Month | Payment | Interest | Principal | Balance")

        While balance > 0
            monthCounter += 1
            Dim interestPayment As Double = balance * monthlyRate
            Dim principalPayment As Double = monthlyPayment - interestPayment + extraPayment

            ' Ensure we don't pay more than remaining balance
            If principalPayment > balance Then
                principalPayment = balance
            End If

            Dim paymentThisMonth As Double = principalPayment + interestPayment
            balance -= principalPayment

            amortization.AppendLine($"{monthCounter} | {paymentThisMonth:C2} | {interestPayment:C2} | {principalPayment:C2} | {balance:C2}")

            totalPaid += paymentThisMonth
            totalInterest += interestPayment
        End While

        ' --- Format result ---
        Dim result As String = $"Monthly Payment: {monthlyPayment:C2} <br/> Total Paid: {totalPaid:C2} <br/> Total Interest: {totalInterest:C2} <br/> Months to Payoff: {monthCounter}"

        ' --- Display result ---
        lblCarLoanResult.Text = result
        divCarLoanAmortization.InnerHtml = Server.HtmlEncode(amortization.ToString).Replace(vbCrLf, "<br/>")

        ' --- Add to history ---
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | {result}"
        divCarLoanHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divCarLoanHistory.InnerHtml
    End Sub




    Protected Sub btnMolarityCalc_Click(sender As Object, e As EventArgs) Handles btnMolarityCalc.Click
        Dim amount As Double
        Dim molarMass As Double
        Dim volume As Double

        ' --- Validate solute amount ---
        If Not Double.TryParse(txtAmount.Text, amount) OrElse amount <= 0 Then
            lblMolarityResult.Text = "Please enter a valid solute amount."
            Return
        End If

        ' --- Convert grams to moles if needed ---
        If ddlAmountUnit.SelectedValue = "grams" Then
            If Not Double.TryParse(txtMolarMass.Text, molarMass) OrElse molarMass <= 0 Then
                lblMolarityResult.Text = "Please enter a valid molar mass in g/mol."
                Return
            End If
            amount = amount / molarMass ' Convert grams → moles
        End If

        ' --- Validate volume ---
        If Not Double.TryParse(txtVolume.Text, volume) OrElse volume <= 0 Then
            lblMolarityResult.Text = "Please enter a valid solution volume."
            Return
        End If

        ' --- Convert volume to liters if needed ---
        If ddlVolumeUnit.SelectedValue = "mL" Then
            volume = volume / 1000
        End If

        ' --- Calculate molarity ---
        Dim molarity As Double = amount / volume

        ' --- Format result ---
        Dim result As String = $"Molarity: {molarity:F3} M"

        ' --- Display result ---
        lblMolarityResult.Text = result

        ' --- Add to history ---
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | {result}"
        divMolarityHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divMolarityHistory.InnerHtml
    End Sub

    Protected Sub btnCICalc_Click(sender As Object, e As EventArgs) Handles btnCICalc.Click
        ' --- Input validation ---
        Dim principal As Double, rate As Double, years As Double
        Dim compoundsPerYear As Integer, extra As Double
        Dim extraFreq As String = ddlCIExtraFreq.SelectedValue

        If Not Double.TryParse(txtCIPrincipal.Text, principal) OrElse principal <= 0 Then
            lblCIResult.Text = "Enter a valid principal."
            Return
        End If
        If Not Double.TryParse(txtCIRate.Text, rate) OrElse rate < 0 Then
            lblCIResult.Text = "Enter a valid interest rate."
            Return
        End If
        If Not Double.TryParse(txtCIYears.Text, years) OrElse years <= 0 Then
            lblCIResult.Text = "Enter a valid number of years."
            Return
        End If
        If Not Integer.TryParse(txtCICompounds.Text, compoundsPerYear) OrElse compoundsPerYear <= 0 Then
            lblCIResult.Text = "Enter a valid number of compounds per year."
            Return
        End If
        If Not Double.TryParse(txtCIExtra.Text, extra) OrElse extra < 0 Then extra = 0

        ' --- Calculate monthly values ---
        Dim totalMonths As Integer = years * 12
        Dim monthlyRate As Double = rate / 100 / 12
        Dim fvNoExtra As Double = principal
        Dim fvWithExtra As Double = principal

        ' Determine monthly extra contribution
        Dim extraPerMonth As Double = 0
        Select Case extraFreq
            Case "monthly"
                extraPerMonth = extra
            Case "quarterly"
                extraPerMonth = extra / 3
            Case "annually"
                extraPerMonth = extra / 12
        End Select

        ' --- Future Value without extra ---
        For m As Integer = 1 To totalMonths
            fvNoExtra *= (1 + monthlyRate)
        Next

        ' --- Future Value with extra ---
        For m As Integer = 1 To totalMonths
            fvWithExtra *= (1 + monthlyRate)
            fvWithExtra += extraPerMonth
        Next

        ' --- Interest calculations ---
        Dim interestNoExtra As Double = fvNoExtra - principal
        Dim totalContributions As Double = extraPerMonth * totalMonths
        Dim interestWithExtra As Double = fvWithExtra - principal - totalContributions

        ' --- Time saved calculation ---
        ' Determine months to reach FV without extra vs. with extra
        Dim targetFV As Double = fvWithExtra
        Dim monthsNoExtra As Double = 0
        Dim balance As Double = principal
        Do While balance < targetFV
            balance *= (1 + monthlyRate)
            monthsNoExtra += 1
            If monthsNoExtra > 1000 Then Exit Do ' prevent infinite loop
        Loop
        Dim timeSavedMonths As Double = monthsNoExtra - totalMonths

        ' --- Format result ---
        Dim result As String = $"Future Value (no extra): ${fvNoExtra:F2} <br/> Interest Earned: ${interestNoExtra:F2} <br/> " &
                           $"With Contributions: ${fvWithExtra:F2} <br/> Interest Earned: ${interestWithExtra:F2} <br/> " &
                           $"Total Contributions: ${totalContributions:F2} <br/> Time Saved: {timeSavedMonths:F1} months"

        lblCIResult.Text = result

        ' --- Add to history ---
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | {result}"
        divCIHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divCIHistory.InnerHtml
    End Sub

    Protected Sub btnCryptoCalc_Click(sender As Object, e As EventArgs) Handles btnCryptoCalc.Click
        Dim initialInvestment As Double
        Dim finalValue As Double
        Dim years As Double
        Dim extraContribution As Double
        Dim contributionFreq As Integer

        ' --- Validate inputs ---
        If Not Double.TryParse(txtCryptoInvestment.Text, initialInvestment) OrElse initialInvestment <= 0 Then
            lblCryptoResult.Text = "Please enter a valid initial investment."
            Return
        End If
        If Not Double.TryParse(txtCryptoFinal.Text, finalValue) OrElse finalValue <= 0 Then
            lblCryptoResult.Text = "Please enter a valid final value."
            Return
        End If
        If Not Double.TryParse(txtCryptoYears.Text, years) OrElse years <= 0 Then
            lblCryptoResult.Text = "Please enter a valid number of years."
            Return
        End If
        If Not Double.TryParse(txtCryptoContribution.Text, extraContribution) OrElse extraContribution < 0 Then
            lblCryptoResult.Text = "Please enter a valid contribution amount."
            Return
        End If
        contributionFreq = Integer.Parse(ddlCryptoFreq.SelectedValue)

        ' --- Calculate total months ---
        Dim totalMonths As Integer = CInt(years * 12)
        Dim periodMonths As Integer = contributionFreq ' e.g., 1 = monthly, 3 = quarterly

        ' --- Future Value with contributions ---
        Dim fvWithExtra As Double = initialInvestment
        Dim totalContributions As Double = 0
        Dim monthlyRate As Double = (finalValue / initialInvestment) ^ (1 / totalMonths) - 1

        For month As Integer = 1 To totalMonths
            fvWithExtra *= (1 + monthlyRate)
            If month Mod periodMonths = 0 Then
                fvWithExtra += extraContribution
                totalContributions += extraContribution
            End If
        Next

        ' --- Profit calculations ---
        Dim profit As Double = fvWithExtra - initialInvestment
        Dim roiPercent As Double = (profit / initialInvestment) * 100
        Dim annualizedROI As Double = ((fvWithExtra / initialInvestment) ^ (1 / years) - 1) * 100

        ' --- Estimate time saved vs. reaching FV without contributions ---
        Dim fvWithoutExtra As Double = initialInvestment
        Dim monthsWithoutExtra As Integer = 0
        While fvWithoutExtra < fvWithExtra
            fvWithoutExtra *= (1 + monthlyRate)
            monthsWithoutExtra += 1
        End While
        Dim monthsSaved As Integer = monthsWithoutExtra - totalMonths

        ' --- Format result ---
        Dim result As String = $"Profit: ${profit:F2} <br/> ROI: {roiPercent:F2}% <br/> Annualized ROI: {annualizedROI:F2}% <br/> " &
                           $"Total Contributions: ${totalContributions:F2} <br/> Time Saved: {monthsSaved} months"

        ' --- Display result ---
        lblCryptoResult.Text = result

        ' --- Add to history ---
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | {result}"
        divCryptoHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divCryptoHistory.InnerHtml
    End Sub


    Protected Sub btnCurrencyCalc_Click(sender As Object, e As EventArgs) Handles btnCurrencyCalc.Click
        Dim amount As Double
        Dim rate As Double
        Dim feePercent As Double
        Dim result As String = ""

        ' --- Validate inputs ---
        If Not Double.TryParse(txtCurrencyAmount.Text, amount) OrElse amount <= 0 Then
            lblCurrencyResult.Text = "Please enter a valid amount."
            Return
        End If
        If Not Double.TryParse(txtCurrencyRate.Text, rate) OrElse rate <= 0 Then
            lblCurrencyResult.Text = "Please enter a valid exchange rate."
            Return
        End If
        If Not Double.TryParse(txtCurrencyFee.Text, feePercent) OrElse feePercent < 0 Then
            lblCurrencyResult.Text = "Please enter a valid fee percentage."
            Return
        End If

        ' --- Apply fee/markup ---
        Dim effectiveRate As Double = rate * (1 - feePercent / 100)
        Dim convertedAmount As Double = amount * effectiveRate
        result &= $"Converted Amount: {convertedAmount:F2} (Rate after {feePercent}% fee: {effectiveRate:F4})"

        ' --- Handle additional target currencies ---
        If Not String.IsNullOrWhiteSpace(txtCurrencyExtraRates.Text) Then
            Dim extraRates = txtCurrencyExtraRates.Text.Split(","c)
            result &= "<br/>Additional Conversions:"
            For Each rStr In extraRates
                Dim r As Double
                If Double.TryParse(rStr.Trim(), r) AndAlso r > 0 Then
                    Dim convertedExtra As Double = amount * r * (1 - feePercent / 100)
                    result &= $"<br/>Rate {r:F4} → {convertedExtra:F2}"
                End If
            Next
        End If

        ' --- Display result ---
        lblCurrencyResult.Text = result

        ' --- Add to history ---
        Dim historyEntry As String = $"{DateTime.Now:HH:mm:ss} | {result}"
        divCurrencyHistory.InnerHtml = Server.HtmlEncode(historyEntry).Replace(vbCrLf, "<br/>") & "<br/>" & divCurrencyHistory.InnerHtml
    End Sub

    Protected Sub btnGoalCalc_Click(sender As Object, e As EventArgs) Handles btnGoalCalc.Click
        Dim goal As String = txtGoalDesc.Text.Trim()
        Dim startDate As DateTime
        Dim days As Integer

        ' Validate goal description
        If String.IsNullOrWhiteSpace(goal) Then
            divGoalResult.InnerHtml = "Please enter a goal description."
            divGoalResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlGoalDetails.Visible = False
            divProgressBar.Attributes("style") = "width:0%"
            divProgressBar.InnerText = "0%"
            Exit Sub
        End If

        ' Validate start date
        If Not DateTime.TryParse(txtStartDate.Text.Trim(), startDate) Then
            divGoalResult.InnerHtml = "Please select a valid start date."
            divGoalResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlGoalDetails.Visible = False
            Exit Sub
        End If

        ' Validate days input
        If Not Integer.TryParse(txtGoalDays.Text.Trim(), days) OrElse days <= 0 Then
            divGoalResult.InnerHtml = "Please enter a valid number of days."
            divGoalResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlGoalDetails.Visible = False
            Exit Sub
        End If

        ' Calculate target date
        Dim targetDate As DateTime = startDate.AddDays(days)

        ' Calculate progress
        Dim totalDays As Integer = (targetDate - startDate).Days
        Dim elapsedDays As Integer = (DateTime.Now - startDate).Days
        Dim progress As Integer = CInt(Math.Min(Math.Max((elapsedDays / totalDays) * 100, 0), 100))

        ' Show results
        divGoalResult.InnerHtml = String.Format("Your goal ""{0}"" should be completed by <span class='text-primary'>{1:MMMM dd, yyyy}</span>.", goal, targetDate)
        divGoalResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Update progress bar
        divProgressBar.Attributes("style") = $"width:{progress}%;"
        divProgressBar.InnerText = progress.ToString() & "%"
        divProgressBar.Attributes("aria-valuenow") = progress.ToString()

        pnlGoalDetails.Visible = True
    End Sub

    Protected Sub btnHRCalc_Click(sender As Object, e As EventArgs) Handles btnHRCalc.Click
        Dim age As Integer
        If Not Integer.TryParse(txtHRAge.Text.Trim(), age) OrElse age <= 0 Then
            divHRResult.InnerHtml = "Enter a valid age."
            divHRResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlHRTips.Visible = False
            ResetHRZones()
            Exit Sub
        End If

        ' Max HR
        Dim maxHR As Integer = 220 - age

        ' Zones
        Dim lightMax As Integer = CInt(maxHR * 0.6)
        Dim moderateMax As Integer = CInt(maxHR * 0.75)
        Dim intenseMax As Integer = CInt(maxHR * 0.9)

        divHRZoneLight.Attributes("data-max") = lightMax.ToString()
        divHRZoneModerate.Attributes("data-max") = moderateMax.ToString()
        divHRZoneIntense.Attributes("data-max") = intenseMax.ToString()

        ' Display results
        divHRResult.InnerHtml = String.Format(
        "Max HR: <strong>{0} bpm</strong><br/>" &
        "Light Zone: 50-60% ({1} bpm)<br/>" &
        "Moderate Zone: 60-75% ({2} bpm)<br/>" &
        "Intense Zone: 75-90% ({3} bpm)",
        maxHR, lightMax, moderateMax, intenseMax)
        divHRResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Set widths
        divHRZoneLight.Attributes("style") = $"width:{CInt(lightMax / maxHR * 100)}%"
        divHRZoneLight.InnerText = $"Light ({lightMax} bpm)"
        divHRZoneModerate.Attributes("style") = $"width:{CInt((moderateMax - lightMax) / maxHR * 100)}%"
        divHRZoneModerate.InnerText = $"Moderate ({moderateMax} bpm)"
        divHRZoneIntense.Attributes("style") = $"width:{CInt((intenseMax - moderateMax) / maxHR * 100)}%"
        divHRZoneIntense.InnerText = $"Intense ({intenseMax} bpm)"

        pnlHRTips.Visible = True
    End Sub

    Private Sub ResetHRZones()
        divHRZoneLight.Attributes("style") = "width:0%"
        divHRZoneLight.InnerText = "Light"
        divHRZoneModerate.Attributes("style") = "width:0%"
        divHRZoneModerate.InnerText = "Moderate"
        divHRZoneIntense.Attributes("style") = "width:0%"
        divHRZoneIntense.InnerText = "Intense"
    End Sub



    Protected Sub btnInventoryCalc_Click(sender As Object, e As EventArgs) Handles btnInventoryCalc.Click
        Dim cogs As Decimal
        Dim avgInventory As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtCOGS.Text.Trim(), cogs) OrElse cogs <= 0 Then
            divInventoryResult.InnerHtml = "Please enter a valid COGS."
            divInventoryResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlInventoryTips.Visible = False
            divInventoryGauge.Attributes("style") = "width:0%"
            divInventoryGauge.InnerText = "0%"
            Exit Sub
        End If

        If Not Decimal.TryParse(txtAvgInventory.Text.Trim(), avgInventory) OrElse avgInventory <= 0 Then
            divInventoryResult.InnerHtml = "Please enter a valid Average Inventory."
            divInventoryResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlInventoryTips.Visible = False
            divInventoryGauge.Attributes("style") = "width:0%"
            divInventoryGauge.InnerText = "0%"
            Exit Sub
        End If

        ' Calculate inventory turnover
        Dim turnover As Decimal = cogs / avgInventory
        Dim turnoverRounded As Decimal = Math.Round(turnover, 2)

        ' Display result
        divInventoryResult.InnerHtml = String.Format(
        "Inventory Turnover Ratio: <strong>{0}</strong>",
        turnoverRounded
    )
        divInventoryResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Update efficiency gauge (capped at 100% for visual)
        Dim gaugePct As Integer = Math.Min(CInt(turnover * 10), 100)
        divInventoryGauge.Attributes("style") = $"width:{gaugePct}%"
        divInventoryGauge.InnerText = $"{turnoverRounded}x"

        ' Show tips panel
        pnlInventoryTips.Visible = True
    End Sub

    Protected Sub btnIRRCalc_Click(sender As Object, e As EventArgs) Handles btnIRRCalc.Click
        Dim initialInvestment As Decimal
        Dim finalValue As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtIRRInvestment.Text.Trim(), initialInvestment) OrElse initialInvestment <= 0 Then
            divIRRResult.InnerHtml = "Please enter a valid Initial Investment."
            divIRRResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlIRRTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtIRRFinal.Text.Trim(), finalValue) OrElse finalValue <= 0 Then
            divIRRResult.InnerHtml = "Please enter a valid Final Value."
            divIRRResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlIRRTips.Visible = False
            Exit Sub
        End If

        ' Calculate simple IRR (assuming 1 period)
        Dim irr As Decimal = (finalValue / initialInvestment) - 1
        Dim irrPct As Decimal = Math.Round(irr * 100, 2)

        ' Display result
        divIRRResult.InnerHtml = String.Format(
        "Estimated IRR: <strong>{0}%</strong>",
        irrPct
    )
        divIRRResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlIRRTips.Visible = True
    End Sub


    Protected Sub btnLoanAmortCalc_Click(sender As Object, e As EventArgs) Handles btnLoanAmortCalc.Click
        Dim loanAmt As Decimal
        Dim interestRate As Decimal
        Dim termYears As Integer

        ' Validate inputs
        If Not Decimal.TryParse(txtLoanAmt.Text.Trim(), loanAmt) OrElse loanAmt <= 0 Then
            divLoanAmortResult.InnerHtml = "Please enter a valid Loan Amount."
            divLoanAmortResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlLoanAmortTips.Visible = False
            divLoanAmortTable.InnerHtml = ""
            Exit Sub
        End If

        If Not Decimal.TryParse(txtLoanInterest.Text.Trim(), interestRate) OrElse interestRate <= 0 Then
            divLoanAmortResult.InnerHtml = "Please enter a valid Interest Rate."
            divLoanAmortResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlLoanAmortTips.Visible = False
            divLoanAmortTable.InnerHtml = ""
            Exit Sub
        End If

        If Not Integer.TryParse(txtLoanTerm.Text.Trim(), termYears) OrElse termYears <= 0 Then
            divLoanAmortResult.InnerHtml = "Please enter a valid Term (Years)."
            divLoanAmortResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlLoanAmortTips.Visible = False
            divLoanAmortTable.InnerHtml = ""
            Exit Sub
        End If

        ' Calculations
        Dim n As Integer = termYears * 12
        Dim r As Decimal = interestRate / 100 / 12
        Dim monthlyPayment As Decimal = loanAmt * r / (1 - Math.Pow(1 + r, -n))
        Dim totalPayment As Decimal = monthlyPayment * n
        Dim totalInterest As Decimal = totalPayment - loanAmt

        ' Display summary
        divLoanAmortResult.InnerHtml = String.Format(
        "Monthly Payment: <strong>${0:N2}</strong><br/>Total Payment: <strong>${1:N2}</strong><br/>Total Interest: <strong>${2:N2}</strong>",
        monthlyPayment, totalPayment, totalInterest
    )
        divLoanAmortResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Build amortization table (first 12 months)
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<table class='table table-bordered table-striped mt-2'><thead><tr><th>Month</th><th>Principal</th><th>Interest</th><th>Balance</th></tr></thead><tbody>")

        Dim balance As Decimal = loanAmt
        For i As Integer = 1 To Math.Min(12, n)
            Dim interestMonth As Decimal = balance * r
            Dim principalMonth As Decimal = monthlyPayment - interestMonth
            balance -= principalMonth
            If balance < 0 Then balance = 0
            sb.AppendFormat("<tr><td>{0}</td><td>${1:N2}</td><td>${2:N2}</td><td>${3:N2}</td></tr>", i, principalMonth, interestMonth, balance)
        Next
        sb.Append("</tbody></table>")

        divLoanAmortTable.InnerHtml = sb.ToString()

        ' Show tips panel
        pnlLoanAmortTips.Visible = True
    End Sub



    Protected Sub btnMacroCalc_Click(sender As Object, e As EventArgs) Handles btnMacroCalc.Click
        Dim calories As Decimal
        Dim pctProtein As Decimal
        Dim pctCarbs As Decimal
        Dim pctFat As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtMacroCalories.Text.Trim(), calories) OrElse calories <= 0 Then
            divMacroResult.InnerHtml = "Please enter a valid number of calories."
            divMacroResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMacroTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtMacroProtein.Text.Trim(), pctProtein) OrElse pctProtein < 0 Then
            divMacroResult.InnerHtml = "Please enter a valid Protein %."
            divMacroResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMacroTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtMacroCarbs.Text.Trim(), pctCarbs) OrElse pctCarbs < 0 Then
            divMacroResult.InnerHtml = "Please enter a valid Carbs %."
            divMacroResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMacroTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtMacroFat.Text.Trim(), pctFat) OrElse pctFat < 0 Then
            divMacroResult.InnerHtml = "Please enter a valid Fat %."
            divMacroResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMacroTips.Visible = False
            Exit Sub
        End If

        ' Optional: check if percentages sum to 100
        Dim totalPct As Decimal = pctProtein + pctCarbs + pctFat
        If totalPct <> 100 Then
            divMacroResult.InnerHtml = "Warning: Percentages do not sum to 100%."
            divMacroResult.Attributes("class") = "text-warning fw-bold history-box"
        End If

        ' Calculate grams
        Dim gramsProtein As Decimal = Math.Round(calories * (pctProtein / 100) / 4, 2)
        Dim gramsCarbs As Decimal = Math.Round(calories * (pctCarbs / 100) / 4, 2)
        Dim gramsFat As Decimal = Math.Round(calories * (pctFat / 100) / 9, 2)

        ' Display result
        divMacroResult.InnerHtml &= String.Format(
        "<br/>Protein: <strong>{0} g</strong><br/>Carbs: <strong>{1} g</strong><br/>Fat: <strong>{2} g</strong>",
        gramsProtein, gramsCarbs, gramsFat
    )
        divMacroResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlMacroTips.Visible = True
    End Sub

    Protected Sub btnMarkupCalc_Click(sender As Object, e As EventArgs) Handles btnMarkupCalc.Click
        Dim cost As Decimal
        Dim price As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtCost.Text.Trim(), cost) OrElse cost <= 0 Then
            divMarkupResult.InnerHtml = "Please enter a valid Cost."
            divMarkupResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMarkupTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtPrice.Text.Trim(), price) OrElse price <= 0 Then
            divMarkupResult.InnerHtml = "Please enter a valid Price."
            divMarkupResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMarkupTips.Visible = False
            Exit Sub
        End If

        ' Calculate markup and margin
        Dim markupPct As Decimal = Math.Round(((price - cost) / cost) * 100, 2)
        Dim marginPct As Decimal = Math.Round(((price - cost) / price) * 100, 2)

        ' Display result
        divMarkupResult.InnerHtml = String.Format(
        "Markup: <strong>{0}%</strong><br/>Margin: <strong>{1}%</strong>",
        markupPct, marginPct
    )
        divMarkupResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips panel
        pnlMarkupTips.Visible = True
    End Sub



    Protected Sub btnMortgageCalc_Click(sender As Object, e As EventArgs) Handles btnMortgageCalc.Click
        Dim loanAmt As Decimal
        Dim interestRate As Decimal
        Dim termYears As Integer

        ' Validate inputs
        If Not Decimal.TryParse(txtMortgageAmt.Text.Trim(), loanAmt) OrElse loanAmt <= 0 Then
            divMortgageResult.InnerHtml = "Please enter a valid Loan Amount."
            divMortgageResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMortgageTips.Visible = False
            divMortgageTable.InnerHtml = ""
            Exit Sub
        End If

        If Not Decimal.TryParse(txtMortgageInterest.Text.Trim(), interestRate) OrElse interestRate <= 0 Then
            divMortgageResult.InnerHtml = "Please enter a valid Interest Rate."
            divMortgageResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMortgageTips.Visible = False
            divMortgageTable.InnerHtml = ""
            Exit Sub
        End If

        If Not Integer.TryParse(txtMortgageTerm.Text.Trim(), termYears) OrElse termYears <= 0 Then
            divMortgageResult.InnerHtml = "Please enter a valid Term (Years)."
            divMortgageResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlMortgageTips.Visible = False
            divMortgageTable.InnerHtml = ""
            Exit Sub
        End If

        ' Calculations
        Dim n As Integer = termYears * 12
        Dim r As Decimal = interestRate / 100 / 12
        Dim monthlyPayment As Decimal = loanAmt * r / (1 - Math.Pow(1 + r, -n))
        Dim totalPayment As Decimal = monthlyPayment * n
        Dim totalInterest As Decimal = totalPayment - loanAmt

        ' Display summary
        divMortgageResult.InnerHtml = String.Format(
        "Monthly Payment: <strong>${0:N2}</strong><br/>Total Payment: <strong>${1:N2}</strong><br/>Total Interest: <strong>${2:N2}</strong>",
        monthlyPayment, totalPayment, totalInterest
    )
        divMortgageResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Build amortization table (first 12 months)
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<table class='table table-bordered table-striped mt-2'><thead><tr><th>Month</th><th>Principal</th><th>Interest</th><th>Balance</th></tr></thead><tbody>")

        Dim balance As Decimal = loanAmt
        For i As Integer = 1 To Math.Min(12, n)
            Dim interestMonth As Decimal = balance * r
            Dim principalMonth As Decimal = monthlyPayment - interestMonth
            balance -= principalMonth
            If balance < 0 Then balance = 0
            sb.AppendFormat("<tr><td>{0}</td><td>${1:N2}</td><td>${2:N2}</td><td>${3:N2}</td></tr>", i, principalMonth, interestMonth, balance)
        Next
        sb.Append("</tbody></table>")

        divMortgageTable.InnerHtml = sb.ToString()

        ' Show tips panel
        pnlMortgageTips.Visible = True
    End Sub



    Protected Sub btnNPVCalc_Click(sender As Object, e As EventArgs) Handles btnNPVCalc.Click
        Dim initialInvestment As Decimal
        Dim futureValue As Decimal
        Dim discountRate As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtNPVInvestment.Text.Trim(), initialInvestment) OrElse initialInvestment <= 0 Then
            divNPVResult.InnerHtml = "Please enter a valid Initial Investment."
            divNPVResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlNPVTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtNPVFuture.Text.Trim(), futureValue) OrElse futureValue <= 0 Then
            divNPVResult.InnerHtml = "Please enter a valid Future Value."
            divNPVResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlNPVTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtNPVRate.Text.Trim(), discountRate) OrElse discountRate < 0 Then
            divNPVResult.InnerHtml = "Please enter a valid Discount Rate."
            divNPVResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlNPVTips.Visible = False
            Exit Sub
        End If

        ' Convert percentage to decimal
        Dim r As Decimal = discountRate / 100

        ' Calculate NPV
        Dim npv As Decimal = Math.Round(futureValue / (1 + r) - initialInvestment, 2)

        ' Display result
        divNPVResult.InnerHtml = String.Format(
        "Net Present Value (NPV): <strong>${0:N2}</strong>",
        npv
    )
        divNPVResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlNPVTips.Visible = True
    End Sub


    Protected Sub btnPetCalc_Click(sender As Object, e As EventArgs) Handles btnPetCalc.Click
        Dim weight As Decimal
        Dim dosePerKg As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtPetWeight.Text.Trim(), weight) OrElse weight <= 0 Then
            divPetResult.InnerHtml = "Please enter a valid Pet Weight."
            divPetResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPetTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtPetDose.Text.Trim(), dosePerKg) OrElse dosePerKg <= 0 Then
            divPetResult.InnerHtml = "Please enter a valid Medication Dose."
            divPetResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPetTips.Visible = False
            Exit Sub
        End If

        ' Calculate total dosage
        Dim totalDose As Decimal = Math.Round(weight * dosePerKg, 2)

        ' Display result
        divPetResult.InnerHtml = String.Format(
        "Total Dosage: <strong>{0} mg</strong>",
        totalDose
    )
        divPetResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlPetTips.Visible = True
    End Sub



    Protected Sub btnPhysicsCalc_Click(sender As Object, e As EventArgs) Handles btnPhysicsCalc.Click
        Dim mass As Decimal
        Dim acceleration As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtMass.Text.Trim(), mass) OrElse mass <= 0 Then
            divPhysicsResult.InnerHtml = "Please enter a valid Mass."
            divPhysicsResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPhysicsTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtAcceleration.Text.Trim(), acceleration) Then
            divPhysicsResult.InnerHtml = "Please enter a valid Acceleration."
            divPhysicsResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPhysicsTips.Visible = False
            Exit Sub
        End If

        ' Calculate force
        Dim force As Decimal = Math.Round(mass * acceleration, 2)

        ' Display result
        divPhysicsResult.InnerHtml = String.Format(
        "Force: <strong>{0} N</strong>",
        force
    )
        divPhysicsResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlPhysicsTips.Visible = True
    End Sub


    Protected Sub btnPriceProfitCalc_Click(sender As Object, e As EventArgs) Handles btnPriceProfitCalc.Click
        Dim cost As Decimal
        Dim salePrice As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtPriceCost.Text.Trim(), cost) OrElse cost < 0 Then
            divPriceProfitResult.InnerHtml = "Please enter a valid Cost."
            divPriceProfitResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPriceProfitTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtPriceSale.Text.Trim(), salePrice) OrElse salePrice < 0 Then
            divPriceProfitResult.InnerHtml = "Please enter a valid Sale Price."
            divPriceProfitResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPriceProfitTips.Visible = False
            Exit Sub
        End If

        ' Calculate profit and profit margin
        Dim profit As Decimal = Math.Round(salePrice - cost, 2)
        Dim margin As Decimal = If(salePrice = 0, 0, Math.Round((profit / salePrice) * 100, 2))

        ' Display result
        divPriceProfitResult.InnerHtml = String.Format(
        "Profit: <strong>${0:N2}</strong><br/>Profit Margin: <strong>{1}%</strong>",
        profit, margin
    )
        divPriceProfitResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlPriceProfitTips.Visible = True
    End Sub

    Protected Sub btnPregCalc_Click(sender As Object, e As EventArgs) Handles btnPregCalc.Click
        Dim weight As Decimal
        Dim height As Decimal
        Dim trimester As Integer

        ' Validate inputs
        If Not Decimal.TryParse(txtPregWeight.Text.Trim(), weight) OrElse weight <= 0 Then
            divPregResult.InnerHtml = "Please enter a valid Pre-pregnancy Weight."
            divPregResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPregTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtPregHeight.Text.Trim(), height) OrElse height <= 0 Then
            divPregResult.InnerHtml = "Please enter a valid Height."
            divPregResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPregTips.Visible = False
            Exit Sub
        End If

        If Not Integer.TryParse(txtPregTrimester.Text.Trim(), trimester) OrElse trimester < 1 OrElse trimester > 3 Then
            divPregResult.InnerHtml = "Please enter a valid Trimester (1, 2, or 3)."
            divPregResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlPregTips.Visible = False
            Exit Sub
        End If

        ' Base calorie calculation (simplified BMR)
        Dim baseCalories As Decimal = 22 * weight + 500 ' rough estimate

        ' Add trimester adjustment
        Select Case trimester
            Case 1
                baseCalories += 0
            Case 2
                baseCalories += 340
            Case 3
                baseCalories += 450
        End Select

        ' Display result
        divPregResult.InnerHtml = String.Format(
        "Estimated Daily Calories: <strong>{0:N0} kcal</strong>",
        baseCalories
    )
        divPregResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlPregTips.Visible = True
    End Sub



    Protected Sub btnProdCalc_Click(sender As Object, e As EventArgs) Handles btnProdCalc.Click
        Dim totalTasks As Integer
        Dim completedTasks As Integer

        ' Validate inputs
        If Not Integer.TryParse(txtProdTasks.Text.Trim(), totalTasks) OrElse totalTasks <= 0 Then
            divProdResult.InnerHtml = "Please enter a valid Total Tasks number."
            divProdResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlProdTips.Visible = False
            Exit Sub
        End If

        If Not Integer.TryParse(txtProdCompleted.Text.Trim(), completedTasks) OrElse completedTasks < 0 Then
            divProdResult.InnerHtml = "Please enter a valid Completed Tasks number."
            divProdResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlProdTips.Visible = False
            Exit Sub
        End If

        If completedTasks > totalTasks Then
            divProdResult.InnerHtml = "Completed Tasks cannot exceed Total Tasks."
            divProdResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlProdTips.Visible = False
            Exit Sub
        End If

        ' Calculate productivity %
        Dim productivity As Decimal = Math.Round((completedTasks / totalTasks) * 100, 2)

        ' Display result
        divProdResult.InnerHtml = String.Format(
        "Productivity: <strong>{0}%</strong>",
        productivity
    )
        divProdResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlProdTips.Visible = True
    End Sub

    Protected Sub btnQuadCalc_Click(sender As Object, e As EventArgs) Handles btnQuadCalc.Click
        Dim a As Decimal
        Dim b As Decimal
        Dim c As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtQuadA.Text.Trim(), a) OrElse a = 0 Then
            divQuadResult.InnerHtml = "Coefficient 'a' must be a non-zero number."
            divQuadResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlQuadTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtQuadB.Text.Trim(), b) Then
            divQuadResult.InnerHtml = "Please enter a valid coefficient 'b'."
            divQuadResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlQuadTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtQuadC.Text.Trim(), c) Then
            divQuadResult.InnerHtml = "Please enter a valid coefficient 'c'."
            divQuadResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlQuadTips.Visible = False
            Exit Sub
        End If

        ' Calculate discriminant
        Dim discriminant As Decimal = b * b - 4 * a * c
        Dim resultText As String = ""

        If discriminant > 0 Then
            Dim root1 As Decimal = Math.Round((-b + Math.Sqrt(discriminant)) / (2 * a), 2)
            Dim root2 As Decimal = Math.Round((-b - Math.Sqrt(discriminant)) / (2 * a), 2)
            resultText = String.Format("Two real roots: <strong>{0}</strong> and <strong>{1}</strong>", root1, root2)
        ElseIf discriminant = 0 Then
            Dim root As Decimal = Math.Round(-b / (2 * a), 2)
            resultText = String.Format("One real root: <strong>{0}</strong>", root)
        Else
            ' Complex roots
            Dim realPart As Decimal = Math.Round(-b / (2 * a), 2)
            Dim imaginaryPart As Decimal = Math.Round(Math.Sqrt(-discriminant) / (2 * a), 2)
            resultText = String.Format("Two complex roots: <strong>{0} + {1}i</strong> and <strong>{0} - {1}i</strong>", realPart, imaginaryPart)
        End If

        ' Display result
        divQuadResult.InnerHtml = resultText
        divQuadResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlQuadTips.Visible = True
    End Sub

    Protected Sub btnRetCalc_Click(sender As Object, e As EventArgs) Handles btnRetCalc.Click
        Dim currentSavings As Decimal
        Dim monthlyContribution As Decimal
        Dim years As Decimal
        Dim annualRate As Decimal = 0.05D ' 5% default annual interest

        ' Validate inputs
        If Not Decimal.TryParse(txtRetSavings.Text.Trim(), currentSavings) OrElse currentSavings < 0 Then
            divRetResult.InnerHtml = "Please enter a valid Current Savings amount."
            divRetResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlRetTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtRetContribution.Text.Trim(), monthlyContribution) OrElse monthlyContribution < 0 Then
            divRetResult.InnerHtml = "Please enter a valid Monthly Contribution."
            divRetResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlRetTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtRetYears.Text.Trim(), years) OrElse years <= 0 Then
            divRetResult.InnerHtml = "Please enter a valid number of Years to Retirement."
            divRetResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlRetTips.Visible = False
            Exit Sub
        End If

        ' Monthly rate and months
        Dim monthlyRate As Decimal = annualRate / 12
        Dim months As Integer = CInt(years * 12)

        ' Future value calculation
        Dim futureValue As Decimal = currentSavings * CDec(Math.Pow(1 + monthlyRate, months)) +
                                 monthlyContribution * ((CDec(Math.Pow(1 + monthlyRate, months)) - 1) / monthlyRate)

        ' Display result
        divRetResult.InnerHtml = String.Format(
        "Estimated Retirement Savings: <strong>${0:N2}</strong>",
        futureValue
    )
        divRetResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlRetTips.Visible = True
    End Sub


    Protected Sub btnROICalc_Click(sender As Object, e As EventArgs) Handles btnROICalc.Click
        Dim initialInvest As Decimal
        Dim finalValue As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtROIInit.Text.Trim(), initialInvest) OrElse initialInvest <= 0 Then
            divROIResult.InnerHtml = "Please enter a valid Initial Investment."
            divROIResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlROITips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtROIFinalVal.Text.Trim(), finalValue) Then
            divROIResult.InnerHtml = "Please enter a valid Final Value."
            divROIResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlROITips.Visible = False
            Exit Sub
        End If

        ' Calculate ROI %
        Dim roi As Decimal = Math.Round(((finalValue - initialInvest) / initialInvest) * 100, 2)

        ' Display result
        divROIResult.InnerHtml = String.Format("Return on Investment: <strong>{0}%</strong>", roi)
        divROIResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlROITips.Visible = True
    End Sub

    Protected Sub btnGoalAmountCalc_Click(sender As Object, e As EventArgs) Handles btnGoalAmountCalc.Click
        Dim goalAmount As Decimal
        Dim monthlySavings As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtGoalAmount.Text.Trim(), goalAmount) OrElse goalAmount <= 0 Then
            divGoalAmountResult.InnerHtml = "Please enter a valid Goal Amount."
            divGoalAmountResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlGoalTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtGoalMonthly.Text.Trim(), monthlySavings) OrElse monthlySavings <= 0 Then
            divGoalAmountResult.InnerHtml = "Please enter a valid Monthly Savings amount."
            divGoalAmountResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlGoalTips.Visible = False
            Exit Sub
        End If

        ' Calculate months needed
        Dim monthsNeeded As Integer = CInt(Math.Ceiling(goalAmount / monthlySavings))

        ' Display result
        divGoalAmountResult.InnerHtml = String.Format(
        "You will need <strong>{0}</strong> month(s) to reach your savings goal.",
        monthsNeeded
    )
        divGoalAmountResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlGoalTips.Visible = True
    End Sub


    Protected Sub btnSleepCalc_Click(sender As Object, e As EventArgs) Handles btnSleepCalc.Click
        Dim age As Integer

        ' Validate input
        If Not Integer.TryParse(txtSleepAge.Text.Trim(), age) OrElse age < 0 Then
            divSleepResult.InnerHtml = "Please enter a valid age."
            divSleepResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlSleepTips.Visible = False
            Exit Sub
        End If

        ' Determine recommended sleep
        Dim recommendedSleep As String = ""
        Select Case age
            Case 0 To 3
                recommendedSleep = "14-17 hours/day"
            Case 4 To 11
                recommendedSleep = "12-15 hours/day"
            Case 12 To 13
                recommendedSleep = "9-11 hours/day"
            Case 14 To 17
                recommendedSleep = "8-10 hours/day"
            Case 18 To 64
                recommendedSleep = "7-9 hours/day"
            Case Else
                recommendedSleep = "7-8 hours/day"
        End Select

        ' Display result
        divSleepResult.InnerHtml = String.Format(
        "Recommended Sleep: <strong>{0}</strong>",
        recommendedSleep
    )
        divSleepResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlSleepTips.Visible = True
    End Sub


    Protected Sub btnStockCalc_Click(sender As Object, e As EventArgs) Handles btnStockCalc.Click
        Dim initialInvest As Decimal
        Dim finalValue As Decimal

        ' Validate inputs
        If Not Decimal.TryParse(txtStockInit.Text.Trim(), initialInvest) OrElse initialInvest <= 0 Then
            divStockResult.InnerHtml = "Please enter a valid Initial Investment."
            divStockResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlStockTips.Visible = False
            Exit Sub
        End If

        If Not Decimal.TryParse(txtStockFinal.Text.Trim(), finalValue) Then
            divStockResult.InnerHtml = "Please enter a valid Final Value."
            divStockResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlStockTips.Visible = False
            Exit Sub
        End If

        ' Calculate ROI %
        Dim roi As Decimal = Math.Round(((finalValue - initialInvest) / initialInvest) * 100, 2)

        ' Display result
        divStockResult.InnerHtml = String.Format("Stock Investment ROI: <strong>{0}%</strong>", roi)
        divStockResult.Attributes("class") = "fw-bold fs-5 text-success history-box"

        ' Show tips
        pnlStockTips.Visible = True
    End Sub


    Protected Sub btnStudyCalc_Click(sender As Object, e As EventArgs) Handles btnStudyCalc.Click
        Dim totalHours As Decimal
        Dim subjectsText As String = txtStudySubjects.Text.Trim()

        ' Validate total hours
        If Not Decimal.TryParse(txtStudyTotalHours.Text.Trim(), totalHours) OrElse totalHours <= 0 Then
            divStudyResult.InnerHtml = "Please enter a valid number of total hours."
            divStudyResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlStudyTips.Visible = False
            Exit Sub
        End If

        ' Validate subjects
        If String.IsNullOrWhiteSpace(subjectsText) Then
            divStudyResult.InnerHtml = "Please enter at least one subject."
            divStudyResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlStudyTips.Visible = False
            Exit Sub
        End If

        Dim subjects() As String = subjectsText.Split(","c).Select(Function(s) s.Trim()).Where(Function(s) s <> "").ToArray()
        If subjects.Length = 0 Then
            divStudyResult.InnerHtml = "Please enter at least one valid subject."
            divStudyResult.Attributes("class") = "text-danger fw-bold history-box"
            pnlStudyTips.Visible = False
            Exit Sub
        End If

        ' Calculate hours per subject
        Dim hoursPerSubject As Decimal = Math.Round(totalHours / subjects.Length, 2)

        ' Build result string
        Dim resultHtml As New System.Text.StringBuilder()
        resultHtml.AppendLine("<ul>")
        For Each subject In subjects
            resultHtml.AppendLine(String.Format("<li>{0}: <strong>{1} hours</strong></li>", subject, hoursPerSubject))
        Next
        resultHtml.AppendLine("</ul>")

        ' Display result
        divStudyResult.InnerHtml = resultHtml.ToString()
        divStudyResult.Attributes("class") = "fw-bold fs-6 text-success history-box"

        ' Show tips
        pnlStudyTips.Visible = True
    End Sub


    ' -------------------------
    ' 32. TDEE Calculator
    ' -------------------------
    Protected Sub btnTDEECalc_Click(sender As Object, e As EventArgs)
        Dim weight, height, age, activity As Double
        Dim gender As String = txtTDEEGender.Text.Trim().ToUpper()

        If Double.TryParse(txtTDEEWeight.Text, weight) AndAlso
           Double.TryParse(txtTDEEHeight.Text, height) AndAlso
           Double.TryParse(txtTDEEAge.Text, age) AndAlso
           Double.TryParse(txtTDEEActivity.Text, activity) Then

            Dim bmr As Double
            If gender = "M" Then
                bmr = 10 * weight + 6.25 * height - 5 * age + 5
            ElseIf gender = "F" Then
                bmr = 10 * weight + 6.25 * height - 5 * age - 161
            Else
                divTDEResult.InnerHtml = "<span class='text-danger'>Invalid gender.</span>"
                Return
            End If

            Dim tdee As Double = bmr * activity
            divTDEResult.InnerHtml = $"Your TDEE is <strong>{tdee:F0} kcal/day</strong>."
        Else
            divTDEResult.InnerHtml = "<span class='text-danger'>Invalid inputs.</span>"
        End If
    End Sub

    ' -------------------------
    ' 33. Travel / Fuel Cost Calculator
    ' -------------------------
    Protected Sub btnTravelCalc_Click(sender As Object, e As EventArgs)
        Dim distance, efficiency, fuelPrice As Double
        If Double.TryParse(txtTravelDistance.Text, distance) AndAlso
           Double.TryParse(txtTravelEfficiency.Text, efficiency) AndAlso
           Double.TryParse(txtTravelFuelPrice.Text, fuelPrice) AndAlso efficiency <> 0 Then

            Dim cost As Double = (distance / efficiency) * fuelPrice
            divTravelResult.InnerHtml = $"Estimated fuel cost: <strong>{cost:C2}</strong>"
        Else
            divTravelResult.InnerHtml = "<span class='text-danger'>Invalid inputs or efficiency = 0.</span>"
        End If
    End Sub

    ' -------------------------
    ' 34. Unit Conversion Calculator
    ' -------------------------
    Protected Sub btnUnitConvert_Click(sender As Object, e As EventArgs)
        Dim value As Double
        Dim fromUnit As String = txtUnitFrom.Text.Trim().ToLower()
        Dim toUnit As String = txtUnitTo.Text.Trim().ToLower()

        If Double.TryParse(txtUnitValue.Text, value) Then
            Dim result As Double = value

            ' Example conversion: kg <-> g, m <-> cm
            Select Case fromUnit & "_" & toUnit
                Case "kg_g"
                    result = value * 1000
                Case "g_kg"
                    result = value / 1000
                Case "m_cm"
                    result = value * 100
                Case "cm_m"
                    result = value / 100
                Case Else
                    divUnitResult.InnerHtml = "<span class='text-danger'>Conversion not defined.</span>"
                    Return
            End Select

            divUnitResult.InnerHtml = $"Converted value: <strong>{result}</strong> {toUnit}"
        Else
            divUnitResult.InnerHtml = "<span class='text-danger'>Invalid value.</span>"
        End If
    End Sub

    ' -------------------------
    ' 35. VAT / Tax Calculator
    ' -------------------------
    Protected Sub btnVATCalc_Click(sender As Object, e As EventArgs)
        Dim price, rate As Double
        If Double.TryParse(txtVATPrice.Text, price) AndAlso Double.TryParse(txtVATRate.Text, rate) Then
            Dim vat As Double = price * rate / 100
            divVATResult.InnerHtml = $"VAT/Tax: <strong>{vat:C2}</strong> | Total: <strong>{price + vat:C2}</strong>"
        Else
            divVATResult.InnerHtml = "<span class='text-danger'>Invalid inputs.</span>"
        End If
    End Sub

    ' -------------------------
    ' 37. Pomodoro / Focus Timer
    ' -------------------------
    Protected Sub btnPomodoroStart_Click(sender As Object, e As EventArgs)
        Dim workMin, breakMin As Integer
        If Integer.TryParse(txtPomWork.Text, workMin) AndAlso Integer.TryParse(txtPomBreak.Text, breakMin) Then
            divPomResult.InnerHtml = $"Pomodoro started: Work {workMin} min, Break {breakMin} min."
        Else
            divPomResult.InnerHtml = "<span class='text-danger'>Invalid inputs.</span>"
        End If
    End Sub

    ' -------------------------
    ' 37. Water Intake Calculator
    ' -------------------------
    Protected Sub btnWaterCalc_Click(sender As Object, e As EventArgs)
        Dim weight As Double
        If Double.TryParse(txtWaterWeight.Text, weight) Then
            Dim liters As Double = weight * 0.033
            divWaterResult.InnerHtml = $"Recommended water intake: <strong>{liters:F2} liters/day</strong>."
        Else
            divWaterResult.InnerHtml = "<span class='text-danger'>Invalid weight.</span>"
        End If
    End Sub

    ' -------------------------
    ' 38. Chemistry Molarity Calculator
    ' -------------------------
    Protected Sub btnChemCalc_Click(sender As Object, e As EventArgs)
        Dim moles, volume As Double
        If Double.TryParse(txtChemMoles.Text, moles) AndAlso Double.TryParse(txtChemVolume.Text, volume) AndAlso volume <> 0 Then
            Dim molarity As Double = moles / volume
            divChemResult.InnerHtml = $"Molarity: <strong>{molarity:F2} M</strong>"
        Else
            divChemResult.InnerHtml = "<span class='text-danger'>Invalid inputs or volume = 0.</span>"
        End If
    End Sub

    ' -------------------------
    ' 39. Body Surface Area (BSA) Calculator
    ' -------------------------
    Protected Sub btnBSACalc_Click(sender As Object, e As EventArgs)
        Dim weight, height As Double
        If Double.TryParse(txtBSAWeight.Text, weight) AndAlso Double.TryParse(txtBSAHeight.Text, height) Then
            Dim bsa As Double = Math.Sqrt((weight * height) / 3600)
            divBSAResult.InnerHtml = $"Body Surface Area: <strong>{bsa:F2} m²</strong>"
        Else
            divBSAResult.InnerHtml = "<span class='text-danger'>Invalid inputs.</span>"
        End If
    End Sub

End Class
