Public Class frmProduct

    Private mcSQL As clsSQL_Transactions

    Public mintPro_ID As Integer
    Public mintFormMode As clsConstants.Form_Modes


    Private Function blnProduit_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Pro_Nom", txtName.Text, clsConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_AddField("ProC_ID", CStr(cboCategory.SelectedIndex + 1), clsConstants.MySQL_FieldTypes.INT_TYPE)
                Case mcSQL.bln_ADOInsert("Expense")
                Case Else
                    blnReturn = True
            End Select
            'test   
        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboType_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductType " & vbCrLf
            strSQL = strSQL & " ORDER BY ProductType.ProT_ID " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "ProT_ID", "ProT_Name", True, cboType)

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboCategory_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductCategory " & vbCrLf
            strSQL = strSQL & " WHERE ProductCategory.ProT_ID = " & cboType.SelectedIndex & vbCrLf
            strSQL = strSQL & " ORDER BY ProductCategory.ProC_ID " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "ProC_ID", "ProC_Name", True, cboCategory)

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboBrand_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT Brand.Bra_ID, " & vbCrLf
            strSQL = strSQL & "        Brand.Bra_Name " & vbCrLf
            strSQL = strSQL & " FROM Brand " & vbCrLf
            strSQL = strSQL & " ORDER BY Brand.Bra_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "Bra_ID", "Bra_Name", True, cboBrand)

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Sub frmProduct_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim blnReturn As Boolean

        Select Case False
            Case blnCboType_Load()
            Case mintFormMode = clsConstants.Form_Modes.INSERT
                blnReturn = True
            Case Else
                blnReturn = True
        End Select

        If Not blnReturn Then
            Me.Close()
        Else
            'Do nothing
        End If
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.SelectedIndex > 0 Then
            blnCboCategory_Load()
        Else
            cboCategory.Items.Clear()
        End If
    End Sub
End Class