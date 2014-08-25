﻿Imports MySql.Data.MySqlClient

Public Class clsSQL_Transactions


    Private mblnTransactionStarted As Boolean

    Private mMySQLCmd As MySqlCommand
    Private mMySQLTrans As MySqlTransaction

    Private mColFields As Dictionary(Of String, String)


    Public ReadOnly Property blnTransactionStarted As Boolean
        Get
            Return mblnTransactionStarted
        End Get
    End Property


    Public Sub New()
        mMySQLCmd = New MySqlCommand

        mColFields = New Dictionary(Of String, String)
    End Sub

    Public Function bln_BeginTransaction() As Boolean
        Dim blnReturn As Boolean

        Try
            mMySQLTrans = gcApp.cMySQLConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            mblnTransactionStarted = True
            mMySQLCmd.Transaction = mMySQLTrans
            mMySQLCmd.Connection = gcApp.cMySQLConnection
            mMySQLCmd.CommandType = CommandType.Text

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function bln_EndTransaction(ByVal vblnCommitChanges As Boolean) As Boolean
        Dim blnReturn As Boolean

        Try
            If vblnCommitChanges Then
                mMySQLTrans.Commit()
            Else
                mMySQLTrans.Rollback()
            End If

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mblnTransactionStarted = False
            mMySQLCmd.Dispose()
        End Try

        Return blnReturn
    End Function

    Public Function bln_AddField(ByVal vstrField As String, ByVal vstrValue As String, ByVal vintDBType As clsConstants.MySQL_FieldTypes) As Boolean
        Dim blnReturn As Boolean

        Try

            Select Case vintDBType
                Case clsConstants.MySQL_FieldTypes.VARCHAR_TYPE, clsConstants.MySQL_FieldTypes.DATETIME_TYPE
                    vstrValue = "'" & vstrValue & "'"

            End Select

            mColFields.Add(vstrField, vstrValue)

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function bln_ADOInsert(ByVal vstrTable As String) As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString
        Dim strFields As String = vbNullString
        Dim strValues As String = vbNullString

        strSQL = " INSERT INTO " & vstrTable & vbCrLf

        Try
            For Each strKey As String In mColFields.Keys

                strFields = strFields & CStr(IIf(strFields = vbNullString, " (", ",")) & strKey

                strValues = strValues & CStr(IIf(strValues = vbNullString, " VALUES (", ",")) & mColFields.Item(strKey).ToString()
            Next

            strFields = strFields & ") " & vbCrLf
            strValues = strValues & ") " & vbCrLf

            mMySQLCmd.CommandText = strSQL & strFields & strValues

            mMySQLCmd.ExecuteNonQuery()

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function bln_ADOUpdate(ByVal vstrTable As String, ByVal vstrWhere As String) As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString
        Dim strFields As String = vbNullString

        strSQL = "          UPDATE " & vstrTable & vbCrLf
        strSQL = strSQL & " SET "

        Try
            For Each strKey As String In mColFields.Keys
                mColFields.Item(strKey).ToString()

                strFields = strFields & CStr(IIf(strFields = vbNullString, vbNullString, ",")) & strKey & "=" & mColFields.Item(strKey).ToString()
            Next

            mMySQLCmd.CommandText = strSQL & strFields & " WHERE " & vstrWhere

            mMySQLCmd.ExecuteNonQuery()

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Public Function bln_ADODelete(ByVal vstrTable As String, ByVal vstrWhere As String) As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        strSQL = " DELETE FROM " & vstrTable & vbCrLf

        Try
            mMySQLCmd.CommandText = strSQL & " WHERE " & vstrWhere

            mMySQLCmd.ExecuteNonQuery()

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

End Class
