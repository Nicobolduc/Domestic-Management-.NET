Namespace Model

    Public Class ExpenseType
        Inherits BaseModel

        'Private members
        Private _intExpenseType_ID As Integer
        Private _strName As String = String.Empty
        Private _intArgbColor As Integer

        'Private class members


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intExpenseType_ID
            End Get
            Set(value As Integer)
                _intExpenseType_ID = value
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

        Public Property ArgbColor As Integer
            Get
                Return _intArgbColor
            End Get
            Set(value As Integer)
                _intArgbColor = value
            End Set
        End Property

#End Region

#Region "Functions / Subs"

        Public Function blnExpenseType_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnExpenseType_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnExpenseType_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnExpenseType_Delete()

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
                    Case SQLController.bln_AddField("ExpT_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("ExpT_ArgbColor", _intArgbColor, MySQLController.MySQL_FieldTypes.INT_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpenseType_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields()
                    Case SQLController.bln_ADOInsert("ExpenseType", _intExpenseType_ID)
                    Case _intExpenseType_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpenseType_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields()
                    Case SQLController.bln_ADOUpdate("ExpenseType", "ExpT_ID = " & _intExpenseType_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpenseType_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("ExpenseType", "ExpT_ID = " & _intExpenseType_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

#End Region

    End Class

End Namespace