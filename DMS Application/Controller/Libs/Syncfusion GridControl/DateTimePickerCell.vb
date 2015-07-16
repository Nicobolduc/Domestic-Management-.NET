Namespace DateTimePickerCell

    Public Class DateTimePickerCellModel
        Inherits GridStaticCellModel

        'Private class members
        Private _myDateTimePickerCellRenderer As DateTimePickerCellRenderer
        Private _blnShowCheckBox As Boolean

        Public Sub New(ByVal vgrdGrid As GridModel, ByVal vblnShowCheckBox As Boolean)
            MyBase.New(vgrdGrid)
            _blnShowCheckBox = vblnShowCheckBox
        End Sub

        Public Overrides Function CreateRenderer(ByVal control As GridControlBase) As GridCellRendererBase
            _myDateTimePickerCellRenderer = New DateTimePickerCellRenderer(control, Me)
            _myDateTimePickerCellRenderer.GetDateTimePickerCell.ShowCheckBox = _blnShowCheckBox

            Return _myDateTimePickerCellRenderer
        End Function

    End Class


    Friend Class DateTimePickerCellRenderer
        Inherits GridStaticCellRenderer

        'Private members
        Private _timer As Timer

        'Private class members
        Private WithEvents _myDateTimePicker As DateTimePickerCell


#Region "Properties"

        Public ReadOnly Property GetDateTimePickerCell As DateTimePickerCell
            Get
                Return _myDateTimePicker
            End Get
        End Property

#End Region

#Region "Constructors"

        Public Sub New(ByVal vgrdGrid As GridControlBase, ByVal vgrdGridCellModel As GridCellModelBase)
            MyBase.New(vgrdGrid, vgrdGridCellModel)

            _myDateTimePicker = New DateTimePickerCell()
            _myDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom

            vgrdGrid.Controls.Add(_myDateTimePicker)

            'show & hide to make sure it is initilized properly for teh first use...
            _myDateTimePicker.Show()
            _myDateTimePicker.Hide()
        End Sub

#End Region

#Region "Subs Overrides"

        'Handle drawing the cell
        Protected Overrides Sub OnDraw(ByVal g As System.Drawing.Graphics, ByVal clientRectangle As System.Drawing.Rectangle, ByVal rowIndex As Integer, ByVal colIndex As Integer, ByVal style As Syncfusion.Windows.Forms.Grid.GridStyleInfo)

            If Grid.CurrentCell.HasCurrentCellAt(rowIndex, colIndex) AndAlso CurrentCell.IsEditing Then

                _myDateTimePicker.Width = clientRectangle.Size.Width
                _myDateTimePicker.Height = 20
                _myDateTimePicker.CustomFormat = style.Format
                _myDateTimePicker.Font = New Font("Tahoma", 8.0F)
                _myDateTimePicker.Top = clientRectangle.Location.Y - 1
                _myDateTimePicker.Left = clientRectangle.Location.X
                _myDateTimePicker.Show()

                If Not _myDateTimePicker.ContainsFocus Then
                    _myDateTimePicker.Focus()
                End If
            Else
                style.TextMargins.Left = 3 'avoid the little jump...
                MyBase.OnDraw(g, clientRectangle, rowIndex, colIndex, style)
            End If
        End Sub

        'Set the value into the cell control & initialize it
        Protected Overrides Sub OnInitialize(ByVal vintRowIndex As Integer, ByVal vintColIndex As Integer)
            'Immeditaly switch into editing mode when cell is initialized.
            Dim style As GridStyleInfo = Grid.Model(vintRowIndex, vintColIndex)

            If style.CellValue.ToString = String.Empty Then
                _myDateTimePicker.Value = DateTime.Now
            Else
                _myDateTimePicker.Value = CDate(style.CellValue)
            End If

            CurrentCell.BeginEdit()

            MyBase.OnInitialize(vintRowIndex, vintColIndex)

            AddHandler _myDateTimePicker.ValueChanged, AddressOf DatePicker_ValueChanged

            _myDateTimePicker.Update()
        End Sub

        'Save the changes from the cell control to the grid cell
        Protected Overrides Function OnSaveChanges() As Boolean
            If CurrentCell.IsModified Then

                Dim style As GridStyleInfo = Grid.Model(Me.RowIndex, Me.ColIndex)

                Grid.Focus()

                If _myDateTimePicker.Checked Then
                    style.CellValue = Me._myDateTimePicker.Value
                Else
                    style.CellValue = String.Empty
                End If

                Return True
            End If
            Return False
        End Function

        'Hide the control
        Protected Overrides Sub OnDeactived(ByVal rowIndex As Integer, ByVal colIndex As Integer)
            If _myDateTimePicker.Visible Then
                _myDateTimePicker.Hide()
                Grid.Focus()
            End If

            RemoveHandler _myDateTimePicker.ValueChanged, AddressOf DatePicker_ValueChanged
        End Sub

        'Simulate a click to place focus where user clicked in the control
        Protected Overrides Sub OnClick(ByVal rowIndex As Integer, ByVal colIndex As Integer, ByVal e As MouseEventArgs)
            MyBase.OnClick(rowIndex, colIndex, e)

            If e.Button = MouseButtons.Left Then
                ClickControl()
            End If
        End Sub

        'handle initial keystroke on inactive cell, passing it to the datetimepicker
        Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            If Not _myDateTimePicker.Focused Then
                _myDateTimePicker.Focus()
                SendKeys.Send(New String(e.KeyChar, 1))
            End If
            MyBase.OnKeyPress(e)
        End Sub

#End Region

        Private Sub DatePicker_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            CurrentCell.IsModified = True
        End Sub

        Private Sub ClickControl()
            _timer = New Timer()
            _timer.Interval = 20
            AddHandler _timer.Tick, AddressOf click
            _timer.Start()
        End Sub

        Private Sub click(ByVal sender As Object, ByVal e As EventArgs)
            Dim p As Point = Me._myDateTimePicker.PointToClient(Control.MousePosition)

            _timer.Stop()

            RemoveHandler _timer.Tick, AddressOf click

            Syncfusion.Drawing.ActiveXSnapshot.FakeLeftMouseClick(Me._myDateTimePicker, p)

            _timer.Dispose()
            _timer = Nothing
        End Sub

        Private Sub _myDateTimePicker_CloseUp(sender As Object, e As EventArgs) Handles _myDateTimePicker.CloseUp
            'OnSaveChanges()
            'TODO lose focus
        End Sub

    End Class


    Friend Class DateTimePickerCell
        Inherits System.Windows.Forms.DateTimePicker

        'Private class members
        'Private keyPressed As Boolean = False

        'Pass the enter key back to the grid and trigger change event on other keystrokes...
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            If keyData = Keys.Enter Then
                Return False
            End If

            If msg.Msg = &H100 AndAlso keyData <> Keys.Tab Then 'keydown...
                Me.OnValueChanged(EventArgs.Empty)
            End If

            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function

    End Class

End Namespace