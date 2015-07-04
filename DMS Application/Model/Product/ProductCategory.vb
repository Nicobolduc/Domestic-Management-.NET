Namespace Model

    Public Class ProductCategory

        Private _intCategory_ID As Integer
        Private _intType_ID As Integer
        Private _strName As String = String.Empty

#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intCategory_ID
            End Get
            Set(value As Integer)
                _intCategory_ID = value
            End Set
        End Property

        Public Property Type_ID As Integer
            Get
                Return _intType_ID
            End Get
            Set(value As Integer)
                _intType_ID = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _strName
            End Get
            Set(value As String)
                _strName = value
            End Set
        End Property

#End Region

    End Class

End Namespace
