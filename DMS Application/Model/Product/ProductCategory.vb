Namespace Model

    Public Class ProductCategory
        Inherits BaseModel

        'Private members
        Private _intCategory_ID As Integer
        Private _strName As String = String.Empty

        'Private class members
        Private mcProductType As Model.ProductType

#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intCategory_ID
            End Get
            Set(ByVal value As Integer)
                _intCategory_ID = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _strName
            End Get
            Set(ByVal value As String)
                _strName = value
            End Set
        End Property

        Public Property Type As ProductType
            Get
                Return mcProductType
            End Get
            Set(ByVal value As ProductType)
                mcProductType = value
            End Set
        End Property

#End Region

#Region "Constructors"

        Public Sub New()

            mcProductType = New Model.ProductType
        End Sub

#End Region

#Region "Functions / Subs"

        Public Function blnProductCategory_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnProductCategory_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnProductCategory_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnProductCategory_Delete()

                    End Select
                Else
                    'Error
                End If

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductCategory_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("ProC_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("ProT_ID", mcProductType.ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductCategory_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProductCategory_AddFields()
                    Case SQLController.bln_ADOInsert("ProductCategory", _intCategory_ID)
                    Case _intCategory_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductCategory_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProductCategory_AddFields()
                    Case SQLController.bln_ADOUpdate("ProductCategory", "ProC_ID = " & _intCategory_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductCategory_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("ProductCategory", "ProC_ID = " & _intCategory_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

#End Region

    End Class

End Namespace
