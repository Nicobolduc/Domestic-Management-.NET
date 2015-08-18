Public Class frmProductBrand

    'Private class members
    Private mcSQL As MySQLController
    Private mcProductBrandModel As Model.ProductBrand


#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcProductBrandModel = gcAppCtrl.GetCoreModelController.GetProductController.Value.GetProductBrand(formController.Item_ID)

            If Not mcProductBrandModel Is Nothing Then

                txtName.Text = mcProductBrandModel.Name

                blnValidReturn = True
            End If

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
                Case blnSyncProductBrandModel()
                Case mcSQL.bln_BeginTransaction
                Case mcProductBrandModel.blnProductBrand_Save()
                Case Else
                    formController.Item_ID = mcProductBrandModel.ID
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

    Public Function blnSyncProductBrandModel() As Boolean
        Dim blnValidReturn As Boolean

        Try
            If mcProductBrandModel Is Nothing Then

                mcProductBrandModel = New Model.ProductBrand
            End If

            mcProductBrandModel.SQLController = mcSQL
            mcProductBrandModel.DLMCommand = formController.FormMode
            mcProductBrandModel.ID = formController.Item_ID
            mcProductBrandModel.Name = txtName.Text

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

#End Region


End Class