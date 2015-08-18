Namespace Model

    Public Class ProductBrand
        Inherits BaseModel

        'Private members
        Private _intBrand_ID As Integer
        Private _strName As String = String.Empty

        'Private class members


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intBrand_ID
            End Get
            Set(ByVal value As Integer)
                _intBrand_ID = value
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

        Public Function blnProductBrand_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnProductBrand_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnProductBrand_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnProductBrand_Delete()

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

        Private Function blnProductBrand_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("ProB_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductBrand_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProductBrand_AddFields()
                    Case SQLController.bln_ADOInsert("ProductBrand", _intBrand_ID)
                    Case _intBrand_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductBrand_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProductBrand_AddFields()
                    Case SQLController.bln_ADOUpdate("ProductBrand", "ProB_ID = " & _intBrand_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductBrand_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("ProductBrand", "ProB_ID = " & _intBrand_ID)
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