﻿Namespace Model

    Public Class ProductType
        Inherits BaseModel

        'Private members
        Private _intProductType_ID As Integer
        Private _strName As String = String.Empty

        'Private class members


#Region "Properties"

        Public Property ID As Integer
            Get
                Return _intProductType_ID
            End Get
            Set(value As Integer)
                _intProductType_ID = value
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

#End Region

#Region "Functions / Subs"

        Public Function blnProductType_Save() As Boolean
            Dim blnValidReturn As Boolean

            Try
                If SQLController.blnTransactionStarted Then

                    Select Case DLMCommand
                        Case mConstants.Form_Mode.INSERT_MODE
                            blnValidReturn = blnProductType_Insert()

                        Case mConstants.Form_Mode.UPDATE_MODE
                            blnValidReturn = blnProductType_Update()

                        Case mConstants.Form_Mode.DELETE_MODE
                            blnValidReturn = blnProductType_Delete()

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

        Private Function blnProduct_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_RefreshFields
                    Case SQLController.bln_AddField("ProT_Name", _strName, MySQLController.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductType_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields()
                    Case SQLController.bln_ADOInsert("ProductType", _intProductType_ID)
                    Case _intProductType_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductType_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields()
                    Case SQLController.bln_ADOUpdate("ProductType", "ProT_ID = " & _intProductType_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProductType_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case SQLController.bln_ADODelete("ProductType", "ProT_ID = " & _intProductType_ID)
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