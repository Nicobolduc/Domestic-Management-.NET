
Public Class clsFlexGridControler

    'Private members
    Private Const mintDefaultActionCol As Short = 0
    Private Const mstrSelectionColName As String = "SelCol"

    'Private class members
    Private WithEvents grdFlexGrid As C1FlexGrid

    'Public events
    Public Event SetDisplay()
    Public Event ValidateData()
    Public Event SaveGridData()

    'Public enum
    Public Enum GridRowActions
        CONSULT_ACTION = 0
        INSERT_ACTION = 1
        UPDATE_ACTION = 2
        DELETE_ACTION = 3
    End Enum

#Region "Properties"

#End Region


#Region "Functions / Subs"

    Public Function bln_Init(ByRef rgrdGrid As C1FlexGrid, Optional ByRef rbtnAddLine As Button = Nothing, Optional ByRef rbtnRemoveLine As Button = Nothing) As Boolean
        Dim blnReturn As Boolean = True
        Dim columnsHeaderStyle As New DataGridViewCellStyle

        Try
            rgrdGrid = New C1FlexGrid()

            grdFlexGrid.BeginInit()

            SetDoubleBuffered(rgrdGrid, True)

            grdFlexGrid = rgrdGrid

            grdFlexGrid.AllowResizing = AllowResizingEnum.Columns
            '            grdFlexGrid.Rows(1).Item(1)
            grdFlexGrid.SelectionMode = SelectionModeEnum.Row

            grdFlexGrid.AutoGenerateColumns = False

        Catch ex As Exception
            blnReturn = False
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            grdFlexGrid.EndInit()
        End Try

        Return blnReturn
    End Function

    Private Sub SetDoubleBuffered(ByRef vgrdGrid As C1FlexGrid, ByVal vblnIsDoubleBuffered As Boolean)
        Dim dgvType As Type = vgrdGrid.GetType()
        Dim propInfos As Reflection.PropertyInfo = dgvType.GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)

        propInfos.SetValue(vgrdGrid, vblnIsDoubleBuffered, Nothing)
    End Sub

#End Region

End Class
