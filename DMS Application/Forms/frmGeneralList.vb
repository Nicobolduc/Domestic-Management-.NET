Option Strict Off

Public Class frmGeneralList
    Inherits System.Windows.Forms.Form

    'Private members
    Private Const mintItem_ID_col As Integer = 0

    Private mListToOpen As mGeneralList.GeneralLists_ID

    Public mintGridTag As String = vbNullString
    Public mstrGridSQL As String = vbNullString

    'Private class members
    Private WithEvents mcGrdList As clsDataGridView


#Region "Constructor"

    Public Sub New(ByVal vstrGenList_ID As mGeneralList.GeneralLists_ID)
        InitializeComponent()

        mListToOpen = vstrGenList_ID
    End Sub

#End Region


#Region "Functions / Subs"

    Private Function blnOpenForm(ByVal vFormMode As mConstants.Form_Modes) As Boolean
        Dim blnReturn As Boolean = True
        Dim frmToOpen As Object = Nothing
        Dim intItem_ID As Integer
        Dim intRowIndex As Integer
        Dim intSelectedRow As Integer

        Try
            Select Case mListToOpen
                Case mGeneralList.GeneralLists_ID.EXPENSES_LIST_ID
                    frmToOpen = New frmExpense

                Case mGeneralList.GeneralLists_ID.PRODUCTS_LIST_ID
                    frmToOpen = New frmProduct

                Case mGeneralList.GeneralLists_ID.PRODUCT_TYPE_LIST_ID
                    frmToOpen = New frmBrandProto

                Case mGeneralList.GeneralLists_ID.PRODUCT_CATEGORY_LIST_ID
                    frmToOpen = New frmProductCategory

                Case mGeneralList.GeneralLists_ID.PRODUCT_BRAND_LIST_ID
                    frmToOpen = New frmProductBrand

                Case mGeneralList.GeneralLists_ID.COMPANY_LIST_ID
                    frmToOpen = New frmCompany

                Case mGeneralList.GeneralLists_ID.GROCERY_LIST_ID
                    frmToOpen = New frmGrocery

            End Select

            If grdList.SelectedRows.Count > 0 Then
                intSelectedRow = grdList.SelectedRows(0).Index
                intItem_ID = CInt(grdList.SelectedRows(0).Cells(mintItem_ID_col).Value)
            End If

            Select Case vFormMode
                Case mConstants.Form_Modes.INSERT_MODE
                    frmToOpen.myFormControler.ShowForm(vFormMode, 0, True)

                Case mConstants.Form_Modes.UPDATE_MODE
                    intSelectedRow = grdList.SelectedRows(0).Index
                    frmToOpen.myFormControler.ShowForm(vFormMode, intItem_ID, True)

                Case mConstants.Form_Modes.DELETE_MODE
                    intSelectedRow = grdList.SelectedRows(0).Index
                    gcAppControler.DisableAllFormControls(frmToOpen)
                    frmToOpen.myFormControler.ShowForm(vFormMode, intItem_ID, True)

            End Select

            myFormControler.LoadFormData()

            Select Case vFormMode
                Case mConstants.Form_Modes.INSERT_MODE
                    For intRowIndex = 0 To grdList.Rows.Count - 1
                        If CInt(grdList.Rows(intRowIndex).Cells.Item(mintItem_ID_col).Value) = intItem_ID Then
                            intSelectedRow = intRowIndex
                        End If
                    Next

            End Select

            If intSelectedRow >= 0 And grdList.Rows.Count > 0 Then
                grdList.Rows(intSelectedRow).Selected = True
                'grdList.FirstDisplayedScrollingRowIndex = grdList.SelectedRows(0).Index
            End If

            txtFilter.Focus()
            txtFilter.SelectAll()

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

    Private Function blnGrdList_Load() As Boolean
        Dim blnReturn As Boolean

        Try

            blnReturn = mcGrdList.bln_FillData(mstrGridSQL)

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnReturn
    End Function

#End Region


#Region "Private events"

    Private Sub mcGrid_SetDisplay() Handles mcGrdList.SetDisplay

        grdList.ReadOnly = True

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            blnOpenForm(mConstants.Form_Modes.INSERT_MODE)

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try
            If grdList.Rows.Count > 0 Then
                blnOpenForm(mConstants.Form_Modes.UPDATE_MODE)
            End If

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim frmToOpen As System.Windows.Forms.Form = Nothing

        Try
            If grdList.Rows.Count > 0 Then
                blnOpenForm(mConstants.Form_Modes.DELETE_MODE)
            End If

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub grdList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.DoubleClick
        If grdList.Rows.Count > 0 And grdList.SelectedRows.Count > 0 Then
            blnOpenForm(mConstants.Form_Modes.UPDATE_MODE)
        End If
    End Sub

    Private Sub grdList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdList.KeyPress
        If grdList.Rows.Count > 0 And grdList.SelectedRows.Count > 0 And e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            blnOpenForm(mConstants.Form_Modes.UPDATE_MODE)
        End If
    End Sub

    Private Sub myFormManager_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormControler.LoadData
        Dim blnReturn As Boolean

        mcGrdList = New clsDataGridView

        grdList.Tag = mintGridTag

        Select Case False
            Case mcGrdList.bln_Init(grdList)
            Case blnGrdList_Load()
            Case Else
                blnReturn = True
        End Select

        If Not blnReturn Then
            Me.Close()
        End If

    End Sub

#End Region
    
    Private Sub txtFiltre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        Dim intSelectedColumn As Short

        If Not IsNothing(grdList.CurrentCell) Then
            intSelectedColumn = grdList.CurrentCell.ColumnIndex
        Else
            intSelectedColumn = 1
        End If

        For Each row As DataGridViewRow In grdList.Rows
            If row.Cells(1).Value.ToString.ToUpper.Contains(txtFilter.Text.ToUpper) Then
                row.Visible = True
            Else
                row.Visible = False
            End If
        Next

        intSelectedColumn = 1

    End Sub

    Protected Overrides Sub Finalize()
        My.Forms.mdiGeneral.RemoveGenListHandle(Me.Handle.ToInt32)
        MyBase.Finalize()
    End Sub

    Private Sub frmGeneralList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        My.Forms.mdiGeneral.RemoveGenListHandle(Me.Handle.ToInt32)
        Me.Dispose()
    End Sub
End Class