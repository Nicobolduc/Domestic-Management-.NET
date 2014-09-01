
Public Class clsUser

    'Private members
    Private mintLanguage As Short = 1

#Region "Properties"

    Public ReadOnly Property GetLanguage As Short
        Get
            Return mintLanguage
        End Get
    End Property

#End Region

End Class
