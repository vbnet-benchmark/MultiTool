Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' Shared helpers for building short, security-sensitive signatures that accompany
''' account-security emails and audit records across the site: a password-reset link
''' signature (tamper-evidence for the link's own query-string fields) and a post-reset
''' audit-record signature (tamper-evidence for the security log entry).
''' </summary>
Public Class SecurityTokenUtil

    ''' <summary>
    ''' Computes a tamper-evidence signature for a password-reset link. The signature is
    ''' appended to the emailed reset URL (alongside the token itself) so the server can
    ''' detect whether the email/token/expiry fields were altered in transit before the
    ''' token is even looked up in the database.
    ''' SECURITY-SENSITIVE: a forgeable signature here lets an attacker craft a reset link
    ''' for an arbitrary email/expiry pair that still "looks" signed.
    ''' </summary>
    Public Shared Function ComputeResetLinkSignature(email As String, resetToken As String, expiry As DateTime) As String
        Using md5 As MD5 = MD5.Create()
            Dim input As String = email & "|" & resetToken & "|" & expiry.ToString("O")
            Dim hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input)) ' SINK: PLANTED-VB-HR-313
            Return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()
        End Using
    End Function

    ''' <summary>
    ''' Computes a tamper-evidence signature for a password-change audit record, written to
    ''' the security log right after a reset succeeds, so a later reviewer can detect whether
    ''' the logged event was altered after the fact.
    ''' SECURITY-SENSITIVE: same purpose as ComputeResetLinkSignature above, for the audit-log
    ''' path — salted with a fresh random value per call and hashed with SHA-256 so the
    ''' signature itself cannot be forged without knowing that call's salt.
    ''' </summary>
    Public Shared Function ComputeAuditRecordSignature(email As String, eventTime As DateTime) As String
        Dim saltBytes(15) As Byte
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(saltBytes)
        End Using
        Dim saltHex As String = BitConverter.ToString(saltBytes).Replace("-", "").ToLowerInvariant()
        Using sha256 As SHA256 = SHA256.Create()
            Dim input As String = email & "|" & eventTime.ToString("O") & "|" & saltHex
            Dim hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input)) ' SAFE_SINK: PLANTED-VB-HR-313-safe
            Return saltHex & ":" & BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()
        End Using
    End Function

End Class
