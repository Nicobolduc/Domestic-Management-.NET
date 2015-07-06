Namespace CoreModelController

    Public NotInheritable Class CoreModelController

        Private Shared _myUniqueInstance As CoreModelController

        Private mcProductController As Lazy(Of ProductController)
        Private mcCompanyController As CompanyController
        Private mcFinanceController As FinanceController


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

        Public ReadOnly Property GetFinanceController As FinanceController
            Get
                If mcFinanceController Is Nothing Then
                    mcFinanceController = New FinanceController
                End If

                Return mcFinanceController
            End Get
        End Property

        Public ReadOnly Property GetCompanyController As CompanyController
            Get
                If mcCompanyController Is Nothing Then
                    mcCompanyController = New CompanyController
                End If

                Return mcCompanyController
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