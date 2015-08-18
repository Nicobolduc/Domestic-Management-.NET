Public Class frmExpenseType

    'Private class members
    Private mcSQL As MySQLController
    Private mcExpenseTypeModel As Model.ExpenseType


#Region "Constructors"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        mcExpenseTypeModel = New Model.ExpenseType

    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcExpenseTypeModel = gcAppCtrl.GetCoreModelController.GetFinanceController.GetExpenseType(formController.Item_ID)

            If Not mcExpenseTypeModel Is Nothing Then

                txtName.Text = mcExpenseTypeModel.Name
                btnColor.BackColor = Color.FromArgb(mcExpenseTypeModel.ArgbColor)

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
                Case blnSyncExpenseTypeModel()
                Case mcSQL.bln_BeginTransaction()
                Case mcExpenseTypeModel.blnExpenseType_Save()
                Case Else
                    formController.Item_ID = mcExpenseTypeModel.ID
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

    Public Function blnSyncExpenseTypeModel() As Boolean
        Dim blnValidReturn As Boolean

        Try
            If mcExpenseTypeModel Is Nothing Then

                mcExpenseTypeModel = New Model.ExpenseType
            End If

            mcExpenseTypeModel.SQLController = mcSQL
            mcExpenseTypeModel.DLMCommand = formController.FormMode
            mcExpenseTypeModel.ID = formController.Item_ID
            mcExpenseTypeModel.Name = txtName.Text

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

    Private Sub btnColor_Click(sender As Object, e As EventArgs) Handles btnColor.Click

        ColorDialog.Color = Color.FromArgb(mcExpenseTypeModel.ArgbColor)

        If ColorDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then

            mcExpenseTypeModel.ArgbColor = ColorDialog.Color.ToArgb
            btnColor.BackColor = ColorDialog.Color

            formController.ChangeMade = True
        End If
    End Sub

#End Region

End Class