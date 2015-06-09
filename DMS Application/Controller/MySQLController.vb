Imports MySql.Data.MySqlClient

Public Class MySQLController

    'Private members
    Private mblnTransactionStarted As Boolean

    Private mMySQLCmd As MySqlCommand
    Private mMySQLTransaction As MySqlTransaction

    Private mColFields As Dictionary(Of String, String)


#Region "Properties"

    Public ReadOnly Property blnTransactionStarted As Boolean
        Get
            Return mblnTransactionStarted
        End Get
    End Property

#End Region


#Region "Constructor"

    Public Sub New()
        mMySQLCmd = New MySqlCommand

        mColFields = New Dictionary(Of String, String)
    End Sub

#End Region


#Region "Shared Functions / Subs"

    Public Shared Function ADOSelect(ByVal vstrSQL As String) As MySqlDataReader
        Dim mySQLCmd As MySqlCommand = Nothing
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            mySQLCmd = New MySqlCommand(vstrSQL, gcAppControler.MySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLCmd) Then
                mySQLCmd.Dispose()
            End If
        End Try

        Return mySQLReader
    End Function

    Public Shared Function str_ADOSingleLookUp(ByVal vstrField As String, ByVal vstrTable As String, ByVal vstrWhere As String) As String
        Dim strReturnValue As String = String.Empty
        Dim mySQLCmd As MySqlCommand = Nothing
        Dim mySQLReader As MySqlDataReader = Nothing
        Dim strSQL As String = String.Empty

        Try
            strSQL = "SELECT " & vstrField & " FROM " & vstrTable & " WHERE " & vstrWhere

            mySQLCmd = New MySqlCommand(strSQL, gcAppControler.MySQLConnection)

            mySQLReader = mySQLCmd.ExecuteReader

            If mySQLReader.Read Then
                strReturnValue = mySQLReader.Item(vstrField).ToString
            End If

            mySQLCmd.Dispose()

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return strReturnValue
    End Function

#End Region


#Region "Functions / Subs"



    Public Function bln_BeginTransaction() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mMySQLTransaction = gcAppControler.MySQLConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            mblnTransactionStarted = True
            mMySQLCmd.Transaction = mMySQLTransaction
            mMySQLCmd.Connection = gcAppControler.MySQLConnection
            mMySQLCmd.CommandType = CommandType.Text

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_EndTransaction(ByVal vblnCommitChanges As Boolean) As Boolean
        Dim blnValidReturn As Boolean

        Try
            If vblnCommitChanges Then
                mMySQLTransaction.Commit()
            Else
                mMySQLTransaction.Rollback()
            End If

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mblnTransactionStarted = False
            mMySQLTransaction.Dispose()
            mMySQLCmd.Dispose()
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_AddField(ByVal vstrField As String, ByVal vobjValue As Object, ByVal vintDBType As mConstants.MySQL_FieldTypes) As Boolean
        Dim blnValidReturn As Boolean = True
        Dim vstrValue As String = String.Empty

        vstrValue = vobjValue.ToString

        Try
            If String.IsNullOrEmpty(vstrValue) Then
                vstrValue = "NULL"
            Else
                Select Case vintDBType
                    Case mConstants.MySQL_FieldTypes.VARCHAR_TYPE
                        vstrValue = gcAppControler.str_FixStringForSQL(vstrValue)

                    Case mConstants.MySQL_FieldTypes.DATETIME_TYPE
                        vstrValue = Format(CDate(vstrValue), gcAppControler.str_GetServerDateTimeFormat)
                        vstrValue = gcAppControler.str_FixStringForSQL(vstrValue)

                    Case mConstants.MySQL_FieldTypes.DOUBLE_TYPE, mConstants.MySQL_FieldTypes.INT_TYPE
                        vstrValue = vstrValue.ToString

                    Case mConstants.MySQL_FieldTypes.INT_TYPE
                        If vstrValue = "0" Then
                            vstrValue = "NULL"
                        Else
                            vstrValue = vstrValue.ToString
                        End If

                    Case mConstants.MySQL_FieldTypes.TINYINT_TYPE
                        If vstrValue = True.ToString Then
                            vstrValue = "1"
                        ElseIf vstrValue = False.ToString Then
                            vstrValue = "0"
                        Else
                            blnValidReturn = False
                        End If

                End Select
            End If

            mColFields.Add(vstrField, vstrValue)

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_ADOInsert(ByVal vstrTable As String, Optional ByRef rintNewItem_ID As Integer = 0) As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim strFields As String = String.Empty
        Dim strValues As String = String.Empty

        strSQL = " INSERT INTO " & vstrTable & vbCrLf

        Try
            For Each strKey As String In mColFields.Keys

                strFields = strFields & CStr(IIf(strFields = String.Empty, " (", ",")) & strKey

                strValues = strValues & CStr(IIf(strValues = String.Empty, " VALUES (", ",")) & mColFields.Item(strKey).ToString()
            Next

            strFields = strFields & ") " & vbCrLf
            strValues = strValues & ") " & vbCrLf

            mMySQLCmd.CommandText = strSQL & strFields & strValues

            mMySQLCmd.ExecuteNonQuery()

            mMySQLCmd.CommandText = "SELECT LAST_INSERT_ID()"

            rintNewItem_ID = CInt(mMySQLCmd.ExecuteScalar)

            mColFields.Clear()

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_ADOUpdate(ByVal vstrTable As String, ByVal vstrWhere As String) As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim strFields As String = String.Empty

        strSQL = "          UPDATE " & vstrTable & vbCrLf
        strSQL = strSQL & " SET "

        Try
            For Each strKey As String In mColFields.Keys
                mColFields.Item(strKey).ToString()

                strFields = strFields & CStr(IIf(strFields = String.Empty, String.Empty, ",")) & strKey & "=" & mColFields.Item(strKey).ToString()
            Next

            mMySQLCmd.CommandText = strSQL & strFields & " WHERE " & vstrWhere

            mMySQLCmd.ExecuteNonQuery()

            mColFields.Clear()

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mColFields.Clear()
        End Try

        Return blnValidReturn
    End Function

    Public Function bln_ADODelete(ByVal vstrTable As String, ByVal vstrWhere As String) As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        strSQL = " DELETE FROM " & vstrTable & vbCrLf

        Try
            mMySQLCmd.CommandText = strSQL & " WHERE " & vstrWhere

            mMySQLCmd.ExecuteNonQuery()

            mColFields.Clear()

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region

End Class
