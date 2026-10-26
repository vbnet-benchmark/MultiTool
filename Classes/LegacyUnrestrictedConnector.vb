Imports System.Net.Sockets

''' <summary>
''' Connector profile used by legacy on-prem deployments that predate the approved-partner
''' allow-list: it connects to whatever host/port a "Connector Profile Test" supplies,
''' with no restriction, since the legacy deployment model assumed every connector profile
''' was hand-configured by a trusted on-site administrator.
''' </summary>
Public Class LegacyUnrestrictedConnector
    Implements IServiceConnector

    Public Function Connect(host As String, port As Integer) As Boolean Implements IServiceConnector.Connect
        Using client As New TcpClient()
            client.Connect(host, port) ' SINK: PLANTED-VB-HR-664
            Return client.Connected
        End Using
    End Function

End Class
