Module mMain


    Public gcAppControler As AppController

    Public gstrCurrencyFormat As String = "##,0.00$"

    Public gintGrid_Action_col As Integer = 0


    Sub main()

        gcAppControler = New AppController

    End Sub

    'Public Sub CreateFormFromString(ByVal vstrFormToOpenName As String)
    '    Dim strFormName As String = String.Empty
    '    Dim frmTemp As System.Windows.Forms.Form = Nothing

    '    strFormName = System.Reflection.Assembly.GetEntryAssembly.GetName.Name & "." & vstrFormToOpenName
    '    'frmTemp = System.Reflection.Assembly.GetEntryAssembly.CreateInstance(strFormName, True)
    '    frmTemp = New DirectCast(strFormName,System.Windows.Forms.Form)
    '    DirectCast(frmTemp, System.Windows.Forms.Form).Show()

    'End Sub

End Module
