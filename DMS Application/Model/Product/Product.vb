Namespace Model

    Public Class Product
        Inherits BaseModel

        'Private members
        Private _intProduct_ID As Integer
        Private _strName As String = String.Empty
        Private _blnIsTaxable As Boolean

        'Private class members
        Private mcLstProductPrice As List(Of ProductPrice)
        Private mcProductCategory As ProductCategory
        Private mcProductType As ProductType


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intProduct_ID
            End Get
            Set(ByVal value As Integer)
                _intProduct_ID = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _strName
            End Get

            Set(ByVal value As String)

                If Not value = String.Empty Then
                    _strName = value
                Else
                    'Non valide
                End If
            End Set
        End Property

        Public Property Type As ProductType
            Get
                Return mcProductType
            End Get
            Set(value As ProductType)
                mcProductType = value
            End Set
        End Property

        Public Property Category As ProductCategory
            Get
                Return mcProductCategory
            End Get
            Set(value As ProductCategory)
                mcProductCategory = value
            End Set
        End Property

        Public Property IsTaxable As Boolean
            Get
                Return _blnIsTaxable
            End Get

            Set(ByVal value As Boolean)
                _blnIsTaxable = value
            End Set
        End Property

        Public ReadOnly Property GetLstProductPrice As List(Of ProductPrice)
            Get
                Return mcLstProductPrice
            End Get
        End Property

#End Region

#Region "Constructors"

        Public Sub New()
            mcLstProductPrice = New List(Of ProductPrice)
            mcProductCategory = New ProductCategory
            mcProductType = New ProductType
        End Sub

#End Region

#Region "Functions / Subs"

        Public Function blnProduct_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnProduct_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnProduct_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnProduct_Delete()

                    End Select
                Else
                    'Error
                End If

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("Pro_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("ProT_ID", mcProductType.ID.ToString, MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case SQLController.bln_AddField("ProC_ID", mcProductCategory.ID.ToString, MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case SQLController.bln_AddField("Pro_Taxable", _blnIsTaxable.ToString, MySQLController.MySQL_FieldTypes.TINYINT_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields()
                    Case SQLController.bln_ADOInsert("Product", _intProduct_ID)
                    Case _intProduct_ID > 0
                    Case blnLstProductPrice_Save()
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields()
                    Case SQLController.bln_ADOUpdate("Product", "Pro_ID = " & _intProduct_ID)
                    Case blnLstProductPrice_Save()
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("ProductPrice", "Pro_ID = " & _intProduct_ID)
                    Case SQLController.bln_ADODelete("Product", "Pro_ID = " & _intProduct_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnLstProductPrice_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                For Each proPrice As ProductPrice In mcLstProductPrice

                    proPrice.Product_ID = _intProduct_ID

                    blnValidReturn = proPrice.blnProductPrice_Save()

                    If Not blnValidReturn Then Exit For
                Next

                blnValidReturn = True

            Catch ex As Exception
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

#End Region

    End Class

End Namespace