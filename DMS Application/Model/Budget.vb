Namespace Model

    Public Class Budget
        Inherits BaseModel

        'Private members
        Private _intBudget_ID As Integer
        Private _strName As String = String.Empty

        'Private class members


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intBudget_ID
            End Get
            Set(ByVal value As Integer)
                _intBudget_ID = value
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

#End Region

#Region "Functions / Subs"

        Public Function blnBudget_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnBudget_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnBudget_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnBudget_Delete()

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

        Private Function blnBudget_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("Cy_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnBudget_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnBudget_AddFields()
                    Case SQLController.bln_ADOInsert("Budget", _intBudget_ID)
                    Case _intBudget_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnBudget_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnBudget_AddFields()
                    Case SQLController.bln_ADOUpdate("Budget", "Bud_ID = " & _intBudget_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnBudget_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("Budget", "Bud_ID = " & _intBudget_ID)
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