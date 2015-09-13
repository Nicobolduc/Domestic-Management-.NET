Option Strict Off

Public Class frmProduct

    'Private members
    Private Const mintGrdPrices_Action_col As Short = 1
    Private Const mintGrdPrices_ProP_ID_col As Short = 2
    Private Const mintGrdPrices_Cy_Seller_ID_col As Short = 3
    Private Const mintGrdPrices_Cy_Seller_Name_col As Short = 4
    Private Const mintGrdPrices_ProB_ID_col As Short = 5
    Private Const mintGrdPrices_ProB_Name_col As Short = 6
    Private Const mintGrdPrices_Price_col As Short = 7

    'Private class members
    Private WithEvents mcGrdPricesController As SyncfusionGridController
    Private mcProductModel As Model.Product
    Private mcSQL As MySQLController


#Region "Constructors"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        mcGrdPricesController = New SyncfusionGridController
    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcProductModel = gcAppCtrl.GetCoreModelController.GetProductController.Value.GetProduct(formController.Item_ID)

            If Not mcProductModel Is Nothing Then

                cboType.SelectedValue = mcProductModel.Type.ID

                If Not mcProductModel.Category Is Nothing Then

                    blnValidReturn = blnCboCategory_Load(mcProductModel.Category.ID)
                Else
                    blnValidReturn = True
                End If

                If blnValidReturn Then
                    txtName.Text = mcProductModel.Name
                    chkTaxable.Checked = mcProductModel.IsTaxable
                End If
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnSyncProductModel() As Boolean
        Dim blnValidReturn As Boolean
        Dim productPrice As ProductPrice

        Try
            If mcProductModel Is Nothing Then

                mcProductModel = New Model.Product
            Else
                mcProductModel.Category = New Model.ProductCategory
            End If

            mcProductModel.SQLController = mcSQL
            mcProductModel.DLMCommand = formController.FormMode
            mcProductModel.ID = formController.Item_ID
            mcProductModel.Name = txtName.Text
            mcProductModel.IsTaxable = chkTaxable.Checked
            mcProductModel.Type.ID = cboType.SelectedValue
            mcProductModel.Category.ID = cboCategory.SelectedValue

            For intRowIdx As Integer = 1 To grdPrices.RowCount

                productPrice = New ProductPrice
                productPrice.SetMySQL = mcSQL

                productPrice.ProductPrice_ID = Val(grdPrices(intRowIdx, mintGrdPrices_ProP_ID_col).CellValue)
                productPrice.DLMCommand = grdPrices(intRowIdx, mintGrdPrices_Action_col).CellValue
                productPrice.Product_ID = mcProductModel.ID
                productPrice.CompanySeller_ID = grdPrices(intRowIdx, mintGrdPrices_Cy_Seller_ID_col).CellValue
                productPrice.Price = grdPrices(intRowIdx, mintGrdPrices_Price_col).CellValue
                productPrice.ProductBrand_ID = Val(grdPrices(intRowIdx, mintGrdPrices_ProB_ID_col).CellValue)

                mcProductModel.GetLstProductPrice.Add(productPrice)
            Next

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            strSQL = strSQL & "  ORDER BY Company.Cy_name, ProductBrand.ProB_Name, ProductPrice.ProP_Price " & vbCrLf

            blnValidReturn = mcGrdPricesController.bln_FillData(strSQL)

            If blnValidReturn Then

                strSQL = String.Empty
                strSQL = strSQL & " SELECT ProductBrand.ProB_ID, " & vbCrLf
                strSQL = strSQL & "        ProductBrand.ProB_Name " & vbCrLf
                strSQL = strSQL & " FROM ProductBrand " & vbCrLf
                strSQL = strSQL & " ORDER BY ProductBrand.ProB_Name " & vbCrLf

                mcGrdPricesController.SetColType_ComboBox(strSQL, mintGrdPrices_ProB_Name_col, "ProB_ID", "ProB_Name", False)

                strSQL = String.Empty
                strSQL = strSQL & " SELECT Company.Cy_ID, " & vbCrLf
                strSQL = strSQL & "        Company.Cy_Name " & vbCrLf
                strSQL = strSQL & " FROM Company " & vbCrLf
                strSQL = strSQL & " ORDER BY Company.Cy_Name " & vbCrLf

                mcGrdPricesController.SetColType_ComboBox(strSQL, mintGrdPrices_Cy_Seller_Name_col, "Cy_ID", "Cy_Name", False)
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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

            If formController.FormMode <> mConstants.Form_Mode.DELETE_MODE And vintSelectedValue >= 0 And cboCategory.Items.Count > 1 Then
                cboCategory.Enabled = True
            Else
                cboCategory.Enabled = False
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
                Case blnSyncProductModel()
                Case mcSQL.bln_BeginTransaction
                Case mcProductModel.blnProduct_Save()
                Case Else
                    formController.Item_ID = mcProductModel.ID
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mcSQL.bln_EndTransaction(blnValidReturn)
        End Try

        Return blnValidReturn
    End Function

    'Private Function blnGrdPrices_Cbo_Show(ByVal vintRowIndex As Integer, ByVal vintColIndex As Integer) As Boolean
    '    Dim blnValidReturn As Boolean

    '    Try
    '        If formController.FormMode <> mConstants.Form_Modes.DELETE_MODE Then

    '            Dim cellRectangle As Rectangle = grdPrices.GetCellRenderer(vintRowIndex, vintColIndex).GetCellClientRectangle(vintRowIndex, vintColIndex, GridStyleInfo.Default, False)

    '            Select Case vintColIndex
    '                Case mintGrdPrices_Cy_Seller_Name_col

    '                    If Not mcGrdPricesController.CellIsEmpty(vintRowIndex, vintColIndex) Then
    '                        cboCompany.SelectedValue = grdPrices(vintRowIndex, mintGrdPrices_Cy_Seller_ID_col).CellValue
    '                    End If

    '                    cboCompany.Location = New Point(cellRectangle.Location.X + 7, cellRectangle.Location.Y + (cellRectangle.Size.Height))

    '                    cboCompany.Size = cellRectangle.Size

    '                    cboCompany.Visible = True

    '                    cboCompany.Focus()

    '            End Select
    '        End If

    '    Catch ex As Exception
    '        blnValidReturn = False
    '        gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
    '    End Try

    '    Return blnValidReturn
    'End Function

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

        Select Case False
            Case mcGrdPricesController.bln_Init(grdPrices, btnAddRow, btnRemoveRow)
            Case blnCboType_Load()
            Case blnCboCategory_Load()
            Case blnGrdPrices_Load()
            Case formController.FormMode <> mConstants.Form_Mode.INSERT_MODE
                blnValidReturn = True
            Case blnFormData_Load()
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

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        formController.ChangeMade = True
    End Sub

    Private Sub formController_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnFormData_Save()
    End Sub

    Private Sub formController_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles formController.ValidateForm

        Select Case False
            Case txtName.Text <> String.Empty
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case cboType.SelectedIndex > -1
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case cboCategory.SelectedIndex > -1
                gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case mcGrdPricesController.bln_ValidateGridEvent
            Case Else
                eventArgs.IsValid = True

        End Select
    End Sub

    Private Sub mcGrdPrices_SetDisplay() Handles mcGrdPricesController.SetDisplay
        mcGrdPricesController.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.EXTEND_LAST_COL

        grdPrices.ColWidths(mintGrdPrices_Cy_Seller_Name_col) = 150
        grdPrices.ColWidths(mintGrdPrices_ProB_Name_col) = 130

        grdPrices.ColStyles(mintGrdPrices_Price_col).Format = mConstants.DataFormat.CURRENCY
        grdPrices.ColStyles(mintGrdPrices_Price_col).CellValueType = GetType(Double)
    End Sub

    Private Sub grdPrices_CellDoubleClick(ByVal sender As Object, ByVal e As GridCellClickEventArgs) Handles grdPrices.CellDoubleClick
        If mcGrdPricesController.GetSelectedRowsCount > 0 Then

            Select Case mcGrdPricesController.GetSelectedCol
                Case mintGrdPrices_Cy_Seller_Name_col ', mintGrdPrices_ProB_Name_col
                    'blnGrdPrices_Cbo_Show(mcGrdPricesController.GetSelectedRow, mcGrdPricesController.GetSelectedCol)

                Case Else
                    'Do nothing
            End Select
        End If
    End Sub

    Private Sub grdPrices_CurrentCellAcceptedChanges(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdPrices.CurrentCellAcceptedChanges

        If mcGrdPricesController.GetSelectedRowsCount > 0 Then

            Select Case mcGrdPricesController.GetSelectedCol
                Case mintGrdPrices_Price_col
                    If Not IsNumeric(grdPrices(mcGrdPricesController.GetSelectedRow, mcGrdPricesController.GetSelectedCol).CellValue) And mcGrdPricesController.CurrentCellIsEmpty Then

                        gcAppCtrl.ShowMessage(mConstants.Validation_Message.NUMERIC_VALUE, MsgBoxStyle.Information)

                        e.Cancel = True
                    End If

                Case mintGrdPrices_ProB_Name_col
                    grdPrices(mcGrdPricesController.GetSelectedRow, mintGrdPrices_ProB_ID_col).CellValue = grdPrices(mcGrdPricesController.GetSelectedRow, mintGrdPrices_ProB_Name_col).CellValue

                Case mintGrdPrices_Cy_Seller_Name_col
                    grdPrices(mcGrdPricesController.GetSelectedRow, mintGrdPrices_Cy_Seller_ID_col).CellValue = grdPrices(mcGrdPricesController.GetSelectedRow, mintGrdPrices_Cy_Seller_Name_col).CellValue

            End Select

        End If

        If Not e.Cancel Then
            formController.ChangeMade = True
        End If
    End Sub

    'Private Sub cboCompany_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompany.Leave
    '    grdPrices(mcGrdPricesController.GetSelectedRow, mintGrdPrices_Cy_Seller_ID_col).CellValue = cboCompany.SelectedValue
    '    grdPrices(mcGrdPricesController.GetSelectedRow, mintGrdPrices_Cy_Seller_Name_col).CellValue = cboCompany.SelectedItem.Value

    '    cboCompany.Visible = False
    '    mcGrdPricesController.ChangeMade = True
    'End Sub

    Private Sub chkTaxable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTaxable.CheckedChanged
        formController.ChangeMade = True
    End Sub

    Private Sub mcGrdPrices_ValidateData(ByVal eventArgs As ValidateGridEventArgs) Handles mcGrdPricesController.ValidateGridData
        If grdPrices.RowCount < 1 Then

            gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)
            eventArgs.IsValid = False
        Else
            For intRowIndex = 1 To grdPrices.RowCount

                eventArgs.IsValid = False

                Select Case True
                    Case mcGrdPricesController.CellIsEmpty(intRowIndex, mintGrdPrices_Cy_Seller_ID_col)
                        gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)

                        mcGrdPricesController.SetSelectedCol(True) = mintGrdPrices_Cy_Seller_Name_col

                    Case mcGrdPricesController.CellIsEmpty(intRowIndex, mintGrdPrices_ProB_ID_col)
                        gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)

                        mcGrdPricesController.SetSelectedCol(True) = mintGrdPrices_ProB_Name_col

                    Case mcGrdPricesController.CellIsEmpty(intRowIndex, mintGrdPrices_Price_col)
                        gcAppCtrl.ShowMessage(mConstants.Validation_Message.MANDATORY_VALUE, MsgBoxStyle.Information)

                        mcGrdPricesController.SetSelectedCol() = mintGrdPrices_Price_col

                    Case Else
                        eventArgs.IsValid = True

                End Select

                If Not eventArgs.IsValid Then Exit For
            Next
        End If
    End Sub

#End Region

End Class