Namespace Model

    Public Class PaidExpense
        Inherits BaseModel

        'Private members
        Private _intPaidExpense_ID As Integer
        Private _intExpense_ID As Integer
        Private _dtExpenseBillingDate_ID As DateTime
        Private _dblAmountPaid As Double
        Private _dtDatePaid As Date
        Private _strComment As String = String.Empty


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intPaidExpense_ID
            End Get
            Set(ByVal value As Integer)
                _intPaidExpense_ID = value
            End Set
        End Property

        Public Property Expense_ID As Integer
            Get
                Return _intExpense_ID
            End Get
            Set(ByVal value As Integer)
                _intExpense_ID = value
            End Set
        End Property

        Public Property ExpenseBillingDate_ID As DateTime
            Get
                Return _dtExpenseBillingDate_ID
            End Get
            Set(value As DateTime)
                _dtExpenseBillingDate_ID = value
            End Set
        End Property

        Public Property AmountPaid As Double
            Get
                Return _dblAmountPaid
            End Get
            Set(ByVal value As Double)
                _dblAmountPaid = value
            End Set
        End Property

        Public Property DatePaid As Date
            Get
                Return _dtDatePaid
            End Get
            Set(ByVal value As Date)
                _dtDatePaid = value
            End Set
        End Property

        Public Property Comment As String
            Get
                Return _strComment
            End Get
            Set(ByVal value As String)
                _strComment = value
            End Set
        End Property

#End Region

#Region "Functions / Subs"

        Public Function blnPaidExpense_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnPaidExpense_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnPaidExense_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnExpense_Delete()

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

        Private Function blnPaidExpense_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("Exp_ID", _intExpense_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case SQLController.bln_AddField("Exp_BilledDate", _dtExpenseBillingDate_ID, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                    Case SQLController.bln_AddField("PExp_AmountPaid", _dblAmountPaid, MySQLController.MySQL_FieldTypes.DECIMAL_TYPE)
                    Case SQLController.bln_AddField("PExp_DatePaid", _dtDatePaid, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                    Case SQLController.bln_AddField("PExp_Comment", _strComment, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnPaidExpense_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnPaidExpense_AddFields()
                    Case SQLController.bln_ADOInsert("PaidExpense", _intPaidExpense_ID)
                    Case _intPaidExpense_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnPaidExense_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnPaidExpense_AddFields()
                    Case SQLController.bln_ADOUpdate("PaidExpense", "PExp_ID = " & _intPaidExpense_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpense_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("PaidExpense", "PExp_ID = " & _intPaidExpense_ID)
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
