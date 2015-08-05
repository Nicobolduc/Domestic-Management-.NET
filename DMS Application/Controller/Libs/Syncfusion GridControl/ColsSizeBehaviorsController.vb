Public Class ColsSizeBehaviorsController

    Private grdSync As GridControlBase = Nothing
    Private _colsSizeBehavior As colsSizeBehaviors
    Private colRatios() As Double = Nothing
    Private colsSize() As Double = Nothing
    Private blnLastColWidthChanged As Boolean

    Public Enum colsSizeBehaviors
        NONE = 0
        EXTEND_LAST_COL
        EXTEND_FIRST_COL
        ALL_COLS_EQUALS
    End Enum

    Public Property ColsSizeBehavior() As colsSizeBehaviors
        Get
            Return _colsSizeBehavior
        End Get

        Set(ByVal value As colsSizeBehaviors)
            _colsSizeBehavior = value
        End Set
    End Property

    Protected Friend Sub AttachGrid(ByVal grid As GridControlBase)
        Dim dblGridWidth As Double

        If grdSync IsNot grid Then
            If grdSync IsNot Nothing Then
                DetachGrid()
            End If

            grdSync = grid

            dblGridWidth = grdSync.Width - grdSync.GetColWidth(0) + 4

            If TypeOf grid Is GridDataBoundGrid Then
                CType(grdSync, GridDataBoundGrid).SmoothControlResize = False
            ElseIf TypeOf grid Is GridControl Then
                CType(grdSync, GridControl).SmoothControlResize = False
            End If

            'Save original col ratios
            'colRatios = New Double(grdSync.Model.ColCount) {}
            colsSize = New Double(grdSync.Model.ColCount) {}

            colsSize(0) = grdSync.GetColWidth(0)

            For col As Integer = 1 To grdSync.Model.ColCount
                'colRatios(col) = grdSync.Model.ColWidths(col) / dblGridWidth
                colsSize(col) = dblGridWidth / grdSync.Model.ColCount - 1
            Next col

            AddHandler grid.Model.QueryColWidth, AddressOf grid_QueryColWidth
            AddHandler grid.Model.ColWidthsChanged, AddressOf grid_ColWidthsChanged
            AddHandler grid.ResizingColumns, AddressOf grid_ResizingColumns
        End If
    End Sub

    Protected Friend Sub DetachGrid()
        RemoveHandler grdSync.Model.QueryColWidth, AddressOf grid_QueryColWidth
        RemoveHandler grdSync.Model.ColWidthsChanged, AddressOf grid_ColWidthsChanged
        RemoveHandler grdSync.ResizingColumns, AddressOf grid_ResizingColumns

        grdSync = Nothing
    End Sub

    Private Sub grid_ResizingColumns(ByVal sender As Object, ByVal e As GridResizingColumnsEventArgs)

        If _colsSizeBehavior = colsSizeBehaviors.ALL_COLS_EQUALS Then
            e.Cancel = True
        ElseIf _colsSizeBehavior = colsSizeBehaviors.EXTEND_LAST_COL AndAlso e.Columns.Right = grdSync.Model.ColCount Then
            e.Cancel = True
        ElseIf _colsSizeBehavior = colsSizeBehaviors.EXTEND_FIRST_COL AndAlso e.Columns.Left = grdSync.Model.Cols.HeaderCount + 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub grid_QueryColWidth(ByVal sender As Object, ByVal e As GridRowColSizeEventArgs)

        Select Case _colsSizeBehavior
            Case colsSizeBehaviors.EXTEND_LAST_COL
                If e.Index = grdSync.Model.ColCount Then
                    e.Size = grdSync.ClientSize.Width - grdSync.Model.ColWidths.GetTotal(0, grdSync.Model.ColCount - 1)
                    e.Handled = True
                End If

            Case colsSizeBehaviors.EXTEND_FIRST_COL
                If e.Index = grdSync.Model.Cols.FrozenCount + 1 Then
                    Dim leftPiece As Integer = grdSync.Model.ColWidths.GetTotal(0, grdSync.Model.Cols.FrozenCount)
                    Dim rightPiece As Integer = grdSync.Model.ColWidths.GetTotal(grdSync.Model.Cols.FrozenCount + 2, grdSync.Model.ColCount)
                    e.Size = grdSync.ClientSize.Width - leftPiece - rightPiece
                    e.Handled = True
                End If
                '				case GridColSizeBehavior.FixedProportional:
                '					if(e.Index == this.grid.Model.ColCount)
                '					{
                '						e.Size = this.grid.ClientSize.Width - this.grid.Model.ColWidths.GetTotal(0, this.grid.Model.ColCount - 1);
                '					}
                '					else
                '					{
                '						e.Size = (int) (this.colRatios[e.Index] * this.grid.ClientSize.Width);
                '					}
                '					e.Handled = true;
                '					break;

            Case colsSizeBehaviors.ALL_COLS_EQUALS
                'If e.Index = grdSync.Model.ColCount Then
                '    e.Size = grdSync.ClientSize.Width - grdSync.Model.ColWidths.GetTotal(0, grdSync.Model.ColCount - 1)
                'Else
                '    e.Size = CInt(Fix(colRatios(e.Index) * (grdSync.ClientSize.Width)))
                'End If
                If e.Index <> 0 Then
                    e.Size = CInt(Fix(colsSize(e.Index)))
                Else
                    e.Size = CInt(Fix(colsSize(e.Index)))
                End If

                e.Handled = True

            Case Else
        End Select
    End Sub

    Private Sub grid_ColWidthsChanged(ByVal sender As Object, ByVal e As GridRowColSizeChangedEventArgs)
        Dim dWidth As Double = grdSync.ClientSize.Width

        If blnLastColWidthChanged Then
            Return
        End If

        blnLastColWidthChanged = True

        If _colsSizeBehavior <> colsSizeBehaviors.ALL_COLS_EQUALS Then
            colRatios = New Double(grdSync.Model.ColCount) {}

            For col As Integer = 0 To grdSync.Model.ColCount
                colRatios(col) = grdSync.Model.ColWidths(col) / dWidth
            Next col
        End If

        blnLastColWidthChanged = False
    End Sub

End Class