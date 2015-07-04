Public Class frmProductType

    'Private class members
    Private mcSQL As MySQLController
    Private mcProductTypeModel As Model.ProductType


#Region "Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        mcProductTypeModel = New Model.ProductType

    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnValidReturn As Boolean
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            mcProductTypeModel = gcAppController.GetCoreModelController.GetProductController.Value.GetProductType(formController.Item_ID)

            If Not mcProductTypeModel Is Nothing Then

                txtName.Text = mcProductTypeModel.Name

                blnValidReturn = True
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            Select Case False
                Case blnSyncProductTypeModel() 'TODO INTERFACE AVEC CA DEDANS
                Case mcSQL.bln_BeginTransaction()
                Case mcProductTypeModel.blnProductType_Save()
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
            mcSQL = Nothing
        End Try

        Return blnValidReturn
    End Function

    Public Function blnSyncProductTypeModel() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcProductTypeModel.SQLController = mcSQL
            mcProductTypeModel.DLMCommand = formController.FormMode
            mcProductTypeModel.ID = formController.Item_ID
            mcProductTypeModel.Name = txtName.Text

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region


#Region "Private events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        Select Case False
            Case formController.FormMode <> mConstants.Form_Modes.INSERT_MODE
                blnValidReturn = True
            Case blnLoadData()
            Case Else
                blnValidReturn = True
        End Select

    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub myFormControler_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Select Case False
            Case txtName.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
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