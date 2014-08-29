Public Class ValidateRulesEventArgs
    Inherits System.EventArgs

    Private mblnIsValid As Boolean

    Public Property IsValid As Boolean
        Get
            Return mblnIsValid
        End Get
        Set(ByVal value As Boolean)
            mblnIsValid = value
        End Set
    End Property

End Class
