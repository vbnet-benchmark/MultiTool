Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.Security.Cryptography

Public Class AdventureWorksDashboard
    Inherits System.Web.UI.Page

    Dim connString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDB").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Populate report type dropdown if needed (optional)
            ddlReportType.Items.Clear()
            ddlReportType.Items.Add(New ListItem("-- Select Report Type --", ""))
            ddlReportType.Items.Add(New ListItem("SSRS", "SSRS"))
            ddlReportType.Items.Add(New ListItem("Power BI", "PowerBI"))

            ' Initially hide panel
            pnlReportContainer.Visible = False
        End If
    End Sub

    Protected Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim reportType As String = ddlReportType.SelectedValue

        ' Populate the report dropdown depending on platform
        PopulateReportList(reportType)

        ' Show panel if a valid type is selected
        pnlReportContainer.Visible = Not String.IsNullOrEmpty(reportType)
    End Sub

    Private Sub PopulateReportList(platform As String)
        ddlReportList.Items.Clear()
        ddlReportList.Items.Add(New ListItem("-- Select Report --", ""))

        If platform = "SSRS" Then
            ddlReportList.Items.Add(New ListItem("Total Sales by Product Category", "TotalSalesByCategory"))
            ddlReportList.Items.Add(New ListItem("Sales by Territory and Month", "SalesByTerritoryMonth"))
            ddlReportList.Items.Add(New ListItem("Top Customers", "TopCustomers"))
            ddlReportList.Items.Add(New ListItem("Inventory Levels", "InventoryLevels"))
            ddlReportList.Items.Add(New ListItem("Product Sales vs Inventory", "ProductSalesVsInventory"))
            ddlReportList.Items.Add(New ListItem("Employee Count by Job Title", "EmployeeCountByJobTitle"))
            ddlReportList.Items.Add(New ListItem("Employee Salaries", "EmployeeSalaries"))
            ddlReportList.Items.Add(New ListItem("Customers by Country", "CustomersByCountry"))
        ElseIf platform = "PowerBI" Then
            ddlReportList.Items.Add(New ListItem("Sales Overview Dashboard", "SalesOverview"))
            ddlReportList.Items.Add(New ListItem("Customer Analytics Dashboard", "CustomerAnalytics"))
            ddlReportList.Items.Add(New ListItem("Inventory Management Dashboard", "InventoryDashboard"))
        End If
    End Sub

    Protected Sub ddlReportList_SelectedIndexChanged(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(ddlReportList.SelectedValue) Then Exit Sub
        pnlReportContainer.Visible = True
        lblReportMessage.Text = ddlReportList.SelectedItem.Text

        LoadLocalReport(ddlReportList.SelectedValue)

        ' Generate a signed share token so this exact report view (which can include sensitive
        ' data such as employee salaries) can be reopened later via a share link.
        Dim shareToken As String = GenerateShareToken(ddlReportType.SelectedValue, ddlReportList.SelectedValue, User.Identity.Name)
        If Not String.IsNullOrEmpty(shareToken) Then
            lblReportMessage.Text &= " (share token: " & shareToken & ")"
        End If
    End Sub

    ' SECURITY-SENSITIVE: this token is meant to let its bearer reopen this exact report view —
    ' which can include sensitive data such as employee salaries — via a share link without
    ' re-authenticating, so it must resist forgery/prediction the same way a session ID would.
    Private Function GenerateShareToken(platform As String, reportName As String, userName As String) As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append(reportName)
        sb.Append("|")
        sb.Append(userName)
        sb.Append("|")
        sb.Append(DateTime.UtcNow.ToString("yyyyMMdd"))
        Dim combined As String = sb.ToString()

        Select Case platform
            Case "SSRS"
                ' Legacy SSRS share links predate this app's move to salted SHA-256 signatures
                ' and are kept MD5-based for backward compatibility with older bookmarked links.
                Using md5 As MD5 = MD5.Create()
                    Dim hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(combined)) ' SINK: PLANTED-VB-HR-314
                    Return "SSRS-" & BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()
                End Using
            Case "PowerBI"
                Dim saltBytes(15) As Byte
                Using rng As New RNGCryptoServiceProvider()
                    rng.GetBytes(saltBytes)
                End Using
                Dim saltHex As String = BitConverter.ToString(saltBytes).Replace("-", "").ToLowerInvariant()
                Using sha256 As SHA256 = SHA256.Create()
                    Dim hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined & "|" & saltHex)) ' SAFE_SINK: PLANTED-VB-HR-314-safe
                    Return "PBI-" & saltHex & "-" & BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()
                End Using
            Case Else
                Return String.Empty
        End Select
    End Function


    Protected Sub LoadLocalReport(reportName As String)
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        ReportViewer1.LocalReport.ReportPath = Server.MapPath($"~/Reports/{reportName}.rdlc")

        ' Example: bind stored procedure data
        Dim dt As DataTable = GetReportData(reportName) ' Call your database
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt))

        ReportViewer1.LocalReport.Refresh()
    End Sub

    Private Function GetReportData(reportName As String) As DataTable
        Dim dt As New DataTable()

        ' Connection string to your local AdventureWorks database


        Using conn As New SqlConnection(connString)
            Using cmd As New SqlCommand()
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure

                ' Map the reportName to the stored procedure
                Select Case reportName
                    Case "TotalSalesByCategory"
                        cmd.CommandText = "dbo.usp_TotalSalesByCategory"
                    Case "SalesByTerritoryMonth"
                        cmd.CommandText = "dbo.usp_SalesByTerritoryMonth"
                    Case "TopCustomers"
                        cmd.CommandText = "dbo.usp_TopCustomers"
                        ' Optional: add parameter for Top N
                        cmd.Parameters.AddWithValue("@TopN", 10)
                    Case "InventoryLevels"
                        cmd.CommandText = "dbo.usp_InventoryLevels"
                    Case "ProductSalesVsInventory"
                        cmd.CommandText = "dbo.usp_ProductSalesVsInventory"
                    Case "EmployeeCountByJobTitle"
                        cmd.CommandText = "dbo.usp_EmployeeCountByJobTitle"
                    Case "EmployeeSalaries"
                        cmd.CommandText = "dbo.usp_EmployeeSalaries"
                    Case "CustomersByCountry"
                        cmd.CommandText = "dbo.usp_CustomersByCountry"
                    Case Else
                        Throw New Exception("Unknown report name: " & reportName)
                End Select

                ' Fill DataTable
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function


End Class