Option Strict Off

Public Class frmGeneralList
    Inherits System.Windows.Forms.Form

    'Public members
    Public mintGridTag As String = String.Empty
    Public mstrGridSQL As String = String.Empty

    'Private members
    Private Const mintItem_ID_col As Integer = 1
    Private mstrFormToOpenName As String = String.Empty

    Private mListToOpen As mGeneralList.GeneralLists_ID
    Private mintSelectedRow As Integer = 1

    'Private class members
    Private WithEvents mcGrdListController As SyncfusionGridController


#Region "Properties"

    Public WriteOnly Property SetFormToOpenName As String
        Set(value As String)
            mstrFormToOpenName = value
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New(ByVal vstrGenList_ID As mGeneralList.GeneralLists_ID)
        InitializeComponent()

        mListToOpen = vstrGenList_ID

    End Sub

#End Region

#Region "Functions / Subs"

    Private Function blnOpenForm(ByVal vFormMode As mConstants.Form_Mode) As Boolean
        Dim blnValidReturn As Boolean = True
        Dim frmToOpen As Object = Nothing
        Dim strFormName As String = String.Empty
        Dim intItem_ID As Integer
        Dim selectedRows As GridRangeInfoList = grdList.Selections.GetSelectedRows(True, True)

        Try
            strFormName = GetType(mdiGeneral).Namespace & "." & mstrFormToOpenName 'System.Reflection.Assembly.GetEntryAssembly.GetName.Name
            frmToOpen = System.Reflection.Assembly.GetEntryAssembly.CreateInstance(strFormName, True)

            If selectedRows.Count > 0 Then
                mintSelectedRow = selectedRows.Item(0).Top
                intItem_ID = CInt(grdList(mintSelectedRow, mintItem_ID_col).CellValue)
            End If

            Select Case vFormMode
                Case mConstants.Form_Mode.INSERT_MODE
                    frmToOpen.formController.ShowForm(vFormMode, 0, True)

                Case mConstants.Form_Mode.UPDATE_MODE
                    frmToOpen.formController.ShowForm(vFormMode, intItem_ID, True)

                Case mConstants.Form_Mode.DELETE_MODE
                    gcAppCtrl.DisableAllFormControls(frmToOpen)
                    frmToOpen.formController.ShowForm(vFormMode, intItem_ID, True)

            End Select

            formController.LoadFormData()

            Select Case vFormMode
                Case mConstants.Form_Mode.INSERT_MODE, Form_Mode.UPDATE_MODE
                    For intRowIndex As Integer = 1 To grdList.RowCount

                        If grdList(intRowIndex, mintItem_ID_col).CellValue = intItem_ID Then

                            mintSelectedRow = intRowIndex

                            Exit For
                        End If
                    Next

            End Select

            If mintSelectedRow >= 0 And grdList.RowCount > 0 Then
                mcGrdListController.SetSelectedRow = mintSelectedRow
            End If

            txtFilter.Focus()
            txtFilter.SelectAll()

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdList_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            SuspendLayout()

            blnValidReturn = mcGrdListController.bln_FillData(mstrGridSQL)

            If blnValidReturn And grdList.RowCount > 0 Then

                mcGrdListController.SetSelectedRow = mintSelectedRow
            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            ResumeLayout()
        End Try

        Return blnValidReturn
    End Function

#End Region

#Region "Private events"

    Private Sub mcGrid_SetDisplay() Handles mcGrdListController.SetDisplay
        grdList.AllowProportionalColumnSizing = True
        grdList.Model.Options.ActivateCurrentCellBehavior = GridCellActivateAction.None
        'mcGrdListController.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.NONE
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            blnOpenForm(mConstants.Form_Mode.INSERT_MODE)

        Catch ex As Exception
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try
            If grdList.RowCount > 0 Then
                blnOpenForm(mConstants.Form_Mode.UPDATE_MODE)
            End If

        Catch ex As Exception
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim frmToOpen As System.Windows.Forms.Form = Nothing

        Try
            If grdList.RowCount > 0 Then
                blnOpenForm(mConstants.Form_Mode.DELETE_MODE)
            End If

        Catch ex As Exception
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub grdList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.DoubleClick
        If grdList.RowCount > 0 And grdList.Selections.GetSelectedRows(True, True).Count > 0 Then
            blnOpenForm(mConstants.Form_Mode.UPDATE_MODE)
        End If
    End Sub

    Private Sub grdList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdList.KeyPress
        If grdList.RowCount > 0 And mcGrdListController.GetSelectedRowsCount > 0 And e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            blnOpenForm(mConstants.Form_Mode.UPDATE_MODE)
        End If
    End Sub

    Private Sub formController_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        If mcGrdListController Is Nothing Then
            mcGrdListController = New SyncfusionGridController
        End If

        grdList.Tag = mintGridTag

        Select Case False
            Case mcGrdListController.bln_Init(grdList)
            Case blnGrdList_Load()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If

    End Sub

    Private Sub txtFiltre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        Dim intSelectedColumn As Short

        If Not IsNothing(grdList.CurrentCell) Then
            'intSelectedColumn = grdList.CurrentCell.ColumnIndex
        Else
            intSelectedColumn = 1
        End If

        'For Each row As GridModelRowColOperations In grdList.Rows.GetCells(1, grdList.RowCount).Item()
        '    If row.Cells(1).Value.ToString.ToUpper.Contains(txtFilter.Text.ToUpper) Then
        '        row.Visible = True
        '    Else
        '        row.Visible = False
        '    End If
        'Next

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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        blnGrdList_Load()
    End Sub

    'Private Sub grdList_QueryColWidth(sender As Object, e As GridRowColSizeEventArgs) Handles grdList.QueryColWidth
    'Dim dblLastColSize As Integer

    ''Resizes the last column to client size.
    'If e.Index > grdList.ColCount - 1 Then

    '    dblLastColSize = grdList.Model.ColWidths.GetTotal(0, grdList.ColCount - 1)
    '    e.Size = grdList.ClientSize.Width - dblLastColSize
    '    e.Handled = True
    'End If
    'End Sub

#End Region

End Class