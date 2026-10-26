Imports System.IO
Imports HtmlToOpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports iText.Html2pdf
Imports SelectPdf
Imports System.Security.Cryptography

Public Class ResumePortfolioGenerator
    Inherits System.Web.UI.Page


    Protected Sub btnGenerateTestData_Click(sender As Object, e As EventArgs) Handles btnGenerateTestData.Click
        FillSampleData()
        ddlTemplate_SelectedIndexChanged(ddlTemplate, EventArgs.Empty) ' Optional: show initial previe
    End Sub

    ' ================== PDF ==================

    Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs) Handles btnGeneratePDF.Click
        Try
            ' 1️⃣ Get HTML content from preview
            Dim html As String = "<html><head><meta charset='utf-8'></head><body>" & resumePreview.InnerHtml & "</body></html>"

            ' 2️⃣ Create HTML to PDF converter
            Dim converter As New HtmlToPdf()

            ' 3️⃣ Optional: set page size and margins
            converter.Options.PdfPageSize = PdfPageSize.A4
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait
            converter.Options.MarginTop = 20
            converter.Options.MarginBottom = 20
            converter.Options.MarginLeft = 15
            converter.Options.MarginRight = 15
            '   converter.Options.DisplayBackgroundImages = True ' WYSIWYG background

            ' 4️⃣ Generate PDF in memory
            Dim pdfDoc As PdfDocument = converter.ConvertHtmlString(html)

            ' 5️⃣ Send PDF to browser
            Dim certificate As String = GenerateDocumentCertificate(txtSkills.Text, txtProjects.Text, txtFullName.Text)
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("X-Resume-Certificate", certificate)
            Response.AddHeader("Content-Disposition", $"attachment; filename={txtFullName.Text.Replace(" ", "_")}_Resume.pdf")
            pdfDoc.Save(Response.OutputStream)
            pdfDoc.Close()
            'Response.End()
            HttpContext.Current.ApplicationInstance.CompleteRequest()


        Catch ex As Exception
            lblMessage.Text = "Error generating PDF: " & ex.Message
        End Try
    End Sub


    ' ================== DOCX ==================

    Protected Sub btnGenerateDocx_Click(sender As Object, e As EventArgs) Handles btnGenerateDocx.Click
        Dim html As String = resumePreview.InnerHtml
        If String.IsNullOrWhiteSpace(html) Then
            lblMessage.Text = "Nothing in preview to export."
            Return
        End If

        Using ms As New MemoryStream()
            Using wordDoc = WordprocessingDocument.Create(ms, DocumentFormat.OpenXml.WordprocessingDocumentType.Document)
                Dim mainPart = wordDoc.AddMainDocumentPart()
                mainPart.Document = New Document(New Body())

                ' Use HtmlToOpenXml converter
                Dim converter As New HtmlToOpenXml.HtmlConverter(mainPart)
                Dim paragraphs = converter.Parse(html) ' returns IEnumerable(Of Paragraph)
                For Each p In paragraphs
                    mainPart.Document.Body.Append(p)
                Next

                mainPart.Document.Save()
            End Using

            ms.Position = 0
            Dim certificate As String = GenerateDocumentCertificate(txtSkills.Text, txtProjects.Text, txtFullName.Text)
            Response.Clear()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            Response.AddHeader("X-Resume-Certificate", certificate)
            Response.AddHeader("Content-Disposition", $"attachment; filename={txtFullName.Text}_Resume.docx")
            Response.BinaryWrite(ms.ToArray())
            Response.Flush()
            'Response.End()
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        End Using
    End Sub

    ' ================== TXT ==================

    Protected Sub btnGenerateText_Click(sender As Object, e As EventArgs) Handles btnGenerateText.Click
        Try
            Dim text As String = HtmlToPlainText(resumePreview.InnerHtml)
            Dim certificate As String = GenerateSecureDocumentCertificate(txtSkills.Text, txtProjects.Text, txtFullName.Text)

            Response.Clear()
            Response.ContentType = "text/plain"
            Response.AddHeader("X-Resume-Certificate", certificate)
            Response.AddHeader("Content-Disposition", $"attachment; filename={txtFullName.Text.Replace(" ", "_")}_Resume.txt")
            Response.Write(text)
            'Response.End()
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            lblMessage.Text = "Error generating TXT: " & ex.Message
        End Try
    End Sub


    ' ========== Helper Functions ==========

    ' Minimal HTML to plain text conversion
    Private Function HtmlToPlainText(html As String) As String
        Dim text As String = System.Text.RegularExpressions.Regex.Replace(html, "<br\s*/?>", vbCrLf, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        text = System.Text.RegularExpressions.Regex.Replace(text, "<[^>]+>", "")
        Return System.Net.WebUtility.HtmlDecode(text)
    End Function

    ' SECURITY-SENSITIVE: this certificate is attached to every generated document (as the
    ' X-Resume-Certificate header) so a recipient can confirm the skills/projects/name content
    ' was not altered after generation — a forgeable certificate defeats that tamper-evidence
    ' guarantee. Used for the PDF and DOCX export paths.
    Private Function GenerateDocumentCertificate(skillsText As String, projectsText As String, fullName As String) As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append(fullName)

        For Each line In skillsText.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            sb.Append("|skill:")
            sb.Append(line.Trim())
        Next

        For Each line In projectsText.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            sb.Append("|project:")
            sb.Append(line.Trim())
        Next

        Using md5 As MD5 = MD5.Create()
            Dim hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())) ' SINK: PLANTED-VB-HR-315
            Return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()
        End Using
    End Function

    ' SECURITY-SENSITIVE: same purpose as GenerateDocumentCertificate above, for the plain-text
    ' export path — salted with a fresh random value per call and hashed with SHA-256.
    Private Function GenerateSecureDocumentCertificate(skillsText As String, projectsText As String, fullName As String) As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append(fullName)

        For Each line In skillsText.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            sb.Append("|skill:")
            sb.Append(line.Trim())
        Next

        For Each line In projectsText.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            sb.Append("|project:")
            sb.Append(line.Trim())
        Next

        Dim saltBytes(15) As Byte
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(saltBytes)
        End Using
        Dim saltHex As String = BitConverter.ToString(saltBytes).Replace("-", "").ToLowerInvariant()

        Using sha256 As SHA256 = SHA256.Create()
            Dim hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString() & "|" & saltHex)) ' SAFE_SINK: PLANTED-VB-HR-315-safe
            Return saltHex & ":" & BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()
        End Using
    End Function




    Private Sub FillSampleData()
        ' Personal Info
        txtFullName.Text = "John Doe"
        txtEmail.Text = "john.doe@example.com"
        txtPhone.Text = "(123) 456-7890"
        txtLocation.Text = "New York, NY"

        ' Work Experience (one per line)
        txtExperience.Text = "Software Engineer | ABC Corp | Jan 2020 - Present | Developed enterprise web applications" & vbCrLf &
                         "Junior Developer | XYZ Inc | Jun 2018 - Dec 2019 | Assisted in building client-facing dashboards"

        ' Education (one per line)
        txtEducation.Text = "B.Sc. Computer Science | University of Example | 2014 - 2018"

        ' Skills (Skill:Level 1-5, one per line)
        txtSkills.Text = "C#" & vbCrLf &
                     "ASP.NET" & vbCrLf &
                     "JavaScript" & vbCrLf &
                     "SQL" & vbCrLf &
                     "Communication"

        ' Projects (Project Name | Description | URL)
        txtProjects.Text = "Portfolio Website | Personal portfolio website with projects and blog | https://example.com" & vbCrLf &
                       "Task Manager App | Cross-platform task management tool | https://github.com/example/taskmanager"

        ' Additional notes
        txtAdditional.Text = "Certifications: Microsoft Certified Professional" & vbCrLf &
                         "Awards: Employee of the Year 2022" & vbCrLf &
                         "Hobbies: Hiking, Photography"

        ' Set default template
        'ddlTemplate.SelectedIndex = 1 ' Classic
    End Sub





    ' Handle template selection
    Protected Sub ddlTemplate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTemplate.SelectedIndexChanged
        resumePreview.InnerHtml = Nothing
        Select Case ddlTemplate.SelectedIndex
            Case 0
                RenderClassicTemplate()
            Case 1
                RenderModernTemplate()
            Case 2
                RenderPortfolioTemplate()
            Case 3
                RenderCreativeTemplate()
            Case 4
                RenderExecutiveTemplate()
            Case 5
                RenderTechFocusTemplate()
            Case 6
                RenderAcademicTemplate()
            Case 7
                RenderStartupTemplate()
            Case 8
                RenderMinimalistTemplate()
            Case 9
                RenderHybridTemplate()
        End Select
    End Sub
    Private Function SplitLines(text As String) As String()
        If String.IsNullOrWhiteSpace(text) Then Return New String() {}
        ' Normalize line endings to LF
        Dim normalized As String = text.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)
        Return normalized.Split({vbLf}, StringSplitOptions.RemoveEmptyEntries)
    End Function


    ' ===== Classic Template =====
    Private Sub RenderClassicTemplate()
        Dim html As New StringBuilder()

        html.AppendLine("<div style='font-family:Georgia, serif; color:#222; line-height:1.5; padding:20px;'>")
        html.AppendLine($"<h1 style='font-size:32px; margin-bottom:5px; color:#333;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:15px; color:#555;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Experience
        html.AppendLine("<h2 style='border-bottom:1px solid #ccc; padding-bottom:3px;'>Experience</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Education
        html.AppendLine("<h2 style='border-bottom:1px solid #ccc; padding-bottom:3px;'>Education</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<li>{edu}</li>")
        Next
        html.AppendLine("</ul>")

        ' Skills
        html.AppendLine("<h2 style='border-bottom:1px solid #ccc; padding-bottom:3px;'>Skills</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")


        For Each skill In SplitLines(txtSkills.Text)
            'For Each skill In txtSkills.Text.Split("vbCrLf", StringSplitOptions.RemoveEmptyEntries)
            Dim parts = skill.Split(":"c)
            Dim name = parts(0).Trim()
            'Dim level = If(parts.Length > 1, Integer.Parse(parts(1).Trim()), 3)
            html.AppendLine($"<li>{name}</li>")
        Next
        html.AppendLine("</ul>")

        ' Projects
        html.AppendLine("<h2 style='border-bottom:1px solid #ccc; padding-bottom:3px;'>Projects</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<li><strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</li>")
        Next
        html.AppendLine("</ul>")

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='border-bottom:1px solid #ccc; padding-bottom:3px;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    ' ===== Modern Template =====
    Private Sub RenderModernTemplate()
        Dim html As New StringBuilder()
        html.AppendLine("<div style='font-family:Arial, sans-serif; background:#f9f9f9; padding:20px; border-radius:8px;'>")
        html.AppendLine($"<h1 style='margin-bottom:5px; color:#222;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<h3 style='margin-top:0; margin-bottom:15px; color:#555;'>{txtLocation.Text} | {txtEmail.Text} | {txtPhone.Text}</h3>")

        ' Experience as cards
        html.AppendLine("<div style='margin-bottom:15px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<div style='background:white; border:1px solid #ddd; padding:10px; margin-bottom:5px; border-radius:5px;'>{exp}</div>")
        Next
        html.AppendLine("</div>")

        ' Education
        html.AppendLine("<div style='margin-bottom:15px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<div style='background:white; border-left:4px solid #007bff; padding:8px; margin-bottom:5px; border-radius:3px;'>{edu}</div>")
        Next
        html.AppendLine("</div>")

        ' Skills as badges
        html.AppendLine("<div style='margin-bottom:15px;'>")
        For Each skill In SplitLines(txtSkills.Text)
            Dim parts = skill.Split(":"c)
            Dim name = parts(0).Trim()
            'Dim level = If(parts.Length > 1, parts(1).Trim(), "3")
            html.AppendLine($"<span style='background:#007bff; color:white; padding:5px 10px; border-radius:3px; margin:2px; display:inline-block;'>{name}</span>")
        Next
        html.AppendLine("</div>")

        ' Projects
        html.AppendLine("<div style='margin-bottom:15px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<div style='background:white; border:1px solid #ccc; padding:10px; margin-bottom:5px; border-radius:5px;'><strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</div>")
        Next
        html.AppendLine("</div>")

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<div style='margin-top:10px;'><strong>Additional:</strong> {txtAdditional.Text}</div>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    ' ===== Portfolio Template =====
    Private Sub RenderPortfolioTemplate()
        Dim html As New StringBuilder()
        html.AppendLine("<div style='font-family:Helvetica, sans-serif; padding:20px;'>")
        html.AppendLine($"<h1 style='margin-bottom:5px; color:#222;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:15px; color:#555;'>{txtLocation.Text} | {txtEmail.Text} | {txtPhone.Text}</p>")

        ' Projects as portfolio cards
        html.AppendLine("<div style='display:flex; flex-wrap:wrap; gap:15px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $"<br/><a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine("<div style='flex:1 1 30%; border:1px solid #ccc; padding:10px; border-radius:6px; box-shadow:0 2px 4px rgba(0,0,0,0.1);'>")
            html.AppendLine($"<h4 style='margin:0 0 5px 0;'>{parts(0).Trim()}</h4>")
            html.AppendLine($"<p>{parts(1).Trim()}{url}</p>")
            html.AppendLine("</div>")
        Next
        html.AppendLine("</div>")

        ' Skills as horizontal bars
        html.AppendLine("<h2 style='margin-top:20px;'>Skills</h2>")
        For Each skill In SplitLines(txtSkills.Text)
            Dim parts = skill.Split(":"c)
            Dim name = parts(0).Trim()
            'Dim level = If(parts.Length > 1, Integer.Parse(parts(1).Trim()), 3)
            html.AppendLine($"<div style='margin-bottom:5px;'><strong>{name}</strong>")
            'html.AppendLine($"<div style='height:10px; width:100%; background:#eee; border-radius:3px;'>")
            'html.AppendLine($"<div style='width:100%; height:100%; background:#007bff; border-radius:3px;'></div>")
            html.AppendLine("</div></div>")
        Next

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<div style='margin-top:15px;'><strong>Additional:</strong> {txtAdditional.Text}</div>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    Private Sub RenderCreativeTemplate()
        Dim html As New StringBuilder()

        html.AppendLine("<div style='font-family:Arial, sans-serif; color:#222; padding:25px; background:#fdf6f0;'>")
        html.AppendLine($"<h1 style='font-size:34px; color:#d35400; margin-bottom:5px;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:20px; color:#555;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Experience
        html.AppendLine("<h2 style='border-bottom:2px solid #d35400; padding-bottom:3px; color:#d35400;'>Experience</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Education
        html.AppendLine("<h2 style='border-bottom:2px solid #d35400; padding-bottom:3px; color:#d35400;'>Education</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<li>{edu}</li>")
        Next
        html.AppendLine("</ul>")

        ' Skills with colored bars
        html.AppendLine("<h2 style='border-bottom:2px solid #d35400; padding-bottom:3px; color:#d35400;'>Skills</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each skill In SplitLines(txtSkills.Text)
            Dim parts = skill.Split(":"c)
            Dim name = parts(0).Trim()
            'Dim level = If(parts.Length > 1, Integer.Parse(parts(1).Trim()), 3)
            html.AppendLine($"<li>{name} <span style='display:inline-block; height:12px; width:100%; background:#e67e22; margin-left:5px; vertical-align:middle; border-radius:3px;'></span></li>")
        Next
        html.AppendLine("</ul>")

        ' Projects as cards
        html.AppendLine("<h2 style='border-bottom:2px solid #d35400; padding-bottom:3px; color:#d35400;'>Projects</h2>")
        html.AppendLine("<div style='display:flex; flex-wrap:wrap; gap:10px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<div style='border:1px solid #e67e22; border-radius:5px; padding:10px; flex:1 1 200px; background:#fff3e0;'>")
            html.AppendLine($"<strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</div>")
        Next
        html.AppendLine("</div>")

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='border-bottom:2px solid #d35400; padding-bottom:3px; color:#d35400;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    Private Sub RenderExecutiveTemplate()
        Dim html As New StringBuilder()
        html.AppendLine("<div style='font-family:Tahoma, sans-serif; color:#111; line-height:1.6; padding:30px; background:#f9f9f9;'>")
        html.AppendLine($"<h1 style='font-size:36px; margin-bottom:5px; color:#000;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:20px; color:#444;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Experience
        html.AppendLine("<h2 style='border-bottom:1px solid #888; padding-bottom:3px; color:#000;'>Professional Experience</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Education
        html.AppendLine("<h2 style='border-bottom:1px solid #888; padding-bottom:3px; color:#000;'>Education</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<li>{edu}</li>")
        Next
        html.AppendLine("</ul>")

        ' Skills as badges
        html.AppendLine("<h2 style='border-bottom:1px solid #888; padding-bottom:3px; color:#000;'>Skills</h2>")
        html.AppendLine("<p>")
        For Each skill In SplitLines(txtSkills.Text)
            Dim parts = skill.Split(":"c)
            Dim name = parts(0).Trim()
            html.AppendLine($"<span style='display:inline-block; padding:3px 8px; background:#555; color:#fff; border-radius:3px; margin:2px;'>{name}</span>")
        Next
        html.AppendLine("</p>")

        ' Projects
        html.AppendLine("<h2 style='border-bottom:1px solid #888; padding-bottom:3px; color:#000;'>Key Projects</h2>")
        html.AppendLine("<ul style='margin-left:20px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<li><strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</li>")
        Next
        html.AppendLine("</ul>")

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='border-bottom:1px solid #888; padding-bottom:3px; color:#000;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    Private Sub RenderTechFocusTemplate()
        Dim html As New StringBuilder()
        html.AppendLine("<div style='font-family:Consolas, monospace; color:#111; padding:25px; background:#eef2f7;'>")
        html.AppendLine($"<h1 style='font-size:36px; margin-bottom:5px; color:#2c3e50;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:20px; color:#34495e;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Left Sidebar: Skills + Education
        html.AppendLine("<div style='display:flex; gap:20px;'>")
        html.AppendLine("<div style='flex:1; min-width:200px;'>")
        ' Skills
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Skills</h2><ul style='margin-left:20px;'>")
        For Each skill In SplitLines(txtSkills.Text)
            Dim parts = skill.Split(":"c)
            Dim name = parts(0).Trim()
            'Dim level = If(parts.Length > 1, Integer.Parse(parts(1).Trim()), 3)
            html.AppendLine($"<li>{name}</li>")
        Next
        html.AppendLine("</ul>")
        ' Education
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Education</h2><ul style='margin-left:20px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<li>{edu}</li>")
        Next
        html.AppendLine("</ul>")
        html.AppendLine("</div>") ' end sidebar

        ' Right Main: Experience + Projects
        html.AppendLine("<div style='flex:2; min-width:300px;'>")
        ' Experience
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Experience</h2><ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Projects
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Projects</h2><ul style='margin-left:20px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<li><strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</li>")
        Next
        html.AppendLine("</ul>")
        html.AppendLine("</div>") ' end main
        html.AppendLine("</div>") ' end flex

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    Private Sub RenderAcademicTemplate()
        Dim html As New StringBuilder()
        html.AppendLine("<div style='font-family:Times New Roman, serif; color:#111; padding:25px; background:#fffdfa;'>")
        html.AppendLine($"<h1 style='font-size:34px; margin-bottom:5px; color:#2c3e50;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:20px; color:#555;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Education and Awards
        html.AppendLine("<h2 style='border-bottom:2px solid #16a085; padding-bottom:3px;'>Education</h2><ul style='margin-left:20px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<li>{edu}</li>")
        Next
        html.AppendLine("</ul>")

        ' Experience
        html.AppendLine("<h2 style='border-bottom:2px solid #16a085; padding-bottom:3px;'>Research & Experience</h2><ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Publications / Projects
        html.AppendLine("<h2 style='border-bottom:2px solid #16a085; padding-bottom:3px;'>Publications / Projects</h2><ul style='margin-left:20px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<li><strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</li>")
        Next
        html.AppendLine("</ul>")

        ' Skills
        html.AppendLine("<h2 style='border-bottom:2px solid #16a085; padding-bottom:3px;'>Skills & Methods</h2><ul style='margin-left:20px;'>")
        For Each skill In SplitLines(txtSkills.Text)
            html.AppendLine($"<li>{skill}</li>")
        Next
        html.AppendLine("</ul>")

        ' Additional Notes
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='border-bottom:2px solid #16a085; padding-bottom:3px;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    Private Sub RenderStartupTemplate()
        Dim html As New StringBuilder()
        html.AppendLine("<div style='font-family:Verdana, sans-serif; color:#111; padding:25px; background:#fef9f3;'>")
        html.AppendLine($"<h1 style='font-size:36px; color:#e74c3c; margin-bottom:5px;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:20px; color:#555;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Experience
        html.AppendLine("<h2 style='border-bottom:2px solid #e74c3c; padding-bottom:3px;'>Experience</h2><ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Projects with badge-style highlights
        html.AppendLine("<h2 style='border-bottom:2px solid #e74c3c; padding-bottom:3px;'>Projects</h2>")
        html.AppendLine("<div style='display:flex; flex-wrap:wrap; gap:10px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<div style='border:1px solid #e74c3c; border-radius:5px; padding:10px; background:#fdecea; flex:1 1 200px;'>")
            html.AppendLine($"<strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</div>")
        Next
        html.AppendLine("</div>")

        ' Skills as inline tags
        html.AppendLine("<h2 style='border-bottom:2px solid #e74c3c; padding-bottom:3px;'>Skills</h2><p>")
        For Each skill In SplitLines(txtSkills.Text)
            html.AppendLine($"<span style='display:inline-block; margin:3px; padding:3px 8px; background:#c0392b; color:#fff; border-radius:3px;'>{skill}</span>")
        Next
        html.AppendLine("</p>")

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='border-bottom:2px solid #e74c3c; padding-bottom:3px;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    Private Sub RenderMinimalistTemplate()
        Dim html As New StringBuilder()
        html.AppendLine("<div style='font-family:Helvetica, sans-serif; color:#111; padding:25px; background:#ffffff;'>")
        html.AppendLine($"<h1 style='font-size:36px; margin-bottom:5px; color:#000;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:20px; color:#555;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Experience
        html.AppendLine("<h2 style='margin-top:20px; margin-bottom:5px;'>Experience</h2><ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Education
        html.AppendLine("<h2 style='margin-top:20px; margin-bottom:5px;'>Education</h2><ul style='margin-left:20px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<li>{edu}</li>")
        Next
        html.AppendLine("</ul>")

        ' Skills
        html.AppendLine("<h2 style='margin-top:20px; margin-bottom:5px;'>Skills</h2><p>")
        For Each skill In SplitLines(txtSkills.Text)
            html.AppendLine($"<span style='margin-right:8px;'>{skill}</span>")
        Next
        html.AppendLine("</p>")

        ' Projects
        html.AppendLine("<h2 style='margin-top:20px; margin-bottom:5px;'>Projects</h2><ul style='margin-left:20px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<li><strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</li>")
        Next
        html.AppendLine("</ul>")

        ' Additional
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='margin-top:20px; margin-bottom:5px;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")
        resumePreview.InnerHtml = html.ToString()
    End Sub

    Private Sub RenderHybridTemplate()
        Dim html As New StringBuilder()

        html.AppendLine("<div style='font-family:Arial, sans-serif; color:#222; padding:25px; background:#f9f9f9;'>")

        ' Header: Name + Contact
        html.AppendLine($"<h1 style='font-size:36px; color:#2c3e50; margin-bottom:5px;'>{txtFullName.Text}</h1>")
        html.AppendLine($"<p style='margin-bottom:20px; color:#555;'>{txtEmail.Text} | {txtPhone.Text} | {txtLocation.Text}</p>")

        ' Flex container: Left (Skills & Education), Right (Experience & Projects)
        html.AppendLine("<div style='display:flex; gap:20px;'>")

        ' Left Column
        html.AppendLine("<div style='flex:1; min-width:200px;'>")

        ' Skills
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Skills</h2><ul style='margin-left:20px;'>")
        For Each skill In SplitLines(txtSkills.Text)
            Dim parts = skill.Split(":"c)
            Dim name = parts(0).Trim()
            'Dim level = If(parts.Length > 1, Integer.Parse(parts(1).Trim()), 3)
            html.AppendLine($"<li>{name}</li>")
        Next
        html.AppendLine("</ul>")

        ' Education
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Education</h2><ul style='margin-left:20px;'>")
        For Each edu In SplitLines(txtEducation.Text)
            html.AppendLine($"<li>{edu}</li>")
        Next
        html.AppendLine("</ul>")
        html.AppendLine("</div>") ' end left column

        ' Right Column
        html.AppendLine("<div style='flex:2; min-width:300px;'>")

        ' Experience
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Experience</h2><ul style='margin-left:20px;'>")
        For Each exp In SplitLines(txtExperience.Text)
            html.AppendLine($"<li>{exp}</li>")
        Next
        html.AppendLine("</ul>")

        ' Projects
        html.AppendLine("<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Projects</h2><ul style='margin-left:20px;'>")
        For Each proj In SplitLines(txtProjects.Text)
            Dim parts = proj.Split("|"c)
            If parts.Length < 2 Then Continue For
            Dim url = If(parts.Length > 2, $" <a href='{parts(2).Trim()}' target='_blank'>{parts(2).Trim()}</a>", "")
            html.AppendLine($"<li><strong>{parts(0).Trim()}:</strong> {parts(1).Trim()}{url}</li>")
        Next
        html.AppendLine("</ul>")

        html.AppendLine("</div>") ' end right column
        html.AppendLine("</div>") ' end flex container

        ' Additional Notes
        If Not String.IsNullOrWhiteSpace(txtAdditional.Text) Then
            html.AppendLine($"<h2 style='border-bottom:2px solid #2980b9; padding-bottom:3px;'>Additional</h2><p>{txtAdditional.Text}</p>")
        End If

        html.AppendLine("</div>")

        resumePreview.InnerHtml = html.ToString()
    End Sub

    Protected Sub btnRefreshTemplate_Click(sender As Object, e As EventArgs) Handles btnRefreshTemplate.Click
        ddlTemplate_SelectedIndexChanged(ddlTemplate, EventArgs.Empty)
    End Sub

    Protected Sub btnClearForm_Click(sender As Object, e As EventArgs) Handles btnClearForm.Click
        txtFullName.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtLocation.Text = ""
        txtExperience.Text = ""
        txtEducation.Text = ""
        txtSkills.Text = ""
        txtProjects.Text = ""
        txtAdditional.Text = ""
        ddlTemplate_SelectedIndexChanged(ddlTemplate, EventArgs.Empty)

    End Sub



End Class

