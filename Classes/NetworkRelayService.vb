''' <summary>
''' Front door for the "External Relay Partner" registration feature on the Network
''' Diagnostics page. A relay partner is registered by host/port; before the registration is
''' accepted, the service performs a one-shot raw-socket reachability check against the
''' partner's declared endpoint.
''' </summary>
Public Class NetworkRelayService

    ''' <summary>
    ''' Checks whether the given relay-partner endpoint is reachable, for the "Register
    ''' External Relay Partner" flow (unrestricted — any endpoint the caller supplies).
    ''' </summary>
    Public Function RelayHealthCheck(host As String, port As Integer) As Boolean
        Return SocketConnectionHelper.OpenRawSocket(host, port)
    End Function

    ''' <summary>
    ''' Checks whether the given relay-partner endpoint is reachable, for the "Register
    ''' Approved Relay Partner" flow (restricted to the pre-approved partner list).
    ''' </summary>
    Public Function RelayHealthCheckSecure(host As String, port As Integer) As Boolean
        Return SocketConnectionHelper.OpenRawSocketAllowListed(host, port)
    End Function

End Class
