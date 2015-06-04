Public Class DGV_Printing_Controller
    Inherits System.Drawing.Printing.PrintDocument

    'Private class members
    Private mPrintPrevDialog As PrintPreviewDialog
    Private mgrdToPrint As DataGridView
    Private mDocumentTitleFont As Font
    Private mColumnsTitleFont As Font
    Private mDetailsFont As Font

    'Private members
    Private pageNumber As Short
    Private mlstColsToPrint() As Short
    Private mintGrid_Selection_col As Integer
    Private mstrDocumentTitle As String = vbNullString
    Private Const mintXDetailStartPosition As Integer = 15
    Private Const mintYDetailStartPosition As Integer = 50


#Region "Properties"

    Public WriteOnly Property SetGridToPrint(Optional ByVal vcolsToPrintArray() As Short = Nothing) As DataGridView
        Set(ByVal value As DataGridView)
            mlstColsToPrint = vcolsToPrintArray
            mgrdToPrint = value
        End Set
    End Property

#End Region


#Region "Constructor"

    Public Sub New(ByVal vstrDocumentTitle As String)
        mstrDocumentTitle = vstrDocumentTitle
    End Sub

#End Region


#Region "Private Functions/Subs"

    Public Sub ShowPrintPreviewDialog()

        Try
            mPrintPrevDialog = New PrintPreviewDialog

            mPrintPrevDialog.FindForm.WindowState = FormWindowState.Maximized
            mPrintPrevDialog.FindForm.FormBorderStyle = FormBorderStyle.FixedToolWindow
            mPrintPrevDialog.Document = Me
            mPrintPrevDialog.PrintPreviewControl.Zoom = 1.0

            mPrintPrevDialog.ShowDialog()

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        Finally
            mPrintPrevDialog.Dispose()
        End Try

    End Sub

    Private Sub PrintAllGridColumns(ByVal veventArgs As Printing.PrintPageEventArgs)
        Dim intXPos As Single = mintXDetailStartPosition
        Dim intYPos As Single = mintYDetailStartPosition
        Dim intRowIndex As Integer
        Dim intColIndex As Integer
        Dim blnDrawHeaderColText As Boolean = True

        Try
            For intRowIndex = 0 To mgrdToPrint.Rows.Count - 1

                If mgrdToPrint.Rows(intRowIndex).Cells(mintGrid_Selection_col).Value.ToString = True.ToString Or blnDrawHeaderColText Then

                    For intColIndex = 0 To mgrdToPrint.Columns.Count - 1

                        If mgrdToPrint.Columns(intColIndex).Visible Then

                            If blnDrawHeaderColText Then

                                veventArgs.Graphics.DrawString(mgrdToPrint.Columns(intColIndex).HeaderText, mColumnsTitleFont, Brushes.Black, intXPos, intYPos)

                            ElseIf Not IsDBNull(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value) Then

                                Select Case mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).ValueType
                                    Case GetType(Double)
                                        veventArgs.Graphics.DrawString(Format(Val(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value), gstrCurrencyFormat), mDetailsFont, Brushes.Black, intXPos, intYPos)

                                    Case GetType(Boolean)
                                        veventArgs.Graphics.DrawString(Integer.Parse(Val(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value).ToString).ToString, mDetailsFont, Brushes.Black, intXPos, intYPos)

                                    Case Else
                                        veventArgs.Graphics.DrawString(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value.ToString, mDetailsFont, Brushes.Black, intXPos, intYPos)

                                End Select

                            Else

                                veventArgs.Graphics.DrawString("", mDetailsFont, Brushes.Black, intXPos, intYPos)

                            End If

                            intXPos += mgrdToPrint.Columns(intColIndex).Width + 10 'DataGridViewCell.MeasureTextWidth(e.Graphics, grdToPrint.Rows(intRowIndex).Cells(intColIndex).Value.ToString, detailsFont, 400, TextFormatFlags.Default) '200

                        End If

                    Next

                    blnDrawHeaderColText = False

                    intXPos = mintXDetailStartPosition
                    intYPos += veventArgs.Graphics.MeasureString("SAMPLE TEXT", mDetailsFont).Height + 4
                End If

            Next

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

    Private Sub PrintSpecificGridColumns(ByVal veventArgs As Printing.PrintPageEventArgs)
        Dim intXPos As Single = mintXDetailStartPosition
        Dim intYPos As Single = mintYDetailStartPosition
        Dim intRowIndex As Integer
        Dim intColIndex As Short
        Dim blnDrawHeaderColText As Boolean = True
        'TODO print first line
        Try
            For intRowIndex = 0 To mgrdToPrint.Rows.Count - 1

                If mgrdToPrint.Rows(intRowIndex).Cells(mintGrid_Selection_col).Value.ToString = True.ToString Or blnDrawHeaderColText Then

                    For Each intColIndex In mlstColsToPrint

                        If mgrdToPrint.Columns(intColIndex).Visible Then

                            If blnDrawHeaderColText Then

                                veventArgs.Graphics.DrawString(mgrdToPrint.Columns(intColIndex).HeaderText, mColumnsTitleFont, Brushes.Black, intXPos, intYPos)

                            ElseIf Not IsDBNull(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value) Then

                                Select Case mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).ValueType
                                    Case GetType(Double)
                                        veventArgs.Graphics.DrawString(Format(Val(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value), gstrCurrencyFormat), mDetailsFont, Brushes.Black, intXPos, intYPos)

                                    Case GetType(Boolean)
                                        veventArgs.Graphics.DrawString(Integer.Parse(Val(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value).ToString).ToString, mDetailsFont, Brushes.Black, intXPos, intYPos)

                                    Case Else
                                        veventArgs.Graphics.DrawString(mgrdToPrint.Rows(intRowIndex).Cells(intColIndex).Value.ToString, mDetailsFont, Brushes.Black, intXPos, intYPos)

                                End Select

                            Else

                                veventArgs.Graphics.DrawString("", mDetailsFont, Brushes.Black, intXPos, intYPos)

                            End If

                            intXPos += mgrdToPrint.Columns(intColIndex).Width + 20

                        End If

                    Next

                    If blnDrawHeaderColText Then
                        blnDrawHeaderColText = False
                        intYPos += veventArgs.Graphics.MeasureString("SAMPLE TEXT", mDetailsFont).Height + 10
                    Else
                        intYPos += veventArgs.Graphics.MeasureString("SAMPLE TEXT", mDetailsFont).Height + 4
                    End If

                    intXPos = mintXDetailStartPosition

                End If

            Next

        Catch ex As Exception
            gcAppControler.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source)
        End Try

    End Sub

#End Region


#Region "Private Events"

    Private Sub clsPrinting_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeginPrint
        mDocumentTitleFont = New Font(New FontFamily("Arial"), 18, FontStyle.Bold, GraphicsUnit.Pixel) 'And FontStyle.Italic
        mColumnsTitleFont = New Font(New FontFamily("Arial"), 15, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Pixel) 'And FontStyle.Underline
        mDetailsFont = New Font(New FontFamily("Arial"), 13, FontStyle.Regular, GraphicsUnit.Pixel)

        mintGrid_Selection_col = mgrdToPrint.Columns.Count - 1
    End Sub

    Private Sub clsPrinting_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles Me.PrintPage

        'e.Graphics.PageUnit = GraphicsUnit.Pixel

        e.Graphics.DrawString(mstrDocumentTitle, mDocumentTitleFont, Brushes.Black, New PointF(CSng((Me.DefaultPageSettings.PaperSize.Width / 2) - (e.Graphics.MeasureString(mstrDocumentTitle, mDocumentTitleFont, 300).Width / 2)), 10))

        If Not IsNothing(mlstColsToPrint) Then
            PrintSpecificGridColumns(e)
        Else
            PrintAllGridColumns(e)
        End If
    End Sub

    Private Sub clsPrinting_QueryPageSettings(ByVal sender As Object, ByVal e As System.Drawing.Printing.QueryPageSettingsEventArgs) Handles Me.QueryPageSettings
        'e.PageSettings.Landscape = True
    End Sub

#End Region

End Class
