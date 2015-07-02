Option Strict Off

Public Class frmProduct

    'Private members
    Private Const mintGrdPrices_Action_col As Short = 1
    Private Const mintGrdPrices_ProP_ID_col As Short = 2
    Private Const mintGrdPrices_Cy_ID_col As Short = 3
    Private Const mintGrdPrices_Cy_Name_col As Short = 4
    Private Const mintGrdPrices_ProB_ID_col As Short = 5
    Private Const mintGrdPrices_ProB_Name_col As Short = 6
    Private Const mintGrdPrices_Price_col As Short = 7

    'Private class members
    Private WithEvents mcGrdPrices As SyncfusionGridController
    Private mcProductModel As Model.Product


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
    
        Try
            mcProductModel = gcAppController.GetCoreModelController.GetProductController.Value.GetProductFromID(formController.Item_ID)

            cboType.SelectedValue = mcProductModel.Type_ID

            Select Case False
                Case Not mcProductModel Is Nothing
                Case blnCboCategory_Load(mcProductModel.Category_ID)
                Case Else
                    txtName.Text = mcProductModel.Name
                    chkTaxable.Checked = mcProductModel.isTaxable

                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdPrices_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & "  SELECT " & DataGridViewController.GridRowActions.CONSULT_ACTION & " AS Action, " & vbCrLf
            strSQL = strSQL & "         ProductPrice.ProP_ID, " & vbCrLf
            strSQL = strSQL & "         Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "         Company.Cy_Name, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_ID, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_Name, " & vbCrLf
            strSQL = strSQL & "         ProductPrice.ProP_Price " & vbCrLf
            strSQL = strSQL & "  FROM ProductPrice " & vbCrLf
            strSQL = strSQL & "     INNER JOIN Company ON Company.Cy_ID = ProductPrice.Cy_ID_Seller " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductBrand ON ProductBrand.ProB_ID = ProductPrice.ProB_ID " & vbCrLf
            strSQL = strSQL & "  WHERE ProductPrice.Pro_ID = " & formController.Item_ID & vbCrLf
            strSQL = strSQL & "  ORDER BY Company.Cy_name " & vbCrLf

            blnValidReturn = mcGrdPrices.bln_FillData(strSQL)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboType_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "        ProductType.ProT_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductType " & vbCrLf
            strSQL = strSQL & " ORDER BY ProductType.ProT_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "ProT_ID", "ProT_Name", False, cboType)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboProductBrand_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT ProductBrand.ProB_ID, " & vbCrLf
            strSQL = strSQL & "        ProductBrand.ProB_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductBrand " & vbCrLf
            strSQL = strSQL & " ORDER BY ProductBrand.ProB_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "ProB_ID", "ProB_Name", False, cboProductBrand)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboCompany_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "        Company.Cy_Name " & vbCrLf
            strSQL = strSQL & " FROM Company " & vbCrLf
            strSQL = strSQL & " ORDER BY Company.Cy_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "Cy_ID", "Cy_Name", False, cboCompany)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboCategory_Load(Optional ByVal vintSelectedValue As Integer = 0) As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "        ProductCategory.ProC_Name " & vbCrLf
            strSQL = strSQL & " FROM ProductCategory " & vbCrLf
            strSQL = strSQL & " WHERE ProductCategory.ProT_ID = " & CInt(cboType.SelectedValue) & vbCrLf
            strSQL = strSQL & " ORDER BY ProductCategory.ProC_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "ProC_ID", "ProC_Name", True, cboCategory)

            cboCategory.SelectedValue = vintSelectedValue

            If formController.FormMode <> mConstants.Form_Modes.DELETE_MODE And vintSelectedValue >= 0 And cboCategory.Items.Count > 1 Then
                cboCategory.Enabled = True
            Else
                cboCategory.Enabled = False
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
            blnValidReturn = mcProductModel.blnProduct_Save(formController.FormMode)

            If blnValidReturn And formController.FormMode <> mConstants.Form_Modes.DELETE_MODE Then
                blnValidReturn = blnGrdPrices_SaveData()
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdPrices_SaveData() As Boolean
        Dim blnValidReturn As Boolean = True
        Dim intRowCpt As Integer

        Try
            For intRowCpt = 1 To grdPrices.RowCount

                Select Case CInt(grdPrices(intRowCpt, mintGrdPrices_Action_col).CellValue)
                    Case DataGridViewController.GridRowActions.INSERT_ACTION
                        'blnValidReturn = blnProductPrice_Insert(intRowCpt)


                    Case DataGridViewController.GridRowActions.UPDATE_ACTION
                        'blnValidReturn = blnProductPrice_Update(intRowCpt)

                    Case DataGridViewController.GridRowActions.DELETE_ACTION
                        'blnValidReturn = blnProductPrice_Delete(intRowCpt)

                    Case Else
                        blnValidReturn = True

                End Select
            Next

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdPrices_Cbo_Show(ByVal vintRowIndex As Integer, ByVal vintColIndex As Integer) As Boolean
        Dim blnValidReturn As Boolean

        Try
            If formController.FormMode <> mConstants.Form_Modes.DELETE_MODE Then

                Dim cellRectangle As Rectangle = grdPrices.GetCellRenderer(vintRowIndex, vintColIndex).GetCellClientRectangle(vintRowIndex, vintColIndex, GridStyleInfo.Default, False)

                Select Case vintColIndex
                    Case mintGrdPrices_Cy_Name_col

                        If Not mcGrdPrices.CellIsEmpty(vintRowIndex, vintColIndex) Then
                            cboCompany.SelectedValue = grdPrices(vintRowIndex, mintGrdPrices_Cy_ID_col).CellValue
                        End If

                        cboCompany.Location = New Point(cellRectangle.Location.X + 7, cellRectangle.Location.Y + (cellRectangle.Size.Height))

                        cboCompany.Size = cellRectangle.Size

                        cboCompany.Visible = True

                        cboCompany.Focus()

                    Case mintGrdPrices_ProB_Name_col

                        If Not mcGrdPrices.CellIsEmpty(vintRowIndex, vintColIndex) Then
                            cboProductBrand.SelectedValue = grdPrices(vintRowIndex, mintGrdPrices_ProB_ID_col).CellValue
                        End If

                        cboProductBrand.Location = New Point(cellRectangle.Location.X + 7, cellRectangle.Location.Y + (cellRectangle.Size.Height))

                        cboProductBrand.Size = cellRectangle.Size

                        cboProductBrand.Visible = True

                        cboProductBrand.Focus()

                End Select
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region


#Region "Private events"

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If Not formController.FormIsLoading Then

            If cboType.SelectedIndex >= 0 Then
                blnCboCategory_Load()
            Else
                cboCategory.DataSource = Nothing
                cboCategory.Enabled = False
            End If

            formController.ChangeMade = True
        End If
    End Sub

    Private Sub formController_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        mcProductModel = New Model.Product

        Select Case False
            Case mcGrdPrices.bln_Init(grdPrices, btnAddRow, btnRemoveRow)
            Case blnCboType_Load()
            Case blnCboCategory_Load()
            Case blnCboCompany_Load()
            Case blnCboProductBrand_Load()
            Case blnGrdPrices_Load()
            Case formController.FormMode <> mConstants.Form_Modes.INSERT_MODE
                blnValidReturn = True
            Case blnLoadData()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If

    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub cboProductBrand_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        formController.ChangeMade = True
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub formController_SetReadRights() Handles formController.SetReadRights
        Select Case formController.FormMode
            Case mConstants.Form_Modes.INSERT_MODE


            Case mConstants.Form_Modes.DELETE_MODE
                ' grdPrices.ClearSelection()

        End Select
    End Sub

    Private Sub formController_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub formController_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm
        Dim intRowIndex As Integer

        Select Case False
            Case txtName.Text <> String.Empty
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case cboType.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case cboCategory.SelectedIndex > -1
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select

        If eventArgs.IsValid And grdPrices.RowCount > 0 Then

            For intRowIndex = 1 To grdPrices.RowCount

                eventArgs.IsValid = False

                Select Case True
                    Case mcGrdPrices.CellIsEmpty(intRowIndex, mintGrdPrices_Cy_ID_col)
                        gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                        blnGrdPrices_Cbo_Show(intRowIndex, mintGrdPrices_Cy_Name_col)

                    Case mcGrdPrices.CellIsEmpty(intRowIndex, mintGrdPrices_ProB_ID_col)
                        gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                        blnGrdPrices_Cbo_Show(intRowIndex, mintGrdPrices_ProB_Name_col)

                    Case mcGrdPrices.CellIsEmpty(intRowIndex, mintGrdPrices_Price_col)
                        gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                        grdPrices.CurrentCell.MoveTo(GridRangeInfo.Cells(intRowIndex, mintGrdPrices_Price_col, intRowIndex, mintGrdPrices_Price_col), GridSetCurrentCellOptions.SetFocus And GridSetCurrentCellOptions.ScrollInView)
                        grdPrices.CurrentCell.BeginEdit()

                    Case Else
                        eventArgs.IsValid = True

                End Select

                If Not eventArgs.IsValid Then Exit For
            Next

        End If
    End Sub

    Private Sub mcGrdPrices_SetDisplay() Handles mcGrdPrices.SetDisplay
        mcGrdPrices.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.EXTEND_LAST_COL

        grdPrices.ColStyles(mintGrdPrices_Cy_Name_col).ReadOnly = True
        grdPrices.ColStyles(mintGrdPrices_ProB_Name_col).ReadOnly = True

        grdPrices.ColWidths(mintGrdPrices_Cy_Name_col) = 146
        grdPrices.ColWidths(mintGrdPrices_ProB_Name_col) = 100

        grdPrices.ColStyles(mintGrdPrices_Price_col).Format = mConstants.DataFormat.CURRENCY
        grdPrices.ColStyles(mintGrdPrices_Price_col).CellValueType = GetType(Double)

        grdPrices.Model.Options.ActivateCurrentCellBehavior = GridCellActivateAction.None

    End Sub

    Private Sub grdPrices_CellDoubleClick(sender As Object, e As GridCellClickEventArgs) Handles grdPrices.CellDoubleClick
        If mcGrdPrices.GetSelectedRowsCount > 0 Then

            Select Case mcGrdPrices.GetSelectedCol
                Case mintGrdPrices_Cy_Name_col, mintGrdPrices_ProB_Name_col
                    blnGrdPrices_Cbo_Show(mcGrdPrices.GetSelectedRow, mcGrdPrices.GetSelectedCol)

                Case Else
                    'grdPrices.BeginEdit(True)
            End Select
        End If
    End Sub

    Private Sub grdPrices_CurrentCellAcceptedChanges(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles grdPrices.CurrentCellAcceptedChanges

        If mcGrdPrices.GetSelectedCol = mintGrdPrices_Price_col And mcGrdPrices.GetSelectedRowsCount > 0 Then

            If Not IsNumeric(grdPrices(mcGrdPrices.GetSelectedRow, mcGrdPrices.GetSelectedCol).CellValue) And mcGrdPrices.CurrentCellIsEmpty Then

                gcAppController.ShowMessage(mConstants.Validation_Messages.NUMERIC_VALUE, MsgBoxStyle.Information)

                e.Cancel = True
            End If
        End If

        If Not e.Cancel Then
            formController.ChangeMade = True
        End If
    End Sub

    Private Sub cboCompany_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompany.Leave
        grdPrices(mcGrdPrices.GetSelectedRow, mintGrdPrices_Cy_ID_col).CellValue = cboCompany.SelectedValue
        grdPrices(mcGrdPrices.GetSelectedRow, mintGrdPrices_Cy_Name_col).CellValue = cboCompany.SelectedItem.Value

        cboCompany.Visible = False
        mcGrdPrices.ChangeMade = True
    End Sub

    Private Sub cboProductBrand_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProductBrand.Leave
        grdPrices(mcGrdPrices.GetSelectedRow, mintGrdPrices_ProB_ID_col).CellValue = cboProductBrand.SelectedValue
        grdPrices(mcGrdPrices.GetSelectedRow, mintGrdPrices_ProB_Name_col).CellValue = cboProductBrand.SelectedItem.Value

        cboProductBrand.Visible = False
    End Sub

    Private Sub chkTaxable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTaxable.CheckedChanged
        formController.ChangeMade = True
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        mcGrdPrices = New SyncfusionGridController
    End Sub

#End Region

End Class