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

    Private Function blnOpenForm(ByVal vFormMode As clsConstants.Form_Modes) As Boolean
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

            End Select

            If grdList.SelectedRows.Count > 0 Then
                intSelectedRow = grdList.SelectedRows(0).Index
                intItem_ID = CInt(grdList.SelectedRows(0).Cells(mintItem_ID_col).Value)
            End If

            Select Case vFormMode
                Case clsConstants.Form_Modes.INSERT
                    frmToOpen.myFormControler.ShowForm(vFormMode, 0, True)

                Case clsConstants.Form_Modes.UPDATE
                    intSelectedRow = grdList.SelectedRows(0).Index
                    frmToOpen.myFormControler.ShowForm(vFormMode, intItem_ID, True)

                Case clsConstants.Form_Modes.DELETE
                    gcAppControler.DisableAllControls(frmToOpen)
                    frmToOpen.myFormControler.ShowForm(vFormMode, intItem_ID, True)

            End Select

            myFormManager.LoadFormData()

            Select Case vFormMode
                Case clsConstants.Form_Modes.INSERT
                    For intRowIndex = 0 To grdList.Rows.Count - 1
                        If CInt(grdList.Rows(intRowIndex).Cells.Item(mintItem_ID_col).Value) = intItem_ID Then
                            intSelectedRow = intRowIndex
                        End If
                    Next

                Case clsConstants.Form_Modes.DELETE
                    intSelectedRow = 0

            End Select

            If intSelectedRow >= 0 And grdList.Rows.Count > 0 Then
                grdList.Rows(intSelectedRow).Selected = True
                grdList.FirstDisplayedScrollingRowIndex = grdList.SelectedRows(0).Index
            End If

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

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Dispose()
        Finalize()
        Me.Close()
    End Sub

    Private Sub mcGrid_SetDisplay() Handles mcGrdList.SetDisplay

        grdList.ReadOnly = True

        grdList.Columns(grdList.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        grdList.AutoSizeColumnsMode = CType(DataGridViewAutoSizeColumnMode.Fill, DataGridViewAutoSizeColumnsMode)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            blnOpenForm(clsConstants.Form_Modes.INSERT)

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        Try
            If grdList.Rows.Count > 0 Then
                blnOpenForm(clsConstants.Form_Modes.UPDATE)
            End If

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim frmToOpen As System.Windows.Forms.Form = Nothing

        Try
            If grdList.Rows.Count > 0 Then
                blnOpenForm(clsConstants.Form_Modes.DELETE)
            End If

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub grdList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.DoubleClick
        If grdList.Rows.Count > 0 And grdList.SelectedRows.Count > 0 Then
            blnOpenForm(clsConstants.Form_Modes.UPDATE)
        End If
    End Sub

    Private Sub grdList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdList.KeyPress
        If grdList.Rows.Count > 0 And grdList.SelectedRows.Count > 0 And e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            blnOpenForm(clsConstants.Form_Modes.UPDATE)
        End If
    End Sub

    Private Sub myFormManager_LoadData(ByVal eventArgs As LoadDataEventArgs) Handles myFormManager.LoadData
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
    
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class