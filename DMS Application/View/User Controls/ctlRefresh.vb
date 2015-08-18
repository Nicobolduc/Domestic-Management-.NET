Option Strict Off
Option Explicit Off

Public Class ctlRefresh
    Inherits System.Windows.Forms.UserControl

    'Private members
    Private _toRefresh As Boolean
    Private blnChangeImage As Boolean
    Private imgOriginalImage As System.Drawing.Image

    'Public events
    Public Shadows Event Click()

    Public WriteOnly Property SetToRefresh As Boolean
        Set(value As Boolean)
            _toRefresh = value

            If _toRefresh Then
                blnChangeImage = True

                tmrBlink.Start()
            Else
                tmrBlink.Stop()

                imgOriginalImage = btnRefresh.BackgroundImage
            End If
        End Set
    End Property

    Private Sub tmrBlink_Tick(sender As Object, e As EventArgs) Handles tmrBlink.Tick

        If blnChangeImage Then
            btnRefresh.BackgroundImage = My.Resources.ToRefresh
            blnChangeImage = False
        Else
            btnRefresh.BackgroundImage = imgOriginalImage
            blnChangeImage = True
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        imgOriginalImage = btnRefresh.BackgroundImage
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As System.EventArgs) Handles btnRefresh.Click
        tmrBlink.Stop()

        btnRefresh.BackgroundImage = imgOriginalImage

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        RaiseEvent Click()

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub
End Class
