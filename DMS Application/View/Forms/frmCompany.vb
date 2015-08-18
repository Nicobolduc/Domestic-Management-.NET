Public Class frmCompany

    'Private class members
    Private mcSQL As MySQLController
    Private mcCompanyModel As Model.Company


#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcCompanyModel = gcAppCtrl.GetCoreModelController.GetCompanyController.GetCompany(formController.Item_ID)

            If Not mcCompanyModel Is Nothing Then

                txtName.Text = mcCompanyModel.Name
                cboCompanyType.SelectedValue = mcCompanyModel.Type_ID

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboCompanyType_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT CompanyType.CyT_ID, " & vbCrLf
            strSQL = strSQL & "        CompanyType.CyT_Name " & vbCrLf
            strSQL = strSQL & " FROM CompanyType " & vbCrLf
            strSQL = strSQL & " ORDER BY CompanyType.CyT_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "CyT_ID", "CyT_Name", False, cboCompanyType)

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnFormData_Save() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            Select Case False
                Case blnSyncCompanyModel()
                Case mcSQL.bln_BeginTransaction()
                Case mcCompanyModel.blnCompany_Save()
                Case Else
                    formController.Item_ID = mcCompanyModel.ID
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
            mcSQL = Nothing
        End Try

        Return blnValidReturn
    End Function

    Public Function blnSyncCompanyModel() As Boolean
        Dim blnValidReturn As Boolean

        Try
            If mcCompanyModel Is Nothing Then

                mcCompanyModel = New Model.Company
            End If

            mcCompanyModel.SQLController = mcSQL
            mcCompanyModel.DLMCommand = formController.FormMode
            mcCompanyModel.ID = formController.Item_ID
            mcCompanyModel.Name = txtName.Text
            mcCompanyModel.Type_ID = CInt(cboCompanyType.SelectedValue)

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region

#Region "Private events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        Select Case False
            Case blnCboCompanyType_Load()
            Case formController.FormMode <> mConstants.Form_Mode.INSERT_MODE
                blnValidReturn = True
            Case blnFormData_Load()
            Case Else
                blnValidReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnFormData_Save()
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Select Case False
            Case txtName.Text <> String.Empty
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboCompanyType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompanyType.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

#End Region


End Class