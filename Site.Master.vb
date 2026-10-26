Partial Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim isSubscribed As Boolean = False

        ' Replace with your subscription check logic
        If Session("IsSubscribed") IsNot Nothing Then
            isSubscribed = Convert.ToBoolean(Session("IsSubscribed"))
        End If

        ' Hide all ad panels for subscribers
        adPanelTop.Visible = Not isSubscribed
        'adPanelMiddle.Visible = Not isSubscribed
        adPanelBottom.Visible = Not isSubscribed
    End Sub
End Class
