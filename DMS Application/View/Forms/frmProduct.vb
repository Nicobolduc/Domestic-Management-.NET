Option Strict Off

Public Class frmProduct

    'Private members
    Private Const mintGrdPrices_Action_col As Short = 0
    Private Const mintGrdPrices_ProP_ID_col As Short = 1
    Private Const mintGrdPrices_Cy_ID_col As Short = 2
    Private Const mintGrdPrices_Cy_Name_col As Short = 3
    Private Const mintGrdPrices_ProB_ID_col As Short = 4
    Private Const mintGrdPrices_ProB_Name_col As Short = 5
    Private Const mintGrdPrices_Price_col As Short = 6

    'Private class members

    Private WithEvents mcGrdPrices As DataGridViewController
    Private mcProductModel As Model.Product


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim intProC_ID As Integer

        Try
            mcProductModel = gcAppControler.CoreModelController.ProductController.GetProductFromID(myFormControler.Item_ID)

            Select Case False
                Case Not mcProductModel Is Nothing
                Case blnCboCategory_Load(intProC_ID)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            strSQL = strSQL & "     INNER JOIN Company ON Company.Cy_ID = ProductPrice.Cy_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductBrand ON ProductBrand.ProB_ID = ProductPrice.ProB_ID " & vbCrLf
            strSQL = strSQL & "  WHERE ProductPrice.Pro_ID = " & myFormControler.Item_ID & vbCrLf
            strSQL = strSQL & "  ORDER BY Company.Cy_name " & vbCrLf

            blnValidReturn = mcGrdPrices.bln_FillData(strSQL)

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
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

            If myFormControler.FormMode <> mConstants.Form_Modes.DELETE_MODE And vintSelectedValue >= 0 And cboCategory.Items.Count > 1 Then
                cboCategory.Enabled = True
            Else
                cboCategory.Enabled = False
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnSaveData() As Boolean
        Dim blnValidReturn As Boolean

        Try
            blnValidReturn = mcProductModel.blnProduct_Save(myFormControler.FormMode)

            If blnValidReturn And myFormControler.FormMode <> mConstants.Form_Modes.DELETE_MODE Then
                blnValidReturn = blnGrdPrices_SaveData()
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdPrices_SaveData() As Boolean
        Dim blnValidReturn As Boolean = True
        Dim intRowCpt As Integer

        Try
            For intRowCpt = 0 To grdPrices.Rows.Count - 1

                Select Case CInt(grdPrices.Rows(intRowCpt).Cells(mintGrdPrices_Action_col).Value)
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
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdPrices_Cbo_Show(ByVal vintRowIndex As Integer, ByVal vintColIndex As Integer) As Boolean
        Dim blnValidReturn As Boolean

        Try
            If myFormControler.FormMode <> mConstants.Form_Modes.DELETE_MODE Then

                Dim cellRectangle As Rectangle = grdPrices.GetCellDisplayRectangle(vintColIndex, vintRowIndex, True)

                Select Case vintColIndex
                    Case mintGrdPrices_Cy_Name_col
                        If Not mcGrdPrices.CellIsEmpty(vintRowIndex, vintColIndex) Then
                            cboCompany.SelectedValue = grdPrices.Rows(vintRowIndex).Cells(mintGrdPrices_Cy_ID_col).Value
                        End If

                        cboCompany.Location = New Point(cellRectangle.Location.X + 6, cellRectangle.Location.Y + (cellRectangle.Size.Height - 4))

                        cboCompany.Size = cellRectangle.Size

                        cboCompany.Visible = True

                        cboCompany.Focus()

                    Case mintGrdPrices_ProB_Name_col
                        If Not mcGrdPrices.CellIsEmpty(vintRowIndex, vintColIndex) Then
                            cboProductBrand.SelectedValue = grdPrices.Rows(vintRowIndex).Cells(mintGrdPrices_ProB_ID_col).Value
                        End If

                        cboProductBrand.Location = New Point(cellRectangle.Location.X + 6, cellRectangle.Location.Y + (cellRectangle.Size.Height - 4))

                        cboProductBrand.Size = cellRectangle.Size

                        cboProductBrand.Visible = True

                        cboProductBrand.Focus()

                End Select
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region


#Region "Private events"

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If Not myFormControler.FormIsLoading Then

            If cboType.SelectedIndex >= 0 Then
                blnCboCategory_Load()
            Else
                cboCategory.DataSource = Nothing
                cboCategory.Enabled = False
            End If

            myFormControler.ChangeMade = True
        End If
    End Sub

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnValidReturn As Boolean

        mcGrdPrices = New DataGridViewController
        mcProductModel = New Model.Product

        Select Case False
            Case mcGrdPrices.bln_Init(grdPrices)
            Case blnCboType_Load()
            Case blnCboCategory_Load()
            Case blnCboCompany_Load()
            Case blnCboProductBrand_Load()
            Case blnGrdPrices_Load()
            Case myFormControler.FormMode <> mConstants.Form_Modes.INSERT_MODE
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
        myFormControler.ChangeMade = True
    End Sub

    Private Sub cboBrand_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        myFormControler.ChangeMade = True
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategory.SelectedIndexChanged
        myFormControler.ChangeMade = True
    End Sub

    Private Sub myFormControler_SetReadRights() Handles myFormControler.SetReadRights
        Select Case myFormControler.FormMode
            Case mConstants.Form_Modes.INSERT_MODE


            Case mConstants.Form_Modes.DELETE_MODE
                grdPrices.ClearSelection()

        End Select
    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles myFormControler.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub myFormControler_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles myFormControler.ValidateRules
        Dim intRowIndex As Integer

        Select Case False
            Case txtName.Text <> String.Empty
                gcAppControler.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                txtName.Focus()

            Case cboType.SelectedIndex > -1
                gcAppControler.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case cboCategory.SelectedIndex > -1
                gcAppControler.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                cboType.DroppedDown = True
                cboType.Focus()

            Case Else
                eventArgs.IsValid = True

        End Select

        If eventArgs.IsValid And grdPrices.Rows.Count > 0 Then

            For intRowIndex = 0 To grdPrices.Rows.Count - 1

                eventArgs.IsValid = False

                Select Case True
                    Case mcGrdPrices.CellIsEmpty(intRowIndex, mintGrdPrices_Cy_ID_col)
                        gcAppControler.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                        blnGrdPrices_Cbo_Show(intRowIndex, mintGrdPrices_Cy_Name_col)

                    Case mcGrdPrices.CellIsEmpty(intRowIndex, mintGrdPrices_ProB_ID_col)
                        gcAppControler.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                        blnGrdPrices_Cbo_Show(intRowIndex, mintGrdPrices_ProB_Name_col)

                    Case mcGrdPrices.CellIsEmpty(intRowIndex, mintGrdPrices_Price_col)
                        gcAppControler.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
                        grdPrices.CurrentCell = grdPrices.Rows(intRowIndex).Cells(mintGrdPrices_Price_col)
                        grdPrices.BeginEdit(True)

                    Case Else
                        eventArgs.IsValid = True

                End Select

                If Not eventArgs.IsValid Then Exit For
            Next

        End If
    End Sub

    Private Sub btnAddLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLine.Click
        mcGrdPrices.AddLine()
        myFormControler.ChangeMade = True
    End Sub

    Private Sub btnRemoveLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLine.Click
        mcGrdPrices.RemoveLine()
        myFormControler.ChangeMade = True
    End Sub

    Private Sub mcGrdPrices_SetDisplay() Handles mcGrdPrices.SetDisplay

        grdPrices.Columns(mintGrdPrices_ProP_ID_col).ReadOnly = True
        grdPrices.Columns(mintGrdPrices_Cy_Name_col).ReadOnly = True
        grdPrices.Columns(mintGrdPrices_ProB_Name_col).ReadOnly = True

        grdPrices.Columns(mintGrdPrices_Price_col).ValueType = GetType(Double)

        grdPrices.Columns(mintGrdPrices_Price_col).DefaultCellStyle.Format = gstrCurrencyFormat
    End Sub

    Private Sub grdPrices_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdPrices.CellValidating
        If e.ColumnIndex = mintGrdPrices_Price_col And grdPrices.SelectedRows.Count > 0 Then

            If Not IsNumeric(grdPrices.SelectedRows(0).Cells(mintGrdPrices_Price_col).EditedFormattedValue) And grdPrices.SelectedRows(0).Cells(mintGrdPrices_Price_col).EditedFormattedValue <> String.Empty Then

                gcAppControler.ShowMessage(mConstants.Validation_Messages.NUMERIC_VALUE, MsgBoxStyle.Information)

                e.Cancel = True
            End If
        End If

        If Not e.Cancel Then
            myFormControler.ChangeMade = True
        End If
    End Sub

    Private Sub grdPrices_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPrices.DoubleClick
        If grdPrices.SelectedRows.Count > 0 Then

            Select Case grdPrices.CurrentCell.ColumnIndex
                Case mintGrdPrices_Cy_Name_col, mintGrdPrices_ProB_Name_col
                    blnGrdPrices_Cbo_Show(grdPrices.SelectedRows(0).Index, grdPrices.CurrentCell.ColumnIndex)

                Case Else
                    grdPrices.BeginEdit(True)

            End Select

        End If
    End Sub

    Private Sub cboCompany_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompany.Leave
        grdPrices.SelectedRows(0).Cells(mintGrdPrices_Cy_ID_col).Value = cboCompany.SelectedValue
        grdPrices.SelectedRows(0).Cells(mintGrdPrices_Cy_Name_col).Value = cboCompany.SelectedItem.Value

        cboCompany.Visible = False
    End Sub

    Private Sub cboProductBrand_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProductBrand.Leave
        grdPrices.SelectedRows(0).Cells(mintGrdPrices_ProB_ID_col).Value = cboProductBrand.SelectedValue
        grdPrices.SelectedRows(0).Cells(mintGrdPrices_ProB_Name_col).Value = cboProductBrand.SelectedItem.Value

        cboProductBrand.Visible = False
    End Sub

    Private Sub chkTaxable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTaxable.CheckedChanged
        myFormControler.ChangeMade = True
    End Sub

#End Region

End Class