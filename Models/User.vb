Imports System.ComponentModel.DataAnnotations


Public Class User
    <Key>
    Public Property UserID As Guid

    <Required>
    <MaxLength(50)>
    Public Property Username As String

    <Required>
    <MaxLength(100)>
    Public Property Email As String

    <Required>
    <MaxLength(256)>
    Public Property PasswordHash As String

    <MaxLength(50)>
    Public Property FirstName As String

    <MaxLength(50)>
    Public Property LastName As String

    Public Property CreatedAt As DateTime = DateTime.Now
    Public Property LastLogin As DateTime?
    Public Property IsEmailVerified As Boolean
    Public Property VerificationToken As Guid

End Class
