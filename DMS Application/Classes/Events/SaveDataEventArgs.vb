Public Class SaveDataEventArgs
    Inherits System.EventArgs

    Private mblnSaveSuccessful As Boolean

    Public Property SaveSuccessful As Boolean
        Get
            Return mblnSaveSuccessful
        End Get
        Set(ByVal value As Boolean)
            mblnSaveSuccessful = value
        End Set
    End Property

End Class
