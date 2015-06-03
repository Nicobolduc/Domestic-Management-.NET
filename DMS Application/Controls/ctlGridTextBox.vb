
Public Class ctlGridTextBox
    Inherits System.Windows.Forms.UserControl

    Private mintItem_ID As Integer

    Private mSQLReader As MySqlDataReader = Nothing


#Region "Properties"

    Public ReadOnly Property getItem_ID As Integer
        Get
            Return mintItem_ID
        End Get
    End Property

#End Region


#Region "Functions / Subs"

    Private Sub ResetControl()

        txtTexte.Text = vbNullString
        myToolTip.SetToolTip(txtTexte, vbNullString)
        mintItem_ID = 0

    End Sub

#End Region


#Region "Private Events"

    Private Sub btnShowGrid_Click(sender As Object, e As EventArgs) Handles btnShowGrid.Click

    End Sub

    Private Sub txtTexte_DoubleClick(sender As Object, e As System.EventArgs) Handles txtTexte.DoubleClick

    End Sub

#End Region
   
End Class
