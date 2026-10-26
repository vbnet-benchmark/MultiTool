Imports System.Data.Entity

Public Class MultiToolContext
    Inherits DbContext


    Public Sub New()
        MyBase.New("name=TinyToolsDB")
    End Sub

    Public Property Users As DbSet(Of User)

End Class
