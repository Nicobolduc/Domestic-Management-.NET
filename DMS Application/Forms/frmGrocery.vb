Public Class frmGrocery

    'Private members
    Private Const mintGrdGrocery_Pro_ID_col As Short = 0
    Private Const mintGrdGrocery_Pro_Name_col As Short = 1
    Private Const mintGrdGrocery_ProT_ID_col As Short = 2
    Private Const mintGrdGrocery_ProT_Name_col As Short = 3
    Private Const mintGrdGrocery_ProC_ID_col As Short = 4
    Private Const mintGrdGrocery_ProC_Name_col As Short = 5
    Private Const mintGrdGrocery_Pro_Taxable_col As Short = 6
    Private Const mintGrdGrocery_ProB_ID_col As Short = 7
    Private Const mintGrdGrocery_ProB_Name_col As Short = 8
    Private Const mintGrdGrocery_ProP_Price_col As Short = 9
    Private Const mintGrdGrocery_Sel_col As Short = 10

    Private mdblTaxe_TPS As Double
    Private mdblTaxe_TVQ As Double

    'Private class members
    Private WithEvents mcGrdGrocery As clsDataGridView
    Private mcSQL As clsSQL_Transactions
    Private mcPrinter As clsPrinting

#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString
        Dim mySQLReader As MySqlDataReader = Nothing

        Try
            mdblTaxe_TPS = Val(mSQL.str_ADOSingleLookUp("Tax_rate", "Tax", "Tax_Name = 'TPS'"))
            mdblTaxe_TVQ = Val(mSQL.str_ADOSingleLookUp("Tax_rate", "Tax", "Tax_Name = 'TVQ'"))

            strSQL = strSQL & " SELECT  Grocery.Gro_Name " & vbCrLf

            strSQL = strSQL & " FROM Grocery " & vbCrLf
            strSQL = strSQL & " WHERE Gro_ID = " & myFormControler.Item_ID & vbCrLf

            mySQLReader = mSQL.ADOSelect(strSQL)

            If mySQLReader.Read() Then

                txtGroceryName.Text = mySQLReader.Item("Gro_Name").ToString
            Else
                'Do nothing
            End If

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

    Private Function blnGrdGrocery_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

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
            strSQL = strSQL & "         CASE WHEN Gro_Pro.Pro_ID IS NOT NULL THEN 1 ELSE 0 END As " & mcGrdGrocery.getSelectionColName & vbCrLf
            strSQL = strSQL & " FROM Product " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductType ON ProductType.ProT_ID = Product.ProT_ID " & vbCrLf
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductBrand ##ON ProductBrand.ProB_ID = 1 " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductPrice ON ProductPrice.Pro_ID = Product.Pro_ID " & vbCrLf
            strSQL = strSQL & "                            AND ProductPrice.ProB_ID = ProductBrand.ProB_ID " & vbCrLf
            strSQL = strSQL & "                            AND ProductPrice.Cy_ID = " & cboGroceryStore.SelectedValue.ToString & vbCrLf
            strSQL = strSQL & "     LEFT JOIN Gro_Pro ON Gro_Pro.Gro_ID = " & myFormControler.Item_ID & " AND Gro_Pro.Pro_ID = Product.Pro_ID " & vbCrLf
            strSQL = strSQL & " ORDER BY Product.Pro_Name, ProductType.ProT_Name, ProductCategory.ProC_Name " & vbCrLf

            blnReturn = mcGrdGrocery.bln_FillData(strSQL)

            CalculateTotals()

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboGroceryStore_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "        Company.Cy_Name " & vbCrLf
            'strSQL = strSQL & "        Grocery.Gro_Default_Cy " & vbCrLf
            strSQL = strSQL & " FROM Company " & vbCrLf
            strSQL = strSQL & " WHERE Company.CyT_ID = " & mConstants.CompanyType.GROCERY_STORE & vbCrLf
            strSQL = strSQL & " ORDER BY Company.Cy_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "Cy_ID", "Cy_Name", False, cboGroceryStore)

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Sub CheckUncheckAll(ByVal vblnCheckAll As Boolean)

        Try
            For Each row As DataGridViewRow In grdGrocery.Rows
                row.Cells(mintGrdGrocery_Sel_col).Value = vblnCheckAll
            Next

            CalculateTotals()

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub CalculateTotals()
        Dim dblSubtotalTaxable As Double = 0.0
        Dim dblSubtotalNotTaxable As Double = 0.0
        Dim dblMontantAvecTPS As Double
        Dim dblMontantAvecTVQ As Double

        Try
            For Each row As DataGridViewRow In grdGrocery.Rows

                If row.Cells(mintGrdGrocery_Sel_col).EditedFormattedValue.ToString = "True" And row.Cells(mintGrdGrocery_Pro_Taxable_col).Value.ToString = "1" Then

                    dblSubtotalTaxable = dblSubtotalTaxable + Val(row.Cells(mintGrdGrocery_ProP_Price_col).Value)

                ElseIf row.Cells(mintGrdGrocery_Sel_col).EditedFormattedValue.ToString = "True" And row.Cells(mintGrdGrocery_Pro_Taxable_col).Value.ToString = "0" Then

                    dblSubtotalNotTaxable = dblSubtotalNotTaxable + Val(row.Cells(mintGrdGrocery_ProP_Price_col).Value)

                End If
            Next

            txtSubTotal.Text = Format(dblSubtotalNotTaxable + dblSubtotalTaxable, gstrCurrencyFormat)

            dblMontantAvecTPS = dblSubtotalTaxable * mdblTaxe_TPS
            dblMontantAvecTVQ = dblSubtotalTaxable * mdblTaxe_TVQ

            txtTotal.Text = Format(dblMontantAvecTPS + dblMontantAvecTVQ + dblSubtotalNotTaxable + dblSubtotalTaxable, gstrCurrencyFormat)

            myFormControler.ChangeMade = True

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Function blnSaveData() As Boolean
        Dim blnReturn As Boolean

        Try
            mcSQL = New clsSQL_Transactions

            mcSQL.bln_BeginTransaction()

            Select Case myFormControler.FormMode
                Case mConstants.Form_Modes.INSERT_MODE
                    blnReturn = blnGrocery_Insert()

                Case mConstants.Form_Modes.UPDATE_MODE
                    blnReturn = blnGrocery_Update()

                Case mConstants.Form_Modes.DELETE_MODE
                    blnReturn = blnGrocery_Delete()

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

    Private Function blnGrocery_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Gro_Name", txtGroceryName.Text, mConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOInsert("Grocery", myFormControler.Item_ID)
                Case myFormControler.Item_ID > 0
                Case Else
                    blnReturn = blnGro_Pro_Insert()
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGrocery_Update() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_AddField("Gro_Name", txtGroceryName.Text, mConstants.MySQL_FieldTypes.VARCHAR_TYPE)
                Case mcSQL.bln_ADOUpdate("Grocery", "Gro_ID = " & myFormControler.Item_ID)
                Case myFormControler.Item_ID > 0
                Case Else
                    blnReturn = blnGro_Pro_Insert()
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGrocery_Delete() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case blnGro_Pro_Delete()
                Case mcSQL.bln_ADODelete("Grocery", "Gro_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGro_Pro_Delete() As Boolean
        Dim blnReturn As Boolean

        Try
            Select Case False
                Case mcSQL.bln_ADODelete("Gro_Pro", "Gro_ID = " & myFormControler.Item_ID)
                Case Else
                    blnReturn = True
            End Select

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGro_Pro_Insert() As Boolean
        Dim blnReturn As Boolean

        Try
            blnReturn = blnGro_Pro_Delete()

            If blnReturn Then

                For cpt As Integer = 0 To grdGrocery.Rows.Count - 1

                    blnReturn = False
                    Select Case False
                        Case grdGrocery.Rows(cpt).Cells(mintGrdGrocery_Sel_col).Value.ToString = "True"
                            blnReturn = True
                        Case mcSQL.bln_AddField("Gro_ID", myFormControler.Item_ID.ToString, mConstants.MySQL_FieldTypes.INT_TYPE)
                        Case mcSQL.bln_AddField("Pro_ID", grdGrocery.Rows(cpt).Cells(mintGrdGrocery_Pro_ID_col).Value.ToString, mConstants.MySQL_FieldTypes.INT_TYPE)
                        Case mcSQL.bln_ADOInsert("Gro_Pro")
                        Case Else
                            blnReturn = True
                    End Select

                    If Not blnReturn Then Exit For
                Next

            End If

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

#End Region


#Region "Private Events"

    Private Sub myFormControler_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnReturn As Boolean

        mcGrdGrocery = New clsDataGridView

        Select Case False
            Case mcGrdGrocery.bln_Init(grdGrocery)
            Case blnCboGroceryStore_Load()
            Case blnGrdGrocery_Load()
            Case blnLoadData()
            Case Else
                blnReturn = True
        End Select

        If Not blnReturn Then
            Me.Close()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        blnGrdGrocery_Load()
    End Sub

    Private Sub mcGrdGrocery_SetDisplay() Handles mcGrdGrocery.SetDisplay

        grdGrocery.Columns(mintGrdGrocery_Pro_Taxable_col).ValueType = GetType(Boolean)
        grdGrocery.Columns(mintGrdGrocery_ProP_Price_col).ValueType = GetType(Double)

        grdGrocery.Columns(mintGrdGrocery_ProP_Price_col).DefaultCellStyle.Format = gstrCurrencyFormat

        'grdGrocery.Columns.Add(New DataGridViewCheckBoxColumn())
        'grdGrocery.Columns(mintGrdGrocery_Sel_col).SortMode = DataGridViewColumnSortMode.Automatic
        'grdGrocery.Columns(mintGrdGrocery_Sel_col).HeaderText = "Sél."
        'grdGrocery.Columns(mintGrdGrocery_Sel_col).ReadOnly = False
        'grdGrocery.Columns(mintGrdGrocery_Sel_col).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For Each column As DataGridViewColumn In grdGrocery.Columns
            If column.Index <> mintGrdGrocery_Sel_col Then
                column.ReadOnly = True
            End If
        Next

        CheckUncheckAll(True)

        grdGrocery.Columns(mintGrdGrocery_Pro_Name_col).Width = 214
        grdGrocery.Columns(mintGrdGrocery_ProT_Name_col).Width = 97
        grdGrocery.Columns(mintGrdGrocery_ProC_Name_col).Width = 100
        grdGrocery.Columns(mintGrdGrocery_Pro_Taxable_col).Width = 59
        grdGrocery.Columns(mintGrdGrocery_ProB_Name_col).Width = 109
        grdGrocery.Columns(mintGrdGrocery_ProP_Price_col).Width = 63
        grdGrocery.Columns(mintGrdGrocery_Sel_col).Width = 37
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckUncheckAll(True)
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        CheckUncheckAll(False)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        mcPrinter = New clsPrinting("Épicerie du: " & Date.Today)

        mcPrinter.SetGridToPrint(New Short() {mintGrdGrocery_Pro_Name_col, mintGrdGrocery_ProC_Name_col, mintGrdGrocery_ProB_Name_col, mintGrdGrocery_ProP_Price_col}) = grdGrocery()

        mcPrinter.ShowPrintPreviewDialog()
    End Sub

    Private Sub grdGrocery_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdGrocery.CellMouseUp
        If e.ColumnIndex = mintGrdGrocery_Sel_col And e.RowIndex <> -1 Then
            grdGrocery.EndEdit()
        End If
    End Sub

    Private Sub grdGrocery_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGrocery.CellValueChanged
        If e.ColumnIndex = mintGrdGrocery_Sel_col And e.RowIndex <> -1 And Not myFormControler.FormIsLoading Then
            CalculateTotals()
        End If
    End Sub

    Private Sub myFormControler_SaveData(ByVal eventArgs As SaveDataEventArgs) Handles myFormControler.SaveData
        eventArgs.SaveSuccessful = blnSaveData()
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGroceryName.TextChanged
        myFormControler.ChangeMade = True
    End Sub

    Private Sub myFormControler_ValidateRules(ByVal eventArgs As ValidateRulesEventArgs) Handles myFormControler.ValidateRules

        Select Case False
            Case txtGroceryName.Text <> vbNullString
                txtGroceryName.Focus()
                gcAppControler.ShowMessage(mConstants.Validation_Messages.MANDATORY_VALUE, MsgBoxStyle.Information)
            Case Else
                eventArgs.IsValid = True
        End Select
    End Sub

#End Region



End Class