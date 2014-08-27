Public Class LoadDataEventArgs
    Inherits System.EventArgs

    Private mintItem_ID As Integer

    Public ReadOnly Property Item_ID As Integer
        Get
            Return mintItem_ID
        End Get
    End Property

    Public Sub New(ByVal vintItem_ID As Integer)
        mintItem_ID = vintItem_ID
    End Sub

End Class
