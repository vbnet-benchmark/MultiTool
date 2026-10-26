Imports System.Net.Sockets

''' <summary>
''' Low-level socket helpers shared by the Network Diagnostics "external relay partner"
''' feature. A relay partner is a legacy backend system (order-processing box, print/label
''' server, franchise licensing server) that speaks a raw TCP protocol rather than HTTP, so
''' connectivity to it is tested with a direct socket connect instead of an HTTP request.
''' </summary>
Public Class SocketConnectionHelper

    ''' <summary>
    ''' Opens a raw TCP connection to the given host/port and immediately closes it again,
    ''' used purely as a "is the relay partner reachable" probe.
    ''' SECURITY-SENSITIVE: the destination is accepted as-is, with no check against a known
    ''' set of legitimate relay partners, so a caller can point this at any host/port the
    ''' server can route to.
    ''' </summary>
    Public Shared Function OpenRawSocket(host As String, port As Integer) As Boolean
        Using client As New TcpClient()
            client.Connect(host, port) ' SINK: PLANTED-VB-HR-663
            Return client.Connected
        End Using
    End Function

    ''' <summary>
    ''' Opens a raw TCP connection to the given host/port, but only after confirming the
    ''' destination is one of the small set of approved relay-partner endpoints this
    ''' deployment is configured to talk to.
    ''' </summary>
    Public Shared Function OpenRawSocketAllowListed(host As String, port As Integer) As Boolean
        If Not ApprovedRelayPartners.IsApproved(host, port) Then
            Return False
        End If

        Using client As New TcpClient()
            client.Connect(host, port) ' SAFE_SINK: PLANTED-VB-HR-663-safe
            Return client.Connected
        End Using
    End Function

End Class

''' <summary>
''' The closed allow-list of relay-partner endpoints this deployment is approved to connect
''' to. Anything not on this list is rejected before a socket is ever opened.
''' </summary>
Public Module ApprovedRelayPartners

    Private ReadOnly ApprovedEndpoints As String() = {
        "relay-east.grimrieber-partners.internal:7001",
        "relay-west.grimrieber-partners.internal:7001",
        "legacy-orders.grimrieber-partners.internal:9100",
        "10.20.30.40:9100"
    }

    Public Function IsApproved(host As String, port As Integer) As Boolean
        If String.IsNullOrWhiteSpace(host) Then
            Return False
        End If

        Dim candidate As String = host.Trim() & ":" & port.ToString()
        For Each entry In ApprovedEndpoints
            If String.Equals(entry, candidate, StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next

        Return False
    End Function

End Module
