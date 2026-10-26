Imports System.Configuration

''' <summary>
''' Resolves the IServiceConnector implementation this deployment should use, based on the
''' "LegacyConnectorMode" app setting. The "Connector Profile Test" button on Network
''' Diagnostics calls through the returned interface reference without ever branching on
''' which concrete connector it got — the dispatch (and therefore whether the destination
''' gets allow-list-checked at all) happens entirely here, driven by deployment
''' configuration rather than by the request.
''' </summary>
Public Class ServiceConnectorFactory

    Public Shared Function GetConnector() As IServiceConnector
        Dim mode As String = ConfigurationManager.AppSettings("LegacyConnectorMode")

        Select Case mode
            Case "legacy"
                Return New LegacyUnrestrictedConnector()
            Case Else
                Return New SecureAllowListConnector()
        End Select
    End Function

End Class
