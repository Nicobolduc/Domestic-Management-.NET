Namespace CoreModelController

    Public NotInheritable Class CoreModelController

        Private mcProductController As Lazy(Of ProductController)
        Private Shared _myUniqueInstance As CoreModelController


#Region "Constructors"

        Private Sub New()
            'Singleton
        End Sub

#End Region

#Region "Properties"

        Public ReadOnly Property GetProductController As Lazy(Of ProductController)
            Get
                If mcProductController Is Nothing Then
                    mcProductController = New Lazy(Of ProductController)
                End If

                Return mcProductController
            End Get
        End Property

#End Region

#Region "Shared Functions /Subs"

        Public Shared ReadOnly Property GetCoreModelController As CoreModelController
            Get
                If _myUniqueInstance Is Nothing Then
                    _myUniqueInstance = New CoreModelController()
                End If

                Return _myUniqueInstance
            End Get
        End Property

#End Region

    End Class

End Namespace