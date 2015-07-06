Namespace Model

    Public Class Company
        Inherits BaseModel

        'Private members
        Private _intCompany_ID As Integer
        Private _strName As String = String.Empty
        Private _intType_ID As Integer

        'Private class members


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intCompany_ID
            End Get
            Set(ByVal value As Integer)
                _intCompany_ID = value
            End Set
        End Property

        Public Property Type_ID As Integer
            Get
                Return _intType_ID
            End Get
            Set(ByVal value As Integer)
                _intType_ID = value
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

        Public Function blnCompany_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnCompagny_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnCompagny_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnCompagny_Delete()

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
                    Case SQLController.bln_AddField("Cy_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case SQLController.bln_AddField("CyT_ID", _intType_ID, MySQLController.MySQL_FieldTypes.ID_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnCompagny_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnCompany_AddFields()
                    Case SQLController.bln_ADOInsert("Company", _intCompany_ID)
                    Case _intCompany_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnCompagny_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnCompany_AddFields()
                    Case SQLController.bln_ADOUpdate("Company", "Cy_ID = " & _intCompany_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnCompagny_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("Company", "Cy_ID = " & _intCompany_ID)
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