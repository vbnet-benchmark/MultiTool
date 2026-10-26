Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class PDFGenerator

    Public Shared Sub GenerateClassic(data As ResumeData, ms As MemoryStream)
        Dim doc As New Document(PageSize.A4, 50, 50, 50, 50)
        Dim writer = PdfWriter.GetInstance(doc, ms)
        doc.Open()
        Dim titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD)
        Dim sectionFont = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.DARK_GRAY)
        Dim textFont = FontFactory.GetFont("Arial", 12, Font.NORMAL)

        ' Name
        Dim pName As New Paragraph(data.FullName, titleFont) With {.Alignment = Element.ALIGN_CENTER}
        doc.Add(pName)
        doc.Add(New Paragraph($"{data.Email} | {data.Phone} | {data.Location}", textFont) With {.Alignment = Element.ALIGN_CENTER})
        doc.Add(Chunk.NEWLINE)

        ' Sections
        AddSection(doc, "Work Experience", data.Experience, sectionFont, textFont)
        AddSection(doc, "Education", data.Education, sectionFont, textFont)
        AddSkills(doc, data.Skills, sectionFont)
        AddProjects(doc, data.Projects, sectionFont, textFont)
        If Not String.IsNullOrEmpty(data.Additional) Then
            doc.Add(New Paragraph("Additional Information", sectionFont))
            doc.Add(New Paragraph(data.Additional, textFont))
        End If

        doc.Close()
    End Sub

    Public Shared Sub GenerateModern(data As ResumeData, ms As MemoryStream)
        ' Modern layout: Two columns, skill bars
        Dim doc As New Document(PageSize.A4, 50, 50, 50, 50)
        Dim writer = PdfWriter.GetInstance(doc, ms)
        doc.Open()
        Dim titleFont = FontFactory.GetFont("Helvetica", 22, Font.BOLD, BaseColor.BLUE)
        Dim sectionFont = FontFactory.GetFont("Helvetica", 14, Font.BOLD, BaseColor.DARK_GRAY)
        Dim textFont = FontFactory.GetFont("Helvetica", 12, Font.NORMAL)

        ' Header
        doc.Add(New Paragraph(data.FullName, titleFont) With {.Alignment = Element.ALIGN_CENTER})
        doc.Add(New Paragraph($"{data.Email} | {data.Phone} | {data.Location}", textFont) With {.Alignment = Element.ALIGN_CENTER})
        doc.Add(Chunk.NEWLINE)

        ' Left Column: Experience + Education
        AddSection(doc, "Work Experience", data.Experience, sectionFont, textFont)
        AddSection(doc, "Education", data.Education, sectionFont, textFont)

        ' Right Column: Skills + Projects
        AddSkills(doc, data.Skills, sectionFont)
        AddProjects(doc, data.Projects, sectionFont, textFont)

        If Not String.IsNullOrEmpty(data.Additional) Then
            doc.Add(New Paragraph("Additional Information", sectionFont))
            doc.Add(New Paragraph(data.Additional, textFont))
        End If

        doc.Close()
    End Sub

    Public Shared Sub GeneratePortfolio(data As ResumeData, ms As MemoryStream)
        ' Portfolio style: emphasize projects
        Dim doc As New Document(PageSize.A4, 50, 50, 50, 50)
        Dim writer = PdfWriter.GetInstance(doc, ms)
        doc.Open()
        Dim titleFont = FontFactory.GetFont("Times-Bold", 24, BaseColor.DARK_GRAY)
        Dim sectionFont = FontFactory.GetFont("Times-Bold", 14, BaseColor.BLUE)
        Dim textFont = FontFactory.GetFont("Times-Roman", 12, BaseColor.BLACK)

        doc.Add(New Paragraph(data.FullName, titleFont) With {.Alignment = Element.ALIGN_CENTER})
        doc.Add(New Paragraph($"{data.Email} | {data.Phone} | {data.Location}", textFont) With {.Alignment = Element.ALIGN_CENTER})
        doc.Add(Chunk.NEWLINE)

        AddSection(doc, "Work Experience", data.Experience, sectionFont, textFont)
        AddSection(doc, "Education", data.Education, sectionFont, textFont)
        AddProjects(doc, data.Projects, sectionFont, textFont, True)
        AddSkills(doc, data.Skills, sectionFont)

        If Not String.IsNullOrEmpty(data.Additional) Then
            doc.Add(New Paragraph("Additional Information", sectionFont))
            doc.Add(New Paragraph(data.Additional, textFont))
        End If

        doc.Close()
    End Sub

    ' ==== Helper Methods ====
    Private Shared Sub AddSection(doc As Document, title As String, items As List(Of String), sectionFont As Font, textFont As Font)
        If items Is Nothing OrElse items.Count = 0 Then Return
        doc.Add(New Paragraph(title, sectionFont))
        For Each item In items
            doc.Add(New Paragraph("• " & item, textFont))
        Next
        doc.Add(Chunk.NEWLINE)
    End Sub

    Private Shared Sub AddSkills(doc As Document, skills As List(Of Skill), sectionFont As Font)
        If skills Is Nothing OrElse skills.Count = 0 Then Return
        doc.Add(New Paragraph("Skills", sectionFont))
        Dim cb = PdfWriter.GetInstance(doc, New MemoryStream()).DirectContent
        For Each skill In skills
            doc.Add(New Paragraph(skill.Name & " " & New String("■"c, skill.Level), FontFactory.GetFont("Arial", 12)))
        Next
        doc.Add(Chunk.NEWLINE)
    End Sub

    Private Shared Sub AddProjects(doc As Document, projects As List(Of Project), sectionFont As Font, textFont As Font, Optional emphasize As Boolean = False)
        If projects Is Nothing OrElse projects.Count = 0 Then Return
        doc.Add(New Paragraph("Projects", sectionFont))
        For Each proj In projects
            Dim p As New Paragraph(proj.Name, FontFactory.GetFont("Arial", 12, Font.BOLD))
            If emphasize Then p.Font.Color = BaseColor.BLUE
            doc.Add(p)
            doc.Add(New Paragraph(proj.Description, textFont))
            If Not String.IsNullOrEmpty(proj.Url) Then
                Dim link As New Chunk(proj.Url, FontFactory.GetFont("Arial", 12, Font.UNDERLINE, BaseColor.BLUE))
                link.SetAnchor(proj.Url)
                doc.Add(New Paragraph(link))
            End If
            doc.Add(Chunk.NEWLINE)
        Next
    End Sub

End Class

Public Class ResumeData
    Public Property FullName As String
    Public Property Email As String
    Public Property Phone As String
    Public Property Location As String
    Public Property Experience As List(Of String)
    Public Property Education As List(Of String)
    Public Property Skills As List(Of Skill)
    Public Property Projects As List(Of Project)
    Public Property Additional As String
End Class

Public Class Skill
    Public Property Name As String
    Public Property Level As Integer ' 1-5
End Class

Public Class Project
    Public Property Name As String
    Public Property Description As String
    Public Property Url As String
    Public Property ImageUrl As String
End Class
