Imports System.Linq
Imports System.Net.Sockets

''' <summary>
''' Network Diagnostics: a small set of IT-admin connectivity tools for checking whether
''' backend services this installation talks to over raw TCP (legacy order-processing
''' systems, print/label servers, franchise licensing servers, partner relays) are
''' reachable. These services speak their own TCP protocols, not HTTP, so connectivity is
''' tested with a direct socket connect rather than an HTTP request.
''' </summary>
Public Class NetworkDiagnostics
    Inherits System.Web.UI.Page

    ' ================== Quick Connectivity Test (direct) ==================

    Protected Sub btnDirectTest_Click(sender As Object, e As EventArgs)
        Dim host As String = txtDirectHost.Text.Trim()
        Dim port As Integer = CInt(txtDirectPort.Text.Trim())

        Try
            Using client As New TcpClient()
                client.Connect(host, port) ' SINK: PLANTED-VB-HR-661
                lblDirectResult.Text = If(client.Connected, "Reachable: " & host & ":" & port, "Not reachable.")
            End Using
        Catch ex As Exception
            lblDirectResult.Text = "Connection failed: " & ex.Message
        End Try
    End Sub

    Protected Sub btnDirectSafeTest_Click(sender As Object, e As EventArgs)
        Dim host As String = txtDirectSafeHost.Text.Trim()
        Dim port As Integer = CInt(txtDirectSafePort.Text.Trim())

        If Not IsApprovedQuickTestTarget(host, port) Then
            lblDirectSafeResult.Text = "Rejected: destination is not on the approved quick-test list."
            Return
        End If

        Try
            Using client As New TcpClient()
                client.Connect(host, port) ' SAFE_SINK: PLANTED-VB-HR-661-safe
                lblDirectSafeResult.Text = If(client.Connected, "Reachable: " & host & ":" & port, "Not reachable.")
            End Using
        Catch ex As Exception
            lblDirectSafeResult.Text = "Connection failed: " & ex.Message
        End Try
    End Sub

    Private Function IsApprovedQuickTestTarget(host As String, port As Integer) As Boolean
        Dim approvedTargets As String() = {
            "diag-east.grimrieber-partners.internal:7501",
            "diag-west.grimrieber-partners.internal:7501",
            "192.168.1.50:7501"
        }

        If String.IsNullOrWhiteSpace(host) Then
            Return False
        End If

        Dim candidate As String = host & ":" & port.ToString()
        Return approvedTargets.Any(Function(t) String.Equals(t, candidate, StringComparison.OrdinalIgnoreCase))
    End Function

    ' ================== Service Health Ping (indirect — same-class helper) ==================

    Protected Sub btnPing_Click(sender As Object, e As EventArgs)
        Dim host As String = txtPingHost.Text.Trim()
        Dim port As Integer = CInt(txtPingPort.Text.Trim())

        Dim reachable As Boolean = ConnectToDiagnosticHost(host, port)
        lblPingResult.Text = If(reachable, "Service is up: " & host & ":" & port, "Service did not respond.")
    End Sub

    Protected Sub btnPingSafe_Click(sender As Object, e As EventArgs)
        Dim host As String = txtPingSafeHost.Text.Trim()
        Dim port As Integer = CInt(txtPingSafePort.Text.Trim())

        Dim reachable As Boolean = ConnectToDiagnosticHostSecure(host, port)
        lblPingSafeResult.Text = If(reachable, "Service is up: " & host & ":" & port, "Service did not respond, or destination is not an approved diagnostic host.")
    End Sub

    ''' <summary>
    ''' Performs the actual health-ping socket connect. Called from btnPing_Click above.
    ''' SECURITY-SENSITIVE: accepts any host/port with no restriction.
    ''' </summary>
    Private Function ConnectToDiagnosticHost(host As String, port As Integer) As Boolean
        Try
            Using client As New TcpClient()
                client.Connect(host, port) ' SINK: PLANTED-VB-HR-662
                Return client.Connected
            End Using
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Performs the actual health-ping socket connect, restricted to the pre-approved
    ''' diagnostic-host list. Called from btnPingSafe_Click above.
    ''' </summary>
    Private Function ConnectToDiagnosticHostSecure(host As String, port As Integer) As Boolean
        Dim approvedDiagnosticHosts As String() = {
            "svc-health-east.grimrieber-partners.internal:7502",
            "svc-health-west.grimrieber-partners.internal:7502",
            "192.168.1.51:7502"
        }

        If String.IsNullOrWhiteSpace(host) Then
            Return False
        End If

        Dim candidate As String = host & ":" & port.ToString()
        If Not approvedDiagnosticHosts.Any(Function(h) String.Equals(h, candidate, StringComparison.OrdinalIgnoreCase)) Then
            Return False
        End If

        Try
            Using client As New TcpClient()
                client.Connect(host, port) ' SAFE_SINK: PLANTED-VB-HR-662-safe
                Return client.Connected
            End Using
        Catch
            Return False
        End Try
    End Function

    ' ================== Register External Relay Partner (interprocedural, cross-file) ==================

    Protected Sub btnRegisterRelay_Click(sender As Object, e As EventArgs)
        Dim host As String = txtRelayHost.Text.Trim()
        Dim port As Integer = CInt(txtRelayPort.Text.Trim())

        Dim relayService As New NetworkRelayService()
        Dim reachable As Boolean = relayService.RelayHealthCheck(host, port)
        lblRelayResult.Text = If(reachable, "Relay partner registered and reachable: " & host & ":" & port, "Relay partner is not reachable.")
    End Sub

    Protected Sub btnRegisterRelaySafe_Click(sender As Object, e As EventArgs)
        Dim host As String = txtRelaySafeHost.Text.Trim()
        Dim port As Integer = CInt(txtRelaySafePort.Text.Trim())

        Dim relayService As New NetworkRelayService()
        Dim reachable As Boolean = relayService.RelayHealthCheckSecure(host, port)
        lblRelaySafeResult.Text = If(reachable, "Approved relay partner registered and reachable: " & host & ":" & port, "Relay partner is not reachable, or is not on the approved partner list.")
    End Sub

    ' ================== Connector Profile Test (type/polymorphism-dependent) ==================

    Protected Sub btnConnectorTest_Click(sender As Object, e As EventArgs)
        Dim host As String = txtConnectorHost.Text.Trim()
        Dim port As Integer = CInt(txtConnectorPort.Text.Trim())

        ' Which concrete IServiceConnector is used — and therefore whether the destination
        ' is allow-list-checked at all — is resolved entirely inside the factory, driven by
        ' this deployment's "LegacyConnectorMode" configuration. This call site never
        ' branches on it.
        Dim connector As IServiceConnector = ServiceConnectorFactory.GetConnector()
        Dim reachable As Boolean = connector.Connect(host, port)
        lblConnectorResult.Text = If(reachable, "Connector profile reachable: " & host & ":" & port, "Connector profile is not reachable, or was rejected by the active connector.")
    End Sub

    ' ================== Batch Relay Connectivity Test (diversity — loop over multiple targets) ==================

    Protected Sub btnBatchTest_Click(sender As Object, e As EventArgs)
        Dim results As New List(Of String)
        Dim lines = txtBatchTargets.Text.Split({Environment.NewLine, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)

        For Each line In lines
            Dim parts = line.Split(":"c)
            If parts.Length <> 2 Then
                results.Add(line & " -> skipped (expected host:port)")
                Continue For
            End If

            Dim host As String = parts(0).Trim()
            Dim port As Integer
            If Not Integer.TryParse(parts(1).Trim(), port) Then
                results.Add(line & " -> skipped (invalid port)")
                Continue For
            End If

            Try
                Using client As New TcpClient()
                    client.Connect(host, port) ' SINK: PLANTED-VB-HR-665
                    results.Add(host & ":" & port & " -> " & If(client.Connected, "reachable", "not reachable"))
                End Using
            Catch ex As Exception
                results.Add(host & ":" & port & " -> connection failed (" & ex.Message & ")")
            End Try
        Next

        lblBatchResult.Text = String.Join("<br/>", results)
    End Sub

    Protected Sub btnBatchSafeTest_Click(sender As Object, e As EventArgs)
        Dim approvedBatchTargets As String() = {
            "batch-relay-east.grimrieber-partners.internal:7503",
            "batch-relay-west.grimrieber-partners.internal:7503",
            "192.168.1.52:7503"
        }

        Dim results As New List(Of String)
        Dim lines = txtBatchSafeTargets.Text.Split({Environment.NewLine, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)

        For Each line In lines
            Dim parts = line.Split(":"c)
            If parts.Length <> 2 Then
                results.Add(line & " -> skipped (expected host:port)")
                Continue For
            End If

            Dim host As String = parts(0).Trim()
            Dim port As Integer
            If Not Integer.TryParse(parts(1).Trim(), port) Then
                results.Add(line & " -> skipped (invalid port)")
                Continue For
            End If

            Dim candidate As String = host & ":" & port.ToString()
            If Not approvedBatchTargets.Any(Function(t) String.Equals(t, candidate, StringComparison.OrdinalIgnoreCase)) Then
                results.Add(candidate & " -> rejected (not on approved batch-relay list)")
                Continue For
            End If

            Try
                Using client As New TcpClient()
                    client.Connect(host, port) ' SAFE_SINK: PLANTED-VB-HR-665-safe
                    results.Add(candidate & " -> " & If(client.Connected, "reachable", "not reachable"))
                End Using
            Catch ex As Exception
                results.Add(candidate & " -> connection failed (" & ex.Message & ")")
            End Try
        Next

        lblBatchSafeResult.Text = String.Join("<br/>", results)
    End Sub

End Class
