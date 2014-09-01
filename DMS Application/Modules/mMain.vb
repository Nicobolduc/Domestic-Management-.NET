Module mMain


    Public gcAppControler As clsApplicationControler


    Sub main()

        gcAppControler = New clsApplicationControler

    End Sub

    'Public Sub CreateFormFromString(ByVal vstrFormToOpenName As String)
    '    Dim strFormName As String = vbNullString
    '    Dim frmTemp As System.Windows.Forms.Form = Nothing

    '    strFormName = System.Reflection.Assembly.GetEntryAssembly.GetName.Name & "." & vstrFormToOpenName
    '    'frmTemp = System.Reflection.Assembly.GetEntryAssembly.CreateInstance(strFormName, True)
    '    frmTemp = New DirectCast(strFormName,System.Windows.Forms.Form)
    '    DirectCast(frmTemp, System.Windows.Forms.Form).Show()

    'End Sub

End Module
