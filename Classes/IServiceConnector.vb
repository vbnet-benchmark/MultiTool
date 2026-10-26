''' <summary>
''' Dispatches a "Connector Profile" connectivity test to whichever concrete connector this
''' deployment is configured to use. New, disclosed hierarchy — GrimrieberMultiTool had no
''' existing MustInherit/Interface hierarchy to extend (confirmed by a project-wide grep for
''' "MustInherit Class" / "Interface " before this was introduced; every pre-existing
''' "Inherits" in the project is a framework base class).
''' </summary>
Public Interface IServiceConnector

    ''' <summary>
    ''' Attempts to connect to the given host/port. Returns True if the connection
    ''' succeeded. What (if anything) is validated about the destination before connecting
    ''' is entirely up to the concrete implementation — callers dispatch through this
    ''' interface and never see which one they got.
    ''' </summary>
    Function Connect(host As String, port As Integer) As Boolean

End Interface
