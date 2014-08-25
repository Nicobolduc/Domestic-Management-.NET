Public Class ctlFormActions
    Inherits System.Windows.Forms.UserControl

    Public mintItem_ID As Integer
    Public mintFormMode As clsConstants.Form_Modes

    Public Event CancelClick()


    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

    End Sub
End Class
