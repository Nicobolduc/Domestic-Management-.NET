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

    'Private class members
    Private WithEvents mcGrdGrocery As clsDataGridView

#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try


            blnReturn = True

        Catch ex As Exception
            blnReturn = False
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGrdGrocery_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & "  SELECT Product.Pro_ID, " & vbCrLf
            strSQL = strSQL & "         Product.Pro_Name, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_ID, " & vbCrLf
            strSQL = strSQL & "         ProductType.ProT_Name, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_ID, " & vbCrLf
            strSQL = strSQL & "         ProductCategory.ProC_Name, " & vbCrLf
            strSQL = strSQL & "         Product.Pro_Taxable, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_ID, " & vbCrLf
            strSQL = strSQL & "         ProductBrand.ProB_Name, " & vbCrLf
            strSQL = strSQL & "         ProductPrice.ProP_Price " & vbCrLf
            strSQL = strSQL & " FROM Product " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductType ON ProductType.ProT_ID = Product.ProT_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductBrand ##ON ProductBrand.ProB_ID = 1 " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductPrice ON ProductPrice.Pro_ID = Product.Pro_ID AND ProductPrice.ProB_ID = ProductBrand.ProB_ID AND ProductPrice.Cy_ID = " & cboGroceryStore.SelectedValue.ToString & vbCrLf
            strSQL = strSQL & " ORDER BY Product.Pro_Name, ProductType.ProT_Name, ProductCategory.ProC_Name " & vbCrLf

            blnReturn = mcGrdGrocery.bln_FillData(strSQL)

        Catch ex As Exception
            blnReturn = False
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnCboGroceryStore_Load() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            strSQL = strSQL & " SELECT Company.Cy_ID, " & vbCrLf
            strSQL = strSQL & "        Company.Cy_Name " & vbCrLf
            strSQL = strSQL & " FROM Company " & vbCrLf
            strSQL = strSQL & " WHERE Company.CyT_ID = " & clsConstants.CompanyType.GROCERY_STORE & vbCrLf
            strSQL = strSQL & " ORDER BY Company.Cy_Name " & vbCrLf

            blnReturn = blnComboBox_LoadFromSQL(strSQL, "Cy_ID", "Cy_Name", False, cboGroceryStore)

        Catch ex As Exception
            blnReturn = False
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Sub CheckUncheckAll(ByVal vblnCheckAll As Boolean)

        Try
            For Each row As DataGridViewRow In grdGrocery.Rows
                row.Cells(mintGrdGrocery_Sel_col).Value = vblnCheckAll
            Next

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

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

        grdGrocery.Columns.Add(New DataGridViewCheckBoxColumn())
        grdGrocery.Columns(mintGrdGrocery_Sel_col).SortMode = DataGridViewColumnSortMode.Automatic
        grdGrocery.Columns(mintGrdGrocery_Sel_col).HeaderText = "Sél."
        grdGrocery.Columns(mintGrdGrocery_Sel_col).ReadOnly = False
        grdGrocery.Columns(mintGrdGrocery_Sel_col).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For Each column As DataGridViewColumn In grdGrocery.Columns
            If column.Index <> mintGrdGrocery_Sel_col Then
                column.ReadOnly = True
            End If
        Next

        CheckUncheckAll(True)

        grdGrocery.Columns(mintGrdGrocery_Pro_Name_col).Width = 200
        grdGrocery.Columns(mintGrdGrocery_Pro_Taxable_col).Width = 63
        grdGrocery.Columns(mintGrdGrocery_Sel_col).Width = 36
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckUncheckAll(True)
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        CheckUncheckAll(False)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'PrintDoc.Print()
        'mcGrdGrocery.Printer_Init()
        'PrintDGV.Print_DataGridView(grdGrocery)
        'Print()
    End Sub


#End Region


End Class