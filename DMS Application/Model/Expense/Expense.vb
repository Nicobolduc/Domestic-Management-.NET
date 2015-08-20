Namespace Model

    Public Class Expense
        Inherits BaseModel

        'Private members
        Private _intExpense_ID As Integer
        Private _strName As String = String.Empty
        Private _period_ID As mConstants.Period
        Private _fixed As Boolean

        'Private class members
        Private mcExpenseType As ExpenseType
        Private mcLstExpPeriod As List(Of ExpensePeriod)


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

        Public Property LstExpPeriod As List(Of ExpensePeriod)
            Get
                Return mcLstExpPeriod
            End Get
            Set(value As List(Of ExpensePeriod))
                mcLstExpPeriod = value
            End Set
        End Property

        Public Property CurrentExpPeriod As ExpensePeriod
            Get
                Return mcLstExpPeriod(mcLstExpPeriod.Count - 1)
            End Get
            Set(value As ExpensePeriod)

                If mcLstExpPeriod.Count = 0 Then

                    mcLstExpPeriod.Add(value)
                Else
                    mcLstExpPeriod(mcLstExpPeriod.Count - 1) = value
                End If
            End Set
        End Property

        Public ReadOnly Property BillingDate As Date
            Get
                Return mcLstExpPeriod(mcLstExpPeriod.Count - 1).DateBegin
            End Get
        End Property

        Public Property Period As mConstants.Period
            Get
                Return _period_ID
            End Get
            Set(ByVal value As mConstants.Period)
                _period_ID = value
            End Set
        End Property

        Public Property Type As ExpenseType
            Get
                Return mcExpenseType
            End Get
            Set(value As ExpenseType)
                mcExpenseType = value
            End Set
        End Property

        Public Property Fixed As Boolean
            Get
                Return _fixed
            End Get
            Set(ByVal value As Boolean)
                _fixed = value
            End Set
        End Property

#End Region

#Region "Constructors"

        Public Sub New()
            mcLstExpPeriod = New List(Of ExpensePeriod)
            mcExpenseType = New ExpenseType
        End Sub

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

                    If blnValidReturn Then

                        For Each ExpAmount As ExpensePeriod In mcLstExpPeriod

                            ExpAmount.SQLController = Me.SQLController
                            ExpAmount._intExpense_ID = _intExpense_ID

                            blnValidReturn = ExpAmount.blnExpensePeriod_Save

                            If Not blnValidReturn Then Exit For
                        Next
                    End If
                Else
                    'Error
                End If

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpense_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("Exp_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("ExpT_ID", mcExpenseType.ID, MySQLController.MySQL_FieldTypes.DECIMAL_TYPE)
                    Case SQLController.bln_AddField("Per_ID", CInt(_period_ID), MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case SQLController.bln_AddField("Exp_Fixed", _fixed, MySQLController.MySQL_FieldTypes.BIT_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExpense_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnExpense_AddFields()
                    Case SQLController.bln_ADOInsert("Expense", _intExpense_ID)
                    Case _intExpense_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnExense_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnExpense_AddFields()
                    Case SQLController.bln_ADOUpdate("Expense", "Exp_ID = " & _intExpense_ID)
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
                    Case SQLController.bln_ADODelete("ExpensePeriod", "Exp_ID = " & _intExpense_ID)
                    Case SQLController.bln_ADODelete("Expense", "Exp_ID = " & _intExpense_ID)
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


        'Class for historic of amount over differents periods
        Public Class ExpensePeriod
            Inherits BaseModel

            'Private members
            Friend _intExpense_ID As Integer
            Private _dtDateBegin As Date
            Private _dtDateEnd As Nullable(Of Date)
            Private _dblAmount As Double


#Region "Properties"

            Public Property DateBegin As Date
                Get
                    Return _dtDateBegin
                End Get
                Set(value As Date)
                    _dtDateBegin = value
                End Set
            End Property

            Public Property DateEnd As Nullable(Of Date)
                Get
                    Return _dtDateEnd
                End Get
                Set(value As Nullable(Of Date))
                    _dtDateEnd = value
                End Set
            End Property

            Public Property Amount As Double
                Get
                    Return _dblAmount
                End Get
                Set(value As Double)
                    _dblAmount = value
                End Set
            End Property

#End Region

            Friend Function blnExpensePeriod_Save() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    If SQLController.blnTransactionStarted Then

                        Select Case DLMCommand
                            Case Form_Mode.INSERT_MODE
                                blnValidReturn = blnExpensePeriod_Insert()

                            Case Form_Mode.UPDATE_MODE
                                blnValidReturn = blnExensePeriod_Update()

                            Case Form_Mode.DELETE_MODE
                                blnValidReturn = blnExensePeriod_Delete()

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

            Private Function blnExpensePeriod_AddFields() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case SQLController.bln_RefreshFields
                        Case SQLController.bln_AddField("Exp_ID", _intExpense_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                        Case SQLController.bln_AddField("ExpA_DtBegin", _dtDateBegin, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                        Case SQLController.bln_AddField("ExpA_DtEnd", _dtDateEnd, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                        Case SQLController.bln_AddField("ExpA_Amount", _dblAmount, MySQLController.MySQL_FieldTypes.DECIMAL_TYPE)
                        Case Else
                            blnValidReturn = True
                    End Select

                Catch ex As Exception
                    blnValidReturn = False
                    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
                End Try

                Return blnValidReturn
            End Function

            Private Function blnExpensePeriod_Insert() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case blnExpensePeriod_AddFields()
                        Case SQLController.bln_ADOInsert("ExpensePeriod")
                        Case Else
                            blnValidReturn = True
                    End Select

                Catch ex As Exception
                    blnValidReturn = False
                    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
                End Try

                Return blnValidReturn
            End Function

            Private Function blnExensePeriod_Update() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case blnExpensePeriod_AddFields()
                        Case SQLController.bln_ADOUpdate("ExpensePeriod", "Exp_ID = " & _intExpense_ID & " AND CASE WHEN (SELECT TNb.Nb FROM (SELECT COUNT(*) AS Nb FROM ExpensePeriod WHERE ExpensePeriod.Exp_ID = " & _intExpense_ID & ") AS TNb) = 1 THEN 1=1 ELSE ExpA_DtBegin = " & gcAppCtrl.str_FixDateForSQL(_dtDateBegin.ToString) & " END ")
                        Case Else
                            blnValidReturn = True
                    End Select

                Catch ex As Exception
                    blnValidReturn = False
                    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
                End Try

                Return blnValidReturn
            End Function

            Private Function blnExensePeriod_Delete() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case SQLController.bln_ADODelete("ExpensePeriod", "Exp_ID = " & _intExpense_ID)
                        Case Else
                            blnValidReturn = True
                    End Select

                Catch ex As Exception
                    blnValidReturn = False
                    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
                End Try

                Return blnValidReturn
            End Function

        End Class

    End Class

End Namespace
