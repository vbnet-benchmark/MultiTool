Imports System.Linq
Imports System.Net.Sockets

''' <summary>
''' Connector profile used by current, hosted deployments: it only connects to one of the
''' handful of connector endpoints this deployment has been explicitly provisioned with,
''' rejecting anything else before a socket is ever opened.
''' </summary>
Public Class SecureAllowListConnector
    Implements IServiceConnector

    Private ReadOnly ApprovedConnectorEndpoints As String() = {
        "connector-primary.grimrieber-partners.internal:8443",
        "connector-secondary.grimrieber-partners.internal:8443",
        "172.16.5.10:8443"
    }

    Public Function Connect(host As String, port As Integer) As Boolean Implements IServiceConnector.Connect
        If String.IsNullOrWhiteSpace(host) Then
            Return False
        End If

        Dim candidate As String = host.Trim() & ":" & port.ToString()
        Dim isApproved As Boolean = ApprovedConnectorEndpoints.Any(
            Function(entry) String.Equals(entry, candidate, StringComparison.OrdinalIgnoreCase))

        If Not isApproved Then
            Return False
        End If

        Using client As New TcpClient()
            client.Connect(host, port) ' SAFE_SINK: PLANTED-VB-HR-664-safe
            Return client.Connected
        End Using
    End Function

End Class
