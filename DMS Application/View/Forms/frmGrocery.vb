Public Class frmGrocery

    'Private members
    Private Const mintGrdGrocery_Pro_ID_col As Short = 1
    Private Const mintGrdGrocery_Pro_Name_col As Short = 2
    Private Const mintGrdGrocery_ProT_ID_col As Short = 3
    Private Const mintGrdGrocery_ProT_Name_col As Short = 4
    Private Const mintGrdGrocery_ProC_ID_col As Short = 5
    Private Const mintGrdGrocery_ProC_Name_col As Short = 6
    Private Const mintGrdGrocery_Pro_Taxable_col As Short = 7
    Private Const mintGrdGrocery_ProB_ID_col As Short = 8
    Private Const mintGrdGrocery_ProB_Name_col As Short = 9
    Private Const mintGrdGrocery_ProP_Price_col As Short = 10
    Private Const mintGrdGrocery_Sel_col As Short = 11

    Private mdblTaxe_TPS As Double
    Private mdblTaxe_TVQ As Double

    'Private class members
    Private WithEvents mcGrdGrocery As SyncfusionGridController
    Private mcSQL As MySQLController
    Private mcPrinter As DGV_Printing_Controller


#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            mdblTaxe_TPS = Val(MySQLController.str_ADOSingleLookUp("Tax_rate", "Tax", "Tax_Name = 'TPS'"))
            mdblTaxe_TVQ = Val(MySQLController.str_ADOSingleLookUp("Tax_rate", "Tax", "Tax_Name = 'TVQ'"))

            strSQL = strSQL & " SELECT  Grocery.Gro_Name, " & vbCrLf
            strSQL = strSQL & "         Gro_Default_Cy_ID " & vbCrLf
            strSQL = strSQL & " FROM Grocery " & vbCrLf
            strSQL = strSQL & " WHERE Gro_ID = " & formController.Item_ID & vbCrLf

            mySQLReader = MySQLController.ADOSelect(strSQL)

            If mySQLReader.Read() Then

                txtGroceryName.Text = mySQLReader.Item("Gro_Name").ToString
                cboGroceryStore.SelectedValue = mySQLReader.Item("Gro_Default_Cy_ID").ToString
            Else
                'Do nothing
            End If

            blnValidReturn = True

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdGrocery_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty
        Dim intRow As Integer
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            strSQL = strSQL & " SELECT  Product.Pro_ID, " & vbCrLf
            strSQL = strSQL & "         Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_Name, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "         CASE WHEN ProductCategory.ProC_Name IS NULL THEN '' ELSE ProductCategory.ProC_Name END AS ProC_Name, " & vbCrLf
            strSQL = strSQL & "         Product.Pro_Taxable, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_ID, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_Name, " & vbCrLf
            strSQL = strSQL & "         ProductPrice.ProP_Price, " & vbCrLf
            strSQL = strSQL & "         0 As SelCol " & vbCrLf
            strSQL = strSQL & " FROM Product " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductType ON ProductType.ProT_ID = Product.ProT_ID " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductBrand ##ON ProductBrand.ProB_ID = 1 " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductPrice ON ProductPrice.Pro_ID = Product.Pro_ID " & vbCrLf
            strSQL = strSQL & "                            AND ProductPrice.ProB_ID = ProductBrand.ProB_ID " & vbCrLf
            strSQL = strSQL & "                            AND ProductPrice.Cy_ID_Seller = " & cboGroceryStore.SelectedValue.ToString & vbCrLf
            strSQL = strSQL & " ORDER BY Product.Pro_Name, ProductType.ProT_Name, ProductCategory.ProC_Name " & vbCrLf

            blnValidReturn = mcGrdGrocery.bln_FillData(strSQL)

            If blnValidReturn Then

                strSQL = String.Empty
                strSQL = strSQL & " SELECT Gro_Pro.Pro_ID " & vbCrLf
                strSQL = strSQL & " FROM Gro_Pro " & vbCrLf
                strSQL = strSQL & " WHERE Gro_Pro.Gro_ID = " & formController.Item_ID & vbCrLf

                mySQLReader = MySQLController.ADOSelect(strSQL)

                While mySQLReader.Read And grdGrocery.RowCount > 0

                    For intRow = 1 To grdGrocery.RowCount

                        If mySQLReader.Item("Pro_ID").ToString = grdGrocery(intRow, mintGrdGrocery_Pro_ID_col).CellValue.ToString Then

                            grdGrocery(intRow, mintGrdGrocery_Sel_col).CellValue = True.ToString
                        Else
                            grdGrocery(intRow, mintGrdGrocery_Sel_col).CellValue = False.ToString
                        End If
                    Next

                End While

                CalculateTotals()

            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            If Not IsNothing(mySQLReader) Then
                mySQLReader.Close()
                mySQLReader.Dispose()
            End If
        End Try

        Return blnValidReturn
    End Function

    Private Function blnCboGroceryStore_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty

        Try
            strSQL = strSQL & " SELECT Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "        Company.Cy_Name " & vbCrLf
            'strSQL = strSQL & "        Grocery.Gro_Default_Cy " & vbCrLf
            strSQL = strSQL & " FROM Company " & vbCrLf
            strSQL = strSQL & " WHERE Company.CyT_ID = " & mConstants.CompanyType.GROCERY_STORE & vbCrLf
            strSQL = strSQL & " ORDER BY Company.Cy_Name " & vbCrLf

            blnValidReturn = mWinControlsFunctions.blnComboBox_LoadFromSQL(strSQL, "Cy_ID", "Cy_Name", False, cboGroceryStore)

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Sub CheckUncheckAll(ByVal vblnCheckAll As Boolean)

        Try
            For intRow As Integer = 1 To grdGrocery.RowCount
                grdGrocery(intRow, mintGrdGrocery_Sel_col).CellValue = vblnCheckAll
            Next

            CalculateTotals()

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub CalculateTotals()
        Dim dblSubtotalTaxable As Double = 0.0
        Dim dblSubtotalNotTaxable As Double = 0.0
        Dim dblMontantAvecTPS As Double
        Dim dblMontantAvecTVQ As Double

        Try
            For intRow As Integer = 1 To grdGrocery.RowCount

                If grdGrocery(intRow, mintGrdGrocery_Sel_col).CellValue.ToString = "True" And grdGrocery(intRow, mintGrdGrocery_Pro_Taxable_col).CellValue.ToString = "1" Then

                    dblSubtotalTaxable = dblSubtotalTaxable + Val(grdGrocery(intRow, mintGrdGrocery_ProP_Price_col).CellValue)

                ElseIf grdGrocery(intRow, mintGrdGrocery_Sel_col).CellValue.ToString = "True" And grdGrocery(intRow, mintGrdGrocery_Pro_Taxable_col).CellValue.ToString = "0" Then

                    dblSubtotalNotTaxable = dblSubtotalNotTaxable + Val(grdGrocery(intRow, mintGrdGrocery_ProP_Price_col).CellValue)

                End If
            Next

            txtSubTotal.Text = Format(dblSubtotalNotTaxable + dblSubtotalTaxable, mConstants.DataFormat.CURRENCY)

            dblMontantAvecTPS = dblSubtotalTaxable * mdblTaxe_TPS
            dblMontantAvecTVQ = dblSubtotalTaxable * mdblTaxe_TVQ

            txtTotal.Text = Format(dblMontantAvecTPS + dblMontantAvecTVQ + dblSubtotalNotTaxable + dblSubtotalTaxable, mConstants.DataFormat.CURRENCY)

            formController.ChangeMade = True

        Catch ex As Exception
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Function blnSaveData() As Boolean
        Dim blnValidReturn As Boolean

        Try
            mcSQL = New MySQLController

            mcSQL.bln_BeginTransaction()

            Select Case formController.FormMode
                Case mConstants.Form_Modes.INSERT_MODE
                    blnValidReturn = blnGrocery_Insert()

                Case mConstants.Form_Modes.UPDATE_MODE
                    blnValidReturn = blnGrocery_Update()

                Case mConstants.Form_Modes.DELETE_MODE
                    blnValidReturn = blnGrocery_Delete()

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

    Private Function blnGrocery_Insert() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Gro_Name", txtGroceryName.Text, mConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOInsert("Grocery", formController.Item_ID)
                Case formController.Item_ID > 0
                Case Else
                    blnValidReturn = blnGro_Pro_Insert()
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrocery_Update() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Gro_Name", txtGroceryName.Text, mConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOUpdate("Grocery", "Gro_ID = " & formController.Item_ID)
                Case formController.Item_ID > 0
                Case Else
                    blnValidReturn = blnGro_Pro_Insert()
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrocery_Delete() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case blnGro_Pro_Delete()
                Case mcSQL.bln_ADODelete("Grocery", "Gro_ID = " & formController.Item_ID)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGro_Pro_Delete() As Boolean
        Dim blnValidReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("Gro_Pro", "Gro_ID = " & formController.Item_ID)
                Case Else
                    blnValidReturn = True
            End Select

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGro_Pro_Insert() As Boolean
        Dim blnValidReturn As Boolean

        Try
            blnValidReturn = blnGro_Pro_Delete()

            If blnValidReturn Then

                For cpt As Integer = 1 To grdGrocery.RowCount

                    blnValidReturn = False
                    Select Case False
                        Case grdGrocery(cpt, mintGrdGrocery_Sel_col).CellValue.ToString = "True"
                            blnValidReturn = True
                        Case mcSQL.bln_AddField("Gro_ID", formController.Item_ID.ToString, mConstants.MySQL_FieldTypes.INT_TYPE)
                        Case mcSQL.bln_AddField("Pro_ID", grdGrocery(cpt, mintGrdGrocery_Pro_ID_col).CellValue.ToString, mConstants.MySQL_FieldTypes.INT_TYPE)
                        Case mcSQL.bln_ADOInsert("Gro_Pro")
                        Case Else
                            blnValidReturn = True
                    End Select

                    If Not blnValidReturn Then Exit For
                Next

            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppController.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region


#Region "Private Events"

    Private Sub formController_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        mcGrdGrocery = New SyncfusionGridController

        Select Case False
            Case mcGrdGrocery.bln_Init(grdGrocery)
            Case blnCboGroceryStore_Load()
            Case blnGrdGrocery_Load()
            Case blnLoadData()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        blnGrdGrocery_Load()
    End Sub

    Private Sub mcGrdGrocery_SetDisplay() Handles mcGrdGrocery.SetDisplay

        grdGrocery.ColStyles(mintGrdGrocery_Sel_col).CellType = "CheckBox"
        grdGrocery.ColStyles(mintGrdGrocery_Sel_col).CheckBoxOptions = New GridCheckBoxCellInfo(True.ToString(), False.ToString(), "", False)
        grdGrocery.ColStyles(mintGrdGrocery_Sel_col).CellValueType = GetType(Boolean)
        grdGrocery.ColStyles(mintGrdGrocery_ProP_Price_col).CellValueType = GetType(Double)

        grdGrocery.ColStyles(mintGrdGrocery_ProP_Price_col).Format = mConstants.DataFormat.CURRENCY

        For intCol As Integer = 1 To grdGrocery.ColCount
            If intCol <> mintGrdGrocery_Sel_col Then
                grdGrocery.ColStyles(intCol).ReadOnly = True
            End If
        Next

        CheckUncheckAll(True)

        grdGrocery.ColWidths(mintGrdGrocery_Pro_Name_col) = 214
        grdGrocery.ColWidths(mintGrdGrocery_ProT_Name_col) = 97
        grdGrocery.ColWidths(mintGrdGrocery_ProC_Name_col) = 100
        grdGrocery.ColWidths(mintGrdGrocery_Pro_Taxable_col) = 59
        grdGrocery.ColWidths(mintGrdGrocery_ProB_Name_col) = 109
        grdGrocery.ColWidths(mintGrdGrocery_ProP_Price_col) = 63
        grdGrocery.ColWidths(mintGrdGrocery_Sel_col) = 37
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckUncheckAll(True)
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        CheckUncheckAll(False)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        mcPrinter = New DGV_Printing_Controller("Épicerie du: " & Date.Today)

        mcPrinter.SetGridToPrint(New Short() {mintGrdGrocery_Pro_Name_col, mintGrdGrocery_ProC_Name_col, mintGrdGrocery_ProB_Name_col, mintGrdGrocery_ProP_Price_col}) = grdGrocery()

        mcPrinter.ShowPrintPreviewDialog()
    End Sub

    Private Sub formController_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles formController.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGroceryName.TextChanged
        formController.ChangeMade = True
    End Sub

    Private Sub formController_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles formController.ValidateRules

        Select Case False
            Case txtGroceryName.Text <> String.Empty
                txtGroceryName.Focus()
                gcAppController.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
            Case Else
                eventArgs.IsValid = True
        End Select
    End Sub

    Private Sub grdGrocery_CellClick(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs) Handles grdGrocery.CheckBoxClick
        'If e.ColIndex = mintGrdGrocery_Sel_col And e.RowIndex > 0 And Not formController.FormIsLoading Then
        '    CalculateTotals()
        'End If
    End Sub

    Private Sub grdGrocery_CellMouseUp(ByVal sender As Object, ByVal e As Syncfusion.Windows.Forms.Grid.GridCellMouseEventArgs) Handles grdGrocery.CellMouseUp
        If e.ColIndex = mintGrdGrocery_Sel_col And e.RowIndex <> -1 Then
            grdGrocery.EndEdit()
        End If
    End Sub

#End Region

    Private Sub grdGrocery_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGrocery.CurrentCellChanged
        If grdGrocery.CurrentCell.ColIndex = mintGrdGrocery_Sel_col And grdGrocery.CurrentCell.RowIndex > 0 And Not formController.FormIsLoading Then
            CalculateTotals()
        End If
    End Sub
End Class