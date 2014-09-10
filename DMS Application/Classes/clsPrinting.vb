Public Class clsPrinting
    Inherits System.Drawing.Printing.PrintDocument

    Private pageNumber As Short

    Private gridToPrint As DataGridView

    Private documentTitleFont As Font
    Private columnsTitleFont As Font
    Private detailsFont As Font


#Region "Properties"

    Public WriteOnly Property SetGridToPrint As DataGridView
        Set(ByVal value As DataGridView)
            gridToPrint = value
        End Set
    End Property

#End Region

#Region "Private Events"

    Private Sub clsPrinting_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeginPrint
        documentTitleFont = New Font(New FontFamily("Arial"), 18, FontStyle.Bold, GraphicsUnit.Pixel) 'And FontStyle.Italic
        columnsTitleFont = New Font(New FontFamily("Arial"), 14, FontStyle.Bold, GraphicsUnit.Pixel) 'And FontStyle.Underline
        detailsFont = New Font(New FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel)
    End Sub

    Private Sub clsPrinting_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles Me.PrintPage
        Dim intXPos As Single = 5
        Dim intYPos As Single = 50
        Dim intRowIndex As Integer
        Dim intColIndex As Integer
        Dim blnDrawHeaderColText As Boolean = True

        'e.Graphics.PageUnit = GraphicsUnit.Pixel

        e.Graphics.DrawString("Epicerie", documentTitleFont, Brushes.Black, 500, 100)
        'e.Graphics.DrawString("Epicerie", documentTitleFont, Brushes.Black, New PointF(CSng(e.PageBounds.Width / 2) - e.Graphics.MeasureString("SOME TEXT", documentTitleFont).Width, 5))

        For intRowIndex = 0 To gridToPrint.Rows.Count - 1

            For intColIndex = 0 To gridToPrint.Columns.Count - 1

                If blnDrawHeaderColText And gridToPrint.Columns(intColIndex).Visible Then

                    e.Graphics.DrawString(gridToPrint.Columns(intColIndex).HeaderText, columnsTitleFont, Brushes.Black, intXPos, intYPos)

                    'intXPos += e.Graphics.MeasureString(gridToPrint.Columns(intColIndex).HeaderText, columnsTitleFont).Width
                    intXPos += 20 'gridToPrint.Columns(intColIndex).Width

                ElseIf gridToPrint.Columns(intColIndex).Visible Then

                    e.Graphics.DrawString(gridToPrint.Rows(intRowIndex).Cells(intColIndex).Value.ToString, detailsFont, Brushes.Black, intXPos, intYPos)

                    'intXPos += e.Graphics.MeasureString(gridToPrint.Columns(intColIndex).HeaderText, columnsTitleFont).Width
                    intXPos += 20 'gridToPrint.Columns(intColIndex).Width
                End If

            Next

            blnDrawHeaderColText = False

            intXPos = 10
            intYPos += e.Graphics.MeasureString("SOME TEXT", detailsFont).Height + 2
        Next
    End Sub

#End Region
    
    Private Sub clsPrinting_QueryPageSettings(ByVal sender As Object, ByVal e As System.Drawing.Printing.QueryPageSettingsEventArgs) Handles Me.QueryPageSettings
        e.PageSettings.Landscape = True
    End Sub
End Class
