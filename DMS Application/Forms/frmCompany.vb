﻿Public Class frmCompany

    'Private class members
    Private mcSQL As clsSQL_Transactions


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT Company.Cy_Name " & vbCrLf
            strSQL = strSQL & " FROM Company " & vbCrLf
            strSQL = strSQL & " WHERE Company.Cy_ID = " & myFormControler.Item_ID & vbCrLf

            mySQLReader = mSQL.ADOSelect(strSQL)

            mySQLReader.Read()

            txtName.Text = mySQLReader.Item("Cy_Name").ToString

            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnReturn As Boolean

        Try
            mcSQL = New clsSQL_Transactions

            mcSQL.bln_BeginTransaction()

            Select Case myFormControler.FormMode
                Case clsConstants.Form_Modes.INSERT_MODE
                    blnReturn = blnCompagny_Insert()

                Case clsConstants.Form_Modes.UPDATE_MODE
                    blnReturn = blnCompagny_Update()

                Case clsConstants.Form_Modes.DELETE_MODE
                    blnReturn = blnCompagny_Delete()

            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnReturn)
            mcSQL = Nothing
        End Try

        Return blnReturn
    End Function

    Private Function blnCompagny_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Cy_Name", txtName.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOInsert("Company", myFormControler.Item_ID)
                Case myFormControler.Item_ID > 0
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCompagny_Update() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Cy_Name", txtName.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOUpdate("Company", "Cy_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCompagny_Delete() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("Company", "Cy_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

#End Region


#Region "Private events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnReturn As Boolean

        Select Case False
            Case myFormControler.FormMode <> clsConstants.Form_Modes.INSERT_MODE
                blnReturn = True
            Case blnLoadData()
            Case Else
                blnReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles myFormControler.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub myFormControler_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles myFormControler.ValidateRules
        Select Case False
            Case txtName.Text <> vbNullString
                gcAppControler.ShowMessage(clsConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        myFormControler.ChangeMade = True
    End Sub

#End Region
End Class