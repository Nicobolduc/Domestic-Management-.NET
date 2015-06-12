﻿Namespace Model

    Public Class Product

        'Private members
        Private _intProduct_ID As Integer
        Private _strName As String = String.Empty
        Private _intType_ID As Integer
        Private _intCategory_ID As Integer
        Private _blnIsTaxable As Boolean

        'Private class members
        Private mLstProductPrice As List(Of ProductPrice)
        Private mcSQL As MySQLController


#Region "Properties"

        Public ReadOnly Property ID As Integer
            Get
                Return _intProduct_ID
            End Get
        End Property

        Public Property Name As String
            Get
                Return _strName
            End Get

            Set(value As String)

                If Not value = String.Empty Then
                    _strName = value
                Else
                    'Non valide
                End If
            End Set
        End Property

        Public Property Type_ID As Integer
            Get
                Return _intType_ID
            End Get

            Set(value As Integer)
                If value > 0 Then
                    _intType_ID = value
                Else
                    'Non valide
                End If
            End Set
        End Property

        Public Property Category_ID As Integer
            Get
                Return _intCategory_ID
            End Get

            Set(value As Integer)
                _intCategory_ID = value
            End Set
        End Property

        Public Property isTaxable As Boolean
            Get
                Return _blnIsTaxable
            End Get

            Set(value As Boolean)
                _blnIsTaxable = value
            End Set
        End Property

#End Region


#Region "Constructors"

        Public Sub New()
            mLstProductPrice = New List(Of ProductPrice)
        End Sub

#End Region


#Region "Functions / Subs"

        Public Function blnProduct_Save(ByVal vFormMode As mConstants.Form_Modes) As Boolean
            Dim blnValidReturn As Boolean

            Try
                mcSQL = New MySQLController

                If mcSQL.bln_BeginTransaction() Then

                    Select Case vFormMode
                        Case mConstants.Form_Modes.INSERT_MODE
                            blnValidReturn = blnProduct_Insert()

                        Case mConstants.Form_Modes.UPDATE_MODE
                            blnValidReturn = blnProduct_Update()

                        Case mConstants.Form_Modes.DELETE_MODE
                            blnValidReturn = blnProduct_Delete()

                    End Select
                Else
                    'Error
                End If

            Catch ex As Exception
                blnValidReturn = False
                gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            Finally
                mcSQL.bln_EndTransaction(blnValidReturn)
                mcSQL = Nothing
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_AddFields() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case mcSQL.bln_AddField("Pro_Name", _strName, mConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                    Case mcSQL.bln_AddField("ProT_ID", _intType_ID.ToString, mConstants.MySQL_FieldTypes.INT_TYPE)
                    Case mcSQL.bln_AddField("ProC_ID", _intCategory_ID.ToString, mConstants.MySQL_FieldTypes.INT_TYPE)
                    Case mcSQL.bln_AddField("Pro_Taxable", _blnIsTaxable.ToString, mConstants.MySQL_FieldTypes.TINYINT_TYPE)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_Insert() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields
                    Case mcSQL.bln_ADOInsert("Product", _intProduct_ID)
                    Case _intProduct_ID > 0
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_Update() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case blnProduct_AddFields
                    Case mcSQL.bln_ADOUpdate("Product", "Pro_ID = " & _intProduct_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

        Private Function blnProduct_Delete() As Boolean
            Dim blnValidReturn As Boolean

            Try
                Select Case False
                    Case mcSQL.bln_ADODelete("ProductPrice", "Pro_ID = " & _intProduct_ID)
                    Case mcSQL.bln_ADODelete("Product", "Pro_ID = " & _intProduct_ID)
                    Case Else
                        blnValidReturn = True
                End Select

            Catch ex As Exception
                blnValidReturn = False
                gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            End Try

            Return blnValidReturn
        End Function

#End Region

    End Class

End Namespace