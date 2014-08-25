Public Class frmGeneralList
    Inherits System.Windows.Forms.Form

    Private Const mintItem_ID_col As Integer = 0

    Private WithEvents mcGrid As clsDataGridView

    Private mListToOpen As mGeneralList.GeneralLists_ID
    Private mintSelectedRow As Integer

    Public mintGridTag As String = vbNullString
    Public mstrGridSQL As String = vbNullString


    Private Sub frmGeneralList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim blnReturn As Boolean

        grdList.Tag = mintGridTag

        mcGrid = New clsDataGridView

        Select Case False
            Case mcGrid.bln_Init(grdList)
            Case blnGrdList_Load()
            Case Else
                blnReturn = True
        End Select

        If Not blnReturn Then
            Me.Close()
        Else
            'Do nothing
        End If
    End Sub

    Private Function blnGrdList_Load() As Boolean
        Dim blnReturn As Boolean

        Try

            blnReturn = mcGrid.bln_FillData(mstrGridSQL)

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub mcGrid_SetDisplay() Handles mcGrid.SetDisplay

        grdList.ReadOnly = True

        grdList.Columns(grdList.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        grdList.AutoSizeColumnsMode = CType(DataGridViewAutoSizeColumnMode.Fill, DataGridViewAutoSizeColumnsMode)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            blnOpenForm(clsConstants.Form_Modes.INSERT)

        Catch ex As Exception
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try
            blnOpenForm(clsConstants.Form_Modes.UPDATE)

        Catch ex As Exception
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim frmToOpen As System.Windows.Forms.Form = Nothing

        Try
            blnOpenForm(clsConstants.Form_Modes.DELETE)

        Catch ex As Exception
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Function blnOpenForm(ByVal vFormMode As clsConstants.Form_Modes) As Boolean
        Dim blnReturn As Boolean = True
        Dim frmToOpen As System.Windows.Forms.Form = Nothing

        Try
            Select Case mListToOpen
                Case mGeneralList.GeneralLists_ID.EXPENSES_ID
                    frmToOpen = New frmExpense
                    DirectCast(frmToOpen, frmExpense).mintFormMode = vFormMode

                    If vFormMode <> clsConstants.Form_Modes.INSERT Then
                        DirectCast(frmToOpen, frmExpense).mintExp_ID = CInt(grdList.SelectedRows(0).Cells(mintItem_ID_col).Value)
                    Else
                        'Do nothing
                    End If

                Case mGeneralList.GeneralLists_ID.PRODUCTS_ID
                    frmToOpen = New frmProduct
                    DirectCast(frmToOpen, frmProduct).mintFormMode = vFormMode

                    If vFormMode <> clsConstants.Form_Modes.INSERT Then
                        DirectCast(frmToOpen, frmProduct).mintPro_ID = CInt(grdList.SelectedRows(0).Cells(mintItem_ID_col).Value)
                    Else
                        'Do nothing
                    End If

                Case mGeneralList.GeneralLists_ID.PRODUCT_CATEGORY_ID
                    frmToOpen = New frmProductCategory
                    DirectCast(frmToOpen, frmProductCategory).mintFormMode = vFormMode

                    If vFormMode <> clsConstants.Form_Modes.INSERT Then
                        'DirectCast(frmToOpen, frmProductCategory).mintPro_ID = CInt(grdList.SelectedRows(0).Cells(mintItem_ID_col).Value)
                    Else
                        'Do nothing
                    End If

            End Select

            If grdList.SelectedRows.Count > 0 Then
                mintSelectedRow = grdList.SelectedRows(0).Index
            End If

            frmToOpen.ShowDialog()

            frmGeneralList_Load(Me, New System.EventArgs)

            grdList.Rows(mintSelectedRow).Selected = True
            grdList.FirstDisplayedScrollingRowIndex = grdList.SelectedRows(0).Index

        Catch ex As Exception
            blnReturn = False
            gcApp.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Sub grdList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.DoubleClick
        If grdList.Rows.Count > 1 And grdList.SelectedRows.Count > 0 Then
            blnOpenForm(clsConstants.Form_Modes.UPDATE)
        Else
            'Do nothing
        End If
    End Sub

    Private Sub grdList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdList.KeyPress
        If grdList.Rows.Count > 1 And grdList.SelectedRows.Count > 0 And e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            blnOpenForm(clsConstants.Form_Modes.UPDATE)
        Else
            'Do nothing
        End If
    End Sub

    Public Sub New(ByVal vstrGenList_ID As mGeneralList.GeneralLists_ID)

        InitializeComponent()

        mListToOpen = vstrGenList_ID
    End Sub
End Class