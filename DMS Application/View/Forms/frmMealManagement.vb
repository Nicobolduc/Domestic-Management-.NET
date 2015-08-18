Public Class frmMealManagement

    'Public members

    'Private members
    Private dtCurrentMonth As Date = New Date(Date.Today.Year, Date.Today.Month, 1)

    'Private class members
    Private WithEvents mcGridMealsController As SyncfusionGridController


#Region "Functions / Subs"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mcGridMealsController = New SyncfusionGridController

    End Sub

    Private Function blnFormData_Load() As Boolean
        Dim blnValidReturn As Boolean

        Try
            blnValidReturn = True

        Catch ex As Exception
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
            blnValidReturn = False
        Finally
            If Not blnValidReturn Then Me.Close()
        End Try

        Return blnValidReturn
    End Function

    Private Function blnGrdMeals_Load() As Boolean
        Dim blnValidReturn As Boolean
        Dim strSQL As String = String.Empty


        Try
            lblDate.Text = Format(dtCurrentMonth, "MMMM yyyy")

            strSQL = strSQL & " CALL sp_LoadCalendarGrid (" & gcAppCtrl.str_FixStringForSQL(Format(dtCurrentMonth, gcAppCtrl.str_GetServerDateFormat)) & _
                              ", " & gcAppCtrl.str_FixStringForSQL(Format(DateAdd(DateInterval.Month, 1, DateAdd(DateInterval.Day, -1, dtCurrentMonth)), gcAppCtrl.str_GetServerDateFormat)) & ");"

            blnValidReturn = mcGridMealsController.bln_FillData(strSQL)

            If blnValidReturn Then


            End If

        Catch ex As Exception
            blnValidReturn = False
            gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

        Return blnValidReturn
    End Function

#End Region


#Region "Private events"

    Private Sub formController_LoadData(eventArgs As LoadDataEventArgs) Handles formController.LoadData
        Dim blnValidReturn As Boolean

        Select Case False
            Case mcGridMealsController.bln_Init(grdMeals)
            Case blnFormData_Load()
            Case blnGrdMeals_Load()
            Case Else
                blnValidReturn = True
        End Select

        If Not blnValidReturn Then
            Me.Close()
        End If
    End Sub

    Private Sub mcGridMealsController_SetDisplay() Handles mcGridMealsController.SetDisplay

        mcGridMealsController.SetColsSizeBehavior = ColsSizeBehaviorsController.colsSizeBehaviors.ALL_COLS_EQUALS

        grdMeals.ListBoxSelectionMode = SelectionMode.None
        'grdMeals.DefaultRowHeight = 100
        grdMeals.Model.TableStyle.ReadOnly = True

    End Sub

    Private Sub grdMeals_CellDoubleClick(sender As Object, e As GridCellClickEventArgs) Handles grdMeals.CellDoubleClick

        If grdMeals.RowCount > 0 Then


        Else

        End If
    End Sub

    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click

        dtCurrentMonth = DateAdd(DateInterval.Month, 1, dtCurrentMonth)

        blnGrdMeals_Load()
    End Sub

    Private Sub btnLeft_Click(sender As Object, e As EventArgs) Handles btnLeft.Click

        dtCurrentMonth = DateAdd(DateInterval.Month, -1, dtCurrentMonth)

        blnGrdMeals_Load()

    End Sub

    Private Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click

        dtCurrentMonth = New Date(Date.Today.Year, Date.Today.Month, 1)

        blnGrdMeals_Load()
    End Sub

#End Region



End Class