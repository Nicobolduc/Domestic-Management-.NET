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

#Region "Functions / Subs"

    Private Function blnLoadData() As Boolean
        Dim blnReturn As Boolean
        Dim strSQL As String = vbNullString

        Try
            mdblTaxe_TPS = Val(mSQL.str_ADOSingleLookUp("Tax_rate", "Tax", "Tax_Name = 'TPS'"))
            mdblTaxe_TVQ = Val(mSQL.str_ADOSingleLookUp("Tax_rate", "Tax", "Tax_Name = 'TVQ'"))

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
            strSQL = strSQL & "     LEFT JOIN ProductCategory ON ProductCategory.ProC_ID = Product.ProC_ID " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductBrand ##ON ProductBrand.ProB_ID = 1 " & vbCrLf
            strSQL = strSQL & "     INNER JOIN ProductPrice ON ProductPrice.Pro_ID = Product.Pro_ID AND ProductPrice.ProB_ID = ProductBrand.ProB_ID AND ProductPrice.Cy_ID = " & cboGroceryStore.SelectedValue.ToString & vbCrLf
            strSQL = strSQL & " ORDER BY Product.Pro_Name, ProductType.ProT_Name, ProductCategory.ProC_Name " & vbCrLf

            blnReturn = mcGrdGrocery.bln_FillData(strSQL)

            CalculTotals()

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

            CalculTotals()

        Catch ex As Exception
            gcApplication.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub CalculTotals()
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
        grdGrocery.Columns(mintGrdGrocery_ProP_Price_col).Width = 80
        grdGrocery.Columns(mintGrdGrocery_Pro_Taxable_col).Width = 63
        grdGrocery.Columns(mintGrdGrocery_Sel_col).Width = 36
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckUncheckAll(True)
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        CheckUncheckAll(False)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintPreviewDialog = New PrintPreviewDialog


        Dim cPrinter As New clsPrinting

        cPrinter.SetGridToPrint = grdGrocery

        PrintPreviewDialog.Document = cPrinter
        PrintPreviewDialog.ShowDialog()
    End Sub

    Private Sub grdGrocery_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdGrocery.CellMouseUp
        If e.ColumnIndex = mintGrdGrocery_Sel_col And e.RowIndex <> -1 Then
            grdGrocery.EndEdit()
        End If
    End Sub

    Private Sub grdGrocery_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdGrocery.CellValueChanged
        If e.ColumnIndex = mintGrdGrocery_Sel_col And e.RowIndex <> -1 And Not myFormControler.FormIsLoading Then
            CalculTotals()
        End If
    End Sub

    'Private Sub frmGrocery_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
    '    txtSubTotal.Location = New Point(grdGrocery.GetCellDisplayRectangle(mintGrdGrocery_ProP_Price_col, 0, True).X + 4, txtSubTotal.Location.Y)
    'End Sub

#End Region


    Private Sub PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument.PrintPage
        Dim strTitle As String = "Epicerie"
        Dim intXPos As Integer = 5
        Dim intYPos As Integer = 100
        Dim intRowIndex As Integer
        Dim intColIndex As Integer
        Dim blnDrawHeaderColText As Boolean = True

        e.Graphics.DrawString(strTitle, New Font(New FontFamily("Arial"), 18, FontStyle.Bold And FontStyle.Underline), Brushes.Black, New PointF(CSng(e.PageBounds.Width / 2) - (strTitle.Length * 5), 20))


        For intRowIndex = 0 To grdGrocery.Rows.Count - 1

            For intColIndex = 0 To grdGrocery.Columns.Count - 1

                If blnDrawHeaderColText And grdGrocery.Columns(intColIndex).Visible Then
                    e.Graphics.DrawString(grdGrocery.Columns(intColIndex).HeaderText, New Font(New FontFamily("Arial"), 14, FontStyle.Bold), Brushes.Black, intXPos, intYPos)

                    intXPos += CInt(grdGrocery.Columns(intColIndex).Width / 3)
                ElseIf grdGrocery.Columns(intColIndex).Visible Then

                    e.Graphics.DrawString(grdGrocery.Rows(intRowIndex).Cells(intColIndex).Value.ToString, New Font(New FontFamily("Arial"), 12, FontStyle.Regular), Brushes.Black, intXPos, intYPos)

                    intXPos += CInt(grdGrocery.Columns(intColIndex).Width / 3)
                End If

            Next

            blnDrawHeaderColText = False

            intXPos = 10
            intYPos += 15
        Next

        PrintDocument.DefaultPageSettings.Landscape = True

    End Sub

End Class