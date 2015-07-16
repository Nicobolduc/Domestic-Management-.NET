Namespace Model

    Public Class Income
        Inherits BaseModel

        'Private members
        Private _intIncome_ID As Integer
        Private _strName As String = String.Empty
        Private _dblAmount As Double
        Private _dtReceptionDate As Nullable(Of Date)
        Private _period_ID As mConstants.Period
        Private _isMainIncome As Boolean

        'Private class members


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

        Public Property Amount As Double
            Get
                Return _dblAmount
            End Get
            Set(ByVal value As Double)
                _dblAmount = value
            End Set
        End Property

        Public Property ReceptionDate As Nullable(Of Date)
            Get
                Return _dtReceptionDate
            End Get
            Set(ByVal value As Nullable(Of Date))
                _dtReceptionDate = value
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

        Public Property IsMainIncome As Boolean
            Get
                Return _isMainIncome
            End Get
            Set(value As Boolean)
                _isMainIncome = value
            End Set
        End Property

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
                Else
                    'Error
                End If

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnIncome_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("Inc_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("Inc_ReceptDate", _dtReceptionDate, MySQLController.MySQL_FieldTypes.DATETIME_TYPE)
                    Case SQLController.bln_AddField("Inc_Amount", _dblAmount, MySQLController.MySQL_FieldTypes.DOUBLE_TYPE)
                    Case SQLController.bln_AddField("Inc_IsMain", _isMainIncome, MySQLController.MySQL_FieldTypes.BIT_TYPE)
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
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnIncome_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("Income", "Inc_ID = " & _intIncome_ID)
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
