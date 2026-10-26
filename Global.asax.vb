Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication


    Sub Application_Start(sender As Object, e As EventArgs)
        ' Register jQuery ScriptResourceMapping
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", New ScriptResourceDefinition() With {
        .Path = "~/Scripts/jquery-3.6.0.min.js",
        .DebugPath = "~/Scripts/jquery-3.6.0.js",
        .CdnPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.6.0.min.js",
        .CdnDebugPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.6.0.js"
    })
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class