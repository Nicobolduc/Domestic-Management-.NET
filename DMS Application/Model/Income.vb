Namespace Model

    Public Class Income
        Inherits BaseModel

        'Private members
        Private _intIncome_ID As Integer
        Private _strName As String = String.Empty
        Private _period_ID As mConstants.Period
        Private _isMainIncome As Boolean
        Private _intBudget_ID As Integer

        'Private class members
        Private mcLstIncPeriod As List(Of IncomePeriod)


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intIncome_ID
            End Get
            Set(ByVal value As Integer)
                _intIncome_ID = value
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

        Public Property CurrentIncAmount As IncomePeriod
            Get
                Return mcLstIncPeriod(mcLstIncPeriod.Count - 1)
            End Get
            Set(ByVal value As IncomePeriod)

                If mcLstIncPeriod.Count = 0 Then

                    mcLstIncPeriod.Add(value)
                Else
                    mcLstIncPeriod(mcLstIncPeriod.Count - 1) = value
                End If
            End Set
        End Property

        Public ReadOnly Property ReceptionDate As Date
            Get
                Return mcLstIncPeriod(mcLstIncPeriod.Count - 1).DateBegin
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

        Public Property IsMainIncome As Boolean
            Get
                Return _isMainIncome
            End Get
            Set(value As Boolean)
                _isMainIncome = value
            End Set
        End Property

        Public Property LstIncPeriod As List(Of IncomePeriod)
            Get
                Return mcLstIncPeriod
            End Get
            Set(ByVal value As List(Of IncomePeriod))
                mcLstIncPeriod = value
            End Set
        End Property

        Public Property Budget_ID As Integer
            Get
                Return _intBudget_ID
            End Get
            Set(value As Integer)
                _intBudget_ID = value
            End Set
        End Property

#End Region

#Region "Constructors"

        Public Sub New()
            mcLstIncPeriod = New List(Of IncomePeriod)
        End Sub

#End Region

#Region "Functions / Subs"

        Public Function blnIncome_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnIncome_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnIncome_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnIncome_Delete()

                    End Select

                    If blnValidReturn Then

                        For Each IncAmount As IncomePeriod In mcLstIncPeriod

                            IncAmount.SQLController = Me.SQLController
                            IncAmount._intIncome_ID = _intIncome_ID

                            blnValidReturn = IncAmount.blnIncomePeriod_Save

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

        Private Function blnIncome_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("Inc_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("Inc_IsMain", _isMainIncome, MySQLController.MySQL_FieldTypes.BIT_TYPE)
                    Case SQLController.bln_AddField("Per_ID", CInt(_period_ID), MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case SQLController.bln_AddField("Bud_ID", _intBudget_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnIncome_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnIncome_AddFields()
                    Case SQLController.bln_ADOInsert("Income", _intIncome_ID)
                    Case _intIncome_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnIncome_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnIncome_AddFields()
                    Case SQLController.bln_ADOUpdate("Income", "Inc_ID = " & _intIncome_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnIncome_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("IncomePeriod", "Inc_ID = " & _intIncome_ID)
                    Case SQLController.bln_ADODelete("Income", "Inc_ID = " & _intIncome_ID)
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
        Public Class IncomePeriod
            Inherits BaseModel

            'Private members
            Friend _intIncome_ID As Integer
            Private _dtDateBegin As Date
            Private _dtDateEnd As Nullable(Of Date)
            Private _dblAmount As Double


#Region "Properties"

            Public Property DateBegin As Date
                Get
                    Return _dtDateBegin
                End Get
                Set(ByVal value As Date)
                    _dtDateBegin = value
                End Set
            End Property

            Public Property DateEnd As Nullable(Of Date)
                Get
                    Return _dtDateEnd
                End Get
                Set(ByVal value As Nullable(Of Date))
                    _dtDateEnd = value
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

#End Region

            Friend Function blnIncomePeriod_Save() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    If SQLController.blnTransactionStarted Then

                        Select Case DLMCommand
                            Case Form_Mode.INSERT_MODE
                                blnValidReturn = blnIncomeAmount_Insert()

                            Case Form_Mode.UPDATE_MODE
                                blnValidReturn = blnIncomeAmount_Update()

                            Case Form_Mode.DELETE_MODE
                                blnValidReturn = blnIncomeAmount_Delete()

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

            Private Function blnIncomeAmount_AddFields() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case SQLController.bln_RefreshFields
                        Case SQLController.bln_AddField("Inc_ID", _intIncome_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                        Case SQLController.bln_AddField("IncP_DtBegin", _dtDateBegin, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                        Case SQLController.bln_AddField("IncP_DtEnd", _dtDateEnd, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                        Case SQLController.bln_AddField("IncP_Amount", _dblAmount, MySQLController.MySQL_FieldTypes.DECIMAL_TYPE)
                        Case Else
                            blnValidReturn = True
                    End Select

                Catch ex As Exception
                    blnValidReturn = False
                    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
                End Try

                Return blnValidReturn
            End Function

            Private Function blnIncomeAmount_Insert() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case blnIncomeAmount_AddFields()
                        Case SQLController.bln_ADOInsert("IncomePeriod")
                        Case Else
                            blnValidReturn = True
                    End Select

                Catch ex As Exception
                    blnValidReturn = False
                    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
                End Try

                Return blnValidReturn
            End Function

            Private Function blnIncomeAmount_Update() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case blnIncomeAmount_AddFields()
                        Case SQLController.bln_ADOUpdate("IncomePeriod", "Inc_ID = " & _intIncome_ID & " AND CASE WHEN (SELECT TNb.Nb FROM (SELECT COUNT(*) AS Nb FROM IncomePeriod WHERE IncomePeriod.Inc_ID = " & _intIncome_ID & ") AS TNb) = 1 THEN 1=1 ELSE IncP_DtBegin = " & gcAppCtrl.str_FixDateForSQL(_dtDateBegin.ToString) & " END ")
                        Case Else
                            blnValidReturn = True
                    End Select

                Catch ex As Exception
                    blnValidReturn = False
                    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
                End Try

                Return blnValidReturn
            End Function

            Private Function blnIncomeAmount_Delete() As Boolean
                Dim blnValidReturn As Boolean

                Try
                    Select Case False
                        Case SQLController.bln_ADODelete("IncomePeriod", "Inc_ID = " & _intIncome_ID)
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
