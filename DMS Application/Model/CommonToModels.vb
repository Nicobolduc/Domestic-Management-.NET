Public MustInherit Class CommonToModels

    'Private members
    Private _intDMLCommand As mConstants.Form_Modes

    'Private class members
    Private mcSQL As MySQLController


#Region "Properties"

    Public Property DLMCommand As mConstants.Form_Modes
        Get
            Return _intDMLCommand
        End Get
        Set(ByVal value As mConstants.Form_Modes)
            _intDMLCommand = value
        End Set
    End Property

    Public Property SQLController As MySQLController
        Get
            Return mcSQL
        End Get
        Set(ByVal value As MySQLController)
            mcSQL = value
        End Set
    End Property

#End Region

End Class
