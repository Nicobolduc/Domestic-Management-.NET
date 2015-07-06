Namespace Model

    Public Class Expense
        Inherits BaseModel

        'Private members
        Private _intExpense_ID As Integer
        Private _strName As String = String.Empty
        Private _dblAmount As Double
        Private _dtBillingDate As Nullable(Of Date)
        Private _period_ID As mConstants.Period

        'Private class members



#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intExpense_ID
            End Get
            Set(ByVal value As Integer)
                _intExpense_ID = value
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

        Public Property Amount As Double
            Get
                Return _dblAmount
            End Get
            Set(ByVal value As Double)
                _dblAmount = value
            End Set
        End Property

        Public Property BillingDate As Nullable(Of Date)
            Get
                Return _dtBillingDate
            End Get
            Set(ByVal value As Nullable(Of Date))
                _dtBillingDate = value
            End Set
        End Property

        Public Property Period As mConstants.Period
            Get
                Return _period_ID
            End Get
            Set(ByVal value As mConstants.Period)
                _period_ID = value
            End Set
        End Property

#End Region

#Region "Functions / Subs"

        Public Function blnExpense_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnExpense_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnExense_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnExpense_Delete()

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

        Private Function blnCompany_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("Exp_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("Exp_BillingDate", _dtBillingDate, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                    Case SQLController.bln_AddField("Exp_Amount", _dblAmount, MySQLController.MySQL_FieldTypes.DOUBLE_TYPE)
                    Case SQLController.bln_AddField("Per_ID", CInt(_period_ID), MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpense_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnCompany_AddFields()
                    Case SQLController.bln_ADOInsert("Expense", _intExpense_ID)
                    Case _intExpense_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExense_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnCompany_AddFields()
                    Case SQLController.bln_ADOUpdate("Expense", "Exp_ID = " & _intExpense_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpense_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("Expense", "Exp_ID = " & _intExpense_ID)
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
